using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

internal sealed class FrmUserMain : Form
{
    private const int MaximumContentWidth = 1220;
    private readonly Passenger _passenger;
    private readonly TableLayoutPanel _hero = new()
    {
        Dock = DockStyle.Fill,
        BackColor = UiKit.Navy,
        ColumnCount = 1,
        RowCount = 2,
        Margin = Padding.Empty,
        Padding = Padding.Empty
    };
    private readonly Panel _searchContent = new()
    {
        Width = MaximumContentWidth,
        Height = 336,
        BackColor = Color.Transparent
    };
    private readonly ComboBox _origin = CreateCombo();
    private readonly ComboBox _destination = CreateCombo();
    private readonly DateTimePicker _departureDate = CreateDatePicker();
    private readonly DateTimePicker _returnDate = CreateDatePicker();
    private readonly ComboBox _passengerAndClass = CreateCombo();
    private readonly Button _roundTripButton = UiKit.Button("Khứ hồi", height: 36);
    private readonly Button _oneWayButton = UiKit.Button("Một chiều", height: 36);
    private readonly Label _welcome = UiKit.Label("", 9, false, Color.FromArgb(199, 215, 234));
    private bool _isRoundTrip = true;

    public FrmUserMain(Passenger passenger)
    {
        _passenger = passenger;
        UiKit.ConfigureForm(this, "Đặt vé máy bay | AirlineTicket");
        _hero.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _hero.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
        _hero.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        BuildNavigation();
        BuildSearchHero();
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Margin = Padding.Empty,
            Padding = Padding.Empty
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 438F));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        root.Controls.Add(_hero, 0, 0);
        root.Controls.Add(BuildDiscoverSection(), 0, 1);
        Controls.Add(root);
    }

    private void BuildNavigation()
    {
        var navigation = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = UiKit.Navy,
            Padding = new Padding(40, 0, 40, 0)
        };

        var brand = UiKit.Label("✈  AIRLINETICKET", 18, true, Color.White);
        brand.Location = new Point(0, 23);
        navigation.Controls.Add(brand);

        var logoutButton = UiKit.Button("Đăng xuất", height: 38);
        logoutButton.Size = new Size(150, 38);
        logoutButton.TextAlign = ContentAlignment.MiddleCenter;
        logoutButton.AutoEllipsis = false;
        logoutButton.Location = new Point(0, 18);
        logoutButton.Click += (_, _) => Close();

        var bookingsButton = UiKit.Button("Vé của tôi", height: 38);
        bookingsButton.Size = new Size(150, 38);
        bookingsButton.TextAlign = ContentAlignment.MiddleCenter;
        bookingsButton.AutoEllipsis = false;
        bookingsButton.Location = new Point(160, 18);
        bookingsButton.Click += (_, _) => OpenBookings();

        var profile = UiKit.Label(
            _passenger.GetFullName(),
            9,
            true,
            Color.White);
        profile.AutoEllipsis = true;
        profile.TextAlign = ContentAlignment.MiddleRight;
        profile.Size = new Size(180, 38);
        profile.Location = new Point(320, 18);

        var right = new Panel
        {
            Size = new Size(520, 76),
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Location = new Point(Width - 560, 0)
        };
        right.Controls.Add(logoutButton);
        right.Controls.Add(bookingsButton);
        right.Controls.Add(profile);
        navigation.Controls.Add(right);

        navigation.Resize += (_, _) =>
        {
            brand.Location = new Point(0, 23);
            right.Location = new Point(Math.Max(0, navigation.ClientSize.Width - right.Width), 0);
        };

        _hero.Controls.Add(navigation, 0, 0);
    }

    private void BuildSearchHero()
    {
        var searchArea = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = UiKit.Navy
        };
        _searchContent.Top = 22;

        var title = UiKit.At("Dễ dàng tìm chuyến bay cho hành trình tiếp theo.", 0, 0, 25, true, Color.White);
        title.AutoSize = false;
        title.Size = new Size(MaximumContentWidth, 52);
        title.TextAlign = ContentAlignment.MiddleLeft;
        _searchContent.Controls.Add(title);
        _welcome.Text = $"Chào {_passenger.GetFullName()}, chọn hành trình bạn muốn khám phá.";
        _welcome.AutoSize = false;
        _welcome.Location = new Point(2, 56);
        _welcome.Size = new Size(MaximumContentWidth - 4, 24);
        _welcome.TextAlign = ContentAlignment.MiddleLeft;
        _welcome.AutoEllipsis = true;
        _searchContent.Controls.Add(_welcome);

        _roundTripButton.Location = new Point(0, 88);
        _roundTripButton.Size = new Size(145, 38);
        _roundTripButton.TextAlign = ContentAlignment.MiddleCenter;
        _roundTripButton.AutoEllipsis = false;
        _oneWayButton.Location = new Point(155, 88);
        _oneWayButton.Size = new Size(155, 38);
        _oneWayButton.TextAlign = ContentAlignment.MiddleCenter;
        _oneWayButton.AutoEllipsis = false;
        _roundTripButton.Click += (_, _) => SetTripType(roundTrip: true);
        _oneWayButton.Click += (_, _) => SetTripType(roundTrip: false);
        _searchContent.Controls.Add(_roundTripButton);
        _searchContent.Controls.Add(_oneWayButton);
        SetTripType(roundTrip: true);

        PopulateSearchChoices();
        var searchRow = new TableLayoutPanel
        {
            Location = new Point(0, 138),
            Size = new Size(MaximumContentWidth, 94),
            ColumnCount = 6,
            RowCount = 1,
            Margin = new Padding(0),
            Padding = new Padding(0),
            BackColor = Color.Transparent
        };
        searchRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19F));
        searchRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19F));
        searchRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
        searchRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
        searchRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        searchRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
        searchRow.Controls.Add(CreateSearchField("TỪ", _origin), 0, 0);
        searchRow.Controls.Add(CreateSearchField("ĐẾN", _destination), 1, 0);
        searchRow.Controls.Add(CreateSearchField("NGÀY ĐI", _departureDate), 2, 0);
        searchRow.Controls.Add(CreateSearchField("NGÀY VỀ", _returnDate), 3, 0);
        searchRow.Controls.Add(CreateSearchField("HÀNH KHÁCH & HẠNG GHẾ", _passengerAndClass), 4, 0);

        var searchButton = UiKit.Button("Tìm kiếm", primary: true, height: 78);
        searchButton.Dock = DockStyle.Fill;
        searchButton.Margin = new Padding(5, 0, 0, 0);
        searchButton.Click += (_, _) => SearchFlights();
        searchRow.Controls.Add(searchButton, 5, 0);
        _searchContent.Controls.Add(searchRow);
        var swapButton = UiKit.Button("⇄", height: 40);
        swapButton.Size = new Size(42, 42);
        swapButton.FlatAppearance.BorderColor = UiKit.Border;
        swapButton.Location = new Point(224, 164);
        swapButton.BringToFront();
        swapButton.Click += (_, _) => SwapLocations();
        _searchContent.Controls.Add(swapButton);

        var nonstop = new CheckBox
        {
            Text = "Chuyến bay thẳng",
            AutoSize = true,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9F),
            Location = new Point(0, 240)
        };
        _searchContent.Controls.Add(nonstop);

        var demoNote = UiKit.At(
            "Các chuyến bay hiển thị là dữ liệu mẫu; đặt và hủy vé được xử lý trong phiên ứng dụng.",
            0,
            271,
            9,
            false,
            Color.FromArgb(177, 197, 220));
        _searchContent.Controls.Add(demoNote);

        _origin.SelectedIndex = _origin.Items.IndexOf(SampleData.HoChiMinh);
        _destination.SelectedIndex = _destination.Items.IndexOf(SampleData.HaNoi);
        _departureDate.Value = DemoData.GetFlights()[0].Departure.Date;
        _returnDate.Value = DemoData.GetFlights()[0].Departure.Date.AddDays(4);
        _passengerAndClass.SelectedIndex = 0;
        _departureDate.ValueChanged += (_, _) =>
        {
            if (_returnDate.Value.Date < _departureDate.Value.Date)
            {
                _returnDate.Value = _departureDate.Value.Date;
            }
            _returnDate.MinDate = _departureDate.Value.Date;
        };
        Font? fittedTitleFont = null;
        void ResizeSearchContent()
        {
            _searchContent.Width = GetContentWidth(searchArea.ClientSize.Width);
            _searchContent.Left = Math.Max(28, (searchArea.ClientSize.Width - _searchContent.Width) / 2);
            searchRow.Width = _searchContent.ClientSize.Width;

            var fontSize = GetFittingTitleFontSize(title.Text, _searchContent.ClientSize.Width);
            if (fittedTitleFont?.Size != fontSize)
            {
                var previous = title.Font;
                fittedTitleFont = new Font("Segoe UI", fontSize, FontStyle.Bold);
                title.Font = fittedTitleFont;
                previous.Dispose();
            }

            title.Width = _searchContent.ClientSize.Width;
            title.Height = Math.Max(52, title.Font.Height + 12);
            _welcome.Location = new Point(2, title.Bottom + 2);
            _welcome.Width = Math.Max(0, _searchContent.ClientSize.Width - 4);
            var tripButtonsTop = _welcome.Bottom + 8;
            _roundTripButton.Top = tripButtonsTop;
            _oneWayButton.Top = tripButtonsTop;
            searchRow.Top = _roundTripButton.Bottom + 10;
            swapButton.Top = searchRow.Top + (searchRow.Height - swapButton.Height) / 2;
            nonstop.Top = searchRow.Bottom + 12;
            demoNote.Top = nonstop.Bottom + 6;
        }

        searchArea.Resize += (_, _) => ResizeSearchContent();
        searchArea.Controls.Add(_searchContent);
        _hero.Controls.Add(searchArea, 0, 1);
        ResizeSearchContent();
    }

    private Panel BuildDiscoverSection()
    {
        var discover = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(247, 249, 252),
            AutoScroll = true
        };

        var content = new Panel
        {
            Width = MaximumContentWidth,
            Height = 610,
            BackColor = Color.Transparent
        };

        var sectionTitle = UiKit.At("Khám phá hành trình", 0, 24, 18, true);
        content.Controls.Add(sectionTitle);
        content.Controls.Add(UiKit.At(
            "Một vài chuyến bay gợi ý để bạn bắt đầu lên kế hoạch.",
            0,
            62,
            10,
            false,
            UiKit.Muted));

        var promo = new TravelPromoPanel
        {
            Location = new Point(0, 104),
            Size = new Size(MaximumContentWidth, 196)
        };
        promo.Controls.Add(UiKit.At("ĐI XA HƠN, KHÁM PHÁ NHIỀU HƠN", 30, 25, 9, true, Color.FromArgb(190, 220, 255)));
        promo.Controls.Add(UiKit.At("Chuyến đi mới đang chờ bạn.", 30, 53, 23, true, Color.White));
        promo.Controls.Add(UiKit.At("Tìm chuyến bay phù hợp và chọn chỗ ngồi yêu thích.", 32, 165, 9, false, Color.White));
        content.Controls.Add(promo);

        content.Controls.Add(UiKit.At("Điểm đến được gợi ý", 0, 335, 16, true));
        var cards = new FlowLayoutPanel
        {
            Location = new Point(0, 375),
            Size = new Size(MaximumContentWidth, 178),
            WrapContents = false,
            BackColor = Color.Transparent,
            Margin = new Padding(0)
        };
        var suggestedFlights = DemoData.GetFlights().Take(4).ToArray();
        foreach (var flight in suggestedFlights)
        {
            cards.Controls.Add(CreateFlightCard(flight, GetSuggestedCardWidth(MaximumContentWidth)));
        }
        content.Controls.Add(cards);

        discover.Resize += (_, _) =>
        {
            var contentWidth = GetContentWidth(discover.ClientSize.Width);
            content.Width = contentWidth;
            content.Left = Math.Max(28, (discover.ClientSize.Width - contentWidth) / 2);
            content.Top = 0;
            promo.Width = contentWidth;
            cards.Width = contentWidth;
            var cardWidth = GetSuggestedCardWidth(contentWidth);
            for (var index = 0; index < cards.Controls.Count; index++)
            {
                if (cards.Controls[index] is Panel card)
                {
                    card.Width = cardWidth;
                    card.Margin = new Padding(0, 0, index == cards.Controls.Count - 1 ? 0 : 12, 0);
                }
            }
        };
        discover.Controls.Add(content);
        return discover;
    }

    private static int GetContentWidth(int availableWidth) =>
        Math.Min(MaximumContentWidth, Math.Max(760, availableWidth - 56));

    private static int GetSuggestedCardWidth(int contentWidth) =>
        Math.Max(220, (contentWidth - 36) / 4);

    private static float GetFittingTitleFontSize(string text, int width)
    {
        for (var size = 25F; size >= 18F; size -= 0.5F)
        {
            using var font = new Font("Segoe UI", size, FontStyle.Bold);
            if (TextRenderer.MeasureText(text, font, Size.Empty, TextFormatFlags.SingleLine).Width <= width - 12)
            {
                return size;
            }
        }

        return 18F;
    }

    private static Panel CreateSearchField(string caption, Control input)
    {
        var field = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Margin = new Padding(2, 0, 2, 0),
            Padding = new Padding(13, 11, 13, 9)
        };
        var label = UiKit.At(caption, 13, 9, 8, true, UiKit.Muted);
        label.Dock = DockStyle.Top;
        label.Height = 18;
        input.Dock = DockStyle.Fill;
        input.Margin = new Padding(0);
        field.Controls.Add(input);
        field.Controls.Add(label);
        return field;
    }

    private void PopulateSearchChoices()
    {
        foreach (var location in DemoData.GetLocations())
        {
            _origin.Items.Add(location);
            _destination.Items.Add(location);
        }

        _passengerAndClass.Items.AddRange(
        [
            "1 hành khách · Phổ thông",
            "1 hành khách · Phổ thông cao cấp",
            "1 hành khách · Thương gia",
            "1 hành khách · Hạng nhất"
        ]);
    }

    private void SetTripType(bool roundTrip)
    {
        _isRoundTrip = roundTrip;
        StyleTripButton(_roundTripButton, roundTrip);
        StyleTripButton(_oneWayButton, !roundTrip);
        _returnDate.Enabled = roundTrip;
        if (_returnDate.Parent is { } returnField)
        {
            returnField.BackColor = roundTrip ? Color.White : Color.FromArgb(238, 241, 245);
        }
    }

    private static void StyleTripButton(Button button, bool selected)
    {
        button.BackColor = selected ? UiKit.Blue : UiKit.Navy;
        button.ForeColor = Color.White;
        button.FlatAppearance.BorderColor = selected ? UiKit.Blue : Color.FromArgb(91, 116, 145);
    }

    private static ComboBox CreateCombo() => new()
    {
        DropDownStyle = ComboBoxStyle.DropDownList,
        Font = new Font("Segoe UI", 9F),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.White,
        ForeColor = UiKit.Navy
    };

    private static DateTimePicker CreateDatePicker() => new()
    {
        Format = DateTimePickerFormat.Custom,
        CustomFormat = "dd MMM yyyy",
        MinDate = DateTime.Today,
        Font = new Font("Segoe UI", 9F),
        CalendarMonthBackground = Color.White,
        CalendarTitleBackColor = UiKit.Navy
    };

    private void SwapLocations()
    {
        var origin = _origin.SelectedItem;
        _origin.SelectedItem = _destination.SelectedItem;
        _destination.SelectedItem = origin;
    }

    private void SearchFlights()
    {
        if (_origin.SelectedItem is null || _destination.SelectedItem is null)
        {
            MessageBox.Show(this, "Chọn điểm đi và điểm đến trước khi tìm kiếm.",
                "Thiếu thông tin hành trình", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (string.Equals(_origin.Text, _destination.Text, StringComparison.Ordinal))
        {
            MessageBox.Show(this, "Điểm đi và điểm đến cần khác nhau.",
                "Hành trình không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (_isRoundTrip && _returnDate.Value.Date < _departureDate.Value.Date)
        {
            MessageBox.Show(this, "Ngày về không thể sớm hơn ngày đi.",
                "Ngày về không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _returnDate.Focus();
            return;
        }

        using var search = new FrmSearchFlight(
            _passenger,
            _origin.Text,
            _destination.Text,
            _departureDate.Value.Date,
            _isRoundTrip,
            _isRoundTrip ? _returnDate.Value.Date : null);
        search.ShowDialog(this);
    }

    private Panel CreateFlightCard(DemoFlight flight, int width)
    {
        var card = UiKit.WhiteCard();
        card.Size = new Size(width, 164);
        card.Margin = new Padding(0, 0, 12, 0);
        card.Controls.Add(UiKit.At(flight.Number, 18, 17, 8, true, UiKit.Blue));
        card.Controls.Add(UiKit.At($"{flight.Origin}  →  {flight.Destination}", 18, 42, 10, true));
        card.Controls.Add(UiKit.At(flight.Departure.ToString("dd MMM yyyy · HH:mm"), 18, 76, 9, false, UiKit.Muted));
        card.Controls.Add(UiKit.At($"Từ {UiKit.Money(flight.BasePrice)}", 18, 102, 10, true, UiKit.Blue));
        var bookButton = UiKit.Button("Chọn chuyến  →", primary: true, height: 34);
        bookButton.Size = new Size(Math.Min(140, width - 36), 34);
        bookButton.Location = new Point(18, 125);
        bookButton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        bookButton.Click += (_, _) => OpenSeatMap(flight);
        card.Controls.Add(bookButton);
        return card;
    }

    private void OpenSeatMap(DemoFlight flight)
    {
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

    private void OpenBookings()
    {
        using var bookings = new FrmMyBookings(_passenger);
        bookings.ShowDialog(this);
    }
}

internal sealed class TravelPromoPanel : Panel
{
    public TravelPromoPanel()
    {
        DoubleBuffered = true;
        BackColor = UiKit.Blue;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        using var gradient = new System.Drawing.Drawing2D.LinearGradientBrush(
            ClientRectangle,
            Color.FromArgb(15, 99, 222),
            Color.FromArgb(38, 153, 227),
            0F);
        e.Graphics.FillRectangle(gradient, ClientRectangle);

        using var circleBrush = new SolidBrush(Color.FromArgb(38, Color.White));
        e.Graphics.FillEllipse(circleBrush, Width - 318, -96, 340, 340);
        e.Graphics.FillEllipse(circleBrush, Width - 208, 67, 146, 146);
        using var routePen = new Pen(Color.FromArgb(172, Color.White), 2F)
        {
            DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
        };
        e.Graphics.DrawArc(routePen, Width - 410, 15, 330, 158, 205, 130);
        using var planeFont = new Font("Segoe UI Symbol", 52F, FontStyle.Regular);
        using var planeBrush = new SolidBrush(Color.White);
        e.Graphics.DrawString("✈", planeFont, planeBrush, Width - 250, 62);
    }
}
