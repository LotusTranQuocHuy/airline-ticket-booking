namespace AirlineBooking.Core;

/// <summary>
/// Báo cáo doanh thu cho Admin, tính từ danh sách vé của BookingService.
/// </summary>
public class RevenueReportService
{
    private readonly BookingService _bookingService;

    public RevenueReportService(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public decimal GetTotalPaid(DateOnly from, DateOnly to)
    {
        // TODO (tuần 3): tổng GetPaidAmount() của các vé có GetBookedAt() trong khoảng [from, to]
        throw new NotImplementedException();
    }

    public decimal GetTotalRefunded(DateOnly from, DateOnly to)
    {
        // TODO (tuần 3): tổng GetRefundAmount() của các vé bị hủy có GetCancelledAt() trong khoảng [from, to]
        throw new NotImplementedException();
    }

    public decimal GetNetRevenue(DateOnly from, DateOnly to)
    {
        // TODO (tuần 3): GetTotalPaid - GetTotalRefunded
        throw new NotImplementedException();
    }

    public IReadOnlyList<RevenueLine> GetRevenueByFlight(DateOnly from, DateOnly to)
    {
        // TODO (tuần 3): nhóm vé theo số hiệu chuyến bay, mỗi nhóm tạo một RevenueLine
        throw new NotImplementedException();
    }

    public IReadOnlyList<RevenueLine> GetRevenueBySeatClass(DateOnly from, DateOnly to)
    {
        // TODO (tuần 3): nhóm vé theo tên hạng ghế, mỗi nhóm tạo một RevenueLine
        throw new NotImplementedException();
    }
}
