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

    public void Reschedule(DateTime newDeparture, DateTime now)
    {

        // TODO (tuần 3): đổi _departure
        if (_departure <= now)
            throw new InvalidOperationException($"Chuyến {_flightNumber} đã cất cánh, không thể đổi giờ.");
        if (newDeparture <= now)
            throw new ArgumentException("Giờ bay mới phải sau thời điểm hiện tại.", nameof(newDeparture));

        _departure = newDeparture;
    }

    public void UpdatePrice(string seatClassName, decimal newPrice)
    {
        // TODO (tuần 3): gọi ChangePrice cho mọi ghế có GetSeatClass().GetName() == seatClassName
        var seatsOfClass = new List<Seat>();
        foreach (Seat seat in _seats)
        {
            if (seat.GetSeatClass().GetName() == seatClassName)
            {
                seatsOfClass.Add(seat);
            }
        }

        if (seatsOfClass.Count == 0)
            throw new ArgumentException($"Chuyến {_flightNumber} không có ghế hạng {seatClassName}.", nameof(seatClassName));

        foreach (var seat in seatsOfClass)
        {
            seat.ChangePrice(newPrice);
        }
    }
}
