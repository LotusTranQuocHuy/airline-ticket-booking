# Báo cáo dự án

## Race condition khi nhiều người cùng đặt một ghế

### Tình huống

Giả sử 50 yêu cầu đồng thời cùng đặt ghế `12A` trên chuyến `VN205`. Nếu việc kiểm tra ghế còn trống và cập nhật trạng thái được thực hiện tách rời, nhiều yêu cầu có thể cùng đọc trạng thái `Available` trước khi yêu cầu đầu tiên cập nhật ghế. Kết quả là nhiều vé có thể được tạo cho cùng một ghế (race condition).

### Cách ngăn chặn

`Seat.TryLock` dùng `lock (_sync)` để kiểm tra trạng thái và chuyển ghế từ `Available` sang `Locked` trong cùng một vùng tới hạn. Vì vậy, chỉ một yêu cầu có thể chiếm được ghế; các yêu cầu sau nhìn thấy ghế không còn `Available` và trả về `false`. `Booking.Create` chuyển kết quả đó thành `SeatUnavailableException`, nên không tạo thêm vé cho các yêu cầu thua cuộc.

Khóa được đặt trên từng đối tượng ghế, vì thế các yêu cầu đặt những ghế khác nhau không cần chờ chung một khóa. `BookingService` cũng khóa danh sách vé khi thêm và đọc dữ liệu để tránh truy cập danh sách đồng thời không an toàn.

### Kiểm thử

Kiểm thử `BookingService_RejectsConcurrentDuplicateSeatAndRepeatedCancellation` đồng bộ 50 yêu cầu trước khi cho cùng gọi đặt ghế `12A`. Kết quả mong đợi và được kiểm tra tự động:

- 1 yêu cầu tạo được vé.
- 49 yêu cầu còn lại bị từ chối do ghế đã được giữ.
- Danh sách dịch vụ chỉ có 1 vé và trạng thái ghế là `Booked`.

Chạy kiểm thử bằng lệnh:

```powershell
dotnet test AirlineBooking.Core.Tests\AirlineBooking.Core.Tests.csproj
```

Kết quả xác minh ngày 02/11/2026: 6 bài test đạt, không có bài test thất bại; project WinForms cũng build thành công, không có cảnh báo hay lỗi.

### Phạm vi kiểm thử khứ hồi

Kiểm thử `RoundTripSearchAndBook_CreatesOutboundAndReturnBookings` xác nhận có thể tìm và đặt chặng đi cùng chặng về trên hai chuyến ngược chiều cho cùng một hành khách. Mỗi chặng hiện là một vé riêng trong `BookingService`. Giao diện WinForms hiện mới lưu và hiển thị ngày về; luồng chọn chuyến và đặt vé trên giao diện vẫn chỉ xử lý chặng đi.
