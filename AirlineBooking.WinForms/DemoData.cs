using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

internal sealed class DemoFlight(Flight flight, decimal basePrice)
{
    public Flight Flight { get; } = flight;
    public string Number => Flight.GetFlightNumber();
    public string Origin => Flight.GetOrigin();
    public string Destination => Flight.GetDestination();
    public DateTime Departure => Flight.GetDeparture();
    public decimal BasePrice { get; } = basePrice;
}

internal static class DemoData
{
    private static readonly DateTime FirstDeparture = DateTime.Today.AddDays(3).AddHours(8);
    private static readonly FlightCatalog FlightCatalog = new();
    private static readonly BookingService BookingService = new(FlightCatalog, TimeProvider.System);
    private static readonly Dictionary<string, Passenger> Passengers = new(StringComparer.OrdinalIgnoreCase);
    private static readonly object PassengerSync = new();
    private static readonly IReadOnlyList<DemoFlight> Flights = CreateFlights();

    static DemoData()
    {
        SeedUnavailableSeats();
    }

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

    public static IReadOnlyList<DemoFlight> GetFlights() => Flights;

    public static IReadOnlyList<DemoFlight> SearchFlights(string origin, string destination, DateOnly date)
    {
        var matches = FlightCatalog.Search(origin, destination, date).ToHashSet();
        return Flights.Where(flight => matches.Contains(flight.Flight))
            .OrderBy(flight => flight.Departure)
            .ToArray();
    }

    public static IReadOnlyList<string> GetLocations() =>
        Flights.SelectMany(flight => new[] { flight.Origin, flight.Destination })
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(location => location)
            .ToArray();

    public static IReadOnlyList<Booking> GetBookings(Passenger passenger) =>
        BookingService.GetBookingsByPassenger(passenger.GetId())
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
        BookingService.Book(flight.Number, seatCode, passenger);

    public static (decimal Fee, decimal Refund) PreviewCancellation(Booking booking) =>
        booking.PreviewCancellation(TimeProvider.System.GetLocalNow().DateTime);

    public static decimal Cancel(Passenger passenger, string bookingId)
    {
        var booking = BookingService.GetBookingsByPassenger(passenger.GetId())
            .FirstOrDefault(candidate => candidate.GetBookingId() == bookingId);
        if (booking is null)
        {
            throw new InvalidBookingStateException("Không tìm thấy vé của hành khách này.");
        }

        return BookingService.Cancel(bookingId);
    }

    public static Passenger Transfer(
        Passenger currentPassenger,
        string bookingId,
        string recipientName,
        string recipientEmail)
    {
        var recipient = GetOrCreatePassenger(recipientName, recipientEmail);
        BookingService.Transfer(bookingId, currentPassenger, recipient);
        return recipient;
    }

    private static IReadOnlyList<DemoFlight> CreateFlights()
    {
        var flights = new[]
        {
            new DemoFlightSpec("VN205", "TP. Hồ Chí Minh", "Hà Nội", FirstDeparture, 1_250_000m),
            new DemoFlightSpec("VJ612", "Hà Nội", "Đà Nẵng", FirstDeparture.AddDays(1).AddHours(3), 780_000m),
            new DemoFlightSpec("QH112", "TP. Hồ Chí Minh", "Đà Nẵng", FirstDeparture.AddDays(2).AddHours(2), 920_000m),
            new DemoFlightSpec("VN181", "Đà Nẵng", "Hà Nội", FirstDeparture.AddDays(2).AddHours(6), 990_000m),
            new DemoFlightSpec("VN207", "TP. Hồ Chí Minh", "Hà Nội", FirstDeparture.AddHours(6), 1_390_000m),
            new DemoFlightSpec("VJ614", "Hà Nội", "Đà Nẵng", FirstDeparture.AddDays(1).AddHours(8), 850_000m),
            new DemoFlightSpec("QH114", "TP. Hồ Chí Minh", "Đà Nẵng", FirstDeparture.AddDays(2).AddHours(7), 1_050_000m),
            new DemoFlightSpec("VN183", "Đà Nẵng", "Hà Nội", FirstDeparture.AddDays(2).AddHours(11), 1_120_000m)
        };
        var result = new List<DemoFlight>(flights.Length);

        foreach (var spec in flights)
        {
            var flight = new Flight(spec.Number, spec.Origin, spec.Destination, spec.Departure);
            for (var row = 1; row <= 8; row++)
            {
                for (var column = 'A'; column <= 'D'; column++)
                {
                    flight.AddSeat($"{row}{column}", CreateSeatClass(GetSeatClassForRow(row)), spec.BasePrice);
                }
            }

            FlightCatalog.Add(flight);
            result.Add(new DemoFlight(flight, spec.BasePrice));
        }

        return result;
    }

    private static void SeedUnavailableSeats()
    {
        var samplePassenger = new Passenger("sample-booking", "Hành khách mẫu", string.Empty);
        BookingService.Book("VN205", "2D", samplePassenger);
        BookingService.Book("VJ612", "5C", samplePassenger);
        BookingService.Book("QH112", "7A", samplePassenger);
    }

    private static string GetSeatClassForRow(int row) => row switch
    {
        1 => "First",
        2 or 3 => "Business",
        4 or 5 => "Premium Economy",
        _ => "Economy"
    };

    private static ISeatClass CreateSeatClass(string name) => name switch
    {
        "First" => new FirstClass(),
        "Business" => new BusinessClass(),
        "Premium Economy" => new PremiumEconomyClass(),
        _ => new EconomyClass()
    };

    private sealed record DemoFlightSpec(
        string Number,
        string Origin,
        string Destination,
        DateTime Departure,
        decimal BasePrice);
}
