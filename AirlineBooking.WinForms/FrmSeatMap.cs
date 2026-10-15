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
    private string? _selectedCode;

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

        var seatGrid = new TableLayoutPanel
        {
            Location = new Point(28, 60),
            Size = new Size(470, 400),
            ColumnCount = 5,
            RowCount = 8,
            BackColor = Color.White
        };
        seatGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        seatGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        seatGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 56F));
        seatGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        seatGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

        for (var row = 0; row < 8; row++)
        {
            seatGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            var seatClass = GetSeatClass(row);
            var color = GetSeatColor(seatClass);
            foreach (var (column, letter) in new[] { (0, 'A'), (1, 'B'), (3, 'C'), (4, 'D') })
            {
                var code = $"{row + 1}{letter}";
                var taken = DemoData.IsSeatTaken(flight, code) ||
                            (row, letter) is (1, 'D') or (4, 'C') or (6, 'A');
                var seatButton = new Button
                {
                    Text = code,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = taken ? Color.FromArgb(220, 225, 232) : color,
                    ForeColor = taken ? Color.FromArgb(143, 151, 163) : UiKit.Navy,
                    Enabled = !taken,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    UseVisualStyleBackColor = false
                };
                seatButton.FlatAppearance.BorderColor = Color.White;
                seatButton.Click += (_, _) => SelectSeat(code, seatClass, seatButton, seatGrid);
                _seatStyles[code] = (seatClass, color);
                seatGrid.Controls.Add(seatButton, column, row);
            }
        }
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
        detailCard.Controls.Add(UiKit.At("Giá vé mẫu", 20, 293, 9, false, UiKit.Muted));
        _price.Location = new Point(20, 318);
        detailCard.Controls.Add(_price);
        detailCard.Controls.Add(UiKit.At("Giá hiển thị dùng dữ liệu demo.", 20, 353, 8, false, UiKit.Muted));

        _continueButton.Enabled = false;
        _continueButton.Width = 210;
        _continueButton.Location = new Point(20, 403);
        _continueButton.Click += (_, _) => ContinueToConfirmation();
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

    private static string GetSeatClass(int row) => row switch
    {
        0 => "First",
        1 or 2 => "Business",
        3 or 4 => "Premium Economy",
        _ => "Economy"
    };

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

    private void SelectSeat(string code, string seatClass, Button seatButton, TableLayoutPanel seatGrid)
    {
        if (_selectedCode is not null)
        {
            var previous = seatGrid.Controls.OfType<Button>()
                .FirstOrDefault(button => button.Text == _selectedCode);
            if (previous is not null)
            {
                previous.BackColor = _seatStyles[_selectedCode].Color;
                previous.ForeColor = UiKit.Navy;
            }
        }

        _selectedCode = code;
        seatButton.BackColor = UiKit.Blue;
        seatButton.ForeColor = Color.White;
        _selectedSeat.Text = $"{code}  ·  {seatClass}";
        _price.Text = UiKit.Money(_flight.GetPrice(seatClass));
        _continueButton.Enabled = true;
    }

    private void ContinueToConfirmation()
    {
        if (_selectedCode is null)
        {
            return;
        }

        var seatClass = _seatStyles[_selectedCode].SeatClass;
        using var confirmation = new FrmBookingConfirm(
            _passenger,
            _flight,
            _selectedCode,
            seatClass,
            _flight.GetPrice(seatClass));
        if (confirmation.ShowDialog(this) == DialogResult.OK)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
