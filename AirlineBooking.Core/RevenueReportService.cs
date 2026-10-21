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

    private static bool InRange(DateTime time, DateOnly from, DateOnly to)
    {
        DateOnly day = DateOnly.FromDateTime(time);
        return day >= from && day <= to;
    }
    public decimal GetTotalPaid(DateOnly from, DateOnly to)
    {
        decimal total = 0;
        foreach (Booking booking in _bookingService.GetBookings())
        {
            if (InRange(booking.GetBookedAt(), from, to))
            {
                total += booking.GetPaidAmount();
            }
        }
        return total;
    }

    public decimal GetTotalRefunded(DateOnly from, DateOnly to)
    {
        // TODO (tuần 3): tổng GetRefundAmount() của các vé bị hủy có GetCancelledAt() trong khoảng [from, to]
        decimal total = 0;
        foreach (Booking booking in _bookingService.GetBookings())
        {
            DateTime? cancelledAt = booking.GetCancelledAt();
            if (cancelledAt != null && InRange(cancelledAt.Value, from, to))
            {
                total += booking.GetRefundAmount();
            }
        }
        return total;
    }

    public decimal GetNetRevenue(DateOnly from, DateOnly to)
    {
        // TODO (tuần 3): GetTotalPaid - GetTotalRefunded
        return GetTotalPaid(from, to) - GetTotalRefunded(from, to);
        
    }

    public IReadOnlyList<RevenueLine> GetRevenueByFlight(DateOnly from, DateOnly to)
    {
        return BuildLines(true, from, to);      // true = nhóm theo chuyến bay
    }

    public IReadOnlyList<RevenueLine> GetRevenueBySeatClass(DateOnly from, DateOnly to)
    {
        return BuildLines(false, from, to);     // false = nhóm theo hạng ghế
    }

    // Vé này thuộc nhóm nào?
    private static string GetGroupName(Booking booking, bool byFlight)
    {
        if (byFlight)
        {
            return booking.GetFlight().GetFlightNumber();      // "VN201"
        }
        return booking.GetSeat().GetSeatClass().GetName();     // "Business"
    }

    private IReadOnlyList<RevenueLine> BuildLines(bool byFlight, DateOnly from, DateOnly to)
    {
        // Bước 1: chia vé vào từng nhóm.  Tên nhóm -> danh sách vé của nhóm đó
        var groups = new Dictionary<string, List<Booking>>();
        foreach (Booking booking in _bookingService.GetBookings())
        {
            string name = GetGroupName(booking, byFlight);
            if (!groups.ContainsKey(name))
            {
                groups[name] = new List<Booking>();     // gặp nhóm mới lần đầu -> tạo danh sách rỗng
            }
            groups[name].Add(booking);
        }

        // Bước 2: mỗi nhóm tính 3 con số -> 1 dòng báo cáo
        var lines = new List<RevenueLine>();
        foreach (var pair in groups)                    // pair.Key = tên nhóm, pair.Value = danh sách vé
        {
            int ticketCount = 0;
            decimal totalPaid = 0;
            decimal totalRefunded = 0;

            foreach (Booking booking in pair.Value)     // chỉ duyệt vé TRONG NHÓM
            {
                if (InRange(booking.GetBookedAt(), from, to))
                {
                    ticketCount++;
                    totalPaid += booking.GetPaidAmount();
                }

                DateTime? cancelledAt = booking.GetCancelledAt();
                if (cancelledAt != null && InRange(cancelledAt.Value, from, to))
                {
                    totalRefunded += booking.GetRefundAmount();
                }
            }

            if (ticketCount == 0 && totalRefunded == 0)
            {
                continue;                               // nhóm không có hoạt động trong khoảng ngày
            }
            lines.Add(new RevenueLine(pair.Key, ticketCount, totalPaid, totalRefunded));
        }
        return lines;
    }
}
