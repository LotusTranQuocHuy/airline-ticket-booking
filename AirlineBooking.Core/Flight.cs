namespace AirlineBooking.Core;

/// <summary>
/// Chuyến bay sở hữu danh sách ghế (Composition): ghế chỉ được tạo qua AddSeat.
/// </summary>
public class Flight
{
    private readonly string _flightNumber;
    private readonly string _origin;
    private readonly string _destination;
    private DateTime _departure;
    private readonly List<Seat> _seats = new();

    public Flight(string flightNumber, string origin, string destination, DateTime departure)
    {
        _flightNumber = flightNumber;
        _origin = origin;
        _destination = destination;
        _departure = departure;
    }

    public string GetFlightNumber() => _flightNumber;

    public string GetOrigin() => _origin;

    public string GetDestination() => _destination;

    public DateTime GetDeparture() => _departure;

    public IReadOnlyList<Seat> GetSeats() => _seats.AsReadOnly();

    public Seat? GetSeat(string code) => _seats.FirstOrDefault(s => s.GetCode() == code);

    public void AddSeat(string seatCode, ISeatClass seatClass, decimal price)
    {
        _seats.Add(new Seat(seatCode, seatClass, price));
    }

    public void Reschedule(DateTime newDeparture)
    {
        // TODO (tuần 3): đổi _departure
        throw new NotImplementedException();
    }

    public void UpdatePrice(string seatClassName, decimal newPrice)
    {
        // TODO (tuần 3): gọi ChangePrice cho mọi ghế có GetSeatClass().GetName() == seatClassName
        throw new NotImplementedException();
    }
}
