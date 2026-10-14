namespace AirlineBooking.Core;

/// <summary>
/// Hợp đồng chung cho mọi hạng ghế (Polymorphism).
/// Seat và Booking chỉ làm việc với interface này, không biết hạng ghế cụ thể.
/// </summary>
public interface ISeatClass
{
    string GetName();

    decimal CalculatePrice(decimal price);

    decimal CancellationFeeRate(TimeSpan timeLeft);
}
