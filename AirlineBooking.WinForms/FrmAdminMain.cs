namespace AirlineBooking.WinForms;

internal sealed class FrmAdminMain : Form
{
    public FrmAdminMain()
    {
        UiKit.ConfigureForm(this, "Khu vực quản trị | AirlineTicket");
        Controls.Add(UiKit.Header(
            "ADMIN",
            "Khu vực quản trị",
            "Bố cục tổng quan — các chức năng quản trị sẽ được bổ sung trong phần Admin."));

        var body = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(38, 28, 38, 20),
            BackColor = UiKit.Background,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };
        body.Controls.Add(UiKit.Label("Xin chào, Admin", 22, true));
        body.Controls.Add(UiKit.Label(
            "Chọn một khu vực để xem bố cục mẫu. Dữ liệu quản trị chưa được kết nối.",
            10,
            false,
            UiKit.Muted));

        var cards = new FlowLayoutPanel
        {
            AutoSize = true,
            WrapContents = false,
            Margin = new Padding(0, 22, 0, 18)
        };
        cards.Controls.Add(CreateAdminCard("Chuyến bay", "Tạo và chỉnh sửa chuyến bay"));
        cards.Controls.Add(CreateAdminCard("Đặt vé", "Tra cứu và quản lý vé"));
        cards.Controls.Add(CreateAdminCard("Doanh thu", "Xem báo cáo tổng hợp"));
        body.Controls.Add(cards);

        var logout = UiKit.Button("Đăng xuất");
        logout.Width = 150;
        logout.Click += (_, _) => Close();
        body.Controls.Add(logout);
        Controls.Add(body);
    }

    private static Panel CreateAdminCard(string title, string description)
    {
        var card = UiKit.WhiteCard();
        card.Size = new Size(260, 160);
        card.Margin = new Padding(0, 0, 16, 0);
        card.Controls.Add(UiKit.At(title, 22, 22, 15, true));
        card.Controls.Add(UiKit.At(description, 22, 55, 9, false, UiKit.Muted));
        card.Controls.Add(UiKit.At("Đang chờ hoàn thiện", 22, 106, 9, true, UiKit.Blue));
        return card;
    }
}
