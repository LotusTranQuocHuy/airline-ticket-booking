namespace AirlineBooking.Core;

/// <summary>
/// Điểm vào cho giao diện: đặt vé, hủy vé, lấy danh sách vé.
/// </summary>
public class BookingService
{
    private readonly FlightCatalog _flightCatalog;
    private readonly TimeProvider _time;
    private readonly List<Booking> _bookings = new();
    private readonly object _sync = new();

    public BookingService(FlightCatalog catalog, TimeProvider time)
    {
        _flightCatalog = catalog;
        _time = time;
    }

    public IReadOnlyList<Booking> GetBookings()
    {
        lock (_sync)
        {
            return _bookings.ToArray();
        }
    }

    public IReadOnlyList<Booking> GetBookingsByPassenger(string passengerId) =>
        GetBookings()
            .Where(b => b.GetPassenger().GetId() == passengerId)
            .ToArray();

    public Booking Book(string flightNumber, string seatCode, Passenger passenger)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(flightNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(seatCode);
        ArgumentNullException.ThrowIfNull(passenger);

        var flight = _flightCatalog.Find(flightNumber)
            ?? throw new FlightNotFoundException(flightNumber);
        var seat = flight.GetSeat(seatCode)
            ?? throw new SeatUnavailableException(seatCode);
        var booking = Booking.Create(flight, seat, passenger, _time.GetLocalNow().DateTime);

        lock (_sync)
        {
            _bookings.Add(booking);
        }

        return booking;
    }

    public decimal Cancel(string bookingId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(bookingId);
        Booking? booking;
        lock (_sync)
        {
            booking = _bookings.FirstOrDefault(b => b.GetBookingId() == bookingId);
        }

        if (booking is null)
        {
            throw new InvalidBookingStateException($"Không tìm thấy vé {bookingId}.");
        }

        return booking.Cancel(_time.GetLocalNow().DateTime);
    }

    public void Transfer(string bookingId, Passenger currentPassenger, Passenger newPassenger)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(bookingId);
        ArgumentNullException.ThrowIfNull(currentPassenger);
        ArgumentNullException.ThrowIfNull(newPassenger);

        Booking? booking;
        lock (_sync)
        {
            booking = _bookings.FirstOrDefault(candidate => candidate.GetBookingId() == bookingId);
        }

        if (booking is null)
        {
            throw new InvalidBookingStateException($"Không tìm thấy vé {bookingId}.");
        }

        booking.TransferTo(currentPassenger, newPassenger);
    }
}
