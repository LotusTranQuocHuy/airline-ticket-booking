namespace AirlineBooking.Core;

/// <summary>
/// Một mốc phí hủy: hủy trước giờ bay ít nhất _minTimeBefore thì mất _feeRate (0.10 = 10%).
/// </summary>
public class CancellationTier
{
    private readonly TimeSpan _minTimeBefore;
    private readonly decimal _feeRate;

    public CancellationTier(TimeSpan minTimeBefore, decimal feeRate)
    {
        _minTimeBefore = minTimeBefore;
        _feeRate = feeRate;
    }

    public TimeSpan GetMinTimeBefore() => _minTimeBefore;

    public decimal GetFeeRate() => _feeRate;
}
