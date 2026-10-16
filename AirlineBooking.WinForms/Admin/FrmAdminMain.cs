namespace AirlineBooking.WinForms.Admin;

public partial class FrmAdminMain : Form
{
    public FrmAdminMain() : this("Quản trị viên")
    {
    }

    public FrmAdminMain(string adminName)
    {
        InitializeComponent();
        lblWelcome.Text = $"Xin chào, {adminName}";
        lblRole.Text = $"Vai trò: Quản trị viên · Đăng nhập lúc {DateTime.Now:HH:mm}, " +
                       AdminTheme.DateText(DateOnly.FromDateTime(DateTime.Today));
        AdminTheme.StyleDangerTextButton(btnLogout);
        RefreshStatus();
    }

    private void RefreshStatus()
    {
        lblOpenFlights.Text = $"Chuyến đang mở bán: {AdminDemoData.Flights.Count(f => !f.HasDeparted)}";
        lblTodayBookings.Text = $"Vé hôm nay: {AdminDemoData.Bookings.Count(b => b.BookedAt.Date == DateTime.Today)}";
    }

    private void OpenDialog(Form form)
    {
        using (form)
        {
            form.ShowDialog(this);
        }
        RefreshStatus();
    }

    private void OpenFlightManager(object? sender, EventArgs e) => OpenDialog(new FrmFlightManager());

    private void OpenBookingManager(object? sender, EventArgs e) => OpenDialog(new FrmBookingManager());

    private void OpenRevenueReport(object? sender, EventArgs e) => OpenDialog(new FrmRevenueReport());

    private void Logout(object? sender, EventArgs e)
    {
        var answer = MessageBox.Show(this, "Bạn muốn đăng xuất khỏi khu vực quản trị?", "Đăng xuất",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (answer == DialogResult.Yes)
        {
            Close();   // quay lại FrmLogin
        }
    }

    private void mnuExit_Click(object? sender, EventArgs e) => Application.Exit();

    private void mnuAbout_Click(object? sender, EventArgs e) =>
        MessageBox.Show(this,
            "Airline Ticket Booking System\nOOP Final Project — Trần Quốc Huy, Ngô Thành Đạt",
            "Giới thiệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
}
