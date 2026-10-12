# Airline Ticket Booking System

OOP Final Project — hệ thống đặt vé máy bay: tìm chuyến bay, chọn/khóa ghế theo hạng, hủy vé và hoàn tiền theo hạng ghế và thời gian còn lại trước giờ bay.

## Cấu trúc solution

| Project | Loại | Nội dung |
|---|---|---|
| `AirlineBooking.Core` | Class Library (.NET 8) | Toàn bộ logic nghiệp vụ, không phụ thuộc giao diện |
| `AirlineBooking.WinForms` | Windows Forms App (.NET 8) | Giao diện, chỉ gọi vào `BookingService` / `FlightCatalog` |

```
AirlineBooking.Core/
├── Models/      Flight, Seat, SeatDefinition, Passenger, Booking, SeatStatus, BookingStatus
├── Fares/       ISeatClass, SeatClassBase, CancellationTier,
│                EconomyClass, BusinessClass, PremiumEconomyClass, FirstClass
├── Services/    FlightCatalog, BookingService
└── Exceptions/  SeatUnavailableException, InvalidBookingStateException, FlightNotFoundException
docs/            Class diagram (ảnh + file Enterprise Architect), kế hoạch thực hiện
```

## Class diagram

![Class diagram](docs/class-diagram.jpg)

Bản Mermaid (GitHub tự vẽ, khớp với code): [docs/class-diagram.md](docs/class-diagram.md)

## Yêu cầu

- Windows 10/11
- .NET 8 SDK (hoặc mới hơn)
- Visual Studio 2022 (tùy chọn)

## Build & Run

```bash
dotnet build AirlineBooking.sln
```

```bash
dotnet run --project AirlineBooking.WinForms
```

Hoặc mở `AirlineBooking.sln` bằng Visual Studio, chọn `AirlineBooking.WinForms` làm Startup Project và nhấn F5.

## Thành viên

| Thành viên | Phụ trách |
|---|---|
| Trần Quốc Huy | Class diagram, kế hoạch, giao diện & chức năng Admin (tạo vé, quản lý vé, báo cáo doanh thu) |
| Ngô Thành Đạt | Hiện thực code từ class diagram, giao diện & chức năng Người dùng (tìm chuyến, chọn ghế, đặt / hủy vé) |

Kế hoạch chi tiết: [docs/PLAN.md](docs/PLAN.md)
