namespace AirlineBooking.Core;

/// <summary>
/// Điểm vào cho giao diện: đặt vé, hủy vé, lấy danh sách vé.
/// </summary>
public class BookingService
{
    private readonly FlightCatalog _flightCatalog;
    private readonly TimeProvider _time;
    private readonly List<Booking> _bookings = new();

    public BookingService(FlightCatalog catalog, TimeProvider time)
    {
        _flightCatalog = catalog;
        _time = time;
    }

    public IReadOnlyList<Booking> GetBookings() => _bookings.AsReadOnly();

    public IReadOnlyList<Booking> GetBookingsByPassenger(string passengerId) =>
        _bookings.Where(b => b.GetPassenger().GetId() == passengerId).ToList();

    public Booking Book(string flightNumber, string seatCode, Passenger passenger)
    {
        // TODO (tuần 3): tìm chuyến (không có thì ném FlightNotFoundException), tìm ghế,
        // Booking.Create(flight, seat, passenger, _time.GetLocalNow().DateTime), thêm vào _bookings
        throw new NotImplementedException();
    }

    public decimal Cancel(string bookingId)
    {
        // TODO (tuần 3): tìm vé theo mã, gọi booking.Cancel(_time.GetLocalNow().DateTime)
        throw new NotImplementedException();
    }
}
