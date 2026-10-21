using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

/// <summary>
/// Dữ liệu mẫu nạp một lần khi mở ứng dụng, vào bộ dịch vụ dùng chung AppServices.
/// Cả phía Admin và phía Người dùng đều thấy cùng các chuyến bay và vé này.
/// </summary>
internal static class SampleData
{
    // Tên sân bay dạng "Thành phố (MÃ)": dễ đọc, và lấy được mã để hiển thị "HAN → SGN"
    public static readonly string[] Airports =
    {
        "Hà Nội (HAN)", "TP. Hồ Chí Minh (SGN)", "Đà Nẵng (DAD)",
        "Phú Quốc (PQC)", "Hải Phòng (HPH)", "Nha Trang (CXR)"
    };

    public const string HaNoi = "Hà Nội (HAN)";
    public const string HoChiMinh = "TP. Hồ Chí Minh (SGN)";
    private const string DaNang = "Đà Nẵng (DAD)";
    private const string PhuQuoc = "Phú Quốc (PQC)";
    private const string NhaTrang = "Nha Trang (CXR)";

    // "Hà Nội (HAN)" -> "HAN"; tên không có mã thì giữ nguyên
    public static string AirportCode(string airport)
    {
        int open = airport.LastIndexOf('(');
        return open >= 0 && airport.EndsWith(')') ? airport[(open + 1)..^1] : airport;
    }

    public static void Seed()
    {
        SeedFlights();
        SeedBookings();
    }

    private static void SeedFlights()
    {
        DateTime today = AppServices.Now.Date;
        DateTime firstDeparture = today.AddDays(3).AddHours(8);

        // Chuyến gần ngày bay: thử các mốc phí hủy dưới 24 giờ / 24–48 giờ
        AddFlight("VN201", HaNoi, HoChiMinh, today.AddDays(1).AddHours(7.5), 1_850_000m);
        AddFlight("VN310", HoChiMinh, PhuQuoc, today.AddDays(2).AddHours(11 + 1 / 3.0), 1_250_000m);

        AddFlight("VN205", HoChiMinh, HaNoi, firstDeparture, 1_250_000m);
        AddFlight("VN207", HoChiMinh, HaNoi, firstDeparture.AddHours(6), 1_390_000m);
        AddFlight("VJ612", HaNoi, DaNang, firstDeparture.AddDays(1).AddHours(3), 780_000m);
        AddFlight("VJ614", HaNoi, DaNang, firstDeparture.AddDays(1).AddHours(8), 850_000m);
        AddFlight("QH112", HoChiMinh, DaNang, firstDeparture.AddDays(2).AddHours(2), 920_000m);
        AddFlight("QH114", HoChiMinh, DaNang, firstDeparture.AddDays(2).AddHours(7), 1_050_000m);
        AddFlight("VN181", DaNang, HaNoi, firstDeparture.AddDays(2).AddHours(6), 990_000m);
        AddFlight("VN183", DaNang, HaNoi, firstDeparture.AddDays(2).AddHours(11), 1_120_000m);

        // Chuyến đã bay: để thử trường hợp không được hủy vé / không được sửa giờ bay
        AddFlight("VN622", HaNoi, NhaTrang, today.AddDays(-3).AddHours(18 + 40 / 60.0), 1_550_000m);
    }

    private static void SeedBookings()
    {
        var sample = new Passenger("sample-booking", "Hành khách mẫu", string.Empty);
        AppServices.Bookings.Book("VN205", "2D", sample);
        AppServices.Bookings.Book("VJ612", "5C", sample);
        AppServices.Bookings.Book("QH112", "7A", sample);

        var an = new Passenger("sample-an", "Nguyễn Văn An", "an.nguyen@gmail.com");
        var binh = new Passenger("sample-binh", "Lê Thị Bình", "binh.le@gmail.com");
        var chau = new Passenger("sample-chau", "Phạm Minh Châu", "chau.pham@gmail.com");
        var dung = new Passenger("sample-dung", "Trần Quốc Dũng", "dung.tran@gmail.com");
        AppServices.Bookings.Book("VN201", "1A", an);
        AppServices.Bookings.Book("VN201", "8C", binh);
        AppServices.Bookings.Book("VN205", "9B", chau);

        // Một vé đã hủy để báo cáo doanh thu có số liệu "đã hoàn"
        var cancelled = AppServices.Bookings.Book("VN310", "2A", dung);
        AppServices.Bookings.Cancel(cancelled.GetBookingId());
    }

    // Máy bay mẫu: 1 hàng First, 2 hàng Business, 2 hàng Premium Economy, 5 hàng Economy (50 ghế)
    private static void AddFlight(string code, string from, string to, DateTime departure, decimal basePrice)
    {
        var flight = new Flight(code, from, to, departure);
        SeatLayout.AddSeats(flight, firstRows: 1, businessRows: 2, premiumEconomyRows: 2, economyRows: 5, basePrice);
        AppServices.Catalog.Add(flight);
    }
}
