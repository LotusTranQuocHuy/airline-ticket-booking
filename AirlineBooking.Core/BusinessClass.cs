namespace AirlineBooking.Core;

public class BusinessClass : SeatClassBase
{
    public BusinessClass()
        : base("Business", 2.5m, new List<CancellationTier>
        {
            new CancellationTier(TimeSpan.FromHours(24), 0.00m),
            new CancellationTier(TimeSpan.Zero, 0.20m),
        })
    {
    }
}
