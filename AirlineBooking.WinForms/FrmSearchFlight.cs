using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

internal sealed class FrmSearchFlight : Form
{
    private readonly Passenger _passenger;
    private readonly ComboBox _origin = CreateCombo();
    private readonly ComboBox _destination = CreateCombo();
    private readonly DateTimePicker _date = new()
    {
        Format = DateTimePickerFormat.Custom,
        CustomFormat = "dd MMM yyyy",
        MinDate = DateTime.Today,
        Value = DateTime.Today.AddDays(3),
        Width = 170
    };
    private readonly DataGridView _results = new();
    private readonly Label _count = UiKit.Label("", 9, false, UiKit.Muted);

    public FrmSearchFlight(
        Passenger passenger,
        string? origin = null,
        string? destination = null,
        DateTime? departureDate = null,
        bool isRoundTrip = false,
        DateTime? returnDate = null)
    {
        _passenger = passenger;
        UiKit.ConfigureForm(this, "Tìm chuyến bay | AirlineTicket");
        var header = UiKit.Header(
            "ĐẶT VÉ",
            "Tìm chuyến bay",
            isRoundTrip && returnDate is { } tripReturn
                ? $"Khứ hồi · Lượt đi {departureDate ?? DateTime.Today:dd/MM/yyyy} · Lượt về {tripReturn:dd/MM/yyyy}"
                : $"Một chiều · Ngày đi {departureDate ?? DateTime.Today:dd/MM/yyyy}");

        var content = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(38, 24, 38, 28),
            BackColor = UiKit.Background
        };
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            Margin = Padding.Empty,
            Padding = Padding.Empty
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 134F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));

        var filterCard = UiKit.WhiteCard();
        filterCard.Dock = DockStyle.Fill;
        filterCard.Controls.Add(UiKit.At("Điểm đi", 22, 17, 9, true));
        filterCard.Controls.Add(UiKit.At("Điểm đến", 242, 17, 9, true));
        filterCard.Controls.Add(UiKit.At("Ngày bay", 462, 17, 9, true));

        PopulateLocations();
        _origin.Location = new Point(22, 47);
        _origin.Size = new Size(200, 38);
        _destination.Location = new Point(242, 47);
        _destination.Size = new Size(200, 38);
        _date.Location = new Point(462, 48);
        filterCard.Controls.Add(_origin);
        filterCard.Controls.Add(_destination);
        filterCard.Controls.Add(_date);

        var searchButton = UiKit.Button("Tìm chuyến", primary: true, height: 40);
        searchButton.Size = new Size(150, 40);
        searchButton.Location = new Point(657, 46);
        searchButton.Click += (_, _) => Search();
        filterCard.Controls.Add(searchButton);
        layout.Controls.Add(filterCard, 0, 0);

        var resultHeader = new Panel { Dock = DockStyle.Fill, BackColor = UiKit.Background };
        resultHeader.Controls.Add(UiKit.At("Chuyến bay phù hợp", 0, 13, 14, true));
        _count.Location = new Point(220, 19);
        resultHeader.Controls.Add(_count);
        layout.Controls.Add(resultHeader, 0, 1);

        UiKit.ConfigureGrid(_results);
        _results.Dock = DockStyle.Fill;
        _results.Columns.Add("flight", "Số hiệu");
        _results.Columns.Add("route", "Hành trình");
        _results.Columns.Add("departure", "Khởi hành");
        _results.Columns.Add("basePrice", "Giá từ");
        _results.CellDoubleClick += (_, _) => SelectFlight();
        layout.Controls.Add(_results, 0, 2);

        var footer = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            BackColor = UiKit.Background,
            Padding = new Padding(0, 8, 0, 0)
        };
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 185F));
        var backButton = UiKit.Button("←  Quay lại", height: 42);
        backButton.Size = new Size(145, 42);
        backButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        backButton.Click += (_, _) => Close();
        footer.Controls.Add(backButton, 0, 0);
        var continueButton = UiKit.Button("Chọn chuyến  →", primary: true, height: 42);
        continueButton.Size = new Size(185, 42);
        continueButton.Click += (_, _) => SelectFlight();
        footer.Controls.Add(continueButton, 1, 0);
        layout.Controls.Add(footer, 0, 3);
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

        _origin.SelectedIndex = _origin.Items.IndexOf(origin ?? SampleData.HoChiMinh);
        _destination.SelectedIndex = _destination.Items.IndexOf(destination ?? SampleData.HaNoi);
        if (departureDate is { } selectedDate &&
            selectedDate.Date >= _date.MinDate.Date &&
            selectedDate.Date <= _date.MaxDate.Date)
        {
            _date.Value = selectedDate.Date;
        }
        Search();
    }

    private static ComboBox CreateCombo() => new()
    {
        DropDownStyle = ComboBoxStyle.DropDownList,
        Font = new Font("Segoe UI", 10F),
        FlatStyle = FlatStyle.Flat
    };

    private void PopulateLocations()
    {
        foreach (var location in DemoData.GetLocations())
        {
            _origin.Items.Add(location);
            _destination.Items.Add(location);
        }
    }

    private void Search()
    {
        if (_origin.SelectedItem is null || _destination.SelectedItem is null)
        {
            MessageBox.Show(this, "Chọn điểm đi và điểm đến trước khi tìm chuyến.",
                "Thiếu thông tin hành trình", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (string.Equals(_origin.Text, _destination.Text, StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show(this, "Điểm đi và điểm đến phải khác nhau.",
                "Hành trình không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _results.Rows.Clear();
        var matches = DemoData.SearchFlights(
            _origin.Text,
            _destination.Text,
            DateOnly.FromDateTime(_date.Value));

        foreach (var flight in matches)
        {
            var rowIndex = _results.Rows.Add(
                flight.Number,
                $"{flight.Origin}  →  {flight.Destination}",
                flight.Departure.ToString("dd/MM/yyyy · HH:mm"),
                UiKit.Money(flight.BasePrice));
            _results.Rows[rowIndex].Tag = flight;
        }

        _count.Text = matches.Count == 0 ? "Không tìm thấy chuyến phù hợp" : $"{matches.Count} chuyến";
        if (matches.Count == 0)
        {
            var index = _results.Rows.Add(
                "Chưa có chuyến",
                "Hãy thử đổi điểm đi, điểm đến hoặc ngày bay.",
                "",
                "");
            _results.Rows[index].Tag = null;
        }
        else
        {
            _results.CurrentCell = _results.Rows[0].Cells[0];
            _results.Rows[0].Selected = true;
        }
    }

    private void SelectFlight()
    {
        if (_results.CurrentRow?.Tag is not DemoFlight flight)
        {
            MessageBox.Show(this, "Chọn một chuyến bay trong danh sách trước khi tiếp tục.",
                "Chưa chọn chuyến", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var seatMap = new FrmSeatMap(_passenger, flight);
        if (seatMap.ShowDialog(this) == DialogResult.OK &&
            seatMap.ConfirmedBooking is { } booking)
        {
            MessageBox.Show(
                this,
                DemoData.FormatBookingReceipt(booking),
                "Đặt vé thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
