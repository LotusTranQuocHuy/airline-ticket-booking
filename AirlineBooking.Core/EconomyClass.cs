namespace AirlineBooking.Core;

public class EconomyClass : SeatClassBase
{
    public EconomyClass()
        : base("Economy", 1.0m, new List<CancellationTier>
        {
            new CancellationTier(TimeSpan.FromHours(48), 0.10m),
            new CancellationTier(TimeSpan.FromHours(24), 0.30m),
            new CancellationTier(TimeSpan.Zero, 1.00m),
        })
    {
    }
}
