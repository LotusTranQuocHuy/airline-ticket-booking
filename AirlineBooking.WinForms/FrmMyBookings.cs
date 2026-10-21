using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

internal sealed class FrmMyBookings : Form
{
    private readonly Passenger _passenger;
    private readonly DataGridView _grid = new();
    private readonly Label _summary = UiKit.Label("", 9, false, UiKit.Muted);

    public FrmMyBookings(Passenger passenger)
    {
        _passenger = passenger;
        UiKit.ConfigureForm(this, "Vé của tôi | AirlineTicket");
        Controls.Add(UiKit.Header(
            "TÀI KHOẢN",
            "Vé của tôi",
            $"Hành khách: {passenger.GetFullName()}"));

        var content = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(38, 24, 38, 28),
            BackColor = UiKit.Background
        };
        var topRow = new Panel { Dock = DockStyle.Top, Height = 52, BackColor = UiKit.Background };
        topRow.Controls.Add(UiKit.At("Danh sách đặt chỗ", 0, 12, 14, true));
        _summary.Location = new Point(200, 19);
        topRow.Controls.Add(_summary);
        content.Controls.Add(topRow);

        UiKit.ConfigureGrid(_grid);
        _grid.Dock = DockStyle.Fill;
        _grid.Columns.Add("id", "Mã vé");
        _grid.Columns.Add("flight", "Chuyến bay");
        _grid.Columns.Add("departure", "Khởi hành");
        _grid.Columns.Add("seat", "Ghế / Hạng");
        _grid.Columns.Add("amount", "Giá vé");
        _grid.Columns.Add("status", "Trạng thái");
        content.Controls.Add(_grid);

        var footer = new Panel { Dock = DockStyle.Bottom, Height = 70, BackColor = UiKit.Background };
        var backButton = UiKit.Button("←  Quay lại", height: 42);
        backButton.Size = new Size(145, 42);
        backButton.Location = new Point(0, 12);
        backButton.Click += (_, _) => Close();
        footer.Controls.Add(backButton);

        var cancelButton = UiKit.Button("Hủy vé đã chọn", height: 42);
        cancelButton.Size = new Size(190, 42);
        cancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        cancelButton.Location = new Point(Math.Max(0, ClientSize.Width - 250), 12);
        cancelButton.Click += (_, _) => CancelSelectedBooking();
        footer.Controls.Add(cancelButton);
        content.Controls.Add(footer);

        Controls.Add(content);
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
        _summary.Text = $"{bookings.Count} vé";
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
}
