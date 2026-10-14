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
        _name = name;
        _priceMultiplier = priceMultiplier;
        _tiers = tiers;
    }

    public string GetName() => _name;

    public decimal CalculatePrice(decimal price)
    {
        // TODO (tuần 3): giá vé = price * _priceMultiplier
        throw new NotImplementedException();
    }

    public decimal CancellationFeeRate(TimeSpan timeLeft)
    {
        // TODO (tuần 3): duyệt _tiers theo mốc giờ giảm dần,
        // trả về GetFeeRate() của mốc đầu tiên có timeLeft >= GetMinTimeBefore()
        throw new NotImplementedException();
    }
}
