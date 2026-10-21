using AirlineBooking.Core;
using Xunit;

namespace AirlineBooking.Core.Tests;

public sealed class BookingFlowTests
{
    [Fact]
    public void SearchBookAndCancel_ReleasesSeatAndRefundsTheQuotedAmount()
    {
        var now = new DateTime(2030, 4, 15, 8, 0, 0, DateTimeKind.Utc);
        var (catalog, flight, service) = CreateBookingSystem(now, now.AddHours(72));
        var seat = AddSeat(flight, "12A", new EconomyClass());
        var passenger = new Passenger("passenger-1", "Nguyen Van A", string.Empty);

        var matchingFlights = catalog.Search(
            "Ho Chi Minh City",
            "Ha Noi",
            DateOnly.FromDateTime(flight.GetDeparture())).ToArray();

        Assert.Single(matchingFlights);
        var booking = service.Book("VN205", "12A", passenger);

        Assert.Equal(1_000_000m, booking.GetPaidAmount());
        Assert.Equal(SeatStatus.Booked, seat.GetStatus());
        var quote = booking.PreviewCancellation(now);

        var refund = service.Cancel(booking.GetBookingId());

        Assert.Equal(quote.Refund, refund);
        Assert.Equal(900_000m, refund);
        Assert.Equal(900_000m, booking.GetRefundAmount());
        Assert.Equal(BookingStatus.Cancelled, booking.GetStatus());
        Assert.Equal(SeatStatus.Available, seat.GetStatus());
    }

    [Fact]
    public async Task BookingService_RejectsConcurrentDuplicateSeatAndRepeatedCancellation()
    {
        var now = new DateTime(2030, 4, 15, 8, 0, 0, DateTimeKind.Utc);
        var (_, flight, service) = CreateBookingSystem(now, now.AddHours(72));
        AddSeat(flight, "12A", new EconomyClass());
        var passengers = new[]
        {
            new Passenger("passenger-1", "Nguyen Van A", string.Empty),
            new Passenger("passenger-2", "Tran Thi B", string.Empty)
        };
        var attempts = await Task.WhenAll(passengers.Select(async passenger =>
        {
            await Task.Yield();
            try
            {
                return service.Book("VN205", "12A", passenger);
            }
            catch (SeatUnavailableException)
            {
                return null;
            }
        }));

        var booking = Assert.Single(attempts.OfType<Booking>());
        Assert.Equal(SeatStatus.Booked, flight.GetSeat("12A")!.GetStatus());

        service.Cancel(booking.GetBookingId());

        Assert.Throws<InvalidBookingStateException>(
            () => service.Cancel(booking.GetBookingId()));
    }

    [Fact]
    public void CancellationWithin24Hours_RefundsDifferentAmountsBySeatClass()
    {
        var now = new DateTime(2030, 4, 15, 8, 0, 0, DateTimeKind.Utc);
        var (_, flight, service) = CreateBookingSystem(now, now.AddHours(12));
        var economySeat = AddSeat(flight, "12A", new EconomyClass());
        var businessSeat = AddSeat(flight, "1A", new BusinessClass());
        var economyPassenger = new Passenger("economy-passenger", "Economy Guest", string.Empty);
        var businessPassenger = new Passenger("business-passenger", "Business Guest", string.Empty);
        var economyBooking = service.Book("VN205", "12A", economyPassenger);
        var businessBooking = service.Book("VN205", "1A", businessPassenger);

        Assert.Equal(1_000_000m, economyBooking.GetPaidAmount());
        Assert.Equal(2_500_000m, businessBooking.GetPaidAmount());
        Assert.Equal(SeatStatus.Booked, economySeat.GetStatus());
        Assert.Equal(SeatStatus.Booked, businessSeat.GetStatus());

        var economyRefund = service.Cancel(economyBooking.GetBookingId());
        var businessRefund = service.Cancel(businessBooking.GetBookingId());

        Assert.Equal(0m, economyRefund);
        Assert.Equal(2_000_000m, businessRefund);
        Assert.NotEqual(economyRefund, businessRefund);
        Assert.Equal(SeatStatus.Available, economySeat.GetStatus());
        Assert.Equal(SeatStatus.Available, businessSeat.GetStatus());
    }

    [Fact]
    public void TransferMovesBookingAndSeatOwnershipToTheRecipient()
    {
        var now = new DateTime(2030, 4, 15, 8, 0, 0, DateTimeKind.Utc);
        var (_, flight, service) = CreateBookingSystem(now, now.AddHours(72));
        var seat = AddSeat(flight, "12A", new EconomyClass());
        var currentPassenger = new Passenger("passenger-1", "Nguyen Van A", "a@example.com");
        var recipient = new Passenger("passenger-2", "Tran Thi B", "b@example.com");
        var booking = service.Book("VN205", "12A", currentPassenger);

        service.Transfer(booking.GetBookingId(), currentPassenger, recipient);

        Assert.Equal(recipient.GetId(), booking.GetPassenger().GetId());
        Assert.Empty(service.GetBookingsByPassenger(currentPassenger.GetId()));
        Assert.Equal(booking, Assert.Single(service.GetBookingsByPassenger(recipient.GetId())));
        Assert.Equal(SeatStatus.Booked, seat.GetStatus());
        Assert.Throws<InvalidBookingStateException>(
            () => service.Transfer(booking.GetBookingId(), currentPassenger, recipient));

        service.Cancel(booking.GetBookingId());

        Assert.Equal(SeatStatus.Available, seat.GetStatus());
    }

    [Fact]
    public void TransferRejectsCancelledBooking()
    {
        var now = new DateTime(2030, 4, 15, 8, 0, 0, DateTimeKind.Utc);
        var (_, flight, service) = CreateBookingSystem(now, now.AddHours(72));
        AddSeat(flight, "12A", new EconomyClass());
        var currentPassenger = new Passenger("passenger-1", "Nguyen Van A", string.Empty);
        var recipient = new Passenger("passenger-2", "Tran Thi B", string.Empty);
        var booking = service.Book("VN205", "12A", currentPassenger);
        service.Cancel(booking.GetBookingId());

        Assert.Throws<InvalidBookingStateException>(
            () => service.Transfer(booking.GetBookingId(), currentPassenger, recipient));
        Assert.Empty(service.GetBookingsByPassenger(recipient.GetId()));
    }

    private static (FlightCatalog Catalog, Flight Flight, BookingService Service) CreateBookingSystem(
        DateTime now,
        DateTime departure)
    {
        var flight = new Flight("VN205", "Ho Chi Minh City", "Ha Noi", departure);
        var catalog = new FlightCatalog();
        catalog.Add(flight);
        var service = new BookingService(catalog, new FrozenTimeProvider(now));
        return (catalog, flight, service);
    }

    private static Seat AddSeat(Flight flight, string seatCode, ISeatClass seatClass)
    {
        flight.AddSeat(seatCode, seatClass, 1_000_000m);
        return flight.GetSeat(seatCode)!;
    }

    private sealed class FrozenTimeProvider(DateTime utcNow) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => new(utcNow);

        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
    }
}
