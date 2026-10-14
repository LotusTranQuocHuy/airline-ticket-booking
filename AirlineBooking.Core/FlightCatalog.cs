namespace AirlineBooking.Core;

public class FlightCatalog
{
    private readonly List<Flight> _flights = new();

    public void Add(Flight flight)
    {
        _flights.Add(flight);
    }

    public Flight? Find(string flightNumber) =>
        _flights.FirstOrDefault(f => f.GetFlightNumber() == flightNumber);

    public IReadOnlyList<Flight> GetAll() => _flights.AsReadOnly();

    public IEnumerable<Flight> Search(string origin, string destination, DateOnly date)
    {
        // TODO (tuần 3): lọc theo điểm đi, điểm đến và ngày bay (DateOnly.FromDateTime(GetDeparture()))
        throw new NotImplementedException();
    }
}
