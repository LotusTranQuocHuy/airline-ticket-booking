namespace AirlineBooking.Core;

public class FlightCatalog
{
    private readonly List<Flight> _flights = new();
    private readonly object _sync = new();

    public void Add(Flight flight)
    {
        ArgumentNullException.ThrowIfNull(flight);
        lock (_sync)
        {
            if (_flights.Any(existing => existing.GetFlightNumber() == flight.GetFlightNumber()))
            {
                throw new ArgumentException($"Chuyến bay {flight.GetFlightNumber()} đã tồn tại.", nameof(flight));
            }

            _flights.Add(flight);
        }
    }

    public Flight? Find(string flightNumber)
    {
        lock (_sync)
        {
            return _flights.FirstOrDefault(f => f.GetFlightNumber() == flightNumber);
        }
    }

    public IReadOnlyList<Flight> GetAll()
    {
        lock (_sync)
        {
            return _flights.ToArray();
        }
    }

    public IEnumerable<Flight> Search(string origin, string destination, DateOnly date)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(origin);
        ArgumentException.ThrowIfNullOrWhiteSpace(destination);
        var flights = GetAll();
        return flights.Where(flight =>
            string.Equals(flight.GetOrigin(), origin, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(flight.GetDestination(), destination, StringComparison.OrdinalIgnoreCase) &&
            DateOnly.FromDateTime(flight.GetDeparture()) == date);
    }
}
