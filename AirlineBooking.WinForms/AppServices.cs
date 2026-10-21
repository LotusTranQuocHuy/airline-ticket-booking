using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

/// <summary>
/// Một bộ dịch vụ duy nhất cho cả ứng dụng: màn hình Admin và màn hình Người dùng
/// cùng đọc / ghi vào đây, nên vé khách đặt sẽ hiện được bên Admin.
/// </summary>
internal static class AppServices
{
    public static TimeProvider Time { get; } = TimeProvider.System;
    public static FlightCatalog Catalog { get; } = new FlightCatalog();
    public static BookingService Bookings { get; } = new BookingService(Catalog, Time);
    public static RevenueReportService Revenue { get; } = new RevenueReportService(Bookings);

    // Giờ hiện tại theo đồng hồ của ứng dụng (form dùng khi cần "now", ví dụ Reschedule)
    public static DateTime Now => Time.GetLocalNow().DateTime;
}