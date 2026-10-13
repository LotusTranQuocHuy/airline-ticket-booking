# Class Diagram

Bản vẽ chính thức (Enterprise Architect): [class-diagram.jpg](class-diagram.jpg) — file gốc: [AirlineBooking.eap](AirlineBooking.eap)

Bản dưới đây viết bằng Mermaid (GitHub tự vẽ thành sơ đồ), cùng nội dung với bản Enterprise Architect.

```mermaid
classDiagram
    direction TB

    class FlightCatalog {
        -List~Flight~ flights
        +Add(flight Flight) void
        +Find(flightNumber string) Flight
        +GetAll() IReadOnlyList~Flight~
        +Search(origin string, destination string, date DateOnly) IEnumerable~Flight~
    }
    class BookingService {
        -List~Booking~ bookings
        -FlightCatalog flightCatalog
        -TimeProvider time
        +BookingService(catalog FlightCatalog, time TimeProvider)
        +Book(flightNumber string, seatCode string, passenger Passenger) Booking
        +Cancel(bookingId string) decimal
        +GetBookings() IReadOnlyList~Booking~
        +GetBookingsByPassenger(passengerId string) IReadOnlyList~Booking~
    }
    class RevenueReportService {
        -BookingService bookingService
        +RevenueReportService(bookingService BookingService)
        +GetTotalPaid(from DateOnly, to DateOnly) decimal
        +GetTotalRefunded(from DateOnly, to DateOnly) decimal
        +GetNetRevenue(from DateOnly, to DateOnly) decimal
        +GetRevenueByFlight(from DateOnly, to DateOnly) IReadOnlyList~RevenueLine~
        +GetRevenueBySeatClass(from DateOnly, to DateOnly) IReadOnlyList~RevenueLine~
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
        -DateTime departure
        -string destination
        -string flightNumber
        -string origin
        -List~Seat~ seats
        +AddSeat(seatCode string, seatClass ISeatClass, price decimal) void
        +GetDeparture() DateTime
        +GetDestination() string
        +GetFlightNumber() string
        +GetOrigin() string
        +GetSeat(code string) Seat
        +GetSeats() IReadOnlyList~Seat~
        +Reschedule(newDeparture DateTime) void
        +UpdatePrice(seatClassName string, newPrice decimal) void
    }
    class Booking {
        -DateTime bookedAt
        -string bookingId
        -DateTime cancelledAt
        -Flight flight
        -decimal paidAmount
        -Passenger passenger
        -decimal refundAmount
        -Seat seat
        -BookingStatus status
        -Booking(bookingId string, flight Flight, seat Seat, passenger Passenger, paidAmount decimal, bookedAt DateTime)
        +Create(flight Flight, seat Seat, passenger Passenger, now DateTime)$ Booking
        +Cancel(now DateTime) decimal
        +GetBookedAt() DateTime
        +GetBookingId() string
        +GetCancelledAt() DateTime
        +GetFlight() Flight
        +GetPaidAmount() decimal
        +GetPassenger() Passenger
        +GetRefundAmount() decimal
        +GetSeat() Seat
        +GetStatus() BookingStatus
    }
    class Seat {
        -Passenger holder
        -decimal price
        -ISeatClass seatClass
        -string seatCode
        -SeatStatus status
        -object sync
        ~Seat(code string, seatClass ISeatClass, price decimal)
        +ChangePrice(newPrice decimal) void
        +Confirm(p Passenger) void
        +GetCode() string
        +GetPrice() decimal
        +GetSeatClass() ISeatClass
        +GetStatus() SeatStatus
        +IsAvailable() bool
        +Release() void
        +TryLock(p Passenger) bool
    }
    class Passenger {
        -string email
        -string fullName
        -string id
        +Passenger(id string, fullName string, email string)
        +GetEmail() string
        +GetFullName() string
        +GetId() string
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
        +CalculatePrice(price decimal) decimal
        +CancellationFeeRate(t TimeSpan) decimal
        +GetName() string
    }
    class SeatClassBase {
        <<abstract>>
        #string name
        #decimal priceMultiplier
        #IReadOnlyList~CancellationTier~ tiers
        #SeatClassBase(name string, priceMultiplier decimal, tiers IReadOnlyList~CancellationTier~)
        +CalculatePrice(price decimal) decimal
        +CancellationFeeRate(t TimeSpan) decimal
        +GetName() string
    }
    class CancellationTier {
        -decimal feeRate
        -TimeSpan minTimeBefore
        +CancellationTier(minTimeBefore TimeSpan, feeRate decimal)
        +GetFeeRate() decimal
        +GetMinTimeBefore() TimeSpan
    }
    class EconomyClass
    class BusinessClass
    class FirstClass
    class PremiumEconomyClass

    BookingService --> FlightCatalog
    RevenueReportService --> BookingService
    RevenueReportService ..> RevenueLine
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
    SeatClassBase *-- CancellationTier
    SeatClassBase <|-- EconomyClass
    SeatClassBase <|-- BusinessClass
    SeatClassBase <|-- FirstClass
    SeatClassBase <|-- PremiumEconomyClass
```

## Ký hiệu

| Mermaid | UML | Ý nghĩa trong bài |
|---|---|---|
| `*--` | Composition | `Flight` sở hữu các `Seat`; `SeatClassBase` sở hữu bảng `CancellationTier` |
| `o--` | Aggregation | `FlightCatalog` quản lý `Flight`, `BookingService` quản lý `Booking` |
| `-->` | Association | `Booking` tham chiếu `Flight` / `Seat` / `Passenger`; `Seat` giữ `Passenger` đang đặt |
| `<\|..` | Realization | `SeatClassBase` cài đặt `ISeatClass` |
| `<\|--` | Generalization | 4 hạng ghế kế thừa `SeatClassBase` |
| `..>` | Dependency | dùng enum; `RevenueReportService` tạo `RevenueLine` |
| `-` `+` `#` `~` | private / public / protected / internal | |
| `$` | static | `Booking.Create` |
