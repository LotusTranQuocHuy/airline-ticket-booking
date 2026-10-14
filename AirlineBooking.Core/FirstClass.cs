namespace AirlineBooking.Core;

public class FirstClass : SeatClassBase
{
    public FirstClass()
        : base("First", 4.0m, new List<CancellationTier>
        {
            new CancellationTier(TimeSpan.FromHours(12), 0.00m),
            new CancellationTier(TimeSpan.Zero, 0.10m),
        })
    {
    }
}
