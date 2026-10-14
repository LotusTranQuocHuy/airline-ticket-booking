namespace AirlineBooking.Core;

/// <summary>
/// Ném ra khi thao tác không hợp lệ với trạng thái hiện tại của vé (ví dụ hủy vé hai lần).
/// </summary>
public class InvalidBookingStateException : Exception
{
    public InvalidBookingStateException(string message)
        : base(message)
    {
    }
}
