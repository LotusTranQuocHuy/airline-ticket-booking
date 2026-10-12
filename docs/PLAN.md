# Kế hoạch 3 tuần — Airline Ticket Booking System (C# / .NET 8 / WinForms, nhóm 2 người A & B)

## Context
Đề bài OOP cuối kỳ: hệ thống đặt vé máy bay (tìm chuyến, chọn/khóa ghế theo hạng, hủy vé & hoàn tiền). Chấm theo "Design quality": trách nhiệm class rõ ràng (không God class), dùng đúng Polymorphism / Encapsulation / Composition, và extensibility. Nhóm quen C# → chọn **C# + .NET 8 + WinForms + xUnit**. Tuần 3 ưu tiên **thêm hạng ghế mới (PremiumEconomy/First) mà không sửa class cũ**. Thư mục làm bài: `D:\OOP_Final`.

## Cấu trúc solution (chốt ngay đầu tuần 1)

```
AirlineBooking.sln
├── AirlineBooking.Core        (class library — KHÔNG tham chiếu WinForms)
│   ├── Models/      Flight, Seat, Passenger, Booking, BookingStatus, SeatStatus
│   ├── Fares/       ISeatClass, SeatClassBase (abstract), EconomyClass, BusinessClass
│   │                (tuần 3: PremiumEconomyClass, FirstClass)
│   ├── Services/    FlightCatalog (search), BookingService (điều phối đặt/hủy)
│   └── Exceptions/  SeatUnavailableException, InvalidBookingStateException, FlightNotFoundException
├── AirlineBooking.WinForms    (tham chiếu Core; form chỉ bắt sự kiện → gọi service → hiển thị)
│   ├── SearchForm, SeatMapForm, MyBookingsForm
│   └── Program.cs (khởi tạo dữ liệu mẫu + service, truyền vào form)
└── AirlineBooking.Tests       (xUnit, chỉ test Core)
```

Quy tắc vàng: **không có logic nghiệp vụ trong `*_Click`**. Form chỉ gọi `BookingService`/`FlightCatalog` và bắt exception để hiện `MessageBox`.

## Thiết kế lõi

- **Polymorphism** — `ISeatClass`: `decimal CalculateFare(decimal baseFare)`, `decimal CancellationFeeRate(TimeSpan timeBeforeDeparture)`, `string Name`, `Color DisplayColor`(tùy chọn, hoặc map ở UI). `SeatClassBase` (abstract) giữ cách tra bảng mốc phí chung; subclass chỉ khai báo hệ số giá + bảng mốc → không lặp logic. Refund = giá − phí tính **một chỗ** trong `Booking.Cancel()`.
- **Encapsulation** — `Seat` có `Status { get; private set; }`, chỉ đổi qua `TryLock(holder)`, `Confirm(holder)`, `Release()`. `Booking` là nơi duy nhất điều khiển chuỗi lock → confirm và kiểm tra chuyển trạng thái hợp lệ (`Confirmed → Cancelled`, cấm hủy 2 lần, cấm hủy sau giờ bay).
- **Composition** — `Flight` tự tạo và sở hữu `IReadOnlyList<Seat>` (ghế không tồn tại ngoài chuyến bay); `Booking` tham chiếu `Seat` cụ thể + `Passenger` + `Flight`.
- **Search** — `FlightCatalog.Search(origin, destination, DateOnly date)` bằng LINQ.
- **Thời gian** — inject `TimeProvider` (.NET 8) vào `BookingService`; test dùng `FakeTimeProvider` (`Microsoft.Extensions.TimeProvider.Testing`) để giả lập hủy trước 48h / 24h / 2h.
- **Tiền** — dùng `decimal`, không dùng `double`.
- **Race condition** — check-then-act (`if (seat.IsAvailable) seat.Book()`) giữa 2 thread. Giảm thiểu: `lock (_sync)` trong `Seat.TryLock()` hoặc `Interlocked.CompareExchange` trên trạng thái; lock giữ ghế có hạn. Báo cáo bàn thêm mức DB: optimistic concurrency (`rowversion`) / `UPDATE ... WHERE Status = 'Available'`.

## Phân công
- **A — Domain & Rules**: `Fares/`, `Booking`, cancellation/refund, test polymorphism, form `MyBookingsForm` (hủy vé).
- **B — Infrastructure & Flow**: `Seat` (locking), `Flight`, `FlightCatalog`, `BookingService`, solution/build, test validation + concurrency, `SearchForm` + `SeatMapForm`.
- Cả hai: viết báo cáo phần của mình; **AI Usage Log ghi ngay mỗi lần dùng AI** (ngày, công cụ, prompt, phần dùng/không dùng, đã sửa gì).

## Tuần 1 — Thiết kế + Core chạy được
| Việc | Người |
|---|---|
| Đọc README gốc + rubric, viết Problem restatement (½ trang) | A |
| Class diagram v1 (PlantUML / Mermaid / Visual Studio Class Diagram), cùng chốt | A + B |
| Tạo solution 3 project, Git, `dotnet build` / `dotnet test` chạy được, README build/run | B |
| `ISeatClass`, `SeatClassBase`, `EconomyClass`, `BusinessClass` + bảng phí hủy | A |
| `Seat` (state + TryLock/Confirm/Release), `Flight` (tạo ghế), `Passenger` | B |
| `FlightCatalog.Search` + dữ liệu mẫu (`SampleData`) | B |
| Unit test cơ bản cho fare & seat | A / B |

**Mốc cuối tuần 1:** `dotnet test` xanh, search chạy, giá khác nhau theo hạng.

## Tuần 2 — Đủ yêu cầu bắt buộc + GUI cơ bản + báo cáo nháp
| Việc | Người |
|---|---|
| `Booking` (state machine), `BookingService.Book/Cancel` | A: Booking, B: Service |
| Cancellation flow: refund theo hạng × thời gian còn lại (`TimeProvider`) | A |
| Exceptions + validation (ghế đã đặt, hủy 2 lần, chuyến không tồn tại, hủy sau giờ bay) | B |
| **Test bắt buộc (xUnit):** (1) happy path search → book → cancel → refund; (2) double-booking bị từ chối + hủy 2 lần bị từ chối; (3) cùng kịch bản hủy, Economy vs Business → refund khác nhau (`[Theory]` + `[InlineData]`) | A: (3), B: (1)(2) |
| `SearchForm` (ComboBox origin/destination, DateTimePicker, DataGridView kết quả) | B |
| `SeatMapForm` (lưới Button tạo động từ `flight.Seats`, màu theo hạng, ghế đã đặt disable) | B |
| `MyBookingsForm` (danh sách vé, nút Hủy → MessageBox hiện phí & tiền hoàn) | A |
| Báo cáo nháp: diagram, Self-Explanation 5 yêu cầu + 3 nguyên lý (class/method cụ thể + lý do) | A: Polymorphism, req 2/4; B: Encapsulation, Composition, req 1/3/5 |
| Mục race condition | B |

**Mốc cuối tuần 2:** đủ toàn bộ yêu cầu đề + GUI demo được → nộp được dù tuần 3 trục trặc.

## Tuần 3 — Mở rộng + bonus + bảo vệ
| Việc | Người |
|---|---|
| Thêm `PremiumEconomyClass`, `FirstClass` **chỉ bằng file mới**; ghi `git diff --stat` chứng minh không sửa class cũ (kể cả form — SeatMapForm tự hiện hạng mới) | A |
| Test hạng mới: đặt & hủy, refund đúng | A |
| Bonus concurrency: `Seat.TryLock()` atomic; test `Parallel.For` / `Task.WhenAll` 50 task giành 1 ghế → đúng 1 thành công | B |
| (Tùy thời gian) giữ ghế có hạn — tự nhả sau N phút (dùng `TimeProvider`) | B |
| Hoàn thiện GUI (thông báo lỗi thân thiện, làm mới sơ đồ ghế sau khi hủy) | A + B |
| Cập nhật diagram + báo cáo, chụp màn hình GUI + output `dotnet test` làm evidence | A + B |
| Hoàn thiện AI Usage Log | A + B |
| **Tập bảo vệ:** mỗi người giải thích toàn bộ code; live-code thêm hạng mới hoặc đổi mốc phí hủy | A + B |

## Cấu trúc báo cáo
1. Problem restatement
2. Class diagram + trách nhiệm từng class; sơ đồ 3 project (Core độc lập UI)
3. Self-Explanation: từng yêu cầu 1–5 và từng nguyên lý (class/method cụ thể + vì sao phù hợp bài toán)
4. Race condition & mitigation
5. Extensibility: thêm hạng mới (diff chứng minh) + đổi UI không đụng Core
6. Test evidence (bảng test case → kết quả, output `dotnet test`, screenshot GUI)
7. AI Usage Log
8. Build/run: yêu cầu Windows + .NET 8 SDK; `dotnet test`; `dotnet run --project AirlineBooking.WinForms`

## Verification
- `dotnet test` toàn bộ xanh: 3 nhóm test bắt buộc + test hạng mới + test đa luồng.
- Chạy WinForms: tìm chuyến → chọn ghế → đặt thành công; bấm lại ghế đã đặt → báo lỗi; hủy vé Economy vs Business → tiền hoàn khác nhau.
- `AirlineBooking.Core.csproj` không có tham chiếu tới WinForms.
- Đối chiếu checklist đề: 5 yêu cầu, 3 nguyên lý có Self-Explanation, 3 loại test, race condition, AI Usage Log.
