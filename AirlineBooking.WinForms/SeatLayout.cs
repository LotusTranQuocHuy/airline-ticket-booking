using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

/// <summary>
/// Quy tắc sắp ghế dùng chung cho cả phía Admin và phía Người dùng.
/// Hàng được đánh số liên tục theo thứ tự hạng: First → Business → Premium Economy → Economy.
/// First, Business, Premium Economy: 4 ghế mỗi hàng (A C | D F); Economy: 6 ghế mỗi hàng (A B C | D E F).
/// Mã ghế = số hàng + chữ cái, ví dụ 1A, 7C.
/// </summary>
internal static class SeatLayout
{
    private static readonly string[] WideLetters = { "A", "C", "D", "F" };
    private static readonly string[] EconomyLetters = { "A", "B", "C", "D", "E", "F" };

    // Mỗi hạng ghế chỉ cần MỘT đối tượng, mọi ghế cùng hạng dùng chung
    public static readonly ISeatClass First = new FirstClass();
    public static readonly ISeatClass Business = new BusinessClass();
    public static readonly ISeatClass PremiumEconomy = new PremiumEconomyClass();
    public static readonly ISeatClass Economy = new EconomyClass();

    public static int WideSeatsPerRow => WideLetters.Length;
    public static int EconomySeatsPerRow => EconomyLetters.Length;

    public static void AddSeats(Flight flight, int firstRows, int businessRows, int premiumEconomyRows,
                                int economyRows, decimal basePrice)
    {
        int row = 1;
        row = AddRows(flight, row, firstRows, WideLetters, First, basePrice);
        row = AddRows(flight, row, businessRows, WideLetters, Business, basePrice);
        row = AddRows(flight, row, premiumEconomyRows, WideLetters, PremiumEconomy, basePrice);
        AddRows(flight, row, economyRows, EconomyLetters, Economy, basePrice);
    }

    // Giá gốc của chuyến: giá thấp nhất đang lưu trong các ghế (Economy có hệ số 1,0)
    public static decimal GetBasePrice(Flight flight)
    {
        decimal lowest = 0;
        foreach (Seat seat in flight.GetSeats())
        {
            decimal price = seat.GetPrice();
            if (lowest == 0 || price < lowest)
            {
                lowest = price;
            }
        }
        return lowest;
    }

    // "12A" -> (12, "A")
    public static (int Row, string Letter) SplitCode(string seatCode)
    {
        int digits = 0;
        while (digits < seatCode.Length && char.IsDigit(seatCode[digits]))
        {
            digits++;
        }
        return (int.Parse(seatCode[..digits]), seatCode[digits..]);
    }

    // Gom ghế theo hàng, mỗi hàng sắp theo chữ cái — dùng để vẽ sơ đồ ghế cho mọi kiểu máy bay
    public static IReadOnlyList<(int Row, IReadOnlyList<Seat> Seats)> GetRows(Flight flight)
    {
        var rows = new SortedDictionary<int, List<Seat>>();
        foreach (Seat seat in flight.GetSeats())
        {
            int row = SplitCode(seat.GetCode()).Row;
            if (!rows.ContainsKey(row))
            {
                rows[row] = new List<Seat>();
            }
            rows[row].Add(seat);
        }

        var result = new List<(int Row, IReadOnlyList<Seat> Seats)>();
        foreach (var pair in rows)
        {
            pair.Value.Sort((a, b) => string.CompareOrdinal(SplitCode(a.GetCode()).Letter, SplitCode(b.GetCode()).Letter));
            result.Add((pair.Key, pair.Value));
        }
        return result;
    }

    private static int AddRows(Flight flight, int startRow, int rowCount, string[] letters,
                               ISeatClass seatClass, decimal basePrice)
    {
        int row = startRow;
        for (int i = 0; i < rowCount; i++)
        {
            foreach (string letter in letters)
            {
                flight.AddSeat($"{row}{letter}", seatClass, basePrice);
            }
            row++;
        }
        return row;
    }
}
