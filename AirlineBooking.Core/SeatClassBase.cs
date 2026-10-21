namespace AirlineBooking.Core;

/// <summary>
/// Chứa logic dùng chung cho mọi hạng ghế, để các lớp con không lặp code.
/// Lớp con chỉ truyền tên, hệ số giá và bảng phí hủy vào constructor.
/// </summary>
public abstract class SeatClassBase : ISeatClass
{
    protected readonly string _name;
    protected readonly decimal _priceMultiplier;
    protected readonly IReadOnlyList<CancellationTier> _tiers;

    protected SeatClassBase(string name, decimal priceMultiplier, IReadOnlyList<CancellationTier> tiers)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(tiers);
        if (priceMultiplier <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(priceMultiplier), "Hệ số giá phải lớn hơn 0.");
        }

        if (tiers.Count == 0 ||
            tiers.Any(tier => tier.GetMinTimeBefore() < TimeSpan.Zero || tier.GetFeeRate() is < 0 or > 1))
        {
            throw new ArgumentException("Bảng phí hủy cần có mốc thời gian và tỷ lệ phí hợp lệ.", nameof(tiers));
        }

        _name = name;
        _priceMultiplier = priceMultiplier;
        _tiers = tiers.OrderByDescending(tier => tier.GetMinTimeBefore()).ToArray();
    }

    public string GetName() => _name;

    public decimal CalculatePrice(decimal price)
    {
        if (price <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Giá cơ bản phải lớn hơn 0.");
        }

        return decimal.Round(price * _priceMultiplier, 0, MidpointRounding.AwayFromZero);
    }

    public decimal CancellationFeeRate(TimeSpan timeLeft)
    {
        foreach (var tier in _tiers)
        {
            if (timeLeft >= tier.GetMinTimeBefore())
            {
                return tier.GetFeeRate();
            }
        }

        return _tiers[^1].GetFeeRate();
    }
}
