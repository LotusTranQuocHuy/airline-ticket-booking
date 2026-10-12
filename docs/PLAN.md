# Kế hoạch 4 tuần — Airline Ticket Booking System (C# / .NET 8 / WinForms)

**Thành viên**

| Thành viên | Vai trò chính |
|---|---|
| Trần Quốc Huy | Thiết kế (class diagram, kế hoạch), giao diện & chức năng **Admin** |
| Ngô Thành Đạt | Hiện thực code từ class diagram, giao diện & chức năng **Người dùng** |

## Bối cảnh
Đề bài OOP cuối kỳ: hệ thống đặt vé máy bay — tìm chuyến bay, chọn/khóa ghế theo hạng (Economy, Business, ...), hủy vé và hoàn tiền theo hạng ghế và thời gian còn lại trước giờ bay. Chấm theo "Design quality": trách nhiệm class rõ ràng (không God class), dùng đúng Polymorphism / Encapsulation / Composition, và khả năng mở rộng.

Nguyên tắc xuyên suốt: **Form không chứa logic nghiệp vụ**. Mọi form chỉ bắt sự kiện → gọi service trong `AirlineBooking.Core` → hiển thị kết quả / bắt exception để hiện `MessageBox`.

---

## Tuần 1 — Thiết kế & khung code

| Người | Công việc | Kết quả |
|---|---|---|
| Trần Quốc Huy | Thiết kế class diagram (Enterprise Architect) | `docs/class-diagram.jpg`, `docs/AirlineBooking.eap`, `docs/class-diagram.md` |
| Trần Quốc Huy | Lập kế hoạch nội dung thực hiện trong 4 tuần | `docs/PLAN.md` |
| Ngô Thành Đạt | Export class diagram sang code C# (solution `AirlineBooking.Core` + `AirlineBooking.WinForms`), build thành công | Code khung trên GitHub |

---

## Tuần 2 — Thiết kế giao diện (WinForms)

### Màn hình chung: Đăng nhập — `FrmLogin`
- Form đơn giản có **ảnh nền** và tên ứng dụng.
- Hai nút lựa chọn: **Admin** và **Người dùng**.
- Chọn **Người dùng** → hiện ô nhập **Họ tên** (và Email, có thể bỏ trống) → bấm **Vào** để tạo `Passenger` cho phiên làm việc. Cần tên để vé gắn với đúng hành khách, giúp Admin quản lý vé và thống kê doanh thu theo khách.
- Chọn **Admin** → vào thẳng màn hình quản trị (không cần mật khẩu, theo phạm vi bài).

### Giao diện Admin — Trần Quốc Huy

| Form | Nội dung | Control chính |
|---|---|---|
| `FrmAdminMain` | Màn hình chính của Admin, menu chuyển sang các chức năng bên dưới, nút Đăng xuất | `MenuStrip` / các nút lớn |
| `FrmFlightManager` | **Tạo vé**: danh sách chuyến bay; thêm chuyến mới (số hiệu, điểm đi, điểm đến, giờ bay, giá gốc, số hàng ghế mỗi hạng) → hệ thống sinh sơ đồ ghế | `DataGridView`, `TextBox`, `ComboBox`, `DateTimePicker`, `NumericUpDown` |
| `FrmFlightEdit` | Chỉnh sửa thông tin chuyến bay (giờ bay, giá gốc), xem sơ đồ ghế của chuyến | `DateTimePicker`, `NumericUpDown`, lưới nút ghế (chỉ xem) |
| `FrmBookingManager` | **Quản lý vé**: danh sách tất cả vé, lọc theo chuyến bay / trạng thái / tên khách; xem chi tiết; hủy vé thay khách (hiện số tiền hoàn) | `DataGridView`, `ComboBox` lọc, `TextBox` tìm kiếm |
| `FrmRevenueReport` | **Báo cáo doanh thu**: tổng tiền đã thu, tổng tiền đã hoàn, doanh thu thực; thống kê theo chuyến bay và theo hạng ghế; lọc theo khoảng ngày | `DateTimePicker` (từ–đến), `DataGridView`, `Label` tổng, `Chart` (tuỳ chọn) |

### Giao diện Người dùng — Ngô Thành Đạt

| Form | Nội dung | Control chính |
|---|---|---|
| `FrmUserMain` | Màn hình chính, hiện "Xin chào, <tên>", menu sang các chức năng bên dưới, nút Đăng xuất | `Label`, các nút |
| `FrmSearchFlight` | **Tìm chuyến**: chọn điểm đi, điểm đến, ngày bay → danh sách chuyến phù hợp; chọn một chuyến để sang chọn ghế | `ComboBox`, `DateTimePicker`, `DataGridView` |
| `FrmSeatMap` | **Chọn ghế**: lưới nút ghế theo hàng/cột, mỗi hạng một màu, ghế đã đặt bị khóa (xám); chú thích màu; hiện giá khi chọn ghế | `FlowLayoutPanel` / `TableLayoutPanel` chứa `Button` tạo động |
| `FrmBookingConfirm` | **Xác nhận đặt vé**: thông tin chuyến, ghế, hạng, giá → bấm Đặt vé | `Label`, nút Xác nhận / Hủy |
| `FrmMyBookings` | **Vé của tôi**: danh sách vé của hành khách; chọn vé → Hủy → hộp thoại hiện phí hủy và số tiền được hoàn | `DataGridView`, `MessageBox` |

### Tuần 2 — Kết quả cần có
- Tất cả form trên đã có bố cục, điều hướng qua lại được (chưa cần dữ liệu thật — có thể dùng dữ liệu mẫu).
- Ảnh chụp từng màn hình để đưa vào báo cáo.

---

## Tuần 3 — Hoàn thành chức năng theo giao diện

Mục tiêu: sau tuần 3, các màn hình của tuần 2 **hoạt động thật** — bấm nút là đặt được vé, hủy được vé, xem được doanh thu — chứ không chỉ là bố cục.

Ứng dụng gồm hai phần:
- **Phần xử lý** (project `AirlineBooking.Core`): nơi chứa toàn bộ quy tắc của hệ thống — tính giá vé theo hạng ghế, kiểm tra ghế còn trống, tính phí hủy và tiền hoàn. Phần này không biết gì về giao diện.
- **Phần giao diện** (project `AirlineBooking.WinForms`): các màn hình đã thiết kế ở tuần 2. Giao diện chỉ nhận thao tác của người dùng, gọi sang phần xử lý, rồi hiển thị kết quả.

Tách như vậy để quy tắc nghiệp vụ chỉ nằm ở một chỗ, dễ kiểm thử và dễ thay giao diện khác mà không phải viết lại.

### Ngô Thành Đạt — chức năng phía Người dùng

| Việc cần làm | Giải thích |
|---|---|
| Tính giá vé và phí hủy theo hạng ghế | Ví dụ: vé Business đắt gấp 2,5 lần Economy; hủy vé Economy trước giờ bay dưới 24 giờ thì mất toàn bộ tiền, còn Business chỉ mất 20% |
| Giữ ghế khi đặt | Khi một người đã chọn ghế 12A thì người khác không đặt được ghế đó nữa |
| Tìm chuyến bay | Lọc danh sách chuyến theo điểm đi, điểm đến và ngày bay |
| Đặt vé và hủy vé | Đặt vé: giữ ghế → tính giá → tạo vé. Hủy vé: tính số tiền được hoàn → trả ghế về trạng thái trống |
| Kết nối các màn hình Người dùng | Tìm chuyến → chọn ghế → xác nhận đặt vé → xem "Vé của tôi" → hủy vé, chạy được từ đầu đến cuối |

### Trần Quốc Huy — chức năng phía Admin

| Việc cần làm | Giải thích |
|---|---|
| Thêm và chỉnh sửa chuyến bay | Admin nhập thông tin chuyến bay mới, hệ thống tự tạo sơ đồ ghế; sửa được giờ bay và giá gốc |
| Lưu số tiền đã hoàn cho mỗi vé | Cần có dữ liệu này thì mới tính được doanh thu thực |
| Tính báo cáo doanh thu | Tổng tiền đã thu, tổng tiền đã hoàn, doanh thu thực; chia theo từng chuyến bay và từng hạng ghế |
| Kết nối các màn hình Admin | Tạo chuyến bay, quản lý vé (lọc, xem, hủy thay khách), xem báo cáo doanh thu |
| Cập nhật class diagram | Bổ sung các lớp và chức năng mới của phần Admin để sơ đồ luôn khớp với code |

### Cả hai cùng làm

| Việc cần làm | Giải thích |
|---|---|
| Dữ liệu mẫu | Tạo sẵn một số chuyến bay để khi mở ứng dụng là có dữ liệu thử ngay |
| Kiểm thử tự động (bắt buộc theo đề) | Viết chương trình kiểm tra tự động 3 tình huống: **(1)** đi trọn một lượt tìm chuyến → đặt vé → hủy vé → nhận tiền hoàn; **(2)** hệ thống từ chối khi hai người đặt cùng một ghế, hoặc khi hủy một vé hai lần; **(3)** cùng một thời điểm hủy, vé Economy và vé Business được hoàn số tiền khác nhau |

Mỗi chức năng hoàn thành được lưu lên GitHub thành **một lần commit riêng** có mô tả rõ ràng, để báo cáo có thể dẫn chứng chính xác ai làm phần nào, ở file nào.

---

## Tuần 4 — Chỉnh sửa và hoàn thiện app

| Người | Công việc |
|---|---|
| Cả hai | Sửa lỗi, làm mượt giao diện (thông báo lỗi thân thiện, làm mới sơ đồ ghế sau khi đặt / hủy) |
| Ngô Thành Đạt | Bonus đa luồng: test nhiều luồng cùng đặt một ghế → chỉ một người thành công |
| Trần Quốc Huy | Minh họa khả năng mở rộng: thêm hạng ghế mới (PremiumEconomy / First) chỉ bằng file mới, chứng minh bằng `git diff --stat` |
| Cả hai | Báo cáo: tóm tắt đề, class diagram, Self-Explanation (dẫn link file + commit trên GitHub), race condition, test evidence, AI Usage Log, hướng dẫn build/run |
| Cả hai | Tập vấn đáp: mỗi người giải thích được toàn bộ code, kể cả phần của người kia |

---

## Quy ước làm việc
- Trước khi làm: **Pull**. Làm xong một việc nhỏ: **Commit** (message tiếng Anh, bắt đầu bằng động từ) → **Push**.
- Ghi **AI Usage Log** ngay mỗi lần dùng AI (ngày, công cụ, nội dung hỏi, phần dùng / không dùng, đã sửa gì).
- Báo cáo dẫn chứng bằng **permalink** GitHub (mở file → chọn dòng → phím `y`) kèm mã commit và message.
