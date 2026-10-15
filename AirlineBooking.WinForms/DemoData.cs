using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

internal sealed record DemoFlight(
    string Number,
    string Origin,
    string Destination,
    DateTime Departure,
    decimal BasePrice)
{
    public decimal GetPrice(string seatClass) =>
        BasePrice * (seatClass switch
        {
            "First" => 4.0m,
            "Business" => 2.5m,
            "Premium Economy" => 1.6m,
            _ => 1.0m
        });
}

internal sealed class DemoBooking(
    string id,
    Passenger passenger,
    DemoFlight flight,
    string seatCode,
    string seatClass,
    decimal amount)
{
    public string Id { get; } = id;
    public Passenger Passenger { get; } = passenger;
    public DemoFlight Flight { get; } = flight;
    public string SeatCode { get; } = seatCode;
    public string SeatClass { get; } = seatClass;
    public decimal Amount { get; } = amount;
    public bool IsCancelled { get; set; }
    public decimal RefundAmount { get; set; }
}

internal static class DemoData
{
    private static readonly DateTime FirstDeparture = DateTime.Today.AddDays(3).AddHours(8);
    private static readonly List<DemoFlight> Flights =
    [
        new("VN205", "TP. Hồ Chí Minh", "Hà Nội", FirstDeparture, 1_250_000m),
        new("VJ612", "Hà Nội", "Đà Nẵng", FirstDeparture.AddDays(1).AddHours(3), 780_000m),
        new("QH112", "TP. Hồ Chí Minh", "Đà Nẵng", FirstDeparture.AddDays(2).AddHours(2), 920_000m),
        new("VN181", "Đà Nẵng", "Hà Nội", FirstDeparture.AddDays(2).AddHours(6), 990_000m),
        new("VN207", "TP. Hồ Chí Minh", "Hà Nội", FirstDeparture.AddHours(6), 1_390_000m),
        new("VJ614", "Hà Nội", "Đà Nẵng", FirstDeparture.AddDays(1).AddHours(8), 850_000m),
        new("QH114", "TP. Hồ Chí Minh", "Đà Nẵng", FirstDeparture.AddDays(2).AddHours(7), 1_050_000m),
        new("VN183", "Đà Nẵng", "Hà Nội", FirstDeparture.AddDays(2).AddHours(11), 1_120_000m)
    ];
    private static readonly List<DemoBooking> Bookings = [];

    public static IReadOnlyList<DemoFlight> GetFlights() => Flights;

    public static IReadOnlyList<string> GetLocations() =>
        Flights.SelectMany(flight => new[] { flight.Origin, flight.Destination })
            .Distinct()
            .OrderBy(location => location)
            .ToArray();

    public static void StartSession(Passenger passenger)
    {
        if (!Bookings.Any(booking => booking.Passenger.GetId() == passenger.GetId()))
        {
            Bookings.Add(new DemoBooking(
                $"DEMO-{Guid.NewGuid().ToString("N")[..8]}",
                passenger,
                Flights[0],
                "5A",
                "Premium Economy",
                Flights[0].GetPrice("Premium Economy")));
        }
    }

    public static IReadOnlyList<DemoBooking> GetBookings(Passenger passenger) =>
        Bookings.Where(booking => booking.Passenger.GetId() == passenger.GetId())
            .OrderByDescending(booking => booking.Flight.Departure)
            .ToArray();

    public static bool IsSeatTaken(DemoFlight flight, string seatCode) =>
        Bookings.Any(booking =>
            booking.Flight.Number == flight.Number &&
            booking.SeatCode == seatCode &&
            !booking.IsCancelled);

    public static DemoBooking AddBooking(
        Passenger passenger,
        DemoFlight flight,
        string seatCode,
        string seatClass,
        decimal amount)
    {
        if (IsSeatTaken(flight, seatCode))
        {
            throw new InvalidOperationException("Ghế này vừa được chọn bởi một vé mẫu khác.");
        }

        var booking = new DemoBooking(
            $"DEMO-{Guid.NewGuid().ToString("N")[..8]}",
            passenger,
            flight,
            seatCode,
            seatClass,
            amount);
        Bookings.Add(booking);
        return booking;
    }

    public static void CancelDemoBooking(DemoBooking booking)
    {
        booking.IsCancelled = true;
        booking.RefundAmount = decimal.Round(booking.Amount * 0.9m, 0);
    }
}
