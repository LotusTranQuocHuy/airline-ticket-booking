namespace AirlineBooking.Core;

/// <summary>
/// Một dòng trong báo cáo doanh thu (theo chuyến bay hoặc theo hạng ghế).
/// </summary>
public class RevenueLine
{
    private readonly string _label;
    private readonly int _ticketCount;
    private readonly decimal _totalPaid;
    private readonly decimal _totalRefunded;

    public RevenueLine(string label, int ticketCount, decimal totalPaid, decimal totalRefunded)
    {
        _label = label;
        _ticketCount = ticketCount;
        _totalPaid = totalPaid;
        _totalRefunded = totalRefunded;
    }

    public string GetLabel() => _label;

    public int GetTicketCount() => _ticketCount;

    public decimal GetTotalPaid() => _totalPaid;

    public decimal GetTotalRefunded() => _totalRefunded;

    public decimal GetNetRevenue() => _totalPaid - _totalRefunded;
}
