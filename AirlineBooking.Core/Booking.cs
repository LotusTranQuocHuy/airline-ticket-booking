namespace AirlineBooking.Core;

/// <summary>
/// Vé đã đặt. Constructor private: chỉ tạo được qua Booking.Create,
/// nơi bắt buộc phải khóa ghế trước (Encapsulation).
/// </summary>
public class Booking
{
    private readonly string _bookingId;
    private readonly Flight _flight;
    private readonly Seat _seat;
    private readonly Passenger _passenger;
    private readonly decimal _paidAmount;
    private BookingStatus _status;
    private readonly DateTime _bookedAt;
    private decimal _refundAmount;
    private DateTime? _cancelledAt;

    private Booking(string bookingId, Flight flight, Seat seat, Passenger passenger,
                    decimal paidAmount, DateTime bookedAt)
    {
        _bookingId = bookingId;
        _flight = flight;
        _seat = seat;
        _passenger = passenger;
        _paidAmount = paidAmount;
        _bookedAt = bookedAt;
        _status = BookingStatus.Confirmed;
    }

    public static Booking Create(Flight flight, Seat seat, Passenger passenger, DateTime now)
    {
        // TODO (tuần 3): seat.TryLock(passenger), nếu false thì ném SeatUnavailableException;
        // tính giá = seat.GetSeatClass().CalculatePrice(seat.GetPrice()); seat.Confirm(passenger);
        // return new Booking(mã vé mới, flight, seat, passenger, giá, now);
        throw new NotImplementedException();
    }

    public decimal Cancel(DateTime now)
    {
        // TODO (tuần 3): nếu đã Cancelled hoặc chuyến đã bay thì ném InvalidBookingStateException;
        // phí = _paidAmount * CancellationFeeRate(GetDeparture() - now); _refundAmount = _paidAmount - phí;
        // _status = Cancelled; _cancelledAt = now; _seat.Release(); return _refundAmount;
        throw new NotImplementedException();
    }

    public string GetBookingId() => _bookingId;

    public Flight GetFlight() => _flight;

    public Seat GetSeat() => _seat;

    public Passenger GetPassenger() => _passenger;

    public decimal GetPaidAmount() => _paidAmount;

    public BookingStatus GetStatus() => _status;

    public DateTime GetBookedAt() => _bookedAt;

    public decimal GetRefundAmount() => _refundAmount;

    public DateTime? GetCancelledAt() => _cancelledAt;
}
