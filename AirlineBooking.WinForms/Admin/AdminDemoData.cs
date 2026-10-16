using AirlineBooking.Core;

namespace AirlineBooking.WinForms.Admin;

// Dữ liệu mẫu cho giao diện Admin tuần 2 (chưa nối với phần xử lý).
// Tuần 3: thay bằng FlightCatalog, BookingService và RevenueReportService trong AirlineBooking.Core,
// khi đó các form chỉ đổi nguồn dữ liệu, không cần sửa bố cục.

public class FlightRow
{
    public const int BusinessSeatsPerRow = 4;
    public const int EconomySeatsPerRow = 6;
    public static readonly string[] BusinessLetters = { "A", "C", "D", "F" };
    public static readonly string[] EconomyLetters = { "A", "B", "C", "D", "E", "F" };

    public FlightRow(string code, string from, string to, DateTime departure, decimal basePrice,
                     int businessRows, int economyRows)
    {
        Code = code;
        From = from;
        To = to;
        Departure = departure;
        BasePrice = basePrice;
        BusinessRows = businessRows;
        EconomyRows = economyRows;
    }

    public string Code { get; }
    public string From { get; }
    public string To { get; }
    public DateTime Departure { get; set; }
    public decimal BasePrice { get; set; }
    public int BusinessRows { get; }
    public int EconomyRows { get; }

    public int TotalSeats => BusinessRows * BusinessSeatsPerRow + EconomyRows * EconomySeatsPerRow;
    public bool HasDeparted => Departure <= DateTime.Now;
    public string StatusText => HasDeparted ? "Đã bay" : "Mở bán";
    public string Route => $"{AirportCode(From)} → {AirportCode(To)}";

    // "Hà Nội (HAN)" -> "HAN"
    private static string AirportCode(string airport)
    {
        int open = airport.LastIndexOf('(');
        return open >= 0 && airport.EndsWith(')') ? airport[(open + 1)..^1] : airport;
    }
}

public class BookingRow
{
    public BookingRow(string id, string passengerName, string passengerEmail, string flightCode,
                      string seatCode, string seatClassName, decimal paidAmount, DateTime bookedAt)
    {
        Id = id;
        PassengerName = passengerName;
        PassengerEmail = passengerEmail;
        FlightCode = flightCode;
        SeatCode = seatCode;
        SeatClassName = seatClassName;
        PaidAmount = paidAmount;
        BookedAt = bookedAt;
        Status = BookingStatus.Confirmed;
    }

    public string Id { get; }
    public string PassengerName { get; }
    public string PassengerEmail { get; }
    public string FlightCode { get; }
    public string SeatCode { get; }
    public string SeatClassName { get; }
    public decimal PaidAmount { get; }
    public DateTime BookedAt { get; }
    public BookingStatus Status { get; set; }
    public decimal RefundAmount { get; set; }
    public DateTime? CancelledAt { get; set; }

    public string StatusText => Status == BookingStatus.Confirmed ? "Đã xác nhận" : "Đã hủy";
}

public sealed record CancellationPreview(bool CanCancel, decimal FeeRate, decimal RefundAmount, string Note);

public static class AdminDemoData
{
    public static readonly string BusinessName = new BusinessClass().GetName();
    public static readonly string EconomyName = new EconomyClass().GetName();

    public static readonly string[] Airports =
    {
        "Hà Nội (HAN)", "TP.HCM (SGN)", "Đà Nẵng (DAD)", "Phú Quốc (PQC)", "Hải Phòng (HPH)", "Nha Trang (CXR)"
    };

    public static List<FlightRow> Flights { get; } = CreateFlights();
    public static List<BookingRow> Bookings { get; } = CreateBookings();

    public static FlightRow? FindFlight(string code) =>
        Flights.FirstOrDefault(f => string.Equals(f.Code, code, StringComparison.OrdinalIgnoreCase));

    public static IReadOnlyList<string> GetBookedSeats(string flightCode) =>
        Bookings.Where(b => b.FlightCode == flightCode && b.Status == BookingStatus.Confirmed)
                .Select(b => b.SeatCode)
                .ToList();

    public static int CountBooked(string flightCode) => GetBookedSeats(flightCode).Count;

    public static bool HasBookings(string flightCode) => Bookings.Any(b => b.FlightCode == flightCode);

    public static void AddFlight(FlightRow flight) => Flights.Add(flight);

    public static void RemoveFlight(FlightRow flight) => Flights.Remove(flight);

    public static CancellationPreview PreviewCancellation(BookingRow booking, DateTime now)
    {
        if (booking.Status == BookingStatus.Cancelled)
        {
            return new(false, 0m, 0m, "Vé đã được hủy trước đó.");
        }

        var flight = FindFlight(booking.FlightCode);
        if (flight is null || flight.Departure <= now)
        {
            return new(false, 0m, 0m, "Chuyến đã bay, không thể hủy.");
        }

        // TẠM THỜI (tuần 2): chép bảng phí hủy của BusinessClass / EconomyClass để giao diện chạy được.
        // Tuần 3: thay bằng seat.GetSeatClass().CancellationFeeRate(departure - now) trong Core rồi xóa đoạn này.
        TimeSpan timeLeft = flight.Departure - now;
        decimal feeRate;
        string rule;
        if (booking.SeatClassName == BusinessName)
        {
            (feeRate, rule) = timeLeft >= TimeSpan.FromHours(24)
                ? (0.00m, "Business, hủy trước giờ bay từ 24 giờ")
                : (0.20m, "Business, hủy trong vòng 24 giờ");
        }
        else if (timeLeft >= TimeSpan.FromHours(48))
        {
            (feeRate, rule) = (0.10m, "Economy, hủy trước giờ bay từ 48 giờ");
        }
        else if (timeLeft >= TimeSpan.FromHours(24))
        {
            (feeRate, rule) = (0.30m, "Economy, hủy trước giờ bay 24 – 48 giờ");
        }
        else
        {
            (feeRate, rule) = (1.00m, "Economy, hủy trong vòng 24 giờ");
        }

        decimal refund = booking.PaidAmount * (1 - feeRate);
        return new(true, feeRate, refund, $"{rule}: phí {feeRate * 100:0}%, hoàn {(1 - feeRate) * 100:0}% giá vé.");
    }

    public static void CancelBooking(BookingRow booking, DateTime now)
    {
        var preview = PreviewCancellation(booking, now);
        if (!preview.CanCancel)
        {
            throw new InvalidBookingStateException(preview.Note);
        }

        booking.Status = BookingStatus.Cancelled;
        booking.RefundAmount = preview.RefundAmount;
        booking.CancelledAt = now;
    }

    // Báo cáo doanh thu: "đã thu" tính theo ngày đặt vé, "đã hoàn" tính theo ngày hủy vé.
    public static decimal GetTotalPaid(DateOnly from, DateOnly to) =>
        Bookings.Where(b => InRange(b.BookedAt, from, to)).Sum(b => b.PaidAmount);

    public static decimal GetTotalRefunded(DateOnly from, DateOnly to) =>
        Bookings.Where(b => IsRefundedIn(b, from, to)).Sum(b => b.RefundAmount);

    public static IReadOnlyList<RevenueLine> GetRevenueByFlight(DateOnly from, DateOnly to) =>
        GroupRevenue(b => b.FlightCode, from, to);

    public static IReadOnlyList<RevenueLine> GetRevenueBySeatClass(DateOnly from, DateOnly to) =>
        GroupRevenue(b => b.SeatClassName, from, to);

    private static IReadOnlyList<RevenueLine> GroupRevenue(Func<BookingRow, string> key, DateOnly from, DateOnly to) =>
        Bookings.Where(b => InRange(b.BookedAt, from, to) || IsRefundedIn(b, from, to))
                .GroupBy(key)
                .OrderBy(g => g.Key)
                .Select(g => new RevenueLine(
                    g.Key,
                    g.Count(b => InRange(b.BookedAt, from, to)),
                    g.Where(b => InRange(b.BookedAt, from, to)).Sum(b => b.PaidAmount),
                    g.Where(b => IsRefundedIn(b, from, to)).Sum(b => b.RefundAmount)))
                .ToList();

    private static bool InRange(DateTime time, DateOnly from, DateOnly to)
    {
        var day = DateOnly.FromDateTime(time);
        return day >= from && day <= to;
    }

    private static bool IsRefundedIn(BookingRow booking, DateOnly from, DateOnly to) =>
        booking.CancelledAt is DateTime cancelledAt && InRange(cancelledAt, from, to);

    private static List<FlightRow> CreateFlights()
    {
        DateTime today = DateTime.Today;
        return new List<FlightRow>
        {
            new("VN201", "Hà Nội (HAN)", "TP.HCM (SGN)", today.AddDays(1).AddHours(7.5), 1_850_000m, 3, 20),
            new("VN245", "Hà Nội (HAN)", "Đà Nẵng (DAD)", today.AddDays(1).AddHours(9.75), 1_450_000m, 3, 20),
            new("VN310", "TP.HCM (SGN)", "Phú Quốc (PQC)", today.AddDays(2).AddHours(11 + 1 / 3.0), 1_250_000m, 3, 20),
            new("VN412", "Đà Nẵng (DAD)", "Hà Nội (HAN)", today.AddDays(3).AddHours(14 + 5 / 60.0), 1_450_000m, 3, 20),
            new("VN518", "TP.HCM (SGN)", "Hải Phòng (HPH)", today.AddDays(4).AddHours(6.25), 1_650_000m, 3, 20),
            new("VN622", "Hà Nội (HAN)", "Nha Trang (CXR)", today.AddDays(-3).AddHours(18 + 40 / 60.0), 1_550_000m, 3, 20),
        };
    }

    private static List<BookingRow> CreateBookings()
    {
        DateTime today = DateTime.Today;

        // TẠM THỜI (tuần 2): giá vé = giá gốc x hệ số hạng ghế (Business 2,5; Economy 1,0).
        // Tuần 3: lấy từ seat.GetSeatClass().CalculatePrice(...) trong Core.
        BookingRow Book(string id, string name, string email, string flightCode, string seat, string seatClass,
                        DateTime bookedAt)
        {
            decimal basePrice = FindFlight(flightCode)!.BasePrice;
            decimal price = seatClass == BusinessName ? basePrice * 2.5m : basePrice;
            return new BookingRow(id, name, email, flightCode, seat, seatClass, price, bookedAt);
        }

        var bookings = new List<BookingRow>
        {
            Book("BK10231", "Nguyễn Văn An", "an.nguyen@gmail.com", "VN201", "2A", BusinessName, today.AddDays(-11).AddHours(9)),
            Book("BK10232", "Lê Thị Bình", "binh.le@gmail.com", "VN201", "7C", EconomyName, today.AddDays(-11).AddHours(10)),
            Book("BK10233", "Trần Minh Khoa", "khoa.tran@gmail.com", "VN201", "12F", EconomyName, today.AddDays(-2).AddHours(15)),
            Book("BK10236", "Phạm Minh Châu", "chau.pham@gmail.com", "VN245", "9D", EconomyName, today.AddDays(-10).AddHours(8)),
            Book("BK10240", "Trần Quốc Dũng", "dung.tran@gmail.com", "VN310", "1F", BusinessName, today.AddDays(-9).AddHours(14)),
            Book("BK10244", "Hoàng Thu Hà", "ha.hoang@gmail.com", "VN310", "11B", EconomyName, today.AddDays(-8).AddHours(16)),
            Book("BK10251", "Đỗ Gia Huy", "huy.do@gmail.com", "VN412", "5A", EconomyName, today.AddDays(-7).AddHours(11)),
            Book("BK10257", "Vũ Ngọc Lan", "lan.vu@gmail.com", "VN518", "3C", BusinessName, today.AddDays(-6).AddHours(9)),
            Book("BK10260", "Bùi Đức Mạnh", "manh.bui@gmail.com", "VN518", "8E", EconomyName, today.AddDays(-5).AddHours(20)),
            Book("BK10266", "Ngô Thanh Nga", "nga.ngo@gmail.com", "VN622", "6B", EconomyName, today.AddDays(-12).AddHours(13)),
            Book("BK10268", "Lý Thanh Tâm", "tam.ly@gmail.com", "VN622", "1A", BusinessName, today.AddDays(-12).AddHours(13.5)),
            Book("BK10271", "Đặng Hữu Phát", "phat.dang@gmail.com", "VN245", "2D", BusinessName, today.AddDays(-4).AddHours(10)),
            Book("BK10275", "Phan Thị Yến", "yen.phan@gmail.com", "VN245", "10A", EconomyName, today.AddHours(8.25)),
        };

        // Hai vé đã hủy sẵn để báo cáo có số liệu "đã hoàn".
        Cancel(bookings[4], today.AddDays(-6).AddHours(10), refundRate: 1.00m);  // Business, hủy sớm: hoàn 100%
        Cancel(bookings[8], today.AddDays(-3).AddHours(9), refundRate: 0.90m);   // Economy, hủy trước 48h: hoàn 90%
        return bookings;
    }

    private static void Cancel(BookingRow booking, DateTime cancelledAt, decimal refundRate)
    {
        booking.Status = BookingStatus.Cancelled;
        booking.CancelledAt = cancelledAt;
        booking.RefundAmount = booking.PaidAmount * refundRate;
    }
}
