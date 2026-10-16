using AirlineBooking.Core;

namespace AirlineBooking.WinForms.Admin;

public partial class FrmBookingManager : Form
{
    private const string AllItems = "Tất cả";
    private const string ConfirmedText = "Đã xác nhận";
    private const string CancelledText = "Đã hủy";

    public FrmBookingManager()
    {
        InitializeComponent();
        AdminTheme.StylePrimaryButton(btnSearch);
        AdminTheme.StyleSecondaryButton(btnClearFilter);
        AdminTheme.StyleSecondaryButton(btnBack);
        AdminTheme.StyleDangerButton(btnCancelBooking);
        AdminTheme.StyleGrid(dgvBookings);
        AdminTheme.AlignRight(colPrice);

        cboFlight.Items.Add(AllItems);
        foreach (var flight in AdminDemoData.Flights)
        {
            cboFlight.Items.Add(flight.Code);
        }
        cboStatus.Items.AddRange(new object[] { AllItems, ConfirmedText, CancelledText });
        cboFlight.SelectedIndex = 0;
        cboStatus.SelectedIndex = 0;
        LoadBookings();
    }

    private BookingRow? SelectedBooking => dgvBookings.CurrentRow?.Tag as BookingRow;

    private void LoadBookings(string? selectId = null)
    {
        selectId ??= SelectedBooking?.Id;
        string flight = cboFlight.SelectedItem as string ?? AllItems;
        string status = cboStatus.SelectedItem as string ?? AllItems;
        string name = txtPassenger.Text.Trim();

        var bookings = AdminDemoData.Bookings.Where(b =>
            (flight == AllItems || b.FlightCode == flight) &&
            (status == AllItems || b.StatusText == status) &&
            (name.Length == 0 || b.PassengerName.Contains(name, StringComparison.CurrentCultureIgnoreCase)));

        dgvBookings.Rows.Clear();
        foreach (var booking in bookings)
        {
            int index = dgvBookings.Rows.Add(
                booking.Id,
                booking.PassengerName,
                booking.FlightCode,
                booking.SeatCode,
                booking.SeatClassName,
                AdminTheme.Money(booking.PaidAmount),
                booking.StatusText);
            var row = dgvBookings.Rows[index];
            row.Tag = booking;
            if (booking.Id == selectId)
            {
                dgvBookings.CurrentCell = row.Cells[0];
            }
        }

        lblStatus.Text = $"{dgvBookings.Rows.Count} vé";
        ShowDetail();
    }

    private void ShowDetail()
    {
        if (SelectedBooking is not BookingRow booking)
        {
            lblDetailTitle.Text = "Chi tiết vé";
            foreach (var label in new[] { lblPassengerValue, lblEmailValue, lblFlightValue, lblDepartureValue,
                                          lblSeatValue, lblBookedAtValue, lblPaidValue, lblStatusValue })
            {
                label.Text = "—";
            }
            lblRefundCaption.Text = "Số tiền hoàn";
            lblRefundAmount.Text = "—";
            lblRefundNote.Text = "Chọn một vé trong danh sách để xem chi tiết.";
            btnCancelBooking.Enabled = false;
            return;
        }

        var flight = AdminDemoData.FindFlight(booking.FlightCode);
        lblDetailTitle.Text = $"Chi tiết vé {booking.Id}";
        lblPassengerValue.Text = booking.PassengerName;
        lblEmailValue.Text = booking.PassengerEmail;
        lblFlightValue.Text = flight is null ? booking.FlightCode : $"{booking.FlightCode} · {flight.Route}";
        lblDepartureValue.Text = flight is null ? "—" : AdminTheme.DateTimeText(flight.Departure);
        lblSeatValue.Text = $"{booking.SeatCode} · {booking.SeatClassName}";
        lblBookedAtValue.Text = AdminTheme.DateTimeText(booking.BookedAt);
        lblPaidValue.Text = AdminTheme.Money(booking.PaidAmount);
        lblStatusValue.Text = booking.StatusText;
        lblStatusValue.ForeColor = StatusColor(booking.Status);

        if (booking.Status == BookingStatus.Cancelled)
        {
            lblRefundCaption.Text = "Đã hoàn";
            lblRefundAmount.Text = AdminTheme.Money(booking.RefundAmount);
            lblRefundNote.Text = booking.CancelledAt is DateTime cancelledAt
                ? $"Vé đã hủy lúc {AdminTheme.DateTimeText(cancelledAt)}."
                : "Vé đã được hủy trước đó.";
            btnCancelBooking.Enabled = false;
            return;
        }

        var preview = AdminDemoData.PreviewCancellation(booking, DateTime.Now);
        lblRefundCaption.Text = preview.CanCancel ? $"Số tiền hoàn (phí {preview.FeeRate * 100:0}%)" : "Số tiền hoàn";
        lblRefundAmount.Text = AdminTheme.Money(preview.RefundAmount);
        lblRefundNote.Text = preview.Note;
        btnCancelBooking.Enabled = preview.CanCancel;
    }

    private static Color StatusColor(BookingStatus status) =>
        status == BookingStatus.Confirmed ? AdminTheme.Success : AdminTheme.Danger;

    private void dgvBookings_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex >= 0 && e.ColumnIndex == colStatus.Index &&
            dgvBookings.Rows[e.RowIndex].Tag is BookingRow booking && e.CellStyle is not null)
        {
            e.CellStyle.ForeColor = StatusColor(booking.Status);
            e.CellStyle.SelectionForeColor = StatusColor(booking.Status);
        }
    }

    private void dgvBookings_SelectionChanged(object? sender, EventArgs e) => ShowDetail();

    private void Filter_Changed(object? sender, EventArgs e) => LoadBookings();

    private void btnSearch_Click(object? sender, EventArgs e) => LoadBookings();

    private void btnClearFilter_Click(object? sender, EventArgs e)
    {
        cboFlight.SelectedIndex = 0;
        cboStatus.SelectedIndex = 0;
        txtPassenger.Clear();
        LoadBookings();
    }

    private void btnCancelBooking_Click(object? sender, EventArgs e)
    {
        if (SelectedBooking is not BookingRow booking)
        {
            return;
        }

        var preview = AdminDemoData.PreviewCancellation(booking, DateTime.Now);
        if (!preview.CanCancel)
        {
            AdminTheme.Warn(this, preview.Note);
            return;
        }

        var answer = MessageBox.Show(this,
            $"Hủy vé {booking.Id} của {booking.PassengerName}?\n\n{preview.Note}\n" +
            $"Số tiền hoàn: {AdminTheme.Money(preview.RefundAmount)}",
            "Hủy vé thay khách", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (answer != DialogResult.Yes)
        {
            return;
        }

        // Tuần 3: gọi BookingService.Cancel(booking.Id) trong Core.
        AdminDemoData.CancelBooking(booking, DateTime.Now);
        LoadBookings(booking.Id);
        MessageBox.Show(this, $"Đã hủy vé {booking.Id}. Hoàn cho khách {AdminTheme.Money(booking.RefundAmount)}.",
            "Hủy vé thay khách", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        Close();
    }
}
