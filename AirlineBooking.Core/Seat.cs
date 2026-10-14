namespace AirlineBooking.Core;

/// <summary>
/// Một ghế trên chuyến bay. Chỉ Flight được tạo Seat (constructor internal).
/// Trạng thái ghế chỉ đổi qua TryLock / Confirm / Release (Encapsulation).
/// </summary>
public class Seat
{
    private readonly string _seatCode;
    private readonly ISeatClass _seatClass;
    private decimal _price;
    private SeatStatus _status;
    private Passenger? _holder;
    private readonly object _sync = new();

    internal Seat(string code, ISeatClass seatClass, decimal price)
    {
        _seatCode = code;
        _seatClass = seatClass;
        _price = price;
        _status = SeatStatus.Available;
    }

    public string GetCode() => _seatCode;

    public ISeatClass GetSeatClass() => _seatClass;

    public decimal GetPrice() => _price;

    public SeatStatus GetStatus() => _status;

    public bool IsAvailable() => _status == SeatStatus.Available;

    public bool TryLock(Passenger p)
    {
        // TODO (tuần 3): lock (_sync) { nếu Available thì chuyển sang Locked, gán _holder = p, trả về true; ngược lại trả về false }
        throw new NotImplementedException();
    }

    public void Confirm(Passenger p)
    {
        // TODO (tuần 3): lock (_sync) { chỉ khi Locked và _holder == p thì chuyển sang Booked }
        throw new NotImplementedException();
    }

    public void Release()
    {
        // TODO (tuần 3): lock (_sync) { chuyển về Available, _holder = null }
        throw new NotImplementedException();
    }

    internal void ChangePrice(decimal newPrice)
    {
        // TODO (tuần 3): chỉ Flight.UpdatePrice được gọi; kiểm tra newPrice > 0
        throw new NotImplementedException();
    }
}
