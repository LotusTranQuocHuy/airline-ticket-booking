namespace AirlineBooking.WinForms.Admin;

public partial class FrmFlightEdit : Form
{
    private const int SeatWidth = 32;
    private const int SeatHeight = 26;
    private const int SeatGap = 5;
    private const int AisleWidth = 18;
    private const int RowNumberWidth = 22;
    private const int SeatsPerHalf = 3;
    private const int LeftBlockStart = RowNumberWidth + SeatGap;
    private const int AisleStart = LeftBlockStart + SeatsPerHalf * (SeatWidth + SeatGap) - SeatGap;
    private const int RightBlockStart = AisleStart + AisleWidth;
    private const int RowWidth = RightBlockStart + SeatsPerHalf * (SeatWidth + SeatGap) - SeatGap;

    private readonly FlightRow _flight;
    private readonly List<Panel> _seatRows = new();

    public FrmFlightEdit(FlightRow flight)
    {
        InitializeComponent();
        _flight = flight;
        AdminTheme.StylePrimaryButton(btnSave);
        AdminTheme.StyleSecondaryButton(btnClose);

        Text = $"Chỉnh sửa chuyến bay — {flight.Code}";
        lblCodeValue.Text = flight.Code;
        lblRouteValue.Text = flight.Route;
        lblTotalValue.Text = flight.TotalSeats.ToString();
        lblBookedValue.Text = AdminDemoData.CountBooked(flight.Code).ToString();
        dtpDeparture.Value = flight.Departure;
        numBasePrice.Value = Math.Clamp(flight.BasePrice, numBasePrice.Minimum, numBasePrice.Maximum);

        if (flight.HasDeparted)
        {
            dtpDeparture.Enabled = false;
            numBasePrice.Enabled = false;
            btnSave.Enabled = false;
            lblPriceNote.Text = "Chuyến đã bay, không thể chỉnh sửa.";
        }

        BuildSeatMap();
    }

    private void BuildSeatMap()
    {
        var booked = AdminDemoData.GetBookedSeats(_flight.Code).ToHashSet();
        flpSeatMap.SuspendLayout();
        int rowNumber = 1;
        for (int i = 0; i < _flight.BusinessRows; i++)
        {
            AddSeatRow(rowNumber++, FlightRow.BusinessLetters, isBusiness: true, booked);
        }
        for (int i = 0; i < _flight.EconomyRows; i++)
        {
            AddSeatRow(rowNumber++, FlightRow.EconomyLetters, isBusiness: false, booked);
        }
        flpSeatMap.ResumeLayout();
        CenterSeatRows();
    }

    private void AddSeatRow(int rowNumber, string[] letters, bool isBusiness, HashSet<string> booked)
    {
        var row = new Panel { Size = new Size(RowWidth, SeatHeight), Margin = new Padding(0, 0, 0, SeatGap) };
        row.Controls.Add(new Label
        {
            Text = rowNumber.ToString(),
            Font = AdminTheme.SmallFont,
            ForeColor = AdminTheme.RowNumber,
            TextAlign = ContentAlignment.MiddleRight,
            Bounds = new Rectangle(0, 0, RowNumberWidth, SeatHeight),
        });

        int half = letters.Length / 2;
        for (int i = 0; i < letters.Length; i++)
        {
            string code = $"{rowNumber}{letters[i]}";
            int x = i < half
                ? AisleStart - (half - i) * (SeatWidth + SeatGap) + SeatGap   // nửa trái, sát lối đi
                : RightBlockStart + (i - half) * (SeatWidth + SeatGap);        // nửa phải
            var seat = booked.Contains(code)
                ? new SeatLabel(code, AdminTheme.BookedSeat, AdminTheme.BookedSeat, Color.White)
                : isBusiness
                    ? new SeatLabel(code, AdminTheme.BusinessSeat, AdminTheme.BusinessSeatBorder, AdminTheme.Text)
                    : new SeatLabel(code, Color.White, AdminTheme.BookedSeat, AdminTheme.Text);
            seat.Location = new Point(x, 0);
            row.Controls.Add(seat);
        }

        _seatRows.Add(row);
        flpSeatMap.Controls.Add(row);
    }

    private void CenterSeatRows()
    {
        int left = Math.Max(0, (flpSeatMap.ClientSize.Width - RowWidth) / 2);
        foreach (var row in _seatRows)
        {
            row.Margin = new Padding(left, 0, 0, SeatGap);
        }
    }

    private void flpSeatMap_Resize(object? sender, EventArgs e) => CenterSeatRows();

    private void btnSave_Click(object? sender, EventArgs e)
    {
        DateTime newDeparture = AdminTheme.TrimSeconds(dtpDeparture.Value);
        if (newDeparture != _flight.Departure && newDeparture <= DateTime.Now)
        {
            AdminTheme.Warn(this, "Giờ bay mới phải ở thời điểm sau hiện tại.");
            return;
        }

        // Tuần 3: flight.Reschedule(newDeparture) và flight.UpdatePrice(tên hạng ghế, giá mới) trong Core.
        _flight.Departure = newDeparture;
        _flight.BasePrice = numBasePrice.Value;
        DialogResult = DialogResult.OK;
    }
}
