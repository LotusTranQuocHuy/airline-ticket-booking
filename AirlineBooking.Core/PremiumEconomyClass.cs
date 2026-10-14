namespace AirlineBooking.Core;

public class PremiumEconomyClass : SeatClassBase
{
    public PremiumEconomyClass()
        : base("Premium Economy", 1.6m, new List<CancellationTier>
        {
            new CancellationTier(TimeSpan.FromHours(48), 0.05m),
            new CancellationTier(TimeSpan.FromHours(24), 0.20m),
            new CancellationTier(TimeSpan.Zero, 0.50m),
        })
    {
    }
}
