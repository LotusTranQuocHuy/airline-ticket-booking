using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

internal sealed class FrmSeatMap : Form
{
    private readonly Passenger _passenger;
    private readonly DemoFlight _flight;
    private readonly Label _selectedSeat = UiKit.Label("Chưa chọn ghế", 11, true);
    private readonly Label _price = UiKit.Label("—", 16, true, UiKit.Blue);
    private readonly Button _continueButton = UiKit.Button("Tiếp tục  →", primary: true, height: 44);
    private readonly Dictionary<string, (string SeatClass, Color Color)> _seatStyles = [];
    private Seat? _selectedSeatModel;
    private string? _selectedCode;

    public Booking? ConfirmedBooking { get; private set; }

    public FrmSeatMap(Passenger passenger, DemoFlight flight)
    {
        _passenger = passenger;
        _flight = flight;
        UiKit.ConfigureForm(this, "Chọn ghế | AirlineTicket");
        var header = UiKit.Header(
            "CHỌN GHẾ",
            $"{flight.Number}  ·  {flight.Origin} → {flight.Destination}",
            flight.Departure.ToString("dddd, dd MMMM yyyy · HH:mm"));

        var content = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(34, 22, 34, 22),
            BackColor = UiKit.Background
        };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));

        var seatCard = UiKit.WhiteCard();
        seatCard.Dock = DockStyle.Fill;
        seatCard.Margin = new Padding(0, 0, 16, 0);
        seatCard.Controls.Add(UiKit.At("SƠ ĐỒ GHẾ", 22, 20, 9, true, UiKit.Blue));
        seatCard.Controls.Add(UiKit.At("Mũi máy bay  ↑", 260, 20, 9, false, UiKit.Muted));

        // Sơ đồ ghế vẽ từ chính danh sách ghế của chuyến bay (SeatLayout.GetRows),
        // nên dùng được cho mọi kiểu máy bay, kể cả chuyến do Admin tạo.
        var seatGrid = new Panel
        {
            Location = new Point(28, 60),
            Size = new Size(470, 400),
            AutoScroll = true,
            BackColor = Color.White
        };
        BuildSeatGrid(seatGrid, flight.Flight);
        seatCard.Controls.Add(seatGrid);
        seatCard.Controls.Add(UiKit.At("Ghế đã đặt", 28, 475, 9, false, UiKit.Muted));

        var detailCard = UiKit.WhiteCard();
        detailCard.Dock = DockStyle.Fill;
        detailCard.Controls.Add(UiKit.At("HẠNG GHẾ", 20, 20, 9, true, UiKit.Blue));
        AddLegend(detailCard, "First", 56);
        AddLegend(detailCard, "Business", 92);
        AddLegend(detailCard, "Premium Economy", 128);
        AddLegend(detailCard, "Economy", 164);
        detailCard.Controls.Add(UiKit.At("Lựa chọn của bạn", 20, 226, 9, true, UiKit.Muted));
        _selectedSeat.Location = new Point(20, 254);
        detailCard.Controls.Add(_selectedSeat);
        detailCard.Controls.Add(UiKit.At("Giá vé", 20, 293, 9, false, UiKit.Muted));
        _price.Location = new Point(20, 318);
        detailCard.Controls.Add(_price);
        detailCard.Controls.Add(UiKit.At("Giá tính theo hạng ghế.", 20, 353, 8, false, UiKit.Muted));

        _continueButton.Enabled = false;
        _continueButton.Width = 210;
        _continueButton.Location = new Point(20, 403);
        _continueButton.Click += (_, _) => ContinueToConfirmation(seatGrid);
        detailCard.Controls.Add(_continueButton);
        var backButton = UiKit.Button("←  Quay lại", height: 40);
        backButton.Width = 210;
        backButton.Location = new Point(20, 454);
        backButton.Click += (_, _) => Close();
        detailCard.Controls.Add(backButton);

        layout.Controls.Add(seatCard, 0, 0);
        layout.Controls.Add(detailCard, 1, 0);
        content.Controls.Add(layout);

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Margin = Padding.Empty,
            Padding = Padding.Empty
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 116F));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        root.Controls.Add(header, 0, 0);
        root.Controls.Add(content, 0, 1);
        Controls.Add(root);
    }

    private const int SeatWidth = 56;
    private const int SeatHeight = 38;
    private const int SeatGap = 6;
    private const int RowLabelWidth = 26;
    private const int AisleWidth = 30;
    private const int MaxSeatsPerSide = 3;
    private const int GridLeft = 12;
    private const int AisleStart = GridLeft + RowLabelWidth + MaxSeatsPerSide * (SeatWidth + SeatGap);
    private const int RightSideStart = AisleStart + AisleWidth;

    private void BuildSeatGrid(Panel seatGrid, Flight flight)
    {
        var rows = SeatLayout.GetRows(flight);
        for (var index = 0; index < rows.Count; index++)
        {
            var (rowNumber, seats) = rows[index];
            var y = index * (SeatHeight + SeatGap);
            seatGrid.Controls.Add(new Label
            {
                Text = rowNumber.ToString(),
                Bounds = new Rectangle(GridLeft, y, RowLabelWidth, SeatHeight),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = UiKit.Muted,
                Font = new Font("Segoe UI", 8.5F)
            });

            // Nửa trái sát lối đi, nửa phải bắt đầu ngay sau lối đi
            var leftCount = (seats.Count + 1) / 2;
            for (var i = 0; i < seats.Count; i++)
            {
                var x = i < leftCount
                    ? AisleStart - (leftCount - i) * (SeatWidth + SeatGap)
                    : RightSideStart + (i - leftCount) * (SeatWidth + SeatGap);
                seatGrid.Controls.Add(CreateSeatButton(seats[i], new Point(x, y), seatGrid));
            }
        }
    }

    private Button CreateSeatButton(Seat seat, Point location, Panel seatGrid)
    {
        var code = seat.GetCode();
        var color = GetSeatColor(seat.GetSeatClass().GetName());
        var taken = !seat.IsAvailable();
        var seatButton = new Button
        {
            Text = code,
            Location = location,
            Size = new Size(SeatWidth, SeatHeight),
            FlatStyle = FlatStyle.Flat,
            BackColor = taken ? Color.FromArgb(220, 225, 232) : color,
            ForeColor = taken ? Color.FromArgb(143, 151, 163) : UiKit.Navy,
            Enabled = !taken,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Cursor = Cursors.Hand,
            UseVisualStyleBackColor = false
        };
        seatButton.FlatAppearance.BorderColor = Color.White;
        seatButton.Click += (_, _) => SelectSeat(seat, seatButton, seatGrid);
        _seatStyles[code] = (seat.GetSeatClass().GetName(), color);
        return seatButton;
    }

    private static Color GetSeatColor(string seatClass) => seatClass switch
    {
        "First" => Color.FromArgb(255, 229, 186),
        "Business" => Color.FromArgb(220, 207, 244),
        "Premium Economy" => Color.FromArgb(204, 231, 250),
        _ => Color.FromArgb(212, 238, 223)
    };

    private void AddLegend(Panel panel, string seatClass, int y)
    {
        panel.Controls.Add(new Panel
        {
            Location = new Point(20, y),
            Size = new Size(18, 18),
            BackColor = GetSeatColor(seatClass)
        });
        panel.Controls.Add(UiKit.At(seatClass, 49, y - 1, 9));
    }

    private void SelectSeat(Seat seat, Button seatButton, Control seatGrid)
    {
        if (!seat.IsAvailable())
        {
            seatButton.Enabled = false;
            seatButton.BackColor = Color.FromArgb(220, 225, 232);
            seatButton.ForeColor = Color.FromArgb(143, 151, 163);
            MessageBox.Show(this, $"Ghế {seat.GetCode()} vừa được đặt. Hãy chọn ghế khác.",
                "Ghế không còn trống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (_selectedCode is not null)
        {
            var previous = seatGrid.Controls.OfType<Button>()
                .FirstOrDefault(button => button.Text == _selectedCode);
            if (previous is not null)
            {
                SetSeatAvailability(previous, _flight.Flight.GetSeat(_selectedCode)!);
            }
        }

        _selectedCode = seat.GetCode();
        _selectedSeatModel = seat;
        seatButton.BackColor = UiKit.Blue;
        seatButton.ForeColor = Color.White;
        _selectedSeat.Text = $"{seat.GetCode()}  ·  {seat.GetSeatClass().GetName()}";
        _price.Text = UiKit.Money(seat.GetSeatClass().CalculatePrice(seat.GetPrice()));
        _continueButton.Enabled = true;
    }

    private void ContinueToConfirmation(Control seatGrid)
    {
        if (_selectedCode is null || _selectedSeatModel is null)
        {
            return;
        }

        using var confirmation = new FrmBookingConfirm(_passenger, _flight, _selectedSeatModel);
        if (confirmation.ShowDialog(this) == DialogResult.OK &&
            confirmation.ConfirmedBooking is { } booking)
        {
            ConfirmedBooking = booking;
            DialogResult = DialogResult.OK;
            Close();
            return;
        }

        RefreshSeatAvailability(seatGrid);
    }

    private void RefreshSeatAvailability(Control seatGrid)
    {
        foreach (var seatButton in seatGrid.Controls.OfType<Button>())
        {
            var seat = _flight.Flight.GetSeat(seatButton.Text);
            if (seat is not null)
            {
                SetSeatAvailability(seatButton, seat);
            }
        }

        if (_selectedSeatModel is null || _selectedSeatModel.IsAvailable())
        {
            return;
        }

        _selectedCode = null;
        _selectedSeatModel = null;
        _selectedSeat.Text = "Ghế vừa được đặt";
        _price.Text = "—";
        _continueButton.Enabled = false;
    }

    private void SetSeatAvailability(Button seatButton, Seat seat)
    {
        var available = seat.IsAvailable();
        seatButton.Enabled = available;
        seatButton.BackColor = available
            ? _seatStyles[seat.GetCode()].Color
            : Color.FromArgb(220, 225, 232);
        seatButton.ForeColor = available
            ? UiKit.Navy
            : Color.FromArgb(143, 151, 163);
    }
}
