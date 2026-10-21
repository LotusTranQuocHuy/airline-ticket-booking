using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

internal sealed class FrmMyBookings : Form
{
    private readonly Passenger _passenger;
    private readonly DataGridView _grid = new();
    private readonly Label _summary = UiKit.Label("", 9, false, UiKit.Muted);
    private readonly Label _emptyState = UiKit.Label("Bạn chưa có vé đang hiệu lực.", 12, false, UiKit.Muted);

    public FrmMyBookings(Passenger passenger)
    {
        _passenger = passenger;
        UiKit.ConfigureForm(this, "Vé của tôi | AirlineTicket");
        var header = UiKit.Header(
            "TÀI KHOẢN",
            "Vé của tôi",
            $"Hành khách: {passenger.GetFullName()}");

        var content = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(38, 24, 38, 28),
            BackColor = UiKit.Background,
            Margin = Padding.Empty
        };
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        var topRow = new Panel { Dock = DockStyle.Fill, BackColor = UiKit.Background };
        topRow.Controls.Add(UiKit.At("Danh sách đặt chỗ", 0, 12, 14, true));
        _summary.Location = new Point(200, 19);
        topRow.Controls.Add(_summary);
        content.Controls.Add(topRow, 0, 0);

        UiKit.ConfigureGrid(_grid);
        _grid.Dock = DockStyle.Fill;
        _grid.Columns.Add("id", "Mã vé");
        _grid.Columns.Add("flight", "Chuyến bay");
        _grid.Columns.Add("departure", "Khởi hành");
        _grid.Columns.Add("seat", "Ghế / Hạng");
        _grid.Columns.Add("amount", "Giá vé");
        _grid.Columns.Add("status", "Trạng thái");
        var gridContainer = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
        _grid.Dock = DockStyle.Fill;
        gridContainer.Controls.Add(_grid);
        _emptyState.AutoSize = false;
        _emptyState.Dock = DockStyle.Fill;
        _emptyState.TextAlign = ContentAlignment.MiddleCenter;
        _emptyState.BackColor = Color.White;
        _emptyState.Visible = false;
        gridContainer.Controls.Add(_emptyState);
        _emptyState.BringToFront();
        content.Controls.Add(gridContainer, 0, 1);

        var footer = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            BackColor = UiKit.Background,
            ColumnCount = 3,
            RowCount = 1,
            Padding = new Padding(0, 8, 0, 0)
        };
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
        var backButton = UiKit.Button("←  Quay lại", height: 42);
        backButton.Size = new Size(145, 42);
        backButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        backButton.Click += (_, _) => Close();
        footer.Controls.Add(backButton, 0, 0);

        var transferButton = UiKit.Button("Chuyển vé", height: 42);
        transferButton.Size = new Size(180, 42);
        transferButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        transferButton.Click += (_, _) => TransferSelectedBooking();
        footer.Controls.Add(transferButton, 1, 0);

        var cancelButton = UiKit.Button("Hủy vé đã chọn", height: 42);
        cancelButton.Size = new Size(180, 42);
        cancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        cancelButton.Click += (_, _) => CancelSelectedBooking();
        footer.Controls.Add(cancelButton, 2, 0);
        content.Controls.Add(footer, 0, 2);

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
        RefreshBookings();
    }

    private void RefreshBookings()
    {
        _grid.Rows.Clear();
        var bookings = DemoData.GetBookings(_passenger);
        foreach (var booking in bookings)
        {
            var flight = booking.GetFlight();
            var seat = booking.GetSeat();
            var status = booking.GetStatus() == BookingStatus.Cancelled ? "Đã hủy" : "Đã xác nhận";
            var index = _grid.Rows.Add(
                booking.GetBookingId(),
                $"{flight.GetFlightNumber()} · {flight.GetOrigin()} → {flight.GetDestination()}",
                flight.GetDeparture().ToString("dd/MM/yyyy · HH:mm"),
                $"{seat.GetCode()} · {seat.GetSeatClass().GetName()}",
                UiKit.Money(booking.GetPaidAmount()),
                status);
            _grid.Rows[index].Tag = booking;
        }
        _summary.Text = $"{bookings.Count} vé đang hiệu lực";
        _emptyState.Visible = bookings.Count == 0;
    }

    private void CancelSelectedBooking()
    {
        if (_grid.CurrentRow?.Tag is not Booking booking)
        {
            MessageBox.Show(this, "Hãy chọn một vé trong danh sách.",
                "Chưa chọn vé", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (booking.GetStatus() == BookingStatus.Cancelled)
        {
            MessageBox.Show(this, "Vé này đã được hủy.",
                "Thông tin vé", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        decimal fee;
        decimal refund;
        try
        {
            (fee, refund) = DemoData.PreviewCancellation(booking);
        }
        catch (InvalidBookingStateException ex)
        {
            MessageBox.Show(this, ex.Message, "Không thể hủy vé",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            RefreshBookings();
            return;
        }

        var answer = MessageBox.Show(
            this,
            $"Phí hủy: {UiKit.Money(fee)}\nTiền hoàn: {UiKit.Money(refund)}\n\n" +
            "Bạn có muốn hủy vé này không?",
            "Xác nhận hủy vé",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (answer == DialogResult.Yes)
        {
            try
            {
                var actualRefund = DemoData.Cancel(_passenger, booking.GetBookingId());
                MessageBox.Show(this, $"Đã hủy vé. Số tiền hoàn: {UiKit.Money(actualRefund)}",
                    "Hủy vé thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshBookings();
            }
            catch (InvalidBookingStateException ex)
            {
                MessageBox.Show(this, ex.Message, "Không thể hủy vé",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RefreshBookings();
            }
        }
    }

    private void TransferSelectedBooking()
    {
        if (_grid.CurrentRow?.Tag is not Booking booking)
        {
            MessageBox.Show(this, "Hãy chọn một vé trong danh sách.",
                "Chưa chọn vé", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (booking.GetStatus() != BookingStatus.Confirmed)
        {
            MessageBox.Show(this, "Chỉ có thể chuyển vé đang được xác nhận.",
                "Không thể chuyển vé", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var transferForm = new FrmTransferBooking();
        if (transferForm.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            var recipient = DemoData.Transfer(
                _passenger,
                booking.GetBookingId(),
                transferForm.RecipientName,
                transferForm.RecipientEmail);
            MessageBox.Show(
                this,
                $"Đã chuyển vé {booking.GetBookingId()} cho {recipient.GetFullName()}.\n" +
                "Người nhận có thể đăng nhập bằng cùng họ tên và email đã nhập để xem vé.",
                "Chuyển vé thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            RefreshBookings();
        }
        catch (InvalidBookingStateException ex)
        {
            MessageBox.Show(this, ex.Message, "Không thể chuyển vé",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            RefreshBookings();
        }
    }
}
