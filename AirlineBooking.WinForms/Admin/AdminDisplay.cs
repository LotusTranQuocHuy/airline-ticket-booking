using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AirlineBooking.Core;

namespace AirlineBooking.WinForms.Admin;

// Kết quả xem trước khi hủy vé: có hủy được không, tỉ lệ phí, số tiền hoàn, ghi chú hiển thị
public sealed record RefundPreview(bool CanCancel, decimal FeeRate, decimal RefundAmount, string Note);

/// <summary>
/// Các thông tin hiển thị mà nhiều form Admin cùng dùng, lấy từ Core qua AppServices.
/// </summary>
internal static class AdminDisplay
{
    // ---------- Chuyến bay ----------
    public static bool HasDeparted(Flight flight) => flight.GetDeparture() <= AppServices.Now;

    public static string StatusText(Flight flight) => HasDeparted(flight) ? "Đã bay" : "Mở bán";

    // "Hà Nội (HAN)" → "TP. Hồ Chí Minh (SGN)"  thành  "HAN → SGN"
    public static string Route(Flight flight) =>
        $"{SampleData.AirportCode(flight.GetOrigin())} → {SampleData.AirportCode(flight.GetDestination())}";

    public static string RouteOf(string flightNumber)
    {
        Flight? flight = AppServices.Catalog.Find(flightNumber);
        return flight is null ? "" : Route(flight);
    }

    public static int BookedCount(Flight flight)
    {
        int count = 0;
        foreach (Seat seat in flight.GetSeats())
        {
            if (!seat.IsAvailable())
            {
                count++;
            }
        }
        return count;
    }

    // Tên các hạng ghế có trên chuyến, ví dụ ["First", "Business", "Economy"]
    public static List<string> SeatClassNames(Flight flight)
    {
        var names = new List<string>();
        foreach (Seat seat in flight.GetSeats())
        {
            string name = seat.GetSeatClass().GetName();
            if (!names.Contains(name))
            {
                names.Add(name);
            }
        }
        return names;
    }

    public static int CountOpenFlights()
    {
        int count = 0;
        foreach (Flight flight in AppServices.Catalog.GetAll())
        {
            if (!HasDeparted(flight))
            {
                count++;
            }
        }
        return count;
    }

    public static int CountBookingsToday()
    {
        int count = 0;
        foreach (Booking booking in AppServices.Bookings.GetBookings())
        {
            if (booking.GetBookedAt().Date == AppServices.Now.Date)
            {
                count++;
            }
        }
        return count;
    }

    // ---------- Vé ----------
    // Mã vé trong Core dài 32 ký tự; trên lưới chỉ hiện 8 ký tự đầu cho vừa cột
    public static string ShortId(Booking booking)
    {
        string id = booking.GetBookingId();
        return (id.Length > 8 ? id[..8] : id).ToUpperInvariant();
    }

    public static string StatusText(Booking booking) =>
        booking.GetStatus() == BookingStatus.Confirmed ? "Đã xác nhận" : "Đã hủy";

    // Phí hủy do HẠNG GHẾ của vé tính (Core) — form Admin không tự chép bảng phí nữa
    public static RefundPreview PreviewCancellation(Booking booking)
    {
        try
        {
            var (fee, refund) = booking.PreviewCancellation(AppServices.Now);
            decimal paid = booking.GetPaidAmount();
            decimal feeRate = paid == 0 ? 0 : fee / paid;
            string seatClass = booking.GetSeat().GetSeatClass().GetName();
            return new RefundPreview(true, feeRate, refund,
                $"Hạng {seatClass}: phí hủy {feeRate * 100:0}%, hoàn {AdminTheme.Money(refund)}.");
        }
        catch (InvalidBookingStateException ex)
        {
            // Vé đã hủy hoặc chuyến đã bay: Core từ chối, ta hiện lý do cho Admin
            return new RefundPreview(false, 0m, 0m, ex.Message);
        }
    }
}
