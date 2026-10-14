namespace AirlineBooking.Core;

/// <summary>
/// Ném ra khi không tìm thấy chuyến bay theo số hiệu.
/// </summary>
public class FlightNotFoundException : Exception
{
    public FlightNotFoundException(string flightNumber)
        : base($"Không tìm thấy chuyến bay {flightNumber}.")
    {
    }
}
