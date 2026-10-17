# AI Usage Log

Ghi lại mọi lần nhóm dùng AI: ai dùng, hỏi gì, AI tạo ra gì, nhóm đã sửa gì và kết quả nằm ở commit nào trên GitHub.

- **Công cụ:** Claude Code (ứng dụng Claude desktop)
- **Model:** Claude Opus 5.5
- **Quy ước:** commit có nội dung do AI tạo ghi rõ trong mô tả commit, hoặc có dòng `Co-Authored-By: Claude ...`

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

### 1. Hoàn thiện giao diện Người dùng — Ngô Thành Đạt (15/10 – 17/10/2026)

| Mục | Nội dung |
|---|---|
| Prompt | Yêu cầu triển khai giao diện Người dùng theo kế hoạch tuần 2 và ảnh tham khảo; chỉnh lựa chọn một chiều / khứ hồi, nhãn nút bị cắt, tìm chuyến và đặt vé từ chuyến gợi ý; đổi thương hiệu thành AirlineTicket; sửa các vùng giao diện bị chồng lấn; tổng hợp nội dung tuần 2 để bổ sung vào tài liệu AI Usage Log hiện có; chỉnh banner khi dòng mô tả bị tiêu đề che. |
| Công cụ AI | Copilot SDK trong VS Code; thông tin model cụ thể không hiển thị trong phiên làm việc. |
| AI tạo ra | Các form WinForms cho đăng nhập, trang chính Người dùng, tìm chuyến, chọn ghế, xác nhận đặt vé và vé của tôi; dữ liệu chuyến / vé mẫu và điều hướng giữa các form; bản tổng hợp công việc tuần 2; các chỉnh sửa bố cục, kích thước nút và vị trí dòng chữ trên banner. |
| Nhóm sửa / quyết định | Ngô Thành Đạt cung cấp ảnh tham khảo, chạy ứng dụng và phản hồi các lỗi giao diện để chỉnh tiếp. Chọn dùng dữ liệu demo trong bộ nhớ; luồng khứ hồi hiện lưu / hiển thị ngày về nhưng đặt vé demo mới tạo vé cho lượt đi. Dòng mô tả banner được đặt thấp hơn phần tiêu đề để không còn đè lên nhau. |
| Kết quả | Các form và dữ liệu demo Người dùng — commit [`7b0f82a`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/7b0f82a9e4815a33b774b7b2f0d238b247c70951). Sửa banner để dòng mô tả không đè tiêu đề — commit [`0d1a75e`](https://github.com/LotusTranQuocHuy/airline-ticket-booking/commit/0d1a75eb6f69f37a70619797fc4eb0f98a353c73). |
