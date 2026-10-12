# Class Diagram

Bản vẽ chính thức (Enterprise Architect): [class-diagram.jpg](class-diagram.jpg) — file gốc: [AirlineBooking.eap](AirlineBooking.eap)

Bản dưới đây viết bằng Mermaid, GitHub tự vẽ thành sơ đồ và luôn khớp với code trong `AirlineBooking.Core`.

```mermaid
classDiagram
    direction TB

    namespace Services {
        class FlightCatalog {
            -List~Flight~ _flights
            +Add(flight Flight) void
            +Find(flightNumber string) Flight
            +Search(origin string, destination string, date DateOnly) IEnumerable~Flight~
        }
        class BookingService {
            -FlightCatalog _catalog
            -TimeProvider _time
            -List~Booking~ _bookings
            +BookingService(catalog FlightCatalog, time TimeProvider)
            +GetBookings() IReadOnlyList~Booking~
            +Book(flightNumber string, seatCode string, passenger Passenger) Booking
            +Cancel(bookingId string) decimal
        }
    }

    namespace Models {
        class Flight {
            -string _flightNumber
            -string _origin
            -string _destination
            -DateTime _departure
            -List~Seat~ _seats
            +Flight(flightNumber string, origin string, destination string, departure DateTime, seatLayout IEnumerable~SeatDefinition~)
            +GetFlightNumber() string
            +GetOrigin() string
            +GetDestination() string
            +GetDeparture() DateTime
            +GetSeats() IReadOnlyList~Seat~
            +GetSeat(code string) Seat
        }
        class SeatDefinition {
            -string _code
            -ISeatClass _seatClass
            -decimal _baseFare
            +SeatDefinition(code string, seatClass ISeatClass, baseFare decimal)
            +GetCode() string
            +GetSeatClass() ISeatClass
            +GetBaseFare() decimal
        }
        class Seat {
            -string _code
            -ISeatClass _seatClass
            -decimal _baseFare
            -SeatStatus _status
            -Passenger _holder
            -object _sync
            ~Seat(code string, seatClass ISeatClass, baseFare decimal)
            +GetCode() string
            +GetSeatClass() ISeatClass
            +GetBaseFare() decimal
            +GetStatus() SeatStatus
            +IsAvailable() bool
            +TryLock(p Passenger) bool
            +Confirm(p Passenger) void
            +Release() void
        }
        class Passenger {
            -string _id
            -string _fullName
            -string _email
            +Passenger(id string, fullName string, email string)
            +GetId() string
            +GetFullName() string
            +GetEmail() string
        }
        class Booking {
            -string _bookingId
            -Flight _flight
            -Seat _seat
            -Passenger _passenger
            -decimal _paidAmount
            -BookingStatus _status
            -Booking(bookingId string, flight Flight, seat Seat, passenger Passenger, paidAmount decimal)
            +Create(flight Flight, seat Seat, passenger Passenger)$ Booking
            +Cancel(now DateTime) decimal
            +GetBookingId() string
            +GetFlight() Flight
            +GetSeat() Seat
            +GetPassenger() Passenger
            +GetPaidAmount() decimal
            +GetStatus() BookingStatus
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
    }

    namespace Fares {
        class ISeatClass {
            <<interface>>
            +GetName() string
            +CalculateFare(baseFare decimal) decimal
            +CancellationFeeRate(timeLeft TimeSpan) decimal
        }
        class SeatClassBase {
            <<abstract>>
            #string _name
            #decimal _priceMultiplier
            #List~CancellationTier~ _tiers
            #SeatClassBase(name string, priceMultiplier decimal, tiers List~CancellationTier~)
            +GetName() string
            +CalculateFare(baseFare decimal) decimal
            +CancellationFeeRate(timeLeft TimeSpan) decimal
        }
        class CancellationTier {
            -TimeSpan _minTimeBefore
            -decimal _feeRate
            +CancellationTier(minTimeBefore TimeSpan, feeRate decimal)
            +GetMinTimeBefore() TimeSpan
            +GetFeeRate() decimal
        }
        class EconomyClass {
            +EconomyClass()
        }
        class BusinessClass {
            +BusinessClass()
        }
        class PremiumEconomyClass {
            +PremiumEconomyClass()
        }
        class FirstClass {
            +FirstClass()
        }
    }

    %% Quan hệ giữa Services và Models
    BookingService --> FlightCatalog
    FlightCatalog "1" o-- "*" Flight
    BookingService "1" o-- "*" Booking

    %% Composition: Flight sở hữu Seat
    Flight "1" *-- "1..*" Seat
    Flight ..> SeatDefinition : uses

    %% Booking tham chiếu tới ghế, chuyến bay, hành khách cụ thể
    Booking --> Flight
    Booking --> Seat
    Booking --> Passenger
    Booking ..> BookingStatus
    Seat ..> SeatStatus
    Seat --> Passenger : _holder

    %% Polymorphism: hạng ghế qua interface chung
    Seat --> ISeatClass
    SeatDefinition --> ISeatClass
    ISeatClass <|.. SeatClassBase
    SeatClassBase "1" o-- "*" CancellationTier
    SeatClassBase <|-- EconomyClass
    SeatClassBase <|-- BusinessClass
    SeatClassBase <|-- PremiumEconomyClass
    SeatClassBase <|-- FirstClass
```

## Ký hiệu

| Mermaid | UML | Ý nghĩa trong bài |
|---|---|---|
| `*--` | Composition | `Flight` sở hữu các `Seat` |
| `o--` | Aggregation | `FlightCatalog` quản lý `Flight`, `BookingService` quản lý `Booking`, `SeatClassBase` chứa `CancellationTier` |
| `-->` | Association | `Booking` tham chiếu `Flight` / `Seat` / `Passenger` |
| `..|>` / `<|..` | Realization | `SeatClassBase` cài đặt `ISeatClass` |
| `<|--` | Generalization | 4 hạng ghế kế thừa `SeatClassBase` |
| `..>` | Dependency | dùng enum / `SeatDefinition` |
| `-` `+` `#` `~` | private / public / protected / internal | |
| `$` | static | `Booking.Create` |
