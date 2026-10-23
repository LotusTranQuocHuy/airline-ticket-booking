# Class Diagram

Bản vẽ chính thức (Enterprise Architect): [class-diagram.jpg](class-diagram.jpg) — file gốc: [AirlineBooking.eap](AirlineBooking.eap)

Bản dưới đây viết bằng Mermaid (GitHub tự vẽ thành sơ đồ), khớp với code project `AirlineBooking.Core` sau tuần 3.

```mermaid
classDiagram
    direction TB

    class FlightCatalog {
        -List~Flight~ flights
        -object sync
        +Add(flight Flight) void
        +Find(flightNumber string) Flight?
        +GetAll() IReadOnlyList~Flight~
        +Search(origin string, destination string, date DateOnly) IEnumerable~Flight~
        +Remove(flight Flight) void
    }
    class BookingService {
        -List~Booking~ bookings
        -FlightCatalog flightCatalog
        -TimeProvider time
        -object sync
        +BookingService(catalog FlightCatalog, time TimeProvider)
        +GetBookings() IReadOnlyList~Booking~
        +GetBookingsByPassenger(passengerId string) IReadOnlyList~Booking~
        +Book(flightNumber string, seatCode string, passenger Passenger) Booking
        +Cancel(bookingId string) decimal
        +Transfer(bookingId string, currentPassenger Passenger, newPassenger Passenger) void
    }
    class RevenueReportService {
        -BookingService bookingService
        +RevenueReportService(bookingService BookingService)
        +GetTotalPaid(from DateOnly, to DateOnly) decimal
        +GetTotalRefunded(from DateOnly, to DateOnly) decimal
        +GetNetRevenue(from DateOnly, to DateOnly) decimal
        +GetRevenueByFlight(from DateOnly, to DateOnly) IReadOnlyList~RevenueLine~
        +GetRevenueBySeatClass(from DateOnly, to DateOnly) IReadOnlyList~RevenueLine~
        -InRange(time DateTime, from DateOnly, to DateOnly)$ bool
        -GetGroupName(booking Booking, byFlight bool)$ string
        -BuildLines(byFlight bool, from DateOnly, to DateOnly) IReadOnlyList~RevenueLine~
    }
    class RevenueLine {
        -string label
        -int ticketCount
        -decimal totalPaid
        -decimal totalRefunded
        +RevenueLine(label string, ticketCount int, totalPaid decimal, totalRefunded decimal)
        +GetLabel() string
        +GetTicketCount() int
        +GetTotalPaid() decimal
        +GetTotalRefunded() decimal
        +GetNetRevenue() decimal
    }
    class Flight {
        -string flightNumber
        -string origin
        -string destination
        -DateTime departure
        -List~Seat~ seats
        +Flight(flightNumber string, origin string, destination string, departure DateTime)
        +AddSeat(seatCode string, seatClass ISeatClass, price decimal) void
        +GetFlightNumber() string
        +GetOrigin() string
        +GetDestination() string
        +GetDeparture() DateTime
        +GetSeat(code string) Seat?
        +GetSeats() IReadOnlyList~Seat~
        +Reschedule(newDeparture DateTime, now DateTime) void
        +UpdatePrice(seatClassName string, newPrice decimal) void
    }
    class Booking {
        -string bookingId
        -Flight flight
        -Seat seat
        -Passenger passenger
        -decimal paidAmount
        -BookingStatus status
        -DateTime bookedAt
        -decimal refundAmount
        -DateTime? cancelledAt
        -object sync
        -Booking(bookingId string, flight Flight, seat Seat, passenger Passenger, paidAmount decimal, bookedAt DateTime)
        +Create(flight Flight, seat Seat, passenger Passenger, now DateTime)$ Booking
        +Cancel(now DateTime) decimal
        +PreviewCancellation(now DateTime) ValueTuple~decimal,decimal~
        +TransferTo(currentPassenger Passenger, newPassenger Passenger) void
        -CalculateCancellationQuote(now DateTime) ValueTuple~decimal,decimal~
        +GetBookingId() string
        +GetFlight() Flight
        +GetSeat() Seat
        +GetPassenger() Passenger
        +GetPaidAmount() decimal
        +GetStatus() BookingStatus
        +GetBookedAt() DateTime
        +GetRefundAmount() decimal
        +GetCancelledAt() DateTime?
    }
    class Seat {
        -string seatCode
        -ISeatClass seatClass
        -decimal price
        -SeatStatus status
        -Passenger? holder
        -object sync
        ~Seat(code string, seatClass ISeatClass, price decimal)
        +GetCode() string
        +GetSeatClass() ISeatClass
        +GetPrice() decimal
        +GetStatus() SeatStatus
        +IsAvailable() bool
        +TryLock(passenger Passenger) bool
        +Confirm(passenger Passenger) void
        ~Release(passenger Passenger) void
        ~TransferTo(currentPassenger Passenger, newPassenger Passenger) void
        ~ChangePrice(newPrice decimal) void
        -IsHeldBy(passenger Passenger) bool
    }
    class Passenger {
        -string id
        -string fullName
        -string email
        +Passenger(id string, fullName string, email string)
        +GetId() string
        +GetFullName() string
        +GetEmail() string
    }
    class SeatStatus {
        <<enumeration>>
        Available
        Locked
        Booked
    }
    class BookingStatus {
        <<enumeration>>
        Confirmed
        Cancelled
    }
    class ISeatClass {
        <<interface>>
        +GetName() string
        +CalculatePrice(price decimal) decimal
        +CancellationFeeRate(timeLeft TimeSpan) decimal
    }
    class SeatClassBase {
        <<abstract>>
        #string name
        #decimal priceMultiplier
        #IReadOnlyList~CancellationTier~ tiers
        #SeatClassBase(name string, priceMultiplier decimal, tiers IReadOnlyList~CancellationTier~)
        +GetName() string
        +CalculatePrice(price decimal) decimal
        +CancellationFeeRate(timeLeft TimeSpan) decimal
    }
    class CancellationTier {
        -TimeSpan minTimeBefore
        -decimal feeRate
        +CancellationTier(minTimeBefore TimeSpan, feeRate decimal)
        +GetMinTimeBefore() TimeSpan
        +GetFeeRate() decimal
    }
    class EconomyClass {
        +EconomyClass()
    }
    class PremiumEconomyClass {
        +PremiumEconomyClass()
    }
    class BusinessClass {
        +BusinessClass()
    }
    class FirstClass {
        +FirstClass()
    }
    class SeatUnavailableException {
        +SeatUnavailableException(seatCode string)
    }
    class InvalidBookingStateException {
        +InvalidBookingStateException(message string)
    }
    class FlightNotFoundException {
        +FlightNotFoundException(flightNumber string)
    }
    class Exception

    BookingService --> FlightCatalog
    RevenueReportService --> BookingService
    RevenueReportService ..> RevenueLine : tạo ra
    FlightCatalog "1" o-- "*" Flight
    BookingService "1" o-- "*" Booking
    Flight "1" *-- "1..*" Seat

    Booking --> Flight
    Booking --> Seat
    Booking --> Passenger
    Booking ..> BookingStatus
    Seat ..> SeatStatus
    Seat --> "0..1" Passenger : holder

    Seat --> ISeatClass
    ISeatClass <|.. SeatClassBase
    SeatClassBase "1" *-- "1..*" CancellationTier
    SeatClassBase <|-- EconomyClass
    SeatClassBase <|-- PremiumEconomyClass
    SeatClassBase <|-- BusinessClass
    SeatClassBase <|-- FirstClass

    Exception <|-- SeatUnavailableException
    Exception <|-- InvalidBookingStateException
    Exception <|-- FlightNotFoundException
    BookingService ..> FlightNotFoundException : «throw»
    Booking ..> SeatUnavailableException : «throw»
    Booking ..> InvalidBookingStateException : «throw»
```

## Thay đổi so với bản tuần 1 – 2

| Class | Thay đổi ở tuần 3 |
|---|---|
| `Flight` | `Reschedule` nhận thêm `now`; `GetSeat` trả về `Seat?` |
| `FlightCatalog` | Thêm `Remove(flight)` (chỉ xóa khi chưa có ghế nào được đặt); khóa `sync` cho mọi thao tác |
| `Seat` | `Release`, `ChangePrice` là internal; `Release` và `TransferTo` nhận hành khách để kiểm tra đúng người giữ ghế |
| `Booking` | Thêm `PreviewCancellation`, `TransferTo`; `GetCancelledAt()` trả về `DateTime?`; khóa `sync` |
| `BookingService` | Thêm `Transfer` (chuyển vé cho hành khách khác) |
| Hạng ghế | Dùng đủ 4 hạng: First, Business, Premium Economy, Economy |
| Exception | Đưa 3 exception và quan hệ «throw» vào sơ đồ |

## Ký hiệu

| Mermaid | UML | Ý nghĩa trong bài |
|---|---|---|
| `*--` | Composition | `Flight` sở hữu các `Seat`; `SeatClassBase` sở hữu bảng `CancellationTier` |
| `o--` | Aggregation | `FlightCatalog` quản lý `Flight`, `BookingService` quản lý `Booking` |
| `-->` | Association | `Booking` tham chiếu `Flight` / `Seat` / `Passenger`; `Seat` giữ `Passenger` đang đặt |
| `<\|..` | Realization | `SeatClassBase` cài đặt `ISeatClass` |
| `<\|--` | Generalization | 4 hạng ghế kế thừa `SeatClassBase`; 3 exception kế thừa `Exception` |
| `..>` | Dependency | dùng enum; `RevenueReportService` tạo `RevenueLine`; ném exception |
| `-` `+` `#` `~` | private / public / protected / internal | |
| `$` | static | `Booking.Create`, các hàm phụ trong `RevenueReportService` |
