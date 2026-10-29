# AI Usage Log

Ghi lại mọi lần nhóm dùng AI: ai dùng, hỏi gì, AI tạo ra gì, nhóm đã sửa gì và kết quả nằm ở commit nào trên GitHub.

**Công cụ AI nhóm đã dùng:**

| Công cụ | Model | Người dùng | Dùng cho |
|---|---|---|---|
| Claude Code (ứng dụng Claude desktop, Anthropic) | Claude Opus 5.5 | Trần Quốc Huy | Kế hoạch, kiểm tra class diagram, khung code C#, code giao diện Admin, nối Admin vào Core và dữ liệu dùng chung (tuần 3) |
| Claude Design (Anthropic) | — | Trần Quốc Huy | Bản thiết kế (mockup) 5 màn hình Admin |
| GitHub Copilot (Copilot SDK trong VS Code) | GPT-6-Luna | Ngô Thành Đạt | Giao diện Người dùng, nghiệp vụ đặt / hủy vé trong Core, kiểm thử tự động (tuần 3) |

**Quy ước:** commit có nội dung do AI tạo ghi rõ trong mô tả commit, hoặc có dòng `Co-Authored-By: Claude ...` / `Co-authored-by: Copilot ...`

---

## Tuần 1

### 1. Lập kế hoạch 4 tuần — Trần Quốc Huy (12/10/2026)

| Mục | Nội dung |
|---|---|
| Prompt | Dán toàn bộ đề bài và nhờ lập kế hoạch theo tuần cho nhóm 2 người. Sau đó gửi kế hoạch tự viết (4 tuần, chia việc Admin / Người dùng, form đăng nhập) và yêu cầu: "Nội dung tuần 3 nếu thầy đọc có thể không hiểu, viết sao cho người ngoài đọc và hiểu." |
| AI tạo ra | Bản kế hoạch ban đầu; sau đó trình bày lại kế hoạch của nhóm thành bảng theo tuần, viết lại phần tuần 3 bằng lời dễ hiểu. |
| Nhóm sửa / quyết định | Tự chia lại thành 4 tuần, tự phân công theo tên từng người, tự đặt ra màn hình Admin (tạo vé, quản lý vé, báo cáo doanh thu) và màn hình Người dùng, ý tưởng form đăng nhập. Đổi ngôn ngữ từ Java sang C# WinForms vì nhóm quen C#. |
| Kết quả | [`docs/PLAN.md`](PLAN.md) — commit [`9426395`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/94263957e59fddb37f3334e7d66e02ef160d2757) (bản đầu), [`87115ee`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/87115ee5b5e7908c9ca3b76ff6ed78c1025f9dc4) (bản 4 tuần) |

### 2. Thiết kế class diagram — Trần Quốc Huy (12/10 – 14/10/2026)

| Mục | Nội dung |
|---|---|
| Prompt | "Vẽ giúp tôi class diagram (dùng ký hiệu ASCII)"; "Các class không phải abstract thì để attribute private hết, vẽ lại giúp tôi"; "Tại sao cần interface ISeatClass? SeatDefinition là gì, có cần không? Exception là gì?"; nhiều lần gửi ảnh xuất từ Enterprise Architect: "Bạn kiểm tra giúp tôi". |
| AI tạo ra | Bản phác thảo class diagram dạng ASCII / Mermaid; giải thích vai trò `ISeatClass`, `SeatClassBase`, `SeatDefinition`, exception; danh sách lỗi trên bản vẽ EA (constructor `Seat` có kiểu `void`, `GetCode` / `GetBookedAt` sai kiểu, `Create` thiếu tham số `now`, tham số thiếu kiểu, quan hệ Seat–Passenger ngược chiều, nên dùng composition giữa `SeatClassBase` và `CancellationTier`). Bản Mermaid [`docs/class-diagram.md`](class-diagram.md). |
| Nhóm sửa / quyết định | Tự vẽ toàn bộ sơ đồ trên Enterprise Architect. Quyết định bỏ `SeatDefinition`, thay bằng `Flight.AddSeat`. Tự bổ sung phần Admin: `RevenueReportService`, `RevenueLine`, `Flight.Reschedule` / `UpdatePrice`, thông tin hoàn tiền trong `Booking`. Sửa lần lượt các lỗi AI chỉ ra qua 3 lần kiểm tra. |
| Kết quả | [`docs/class-diagram.jpg`](class-diagram.jpg), [`docs/AirlineBooking.eap`](AirlineBooking.eap), [`docs/class-diagram.md`](class-diagram.md) — commit [`9426395`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/94263957e59fddb37f3334e7d66e02ef160d2757) (bản đầu), [`73a02d9`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/73a02d911a802745800d7778532559ced49770e7) (bản sửa, có dòng `Co-Authored-By: Claude Opus 5.5`) |

### 3. Chuyển class diagram sang code C# — Trần Quốc Huy, Ngô Thành Đạt (14/10/2026)

| Mục | Nội dung |
|---|---|
| Prompt | "Tôi lỡ xuất code từ EA sang Java, bạn chuyển sang C#, sửa cho đúng cú pháp, thêm các class exception và xóa file Java"; "Cập nhật code C# cho khớp với class diagram đã sửa". |
| AI tạo ra | 21 file C# khung (.NET 8) chuyển từ code Java do Enterprise Architect xuất ra: field private, constructor, getter, constructor 4 hạng ghế với hệ số giá và bảng phí hủy; 3 class exception `SeatUnavailableException`, `InvalidBookingStateException`, `FlightNotFoundException`. Các method nghiệp vụ (`TryLock`, `Book`, `Cancel`, `Search`, báo cáo doanh thu) chỉ để khung, ghi chú `TODO (tuần 3)`, **chưa cài đặt**. |
| Nhóm sửa / quyết định | Code được sinh từ class diagram do nhóm tự vẽ, nên tên class / method / tham số theo đúng sơ đồ của nhóm. Đổi tên tham số constructor `Seat` theo sơ đồ đã sửa. Ngô Thành Đạt đưa code lên GitHub. Phần logic nghiệp vụ nhóm sẽ tự cài đặt ở tuần 3. |
| Kết quả | [`AirlineBooking.Core/`](../AirlineBooking.Core), [`AirlineBooking.WinForms/`](../AirlineBooking.WinForms) — commit [`e020817`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/e02081716adb15fc47f87595bcfe918a8662f884) |

## Tuần 2

### 1. Giao diện Admin — Trần Quốc Huy (16/10 – 18/10/2026)

| Mục | Nội dung |
|---|---|
| Công cụ AI | **Claude Design** (thiết kế mockup) và **Claude Code — model Claude Opus 5.5** (viết code WinForms). |
| Prompt | Claude Design: gửi ảnh nhiệm vụ tuần 2 (5 màn hình Admin) kèm mô tả "Airline Ticket Booking System (C# / .NET 8 / WinForms)"; "Bạn xuất ra file để tôi đưa cho Claude Code". Claude Code: "Từ các file tôi gửi (Admin Forms.dc.html, README.md), hãy hoàn thiện form giao diện admin"; sau khi tự chạy thử: "Tôi chọn tông navy, đổi màu bên Admin cho đồng bộ"; "Hình đăng nhập có vẻ bị lỗi, nút chọn Role admin nên là màu xanh dương"; "Thêm nút Quay lại về trang chính"; "Hướng dẫn tôi tự sửa"; "Bước 2.3 tôi không biết đổi, bạn làm giúp"; "Bạn sửa phần đó giúp tôi". |
| AI tạo ra | Claude Design: mockup HTML 5 form và tài liệu bàn giao (tên control, kích thước, màu); mức hoàn tiền 70% trong mockup do AI tự đặt làm ví dụ. Claude Code: 5 form `FrmAdminMain`, `FrmFlightManager`, `FrmFlightEdit`, `FrmBookingManager`, `FrmRevenueReport` (`.cs` + `.Designer.cs`), `AdminTheme`, các control tự vẽ (`CardPanel`, `MenuCard`, `BarChart`, `SeatLabel`) và dữ liệu mẫu `AdminDemoData`; thay mức hoàn 70% bằng bảng phí hủy của đề (Economy 10% / 30% / 100%, Business 0% / 20%); hướng dẫn từng bước đổi màu, sửa màn hình đăng nhập, thêm nút Quay lại; trực tiếp sửa màu chữ mặc định của 5 form và chỉnh vị trí, kiểu, phím Esc của nút Quay lại. |
| Nhóm sửa / quyết định | Trần Quốc Huy tự chạy thử toàn bộ giao diện, phát hiện hình nền màn hình đăng nhập bị vỡ khi đổi kích thước cửa sổ; quyết định đổi giao diện Admin sang tông navy cho đồng bộ với phần Người dùng, đổi nút "Quản trị viên" sang màu xanh, thêm nút "Quay lại" cho 3 form. Tự sửa code: thêm `ResizeRedraw` cho hình nền đăng nhập (`UiKit.cs`), đổi nút "Quản trị viên" thành nút chính (`FrmLogin.cs`), đổi bảng màu `AdminTheme` sang màu của `UiKit`, thay màu trong các file Designer, thêm nút Quay lại vào 3 form bằng Visual Studio Designer. Logic dữ liệu mẫu chép tạm bảng phí hủy, sẽ thay bằng Core ở tuần 3. |
| Kết quả | [`AirlineBooking.WinForms/Admin/`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/tree/9b0aceb04ef9b48d2148e5134ac8055367366ff8/AirlineBooking.WinForms/Admin) — commit [`9b0aceb`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/9b0aceb04ef9b48d2148e5134ac8055367366ff8) "Add admin UI forms for flight, booking and revenue management" |

### 2. Hoàn thiện giao diện Người dùng — Ngô Thành Đạt (15/10 – 17/10/2026)

| Mục | Nội dung |
|---|---|
| Công cụ AI | **GitHub Copilot** (Copilot SDK trong VS Code) — AI assistant Copilot, model GPT-6-Luna. |
| Prompt | Yêu cầu làm giao diện Người dùng theo kế hoạch tuần 2 và ảnh tham khảo; chỉnh lựa chọn một chiều / khứ hồi, nút đăng xuất, bố cục phần khám phá và chức năng đặt vé từ chuyến gợi ý; sửa nhãn bị cắt, đổi tên Skyline thành AirlineTicket, sửa sơ đồ ghế để thấy ghế 1A và 1B; chỉnh banner để tiêu đề nằm trên một dòng và dòng mô tả sát góc trái dưới; tổng hợp nội dung tuần 2 theo bố cục báo cáo. |
| AI tạo ra | Các form WinForms và điều hướng luồng Người dùng (đăng nhập → tìm chuyến → chọn ghế → xác nhận đặt vé → vé của tôi); dữ liệu chuyến bay / vé mẫu trong bộ nhớ; sơ đồ ghế phân biệt hạng bằng màu và khóa ghế đã đặt; các chỉnh sửa giao diện theo phản hồi; nội dung tổng hợp báo cáo tuần 2. |
| Nhóm sửa / quyết định | Ngô Thành Đạt cung cấp ảnh tham khảo, chạy thử và phản hồi để điều chỉnh giao diện. Chọn sử dụng dữ liệu demo trong bộ nhớ. Khứ hồi hiện lưu / hiển thị ngày về nhưng luồng đặt vé demo mới tạo vé cho lượt đi. |
| Kết quả | Các form và dữ liệu demo Người dùng — commit [`7b0f82a`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/7b0f82a9e4815a33b774b7b2f0d238b247c70951). Chỉnh banner tránh chồng lấn — commit [`0d1a75e`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/0d1a75eb6f69f37a70619797fc4eb0f98a353c73). Căn dòng mô tả banner — commit [`7753981`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/7753981). |

## Tuần 3

### 1. Nối giao diện Admin vào Core, dùng chung dữ liệu — Trần Quốc Huy (19/10 – 23/10/2026)

| Mục | Nội dung |
|---|---|
| Công cụ AI | **Claude Code — model Claude Opus 5.5** (Anthropic). |
| Prompt | "Cho tôi quy trình hướng dẫn để hoàn thành tuần 3"; "Kiểm tra các file 1.1 – 1.6 tôi đã làm và góp ý"; "Tôi chưa hiểu cách làm của bước D"; "Chỗ lambda vẫn khó hiểu, chỉ cách viết thay thế"; "Ở bước 2 tôi nghĩ sẽ dùng database"; "Bạn giúp tôi hoàn thành 3.0 và 3.1, sửa DemoData.cs và vấn đề sắp ghế"; "Hướng dẫn tôi bước 3"; "Sửa Designer 3.5b, các chỗ gạch đỏ và 3.6 giúp tôi"; "Hướng dẫn bổ sung class diagram, làm bước 5 và 6". |
| AI tạo ra | Kế hoạch các bước tuần 3; nhận xét lỗi trong code 1.1 – 1.6 kèm đoạn code sửa mẫu (`Booking.Cancel`, `Flight.UpdatePrice`, `FlightCatalog.Remove`, `RevenueReportService.GetTotalPaid` và khung `BuildLines`, cách viết không dùng lambda); lời khuyên không dùng database ở tuần 3. Gộp code với 5 commit của Ngô Thành Đạt và giải quyết xung đột (`Booking.cs`, `Seat.cs` lấy theo bản của Đạt); viết `SeatLayout` (quy tắc sắp ghế 4 hạng), `SampleData` (dữ liệu mẫu, tên sân bay dạng "Hà Nội (HAN)"); sửa `DemoData`, `FrmSeatMap` để phía Người dùng dùng chung `AppServices` và vẽ sơ đồ ghế theo ghế thật của chuyến. Viết `AdminDisplay` và code mẫu nối 5 form Admin vào Core; thêm 2 ô nhập First / Premium trong Designer; sửa lỗi build, lỗi "0 vé" ở Quản lý vé, phóng to lưới và sơ đồ ghế theo độ phóng to màn hình; xóa `AdminDemoData`. Cập nhật README, bản Mermaid class diagram, AI Usage Log, soạn báo cáo tuần 3. |
| Nhóm sửa / quyết định | Trần Quốc Huy tự viết code `Flight.Reschedule`, `Flight.UpdatePrice`, `FlightCatalog.Remove`, các hàm `RevenueReportService` (chọn cách viết bằng `foreach` / `Dictionary`, không dùng lambda) sau khi được góp ý; tự viết `AppServices.cs` và bản đầu của `SeatLayout`; quyết định không dùng database để giữ đúng kế hoạch đã duyệt; tự nối 5 form Admin vào Core theo hướng dẫn (đổi kiểu dữ liệu, thay lời gọi, sửa bộ lọc, nút hủy vé, nút lưu chuyến bay); tự chạy thử toàn bộ luồng Admin – Người dùng. Lưu ý: `Booking.Cancel` và `Seat.ChangePrice` trong code cuối cùng là bản của Ngô Thành Đạt (giữ lại khi gộp code). |
| Kết quả | Commit [`ace53a0`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/ace53a0b94c5f519f18d333d495f5961e976ab56) "Implement admin-side Core logic for flights, cancellation and revenue"; [`a631efe`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/a631efe572c9ec3b53c240f45bf9d7bfea7b483b) "Merge Dat's week 3 booking flow into admin Core changes"; [`58325a5`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/58325a538ab25c0e4bfb502b362cdae1779b1799) "Share one data store and seat layout between admin and user screens"; [`094da9c`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/094da9cafd9a148c4c5d69f1b42160c691a52c9e) "Connect admin screens to Core services and remove admin demo data" — đều có dòng `Co-Authored-By: Claude Opus 5.5`. |

### 2. Chức năng Người dùng, kiểm thử tự động, chuyển vé — Ngô Thành Đạt (19/10 – 21/10/2026)

| Mục | Nội dung |
|---|---|
| Công cụ AI | **GitHub Copilot** (Copilot trong VS Code), model GPT-6-Luna. |
| Prompt | Triển khai các chức năng tuần 3 cho phần Người dùng bằng C# WinForms .NET 8, tách quy tắc nghiệp vụ trong `AirlineBooking.Core` khỏi giao diện; hoàn thiện tìm chuyến theo điểm đi, điểm đến, ngày bay; tính giá vé và phí hủy theo hạng ghế; giữ ghế để tránh đặt trùng; đặt vé, hủy vé, tính tiền hoàn; nối luồng tìm chuyến → chọn ghế → xác nhận → vé của tôi; viết kiểm thử tự động cho đặt – hủy – hoàn tiền, đặt trùng ghế, hủy vé hai lần, mức hoàn khác nhau giữa các hạng ghế; sửa lỗi giao diện bị che / chồng lấn theo phản hồi. |
| AI tạo ra | Nghiệp vụ đặt vé, hủy vé, giữ / trả ghế, tính giá vé và tiền hoàn trong Core (`Seat`, `Booking`, `BookingService`, `FlightCatalog.Search`, `SeatClassBase`); kết nối các màn hình Người dùng với Core; dữ liệu chuyến bay mẫu; project `AirlineBooking.Core.Tests` với 5 test (đặt – hủy – hoàn tiền, đặt trùng ghế đồng thời và hủy hai lần, hoàn tiền theo hạng ghế, chuyển vé, từ chối chuyển vé đã hủy); chức năng chuyển vé cho hành khách khác; chỉnh bố cục màn hình Người dùng. |
| Nhóm sửa / quyết định | Ngô Thành Đạt kiểm tra bằng build và chạy kiểm thử tự động; chạy thử giao diện, phản hồi lỗi bố cục để tiêu đề và nội dung không bị che hoặc chồng lên nhau; quyết định thêm chức năng chuyển vé. |
| Kết quả | Commit [`7348164`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/7348164be2b121d1977674350a8d92ba92656fb6) "Implement user booking domain flow"; [`38bef0d`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/38bef0d364f435ad5fe0e5ca3e498e5774081d0c) "Connect user forms to booking services"; [`707fb13`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/707fb13af7efc4fd31e628da4f77b57d3b22c0b0) "Add booking flow tests and documentation"; [`0d8ea20`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/0d8ea2073afe647cda876cf6e0c5a0a1b114a063) "Fix user booking screens and add ticket transfer"; [`5e4a090`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/5e4a0908491ee615e4a585f86ae2e101bb62f741) "Fix user booking screen layout" — đều có dòng `Co-authored-by: Copilot`. |

## Tuần 4

### 1. Kiểm thử tự động phía Admin — Trần Quốc Huy (28/10 – 29/10/2026)

| Mục | Nội dung |
|---|---|
| Công cụ AI | **Claude Code — model Claude Opus 5.5** (Anthropic). |
| Prompt | "Show kế hoạch tuần 4 và lên kế hoạch để tôi hoàn thành"; "Bắt đầu từ 4 test Admin"; "Giải thích giúp tôi đoạn Assert của test báo cáo doanh thu"; "Tôi đã viết xong test 2–4, bạn kiểm tra giúp tôi". |
| AI tạo ra | Kế hoạch tuần 4; khung file `AdminFeatureTests.cs` (đồng hồ giả `FrozenTimeProvider`, hàm phụ `CreateSystem`, `AddSeat`, `FindLine`); test 1 `RevenueReport_SeparatesPaidAndRefundedAmounts` viết mẫu đầy đủ kèm giải thích từng dòng Assert; khung và gợi ý cho test 2–4; chỉ ra lỗi trong bản nháp test 2 (viết `1.5m` — một phẩy năm đồng — thay vì `1_500_000m`) và test 4 (so nhầm giờ bay của `departedFlight` thay vì `flight`, thừa dấu ngoặc trong `Assert.Equal`). |
| Nhóm sửa / quyết định | Trần Quốc Huy tự viết test 2 `UpdatePrice_DoesNotChangeAlreadyBookedTicket`, test 3 `Remove_RejectsFlightWithBookedSeat`, test 4 `Reschedule_RejectsPastTimeAndDepartedFlight`, tự sửa các lỗi được chỉ ra và chạy Test Explorer: 9/9 test đạt (5 test của Ngô Thành Đạt + 4 test Admin). |
| Kết quả | [`AirlineBooking.Core.Tests/AdminFeatureTests.cs`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/blob/c8676374f2b4d19636a7716bae8a79edb44c4c63/AirlineBooking.Core.Tests/AdminFeatureTests.cs) — commit [`c867637`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/c8676374f2b4d19636a7716bae8a79edb44c4c63) "Add admin feature tests for revenue, pricing, flight removal and rescheduling" (dòng Co-Authored-By nằm chung đoạn tiêu đề và thiếu email nên GitHub không hiện đồng tác giả; phần AI hỗ trợ được ghi đầy đủ tại đây). |
