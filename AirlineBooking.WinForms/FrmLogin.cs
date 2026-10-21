using AirlineBooking.Core;
using AirlineBooking.WinForms.Admin;

namespace AirlineBooking.WinForms;

public sealed class FrmLogin : Form
{
    private readonly TextBox _nameInput = UiKit.TextBox("Ví dụ: Nguyễn Minh Anh");
    private readonly TextBox _emailInput = UiKit.TextBox("ban@email.com (không bắt buộc)");
    private readonly FlowLayoutPanel _passengerFields = new()
    {
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        AutoSize = true,
        Visible = false,
        Margin = new Padding(0, 18, 0, 0),
        Padding = new Padding(0)
    };

    public FrmLogin()
    {
        UiKit.ConfigureForm(this, "Đăng nhập | AirlineTicket");
        MinimumSize = new Size(920, 620);
        Size = new Size(1120, 720);
        WindowState = FormWindowState.Maximized;
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
        layout.Controls.Add(new LoginIllustrationPanel(), 0, 0);

        var right = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(26, 38, 26, 30)
        };
        var content = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true
        };
        content.Controls.Add(UiKit.Label("AIRLINETICKET", 9, true, UiKit.Blue));
        content.Controls.Add(UiKit.Label("Chào mừng bạn", 22, true));
        content.Controls.Add(UiKit.Label("Chọn vai trò để tiếp tục vào hệ thống.", 10, false, UiKit.Muted));

        var roleChoices = new FlowLayoutPanel
        {
            AutoSize = true,
            WrapContents = false,
            Margin = new Padding(0, 22, 0, 10)
        };
        var adminButton = UiKit.Button("Quản trị viên", primary: true, height: 48);
        adminButton.Width = 145;
        adminButton.Margin = new Padding(0, 4, 10, 4);
        adminButton.Click += (_, _) => OpenAdmin();
        var userButton = UiKit.Button("Người dùng", primary: true, height: 48);
        userButton.Width = 145;
        userButton.Margin = new Padding(0, 4, 0, 4);
        userButton.Click += (_, _) => ShowPassengerFields();
        roleChoices.Controls.Add(adminButton);
        roleChoices.Controls.Add(userButton);
        content.Controls.Add(roleChoices);

        _passengerFields.Controls.Add(UiKit.Label("Họ và tên", 9, true));
        _nameInput.Width = 300;
        _passengerFields.Controls.Add(_nameInput);
        _passengerFields.Controls.Add(UiKit.Label("Email", 9, true));
        _emailInput.Width = 300;
        _passengerFields.Controls.Add(_emailInput);
        var enterButton = UiKit.Button("Vào hệ thống  →", primary: true, height: 48);
        enterButton.Width = 300;
        enterButton.Click += (_, _) => EnterAsPassenger();
        _passengerFields.Controls.Add(enterButton);
        _passengerFields.Controls.Add(UiKit.Label("Email có thể để trống.", 9, false, UiKit.Muted));
        content.Controls.Add(_passengerFields);
        right.Controls.Add(content);
        layout.Controls.Add(right, 1, 0);
        Controls.Add(layout);
    }

    private void ShowPassengerFields()
    {
        _passengerFields.Visible = true;
        _nameInput.Focus();
    }

    private void EnterAsPassenger()
    {
        var fullName = _nameInput.Text.Trim();
        if (fullName.Length == 0)
        {
            MessageBox.Show(this, "Vui lòng nhập họ tên để thông tin hành khách được gắn với vé.",
                "Thiếu họ tên", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _nameInput.Focus();
            return;
        }

        var email = _emailInput.Text.Trim();
        if (email.Length > 0 && !email.Contains('@'))
        {
            MessageBox.Show(this, "Email chưa đúng định dạng. Hãy nhập email hợp lệ hoặc để trống.",
                "Email không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _emailInput.Focus();
            return;
        }

        var passenger = new Passenger(Guid.NewGuid().ToString("N"), fullName, email);
        Hide();
        using (var main = new FrmUserMain(passenger))
        {
            main.ShowDialog(this);
        }
        Show();
        Activate();
    }

    private void OpenAdmin()
    {
        Hide();
        using (var admin = new FrmAdminMain())
        {
            admin.ShowDialog(this);
        }
        Show();
        Activate();
    }
}
