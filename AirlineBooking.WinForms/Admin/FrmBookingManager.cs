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
        foreach (Flight flight in AppServices.Catalog.GetAll())
        {
            cboFlight.Items.Add(flight.GetFlightNumber());
        }
        cboStatus.Items.AddRange(new object[] { AllItems, ConfirmedText, CancelledText });
        cboFlight.SelectedIndex = 0;
        cboStatus.SelectedIndex = 0;
        LoadBookings();
    }

    private Booking? SelectedBooking => dgvBookings.CurrentRow?.Tag as Booking;

    private void LoadBookings(string? selectId = null)
    {
        selectId ??= SelectedBooking?.GetBookingId();
        string flight = cboFlight.SelectedItem as string ?? AllItems;
        string status = cboStatus.SelectedItem as string ?? AllItems;
        string name = txtPassenger.Text.Trim();

        dgvBookings.Rows.Clear();
        foreach (Booking booking in AppServices.Bookings.GetBookings())
        {
            // Bỏ qua vé không khớp bộ lọc
            if (flight != AllItems && booking.GetFlight().GetFlightNumber() != flight) continue;
            if (status != AllItems && AdminDisplay.StatusText(booking) != status) continue;
            if (name.Length > 0 &&
                !booking.GetPassenger().GetFullName().Contains(name, StringComparison.CurrentCultureIgnoreCase)) continue;

            int index = dgvBookings.Rows.Add(
                AdminDisplay.ShortId(booking),
                booking.GetPassenger().GetFullName(),
                booking.GetFlight().GetFlightNumber(),
                booking.GetSeat().GetCode(),
                booking.GetSeat().GetSeatClass().GetName(),
                AdminTheme.Money(booking.GetPaidAmount()),
                AdminDisplay.StatusText(booking));
            var row = dgvBookings.Rows[index];
            row.Tag = booking;
            if (booking.GetBookingId() == selectId)
            {
                dgvBookings.CurrentCell = row.Cells[0];
            }
        }

        lblStatus.Text = $"{dgvBookings.Rows.Count} vé";
        ShowDetail();
    }

    private void ShowDetail()
    {
        if (SelectedBooking is not Booking booking)
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

        Flight flight = booking.GetFlight();
        Passenger passenger = booking.GetPassenger();
        lblDetailTitle.Text = $"Chi tiết vé {AdminDisplay.ShortId(booking)}";
        lblPassengerValue.Text = passenger.GetFullName();
        lblEmailValue.Text = passenger.GetEmail().Length > 0 ? passenger.GetEmail() : "—";
        lblFlightValue.Text = $"{flight.GetFlightNumber()} · {AdminDisplay.Route(flight)}";
        lblDepartureValue.Text = AdminTheme.DateTimeText(flight.GetDeparture());
        lblSeatValue.Text = $"{booking.GetSeat().GetCode()} · {booking.GetSeat().GetSeatClass().GetName()}";
        lblBookedAtValue.Text = AdminTheme.DateTimeText(booking.GetBookedAt());
        lblPaidValue.Text = AdminTheme.Money(booking.GetPaidAmount());
        lblStatusValue.Text = AdminDisplay.StatusText(booking);
        lblStatusValue.ForeColor = StatusColor(booking.GetStatus());

        if (booking.GetStatus() == BookingStatus.Cancelled)
        {
            lblRefundCaption.Text = "Đã hoàn";
            lblRefundAmount.Text = AdminTheme.Money(booking.GetRefundAmount());
            DateTime? cancelledAt = booking.GetCancelledAt();
            lblRefundNote.Text = cancelledAt != null
                ? $"Vé đã hủy lúc {AdminTheme.DateTimeText(cancelledAt.Value)}."
                : "Vé đã được hủy trước đó.";
            btnCancelBooking.Enabled = false;
            return;
        }

        var preview = AdminDisplay.PreviewCancellation(booking);
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
            dgvBookings.Rows[e.RowIndex].Tag is Booking booking && e.CellStyle is not null)
        {
            e.CellStyle.ForeColor = StatusColor(booking.GetStatus());
            e.CellStyle.SelectionForeColor = StatusColor(booking.GetStatus());
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
        if (SelectedBooking is not Booking booking)
        {
            return;
        }

        var preview = AdminDisplay.PreviewCancellation(booking);
        if (!preview.CanCancel)
        {
            AdminTheme.Warn(this, preview.Note);
            return;
        }

        string id = AdminDisplay.ShortId(booking);
        var answer = MessageBox.Show(this,
            $"Hủy vé {id} của {booking.GetPassenger().GetFullName()}?\n\n{preview.Note}",
            "Hủy vé thay khách", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (answer != DialogResult.Yes)
        {
            return;
        }

        try
        {
            decimal refund = AppServices.Bookings.Cancel(booking.GetBookingId());
            LoadBookings(booking.GetBookingId());
            MessageBox.Show(this, $"Đã hủy vé {id}. Hoàn cho khách {AdminTheme.Money(refund)}.",
                "Hủy vé thay khách", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (InvalidBookingStateException ex)
        {
            // Ví dụ: khách vừa tự hủy vé này bên màn hình Người dùng
            AdminTheme.Warn(this, ex.Message);
            LoadBookings();
        }
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        Close();
    }
}
