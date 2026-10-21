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

    public decimal GetPrice()
    {
        lock (_sync)
        {
            return _price;
        }
    }

    public SeatStatus GetStatus()
    {
        lock (_sync)
        {
            return _status;
        }
    }

    public bool IsAvailable()
    {
        lock (_sync)
        {
            return _status == SeatStatus.Available;
        }
    }

    public bool TryLock(Passenger passenger)
    {
        ArgumentNullException.ThrowIfNull(passenger);
        lock (_sync)
        {
            if (_status != SeatStatus.Available)
            {
                return false;
            }

            _status = SeatStatus.Locked;
            _holder = passenger;
            return true;
        }
    }

    public void Confirm(Passenger passenger)
    {
        ArgumentNullException.ThrowIfNull(passenger);
        lock (_sync)
        {
            if (_status != SeatStatus.Locked || !IsHeldBy(passenger))
            {
                throw new InvalidBookingStateException(
                    $"Không thể xác nhận ghế {_seatCode} vì ghế không được giữ bởi hành khách này.");
            }

            _status = SeatStatus.Booked;
        }
    }

    internal void Release(Passenger passenger)
    {
        ArgumentNullException.ThrowIfNull(passenger);
        lock (_sync)
        {
            if ((_status != SeatStatus.Locked && _status != SeatStatus.Booked) || !IsHeldBy(passenger))
            {
                throw new InvalidBookingStateException(
                    $"Không thể giải phóng ghế {_seatCode} vì ghế không thuộc đặt chỗ này.");
            }

            _status = SeatStatus.Available;
            _holder = null;
        }
    }

    internal void ChangePrice(decimal newPrice)
    {
        if (newPrice <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(newPrice), "Giá ghế phải lớn hơn 0.");
        }

        lock (_sync)
        {
            _price = newPrice;
        }
    }

    private bool IsHeldBy(Passenger passenger) =>
        _holder is not null &&
        string.Equals(_holder.GetId(), passenger.GetId(), StringComparison.Ordinal);
}
