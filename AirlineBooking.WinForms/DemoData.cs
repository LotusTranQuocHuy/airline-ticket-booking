using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

internal sealed class DemoFlight(Flight flight)
{
    public Flight Flight { get; } = flight;
    public string Number => Flight.GetFlightNumber();
    public string Origin => Flight.GetOrigin();
    public string Destination => Flight.GetDestination();
    public DateTime Departure => Flight.GetDeparture();
    public decimal BasePrice => SeatLayout.GetBasePrice(Flight);
}

/// <summary>
/// Các thao tác của phía Người dùng. Chuyến bay và vé nằm trong AppServices (dùng chung với phía Admin);
/// dữ liệu mẫu được nạp ở SampleData khi mở ứng dụng.
/// </summary>
internal static class DemoData
{
    private static readonly Dictionary<string, Passenger> Passengers = new(StringComparer.OrdinalIgnoreCase);
    private static readonly object PassengerSync = new();

    public static Passenger GetOrCreatePassenger(string fullName, string email)
    {
        var normalizedName = fullName.Trim();
        var normalizedEmail = email.Trim();
        ArgumentException.ThrowIfNullOrWhiteSpace(normalizedName);
        var passengerKey = normalizedEmail.Length > 0
            ? $"email:{normalizedEmail}"
            : $"name:{normalizedName}";

        lock (PassengerSync)
        {
            if (Passengers.TryGetValue(passengerKey, out var passenger))
            {
                return passenger;
            }

            passenger = new Passenger(Guid.NewGuid().ToString("N"), normalizedName, normalizedEmail);
            Passengers.Add(passengerKey, passenger);
            return passenger;
        }
    }

    // Chỉ các chuyến chưa bay, sắp theo giờ khởi hành
    public static IReadOnlyList<DemoFlight> GetFlights() =>
        ToDemoFlights(AppServices.Catalog.GetAll());

    public static IReadOnlyList<DemoFlight> SearchFlights(string origin, string destination, DateOnly date) =>
        ToDemoFlights(AppServices.Catalog.Search(origin, destination, date));

    public static IReadOnlyList<string> GetLocations() =>
        GetFlights().SelectMany(flight => new[] { flight.Origin, flight.Destination })
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(location => location)
            .ToArray();

    public static IReadOnlyList<Booking> GetBookings(Passenger passenger) =>
        AppServices.Bookings.GetBookingsByPassenger(passenger.GetId())
            .Where(booking => booking.GetStatus() == BookingStatus.Confirmed)
            .OrderByDescending(booking => booking.GetBookedAt())
            .ToArray();

    public static string FormatBookingReceipt(Booking booking)
    {
        var flight = booking.GetFlight();
        var seat = booking.GetSeat();
        return $"Mã vé: {booking.GetBookingId()}\n" +
               $"Chuyến: {flight.GetFlightNumber()} · {flight.GetOrigin()} → {flight.GetDestination()}\n" +
               $"Khởi hành: {flight.GetDeparture():dd/MM/yyyy HH:mm}\n" +
               $"Ghế: {seat.GetCode()} · {seat.GetSeatClass().GetName()}\n" +
               $"Tổng tiền: {UiKit.Money(booking.GetPaidAmount())}\n\n" +
               "Bạn có thể xem vé trong mục “Vé của tôi”.";
    }

    public static Booking Book(Passenger passenger, DemoFlight flight, string seatCode) =>
        AppServices.Bookings.Book(flight.Number, seatCode, passenger);

    public static (decimal Fee, decimal Refund) PreviewCancellation(Booking booking) =>
        booking.PreviewCancellation(AppServices.Now);

    public static decimal Cancel(Passenger passenger, string bookingId)
    {
        var booking = AppServices.Bookings.GetBookingsByPassenger(passenger.GetId())
            .FirstOrDefault(candidate => candidate.GetBookingId() == bookingId);
        if (booking is null)
        {
            throw new InvalidBookingStateException("Không tìm thấy vé của hành khách này.");
        }

        return AppServices.Bookings.Cancel(bookingId);
    }

    public static Passenger Transfer(
        Passenger currentPassenger,
        string bookingId,
        string recipientName,
        string recipientEmail)
    {
        var recipient = GetOrCreatePassenger(recipientName, recipientEmail);
        AppServices.Bookings.Transfer(bookingId, currentPassenger, recipient);
        return recipient;
    }

    private static IReadOnlyList<DemoFlight> ToDemoFlights(IEnumerable<Flight> flights) =>
        flights.Where(flight => flight.GetDeparture() > AppServices.Now)
            .OrderBy(flight => flight.GetDeparture())
            .Select(flight => new DemoFlight(flight))
            .ToArray();
}
