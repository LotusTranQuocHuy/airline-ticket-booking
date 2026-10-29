using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AirlineBooking.Core;

namespace AirlineBooking.Core.Tests;

/// <summary>
/// Kiểm thử các chức năng phía Admin: báo cáo doanh thu, đổi giá, xóa chuyến, đổi giờ bay.
/// </summary>
public sealed class AdminFeatureTests
{
    // Mốc thời gian cố định để kết quả test không phụ thuộc giờ thật
    private static readonly DateTime Now = new(2030, 4, 15, 8, 0, 0, DateTimeKind.Utc);
    private static readonly DateOnly Today = DateOnly.FromDateTime(Now);

    // ================= TEST 1 (mẫu đầy đủ) =================
    [Fact]
    public void RevenueReport_SeparatesPaidAndRefundedAmounts()
    {
        // Arrange: chuyến bay sau 72 giờ, 1 ghế Economy và 1 ghế Business, giá gốc 1.000.000
        var (_, flight, bookings) = CreateSystem(Now.AddHours(72));
        AddSeat(flight, "12A", new EconomyClass());
        AddSeat(flight, "1A", new BusinessClass());
        var report = new RevenueReportService(bookings);

        // Act: đặt 2 vé, hủy vé Business
        bookings.Book("VN205", "12A", new Passenger("p1", "Khách Economy", ""));
        var business = bookings.Book("VN205", "1A", new Passenger("p2", "Khách Business", ""));
        bookings.Cancel(business.GetBookingId());

        // Assert: tổng số liệu
        // Economy 1.000.000 x 1,0 + Business 1.000.000 x 2,5 = 3.500.000 đã thu
        // Business hủy trước 24 giờ -> phí 0% -> hoàn đủ 2.500.000
        Assert.Equal(3_500_000m, report.GetTotalPaid(Today, Today));
        Assert.Equal(2_500_000m, report.GetTotalRefunded(Today, Today));
        Assert.Equal(1_000_000m, report.GetNetRevenue(Today, Today));

        // Assert: theo hạng ghế có đúng 2 dòng, số liệu từng dòng đúng
        var byClass = report.GetRevenueBySeatClass(Today, Today);
        Assert.Equal(2, byClass.Count);
        RevenueLine businessLine = FindLine(byClass, "Business");
        Assert.Equal(1, businessLine.GetTicketCount());
        Assert.Equal(0m, businessLine.GetNetRevenue());          // thu 2,5 triệu rồi hoàn 2,5 triệu
        Assert.Equal(1_000_000m, FindLine(byClass, "Economy").GetNetRevenue());

        // Assert: ngoài khoảng ngày thì không có số liệu nào
        DateOnly tomorrow = Today.AddDays(1);
        Assert.Equal(0m, report.GetTotalPaid(tomorrow, tomorrow));
    }

    // ================= TEST 2 =================
    [Fact]
    public void UpdatePrice_DoesNotChangeAlreadyBookedTicket()
    {
        // Arrange
        var (_, flight, bookings) = CreateSystem(Now.AddHours(72));
        AddSeat(flight, "12A", new EconomyClass());
        Seat emptySeat = AddSeat(flight, "13A", new EconomyClass());
        var booking = bookings.Book("VN205", "12A", new Passenger("p1", "Khách", ""));

        // Act: gọi flight.UpdatePrice cho hạng "Economy", giá mới 1.500.000
        flight.UpdatePrice("Economy", 1_500_000m);
        // Assert
        //  1) booking.GetPaidAmount() vẫn là 1.000.000 (vé đã đặt giữ giá cũ)
        Assert.Equal(1_000_000m, booking.GetPaidAmount());
        //  2) emptySeat.GetPrice() đã thành 1.500.000 (ghế còn trống nhận giá mới)
        Assert.Equal(1_500_000m, emptySeat.GetPrice());
    }

    // ================= TEST 3 =================
    [Fact]
    public void Remove_RejectsFlightWithBookedSeat()
    {
        // Arrange: chuyến VN205 có 1 vé; chuyến VN999 chưa ai đặt
        var (catalog, flight, bookings) = CreateSystem(Now.AddHours(72));
        AddSeat(flight, "12A", new EconomyClass());
        bookings.Book("VN205", "12A", new Passenger("p1", "Khách", ""));
        var emptyFlight = new Flight("VN999", "Hà Nội (HAN)", "Đà Nẵng (DAD)", Now.AddHours(72));
        AddSeat(emptyFlight, "12A", new EconomyClass());
        catalog.Add(emptyFlight);

        // Act + Assert
        //  1) catalog.Remove(flight) phải báo lỗi InvalidOperationException   -> Assert.Throws
        Assert.Throws<InvalidOperationException>(() => catalog.Remove(flight));
        //  2) flight vẫn còn trong catalog.GetAll()                           -> Assert.Contains
        Assert.Contains(flight, catalog.GetAll());
        //  3) catalog.Remove(emptyFlight) chạy bình thường, sau đó
        //     catalog.Find("VN999") phải là null                              -> Assert.Null
        catalog.Remove(emptyFlight);
        Assert.Null(catalog.Find("VN999"));
    }

    // ================= TEST 4 =================
    [Fact]
    public void Reschedule_RejectsPastTimeAndDepartedFlight()
    {
        // Arrange: một chuyến chưa bay, một chuyến đã bay 1 giờ trước
        var (_, flight, _) = CreateSystem(Now.AddHours(72));
        DateTime oldDeparture = flight.GetDeparture();
        var departedFlight = new Flight("VN100", "Hà Nội (HAN)", "TP. Hồ Chí Minh (SGN)", Now.AddHours(-1));

        // Act + Assert
        //  1) flight.Reschedule(Now.AddHours(-2), Now) phải báo ArgumentException (giờ mới ở quá khứ)
        Assert.Throws<ArgumentException>(() => flight.Reschedule(Now.AddHours(-2), Now));
        //  2) giờ bay của flight không bị đổi: vẫn bằng oldDeparture
        Assert.Equal(oldDeparture, flight.GetDeparture());
        //  3) departedFlight.Reschedule(Now.AddDays(1), Now) phải báo InvalidOperationException (chuyến đã bay)
        Assert.Throws<InvalidOperationException>(() => departedFlight.Reschedule(Now.AddDays(1), Now));
        //  4) flight.Reschedule(Now.AddHours(96), Now) chạy được, và GetDeparture() bằng Now.AddHours(96)
        flight.Reschedule(Now.AddHours(96), Now);
        Assert.Equal(Now.AddHours(96), flight.GetDeparture());
    }

    // ================= Hàm phụ =================
    private static (FlightCatalog Catalog, Flight Flight, BookingService Bookings) CreateSystem(DateTime departure)
    {
        var flight = new Flight("VN205", "TP. Hồ Chí Minh (SGN)", "Hà Nội (HAN)", departure);
        var catalog = new FlightCatalog();
        catalog.Add(flight);
        var bookings = new BookingService(catalog, new FrozenTimeProvider(Now));
        return (catalog, flight, bookings);
    }

    private static Seat AddSeat(Flight flight, string seatCode, ISeatClass seatClass)
    {
        flight.AddSeat(seatCode, seatClass, 1_000_000m);   // giá gốc 1.000.000
        return flight.GetSeat(seatCode)!;
    }

    // Tìm dòng báo cáo theo tên (không dùng lambda)
    private static RevenueLine FindLine(IReadOnlyList<RevenueLine> lines, string label)
    {
        foreach (RevenueLine line in lines)
        {
            if (line.GetLabel() == label)
            {
                return line;
            }
        }
        throw new Xunit.Sdk.XunitException($"Không có dòng báo cáo \"{label}\".");
    }

    // Đồng hồ giả: luôn trả về đúng thời điểm Now
    private sealed class FrozenTimeProvider(DateTime utcNow) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => new(utcNow);
        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
    }
}
