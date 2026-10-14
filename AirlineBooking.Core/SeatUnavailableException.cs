namespace AirlineBooking.Core;

/// <summary>
/// Ném ra khi đặt một ghế đã bị người khác giữ hoặc đã được đặt.
/// </summary>
public class SeatUnavailableException : Exception
{
    public SeatUnavailableException(string seatCode)
        : base($"Ghế {seatCode} đã có người giữ hoặc đã được đặt.")
    {
    }
}
