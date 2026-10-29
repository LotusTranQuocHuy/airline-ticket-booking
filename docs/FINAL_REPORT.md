# BÁO CÁO CUỐI KỲ — AIRLINE TICKET BOOKING SYSTEM

| | |
|---|---|
| **Môn học** | Lập trình hướng đối tượng — Đồ án cuối kỳ |
| **Nhóm** | 2 thành viên: Trần Quốc Huy, Ngô Thành Đạt |
| **Công nghệ** | C# / .NET 8 / Windows Forms / xUnit |
| **Repository** | https://github.com/LotusTranQuocHuy/airline-ticket-booking |
| **Phiên bản báo cáo** | Các link file trong báo cáo gắn với commit [`4cac3b5`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/tree/4cac3b539bc998ede391bb11df41b6945e124490) |

> **Quy ước cho người điền:** các khung có nhãn **🟨 [ĐẠT ĐIỀN]** là phần của Ngô Thành Đạt. Link file nên dùng permalink gắn mã commit: mở file trên GitHub → chọn dòng → nhấn phím `y` → copy đường dẫn.

## Mục lục

1. [Tóm tắt đề bài](#1-tóm-tắt-đề-bài)
2. [Phân công và tiến trình](#2-phân-công-và-tiến-trình)
3. [Kiến trúc và class diagram](#3-kiến-trúc-và-class-diagram)
4. [Self-Explanation](#4-self-explanation)
5. [Race condition](#5-race-condition)
6. [Kiểm thử](#6-kiểm-thử)
7. [AI Usage Log](#7-ai-usage-log)
8. [Hướng dẫn build và chạy](#8-hướng-dẫn-build-và-chạy)
9. [Giới hạn và hướng phát triển](#9-giới-hạn-và-hướng-phát-triển)
10. [Phụ lục: danh sách commit](#phụ-lục-danh-sách-commit)

---

## 1. Tóm tắt đề bài

*Người viết: Trần Quốc Huy*

Xây dựng hệ thống đặt vé máy bay gồm:

- Mô hình hóa **Flight, Seat, Passenger, Booking**.
- Ít nhất **2 hạng ghế** có giá và quy tắc hủy khác nhau, **không lặp logic**. Nhóm làm 4 hạng: First, Business, Premium Economy, Economy.
- **Khóa ghế**: hai hành khách không thể đặt cùng một ghế.
- **Hủy vé hoàn tiền** theo hạng ghế và thời gian còn lại trước giờ bay.
- **Tìm chuyến** theo điểm đi, điểm đến, ngày bay.
- Thể hiện **Polymorphism** (interface chung), **Encapsulation** (Booking bảo vệ việc khóa ghế), **Composition** (Flight sở hữu Seat).

Ngoài yêu cầu của đề, nhóm làm thêm phần **Admin**: tạo / sửa / xóa chuyến bay, quản lý và hủy vé thay khách, báo cáo doanh thu; phía **Người dùng** có thêm chức năng **chuyển vé** cho hành khách khác.

**Bảng phí hủy theo hạng ghế** (phí tính trên số tiền khách đã trả):

| Hạng | Hệ số giá | Còn ≥ 48 giờ | 24 – 48 giờ | Còn < 24 giờ |
|---|---|---|---|---|
| Economy | × 1,0 | 10% | 30% | 100% |
| Premium Economy | × 1,6 | 5% | 20% | 50% |
| Business | × 2,5 | 0% | 0% | 20% |
| First | × 4,0 | 0% (≥ 12 giờ) | 0% | 10% (< 12 giờ) |

---

## 2. Phân công và tiến trình

*Người viết: Trần Quốc Huy — 🟨 [ĐẠT ĐIỀN] kiểm tra lại phần của mình*

**Phân công**

- **Trần Quốc Huy** — thiết kế và phía Admin:
    - Class diagram (Enterprise Architect), kế hoạch 4 tuần
    - Giao diện Admin: màn hình chính, quản lý chuyến bay, sửa chuyến bay, quản lý vé, báo cáo doanh thu
    - Core phía Admin: `Flight.Reschedule`, `Flight.UpdatePrice`, `FlightCatalog.Remove`, `RevenueReportService`
    - Dữ liệu dùng chung cho cả ứng dụng: `AppServices`, `SeatLayout`, `SampleData`
    - Kiểm thử phía Admin (4 test), README, AI Usage Log
- **Ngô Thành Đạt** — phía Người dùng:
    - Chuyển class diagram thành khung code C#
    - Giao diện Người dùng: đăng nhập, trang chính, tìm chuyến, chọn ghế, xác nhận, vé của tôi, chuyển vé
    - Core phía Người dùng: khóa ghế (`Seat`), đặt / hủy vé (`Booking`, `BookingService`), tìm chuyến (`FlightCatalog.Search`), giá và phí hủy (`SeatClassBase`)
    - Project kiểm thử `AirlineBooking.Core.Tests`

**Tiến trình 4 tuần**

| Tuần | Trần Quốc Huy | Ngô Thành Đạt |
|---|---|---|
| 1 | Class diagram, kế hoạch 4 tuần (`9426395`, `87115ee`, `73a02d9`) | Khung code C# từ class diagram (`e020817`) |
| 2 | 5 màn hình Admin, đồng bộ màu, sửa màn hình đăng nhập (`9b0aceb`) | Giao diện Người dùng, dữ liệu demo (`7b0f82a` … `5d6ddbd`) |
| 3 | Core Admin, dữ liệu dùng chung, nối Admin vào Core (`ace53a0`, `a631efe`, `58325a5`, `094da9c`) | Core đặt / hủy vé, nối Người dùng vào Core, 5 test, chuyển vé (`7348164` … `5e4a090`) |
| 4 | 4 test Admin, làm mượt giao diện, class diagram bản cuối (`c867637`, `40b90fc`) | 🟨 [ĐẠT ĐIỀN] test khứ hồi, test đa luồng, sửa giao diện — kèm mã commit |

Danh sách đầy đủ ở [Phụ lục](#phụ-lục-danh-sách-commit).

---

## 3. Kiến trúc và class diagram

*Người viết: Trần Quốc Huy*

### 3.1. Ba project, tách nghiệp vụ khỏi giao diện

| Project | Vai trò |
|---|---|
| `AirlineBooking.Core` | Toàn bộ quy tắc nghiệp vụ: giá vé, phí hủy, khóa ghế, đặt / hủy vé, báo cáo doanh thu. **Không biết gì về giao diện**. |
| `AirlineBooking.WinForms` | Giao diện. Form chỉ nhận thao tác của người dùng, gọi sang Core, hiển thị kết quả hoặc thông báo lỗi. |
| `AirlineBooking.Core.Tests` | Kiểm thử tự động (xUnit) chạy thẳng trên Core, không cần mở giao diện. |

Vì quy tắc nghiệp vụ chỉ nằm trong Core, nên:
- Kiểm thử được mà không cần giao diện.
- Ở tuần 2, phía Admin có đoạn **chép tạm bảng phí hủy** trong dữ liệu demo; tuần 3 đoạn đó **bị xóa hẳn** và thay bằng lời gọi `Booking.PreviewCancellation` của Core — bảng phí hủy chỉ còn ở **một chỗ** là các lớp hạng ghế.

### 3.2. Class diagram

![Class diagram](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/docs/class-diagram.jpg?raw=true)

- Ảnh (Enterprise Architect): [docs/class-diagram.jpg](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/docs/class-diagram.jpg) — file gốc: [docs/AirlineBooking.eap](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/docs/AirlineBooking.eap)
- Bản Mermaid (có cả 3 class exception và quan hệ «throw»): [docs/class-diagram.md](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/docs/class-diagram.md)

### 3.3. Các quan hệ chính

| Quan hệ | Giữa | Ý nghĩa |
|---|---|---|
| **Composition** ◆ | `Flight` ◆— `Seat` (1 – 1..*) | Ghế chỉ tồn tại trong một chuyến bay; chỉ `Flight` tạo được ghế |
| **Composition** ◆ | `SeatClassBase` ◆— `CancellationTier` | Bảng phí hủy thuộc về đúng một hạng ghế |
| **Aggregation** ◇ | `FlightCatalog` ◇— `Flight`, `BookingService` ◇— `Booking` | Danh mục chỉ quản lý danh sách, chuyến bay / vé vẫn có ý nghĩa riêng |
| **Realization** | `SeatClassBase` ⇢ `ISeatClass` | Lớp trừu tượng cài đặt interface chung |
| **Generalization** | `EconomyClass`, `PremiumEconomyClass`, `BusinessClass`, `FirstClass` → `SeatClassBase` | 4 hạng ghế kế thừa phần dùng chung |
| **Association** | `Booking` → `Flight`, `Seat`, `Passenger`; `Seat` → `Passenger` (0..1, người đang giữ ghế) | |
| **Dependency** | `RevenueReportService` ⇢ `RevenueLine` | Báo cáo tạo ra các dòng doanh thu |

---

## 4. Self-Explanation

### 4.1. Các nguyên lý OOP trong thiết kế

*Người viết: Trần Quốc Huy*

#### a) Polymorphism — interface chung `ISeatClass`

- [`ISeatClass`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/ISeatClass.cs#L7-L14) là **hợp đồng** của mọi hạng ghế: `GetName()`, `CalculatePrice(price)`, `CancellationFeeRate(timeLeft)`.
- `Seat` chỉ giữ một biến kiểu `ISeatClass`. `Booking` tính giá và phí hủy **qua interface này**, không hề biết ghế là Economy hay Business:
    - Tính giá khi đặt vé — [`Booking.Create`, dòng 50](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Booking.cs#L50): `seat.GetSeatClass().CalculatePrice(seat.GetPrice())`
    - Tính phí khi hủy vé — [`Booking.CalculateCancellationQuote`, dòng 128](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Booking.cs#L128): `_seat.GetSeatClass().CancellationFeeRate(timeLeft)`
- Cùng một dòng code đó, ghế Economy và ghế Business trả về kết quả khác nhau — chứng minh bằng test ở [mục 6](#6-kiểm-thử).

#### b) Không lặp logic — lớp trừu tượng `SeatClassBase`

- Cách tính giá và cách **tra bảng phí hủy theo mốc giờ** giống nhau cho mọi hạng, nên đặt một lần trong [`SeatClassBase`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/SeatClassBase.cs#L35-L56) (`CalculatePrice`, `CancellationFeeRate`).
- Mỗi hạng ghế con chỉ truyền **tên, hệ số giá và bảng phí** vào constructor — ví dụ [`EconomyClass`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/EconomyClass.cs) chỉ có 14 dòng, không có vòng lặp hay câu `if` nào.
- **Vì sao cần cả interface lẫn lớp trừu tượng?** `ISeatClass` là hợp đồng mà `Seat` / `Booking` phụ thuộc vào; `SeatClassBase` là cách cài đặt dùng chung. Một hạng ghế có quy tắc hoàn toàn khác (ví dụ vé khuyến mãi không hoàn tiền) có thể cài đặt thẳng `ISeatClass` mà không phải kế thừa bảng mốc giờ.

#### c) Khả năng mở rộng

- Thêm một hạng ghế mới chỉ cần **thêm một file** kế thừa `SeatClassBase`, **không sửa** `Seat`, `Booking`, `BookingService`.
- Minh chứng: [`PremiumEconomyClass`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/PremiumEconomyClass.cs) và [`FirstClass`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/FirstClass.cs) mỗi file khoảng 13 dòng; cả hai được dùng trong dữ liệu mẫu và hiển thị trên giao diện mà không cần sửa lớp nghiệp vụ nào. Muốn hạng mới xuất hiện trong form tạo chuyến bay thì thêm một dòng trong [`SeatLayout`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.WinForms/SeatLayout.cs) (phía giao diện).

#### d) Encapsulation — `Booking` và `Seat` tự bảo vệ trạng thái

- Constructor của `Booking` là **private** ([dòng 20](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Booking.cs#L20)). Cách duy nhất tạo vé là [`Booking.Create`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Booking.cs#L32-L65) — hàm này **bắt buộc khóa ghế trước** (`seat.TryLock`), nếu ghế đã có người thì ném `SeatUnavailableException`. Không thể tạo ra một vé mà không giữ ghế.
- Trạng thái ghế (`Available → Locked → Booked`) là field private, chỉ đổi qua `TryLock` / `Confirm` / `Release`. `Release` và `ChangePrice` là **internal** — form không gọi được, chỉ `Booking` và `Flight` trong Core gọi.
- Mọi field đều private, chỉ đọc qua getter (`GetPaidAmount()`, `GetStatus()`…). Ví dụ: số tiền khách trả được chụp lại vào `_paidAmount` lúc đặt vé ([dòng 27](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Booking.cs#L27)) và không có setter — nên Admin đổi giá sau đó **không** ảnh hưởng tới vé đã bán.

#### e) Composition — `Flight` sở hữu `Seat`

- Constructor của `Seat` là **internal** ([dòng 16](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Seat.cs#L16)), nên giao diện không tự `new Seat(...)` được. Ghế chỉ được tạo bên trong [`Flight.AddSeat`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Flight.cs#L35-L50).
- Bên ngoài chỉ nhận danh sách ghế **chỉ đọc**: [`GetSeats()` trả về `IReadOnlyList<Seat>`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Flight.cs#L30), không thêm / xóa ghế được.

### 4.2. Chức năng Admin

*Người viết: Trần Quốc Huy*

#### a) Đổi giờ bay — [`Flight.Reschedule(newDeparture, now)`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Flight.cs#L52-L62)

- Từ chối nếu chuyến **đã cất cánh** (`InvalidOperationException`) hoặc giờ mới **ở quá khứ** (`ArgumentException`).
- Nhận `now` làm tham số thay vì tự gọi `DateTime.Now` — giống `Booking.Cancel(now)` — để kiểm thử được với mốc thời gian cố định.

#### b) Đổi giá theo hạng ghế — [`Flight.UpdatePrice(seatClassName, newPrice)`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Flight.cs#L64-L83)

- Gom các ghế đúng hạng, **kiểm tra có ghế nào không rồi mới đổi** (để không đổi giá một nửa rồi báo lỗi), sau đó gọi `Seat.ChangePrice` cho từng ghế.
- Vé đã đặt **giữ nguyên giá** vì `Booking` lưu `_paidAmount` lúc đặt — ghế còn trống nhận giá mới cho lần bán sau (giống cách hãng bay tăng giá khi gần ngày bay).

#### c) Xóa chuyến bay — [`FlightCatalog.Remove(flight)`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/FlightCatalog.cs#L48-L59)

- Chỉ xóa khi **chưa có ghế nào được đặt**; ngược lại ném `InvalidOperationException`. Dùng cho trường hợp Admin nhập nhầm chuyến.
- Việc **hãng hủy một chuyến đã có khách** (hoàn 100%, giữ lịch sử) là nghiệp vụ khác, chưa làm — xem [mục 9](#9-giới-hạn-và-hướng-phát-triển).

#### d) Báo cáo doanh thu — [`RevenueReportService`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/RevenueReportService.cs)

| Hàm | Cách tính |
|---|---|
| [`GetTotalPaid`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/RevenueReportService.cs#L21-L32) | Cộng `GetPaidAmount()` các vé có **ngày đặt** trong khoảng; vé đã hủy vẫn tính vì khách đã thật sự trả tiền |
| [`GetTotalRefunded`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/RevenueReportService.cs#L34-L47) | Cộng `GetRefundAmount()` các vé có **ngày hủy** trong khoảng (`GetCancelledAt()` kiểu `DateTime?`, null nếu chưa hủy) |
| `GetNetRevenue` | Đã thu − đã hoàn |
| `GetRevenueByFlight`, `GetRevenueBySeatClass` | Cùng dùng hàm [`BuildLines`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/RevenueReportService.cs#L76-L120): chia vé vào từng nhóm bằng `Dictionary` (tên nhóm → danh sách vé), mỗi nhóm tạo một `RevenueLine` |

"Đã thu" và "đã hoàn" tính theo hai mốc ngày khác nhau vì tiền đi vào và đi ra ở hai thời điểm khác nhau: vé đặt 28/9 nhưng hủy 3/10 thì khoản thu thuộc tháng 9, khoản hoàn thuộc tháng 10.

#### e) Một kho dữ liệu dùng chung — [`AppServices`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.WinForms/AppServices.cs#L15-L23)

- Một bộ `FlightCatalog` / `BookingService` / `RevenueReportService` duy nhất cho cả ứng dụng, nên **vé khách đặt bên Người dùng hiện ngay bên Admin**.
- Đặt ở project WinForms (tầng giao diện), không đặt trong Core, để Core vẫn kiểm thử được: mỗi test tự tạo `BookingService` riêng với đồng hồ giả.
- [`SeatLayout`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.WinForms/SeatLayout.cs#L25) giữ **một quy tắc sắp ghế** cho cả hai phía (First, Business, Premium Economy: 4 ghế A C | D F; Economy: 6 ghế A–F); sơ đồ ghế ở cả hai phía vẽ từ danh sách ghế thật của chuyến bay. [`SampleData`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.WinForms/SampleData.cs) nạp dữ liệu mẫu lúc mở ứng dụng.

#### f) Giao diện Admin chỉ gọi Core

| Màn hình | Gọi vào Core |
|---|---|
| [Quản lý chuyến bay](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.WinForms/Admin/FrmFlightManager.cs) | `new Flight` + `SeatLayout.AddSeats` + `FlightCatalog.Add`; `FlightCatalog.Remove` |
| [Sửa chuyến bay](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.WinForms/Admin/FrmFlightEdit.cs) | `Flight.Reschedule`, `Flight.UpdatePrice` |
| [Quản lý vé](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.WinForms/Admin/FrmBookingManager.cs) | `BookingService.GetBookings`, `BookingService.Cancel`; phí hủy xem trước qua [`AdminDisplay.PreviewCancellation`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.WinForms/Admin/AdminDisplay.cs#L100-L116) → `Booking.PreviewCancellation` |
| [Báo cáo doanh thu](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.WinForms/Admin/FrmRevenueReport.cs) | Các hàm của `RevenueReportService` |

Mọi lỗi Core ném ra (`ArgumentException`, `InvalidOperationException`, `InvalidBookingStateException`) đều được form bắt và hiển thị bằng hộp thoại tiếng Việt — form **không tự kiểm tra lại** các quy tắc nghiệp vụ.

### 4.3. Chức năng Người dùng

> 🟨 **[ĐẠT ĐIỀN]** — mỗi ý ghi: làm gì, ở class / method nào, link permalink tới đúng dòng code (commit `4cac3b5` hoặc commit mới hơn của bạn).
>
> - **Khóa ghế** — `Seat.TryLock` / `Confirm` / `Release`: ghế đi qua các trạng thái Available → Locked → Booked thế nào; vì sao `Release` cần truyền hành khách (`IsHeldBy`).
> - **Đặt vé** — `BookingService.Book` → `Booking.Create`: tìm chuyến (không có thì `FlightNotFoundException`), tìm ghế, khóa ghế, tính giá qua hạng ghế, xác nhận; nếu có lỗi giữa chừng thì `Release` ghế (khối `try / catch` trong `Create`).
> - **Hủy vé và hoàn tiền** — `Booking.Cancel` / `PreviewCancellation` / `CalculateCancellationQuote`: kiểm tra trạng thái, tính thời gian còn lại, lấy tỉ lệ phí từ hạng ghế, làm tròn, trả ghế.
> - **Tìm chuyến** — `FlightCatalog.Search(origin, destination, date)`.
> - **Chuyển vé** — `BookingService.Transfer` → `Booking.TransferTo` → `Seat.TransferTo`.
> - **Khứ hồi** — cách đặt vé khứ hồi hiện tại và test tương ứng.
> - **Giao diện** — `FrmSearchFlight`, `FrmSeatMap`, `FrmBookingConfirm`, `FrmMyBookings`, `FrmTransferBooking` gọi vào Core qua `DemoData` (dùng `AppServices`).

---

## 5. Race condition

### 5.1. Đặt trùng ghế (check-then-act)

> 🟨 **[ĐẠT ĐIỀN]** — gợi ý nội dung:
>
> - **Vấn đề:** nếu "kiểm tra ghế còn trống" và "đánh dấu ghế đã giữ" là hai bước tách rời, hai hành khách bấm đặt cùng lúc có thể cùng thấy ghế trống và cùng đặt được (check-then-act). Vẽ trình tự hai luồng xen kẽ để minh họa.
> - **Cách nhóm xử lý:** [`Seat.TryLock`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Seat.cs#L52-L66) gộp kiểm tra và đổi trạng thái vào **một khối `lock (_sync)`**: luồng thứ hai phải chờ, và khi vào được thì ghế đã là Locked → trả về `false` → `Booking.Create` ném `SeatUnavailableException`.
> - **Vì sao khóa theo từng ghế** (mỗi `Seat` có `_sync` riêng) mà không khóa cả hệ thống: hai người đặt hai ghế khác nhau không phải chờ nhau.
> - **Minh chứng:** test đặt đồng thời trong `BookingFlowTests` (và test nhiều luồng ở tuần 4) — chỉ đúng một người thành công.

### 5.2. Các chỗ khóa khác

*Người viết: Trần Quốc Huy*

- **Giá ghế** — [`Seat.ChangePrice`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Seat.cs#L115-L125) ghi `_price` trong `lock (_sync)`, và [`GetPrice`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/Seat.cs#L28-L34) cũng đọc trong cùng khóa. Kiểu `decimal` dài 16 byte nên máy **không ghi xong trong một bước**: nếu Admin đổi giá đúng lúc khách đang đặt, khách có thể đọc được một con số "nửa cũ nửa mới". Đã khóa khi ghi thì khi đọc cũng phải khóa cùng một đối tượng.
- **Danh mục chuyến bay** — mọi thao tác của `FlightCatalog` (`Add`, `Find`, `GetAll`, [`Remove`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core/FlightCatalog.cs#L48-L59)) chạy trong `lock (_sync)`, vì `List<Flight>` không an toàn khi một luồng đang duyệt mà luồng khác thêm / xóa. `GetAll()` trả về **bản sao** (`ToArray()`) nên form duyệt danh sách thoải mái mà không giữ khóa.
- **Xóa chuyến** — `Remove` kiểm tra "chưa có ghế nào được đặt" rồi mới xóa, cả hai bước trong cùng một khóa của danh mục.
- **Giới hạn còn lại:** `Remove` khóa danh mục nhưng không khóa từng ghế, nên về lý thuyết một vé có thể được đặt đúng khoảnh khắc giữa lúc kiểm tra và lúc xóa. Trong ứng dụng hiện tại, mọi thao tác giao diện chạy trên một luồng nên trường hợp này không xảy ra; nếu chuyển sang máy chủ nhiều người dùng thì cần đánh dấu chuyến "đang xóa" trước khi kiểm tra.

---

## 6. Kiểm thử

### 6.1. Tổng hợp

*Người viết: Trần Quốc Huy — 🟨 [ĐẠT ĐIỀN] bổ sung các test mới của tuần 4*

Chạy bằng `dotnet test` hoặc Test Explorer trong Visual Studio. Kết quả tại commit `4cac3b5`: **9/9 test đạt**.

| Loại test đề yêu cầu | Test | File |
|---|---|---|
| **Happy path** (đặt → hủy → hoàn tiền) | `SearchBookAndCancel_ReleasesSeatAndRefundsTheQuotedAmount` | `BookingFlowTests` |
| **Lỗi validation** | `BookingService_RejectsConcurrentDuplicateSeatAndRepeatedCancellation` (đặt trùng ghế, hủy hai lần) | `BookingFlowTests` |
| | `Remove_RejectsFlightWithBookedSeat`, `Reschedule_RejectsPastTimeAndDepartedFlight` | `AdminFeatureTests` |
| **Đổi hạng ghế cho thấy đa hình** | `CancellationWithin24Hours_RefundsDifferentAmountsBySeatClass` (cùng thời điểm hủy, Economy và Business hoàn khác nhau) | `BookingFlowTests` |
| | `RevenueReport_SeparatesPaidAndRefundedAmounts` (cùng luồng đặt vé, Business trả × 2,5 so với Economy) | `AdminFeatureTests` |
| Chức năng thêm | `TransferMovesBookingAndSeatOwnershipToTheRecipient`, `TransferRejectsCancelledBooking` | `BookingFlowTests` |
| | `UpdatePrice_DoesNotChangeAlreadyBookedTicket` | `AdminFeatureTests` |
| Khứ hồi | 🟨 [ĐẠT ĐIỀN] tên test + link | |
| Đa luồng (bonus) | 🟨 [ĐẠT ĐIỀN] tên test + link | |

Mọi test dùng **đồng hồ giả** (`FrozenTimeProvider`) cố định ở 15/04/2030 08:00, nên kết quả không phụ thuộc giờ chạy.

### 6.2. Test phía Admin — [`AdminFeatureTests.cs`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core.Tests/AdminFeatureTests.cs)

*Người viết: Trần Quốc Huy*

| Test | Tình huống | Kiểm tra |
|---|---|---|
| [`RevenueReport_SeparatesPaidAndRefundedAmounts`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core.Tests/AdminFeatureTests.cs#L21-L54) | Đặt 1 vé Economy (1.000.000) và 1 vé Business (2.500.000), hủy vé Business khi còn 72 giờ | Đã thu 3.500.000; đã hoàn 2.500.000 (Business hủy sớm, phí 0%); doanh thu thực 1.000.000; theo hạng ghế có đúng 2 dòng (Business thực 0, Economy thực 1.000.000); xem ngày hôm sau thì đã thu = 0 |
| [`UpdatePrice_DoesNotChangeAlreadyBookedTicket`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core.Tests/AdminFeatureTests.cs#L56-L73) | Đặt vé giá 1.000.000, Admin tăng giá Economy lên 1.500.000 | Vé đã đặt vẫn 1.000.000; ghế còn trống thành 1.500.000 |
| [`Remove_RejectsFlightWithBookedSeat`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core.Tests/AdminFeatureTests.cs#L75-L96) | Xóa chuyến đã có vé và chuyến chưa có vé | Chuyến có vé: `InvalidOperationException`, vẫn còn trong danh mục; chuyến chưa có vé: xóa được, `Find` trả về null |
| [`Reschedule_RejectsPastTimeAndDepartedFlight`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/AirlineBooking.Core.Tests/AdminFeatureTests.cs#L98-L117) | Dời giờ về quá khứ; dời chuyến đã bay; dời hợp lệ | Lần lượt `ArgumentException` (giờ bay giữ nguyên), `InvalidOperationException`, và đổi giờ thành công |

**Hai lỗi chính test đã bắt được trong lúc viết** (ghi lại để thấy giá trị của kiểm thử):
- Viết `1.5m` (một phẩy năm đồng) thay vì `1_500_000m` — một assert vô tình "qua" mà không kiểm tra gì; sửa lại thì test có ý nghĩa.
- So nhầm giờ bay của chuyến khác — test báo đỏ đúng chỗ.

### 6.3. Test phía Người dùng — `BookingFlowTests.cs`

> 🟨 **[ĐẠT ĐIỀN]** — bảng giống mục 6.2 (tên test + link dòng, tình huống, kiểm tra) cho các test: đặt – hủy – hoàn tiền, đặt trùng ghế đồng thời và hủy hai lần, hoàn tiền khác nhau theo hạng ghế, chuyển vé, từ chối chuyển vé đã hủy, **khứ hồi**, **đa luồng**.

### 6.4. Kiểm thử thủ công cả luồng

*Người viết: Trần Quốc Huy*

- Khách đặt vé bên Người dùng → vé hiện trong Quản lý vé bên Admin → Admin hủy thay khách → tiền hoàn đúng theo hạng ghế → Báo cáo doanh thu tăng đúng khoản "đã hoàn".
- Admin thêm chuyến mới → bên Người dùng tìm thấy và mở được sơ đồ ghế.
- Thêm trùng số hiệu, xóa chuyến đã có vé, dời giờ về quá khứ, sửa chuyến đã bay → hiển thị thông báo tiếng Việt, ứng dụng không bị tắt.
- Hiển thị đúng ở mức phóng to màn hình 125%.

---

## 7. AI Usage Log

*Người viết: Trần Quốc Huy — 🟨 [ĐẠT ĐIỀN] kiểm tra dòng của mình*

Nhật ký chi tiết (prompt, nội dung AI tạo ra, phần nhóm tự sửa, commit): [docs/AI_USAGE_LOG.md](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/4cac3b539bc998ede391bb11df41b6945e124490/docs/AI_USAGE_LOG.md)

| Công cụ | Model | Người dùng | Dùng cho |
|---|---|---|---|
| Claude Code (Anthropic) | Claude Opus 5.5 | Trần Quốc Huy | Kế hoạch; kiểm tra class diagram; khung code C# (tuần 1); code giao diện Admin; hướng dẫn và sửa mẫu code Core phía Admin; gộp code và dữ liệu dùng chung; khung test Admin; tài liệu |
| Claude Design (Anthropic) | — | Trần Quốc Huy | Bản thiết kế 5 màn hình Admin (tuần 2) |
| GitHub Copilot | GPT-6-Luna | Ngô Thành Đạt | Giao diện Người dùng; Core đặt / hủy vé; kiểm thử tự động; chuyển vé |

**Phần nhóm tự làm** (tóm tắt — chi tiết trong log):
- **Trần Quốc Huy:** tự vẽ và sửa class diagram qua nhiều vòng; tự viết `Flight.Reschedule`, `UpdatePrice`, `FlightCatalog.Remove`, các hàm `RevenueReportService` sau khi được góp ý; tự viết `AppServices`; tự nối 5 form Admin vào Core theo hướng dẫn; tự viết 3 / 4 test Admin; tự quyết định các hướng: tông màu navy, không dùng database, chọn cách viết không dùng lambda; tự chạy thử toàn bộ luồng.
- **Ngô Thành Đạt:** 🟨 [ĐẠT ĐIỀN]

Quy ước: commit có nội dung do AI viết ghi `Co-Authored-By: Claude Opus 5.5` hoặc `Co-authored-by: Copilot` trong nội dung commit.

---

## 8. Hướng dẫn build và chạy

*Người viết: Trần Quốc Huy*

**Yêu cầu:** Windows 10/11, .NET 8 SDK; Visual Studio 2022 (tùy chọn).

```bash
git clone https://github.com/LotusTranQuocHuy/airline-ticket-booking.git
cd airline-ticket-booking
dotnet build AirlineBooking.sln
dotnet run --project AirlineBooking.WinForms
dotnet test AirlineBooking.Core.Tests
```

Hoặc mở `AirlineBooking.sln` bằng Visual Studio, chọn `AirlineBooking.WinForms` làm Startup Project, nhấn F5.

**Sử dụng:**
- Màn hình đăng nhập → **Quản trị viên** (không cần mật khẩu) hoặc **Người dùng** (nhập họ tên, email không bắt buộc).
- Dữ liệu mẫu nạp sẵn: 11 chuyến bay (có 1 chuyến đã bay để thử trường hợp không được hủy / sửa), 7 vé (có 1 vé đã hủy).

---

## 9. Giới hạn và hướng phát triển

*Người viết: Trần Quốc Huy — 🟨 [ĐẠT ĐIỀN] bổ sung phía Người dùng*

**Giới hạn hiện tại**
- Dữ liệu lưu trong bộ nhớ, tắt ứng dụng là mất. Vé mẫu nạp lúc mở ứng dụng nên đều có ngày đặt là hôm nay.
- Chưa có nghiệp vụ **hãng hủy chuyến** đã có khách (hoàn 100% cho mọi vé, giữ lịch sử) — hiện chỉ xóa được chuyến chưa có vé.
- Đăng nhập Admin không cần mật khẩu (theo phạm vi đề).
- 🟨 [ĐẠT ĐIỀN]

**Hướng phát triển**
- Lưu dữ liệu: thêm interface `IBookingRepository` với hai cách cài đặt (bộ nhớ cho test, cơ sở dữ liệu cho bản chạy thật) — Core không phải sửa nhờ phụ thuộc vào interface.
- Hủy chuyến: thêm trạng thái chuyến bay (`Scheduled`, `Cancelled`) và `BookingService.CancelFlight`.
- 🟨 [ĐẠT ĐIỀN]

---

## Phụ lục: danh sách commit

*Người viết: Trần Quốc Huy — 🟨 [ĐẠT ĐIỀN] thêm commit tuần 4 của mình*

Ghi chú: một số commit của Ngô Thành Đạt hiển thị tên `SSGMOkitaSouji` (cùng email, khác tên cấu hình Git).

| Tuần | Commit | Người | Nội dung |
|---|---|---|---|
| 1 | [`9426395`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/9426395) | Huy | Add README, class diagram and project plan |
| 1 | [`87115ee`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/87115ee) | Huy | Update plan to 4 weeks with member names and UI task split |
| 1 | [`73a02d9`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/73a02d9) | Huy | Update class diagram with admin features and fixed signatures |
| 1 | [`e020817`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/e020817) | Đạt | Add airline booking application source |
| 1 | [`d279b7a`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/d279b7a) | Huy | Add AI usage log for week 1 |
| 2 | [`7b0f82a`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/7b0f82a) | Đạt | Add user booking UI |
| 2 | [`9b0aceb`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/9b0aceb) | Huy | Add admin UI forms for flight, booking and revenue management |
| 2 | [`0d1a75e`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/0d1a75e) | Đạt | Fix discover banner text overlap |
| 2 | [`7753981`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/7753981) | Đạt | Adjust discovery banner subtitle position |
| 2 | [`3385f8a`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/3385f8a) | Huy | Document week 2 admin UI AI usage and update README structure |
| 2 | [`a4f1501`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/a4f1501), [`360fc72`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/360fc72), [`1423395`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/1423395) | Đạt | AI usage log tuần 2 |
| 2 | [`5d6ddbd`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/5d6ddbd) | Đạt | Simplify booking demo labels |
| 3 | [`7348164`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/7348164) | Đạt | Implement user booking domain flow |
| 3 | [`38bef0d`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/38bef0d) | Đạt | Connect user forms to booking services |
| 3 | [`707fb13`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/707fb13) | Đạt | Add booking flow tests and documentation |
| 3 | [`0d8ea20`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/0d8ea20) | Đạt | Fix user booking screens and add ticket transfer |
| 3 | [`5e4a090`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/5e4a090) | Đạt | Fix user booking screen layout |
| 3 | [`ace53a0`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/ace53a0) | Huy | Implement admin-side Core logic for flights, cancellation and revenue |
| 3 | [`a631efe`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/a631efe) | Huy | Merge Dat's week 3 booking flow into admin Core changes |
| 3 | [`58325a5`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/58325a5) | Huy | Share one data store and seat layout between admin and user screens |
| 3 | [`094da9c`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/094da9c) | Huy | Connect admin screens to Core services and remove admin demo data |
| 3 | [`fb4b9c2`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/fb4b9c2) | Huy | Document week 3: AI usage log, class diagram and README structure |
| 3 | [`2a9774b`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/2a9774b) | Huy | Update class diagram for week 3 |
| 4 | [`c867637`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/c867637) | Huy | Add admin feature tests for revenue, pricing, flight removal and rescheduling |
| 4 | [`d2efdea`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/d2efdea) | Huy | Document week 4 admin test AI usage |
| 4 | [`40b90fc`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/40b90fc) | Huy | Vietnamese culture, friendly admin error messages, clipped buttons, final class diagram |
| 4 | [`4cac3b5`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/4cac3b5) | Huy | Document week 4 admin polish and final class diagram AI usage |
| 4 | 🟨 [ĐẠT ĐIỀN] | Đạt | |
