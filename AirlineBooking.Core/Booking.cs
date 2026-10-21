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
    private readonly object _sync = new();

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
        ArgumentNullException.ThrowIfNull(flight);
        ArgumentNullException.ThrowIfNull(seat);
        ArgumentNullException.ThrowIfNull(passenger);

        if (!flight.GetSeats().Contains(seat))
        {
            throw new ArgumentException("Ghế không thuộc chuyến bay đã chọn.", nameof(seat));
        }

        if (!seat.TryLock(passenger))
        {
            throw new SeatUnavailableException(seat.GetCode());
        }

        try
        {
            var paidAmount = seat.GetSeatClass().CalculatePrice(seat.GetPrice());
            seat.Confirm(passenger);
            return new Booking(
                Guid.NewGuid().ToString("N"),
                flight,
                seat,
                passenger,
                paidAmount,
                now);
        }
        catch
        {
            seat.Release(passenger);
            throw;
        }
    }

    public decimal Cancel(DateTime now)
    {
        lock (_sync)
        {
            var quote = CalculateCancellationQuote(now);
            _refundAmount = quote.Refund;
            _seat.Release(_passenger);
            _cancelledAt = now;
            _status = BookingStatus.Cancelled;
            return _refundAmount;
        }
    }

    public (decimal Fee, decimal Refund) PreviewCancellation(DateTime now)
    {
        lock (_sync)
        {
            return CalculateCancellationQuote(now);
        }
    }

    private (decimal Fee, decimal Refund) CalculateCancellationQuote(DateTime now)
    {
        if (_status != BookingStatus.Confirmed)
        {
            throw new InvalidBookingStateException($"Vé {_bookingId} đã được hủy.");
        }

        var timeLeft = _flight.GetDeparture() - now;
        if (timeLeft <= TimeSpan.Zero)
        {
            throw new InvalidBookingStateException("Không thể hủy vé sau giờ khởi hành.");
        }

        var feeRate = _seat.GetSeatClass().CancellationFeeRate(timeLeft);
        var fee = decimal.Round(_paidAmount * feeRate, 0, MidpointRounding.AwayFromZero);
        return (fee, _paidAmount - fee);
    }

    public string GetBookingId() => _bookingId;

    public Flight GetFlight() => _flight;

    public Seat GetSeat() => _seat;

    public Passenger GetPassenger() => _passenger;

    public decimal GetPaidAmount() => _paidAmount;

    public BookingStatus GetStatus()
    {
        lock (_sync)
        {
            return _status;
        }
    }

    public DateTime GetBookedAt() => _bookedAt;

    public decimal GetRefundAmount()
    {
        lock (_sync)
        {
            return _refundAmount;
        }
    }

    public DateTime? GetCancelledAt()
    {
        lock (_sync)
        {
            return _cancelledAt;
        }
    }
}
