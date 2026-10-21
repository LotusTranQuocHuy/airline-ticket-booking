namespace AirlineBooking.WinForms;

internal sealed class FrmTransferBooking : Form
{
    private readonly TextBox _nameInput = UiKit.TextBox("Họ và tên người nhận");
    private readonly TextBox _emailInput = UiKit.TextBox("Email người nhận (không bắt buộc)");

    public string RecipientName => _nameInput.Text.Trim();

    public string RecipientEmail => _emailInput.Text.Trim();

    public FrmTransferBooking()
    {
        UiKit.ConfigureForm(this, "Chuyển vé | AirlineTicket");
        WindowState = FormWindowState.Normal;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new Size(560, 480);
        Size = new Size(560, 480);

        var header = UiKit.Header(
            "VÉ CỦA TÔI",
            "Chuyển vé cho hành khách khác",
            "Quyền sở hữu vé sẽ chuyển sang người nhận.");

        var card = UiKit.WhiteCard();
        card.Dock = DockStyle.Fill;
        card.Padding = new Padding(32);
        card.Controls.Add(UiKit.At("Họ và tên người nhận", 32, 34, 9, true));
        _nameInput.Location = new Point(32, 58);
        _nameInput.Size = new Size(430, 38);
        card.Controls.Add(_nameInput);
        card.Controls.Add(UiKit.At("Email người nhận (không bắt buộc)", 32, 116, 9, true));
        _emailInput.Location = new Point(32, 140);
        _emailInput.Size = new Size(430, 38);
        card.Controls.Add(_emailInput);
        card.Controls.Add(UiKit.At(
            "Người nhận đăng nhập bằng cùng email; nếu bỏ trống email thì dùng cùng họ tên.",
            32,
            194,
            9,
            false,
            UiKit.Muted));

        var cancelButton = UiKit.Button("Quay lại", height: 42);
        cancelButton.Size = new Size(140, 42);
        cancelButton.Location = new Point(32, 250);
        cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
        card.Controls.Add(cancelButton);

        var transferButton = UiKit.Button("Chuyển vé", primary: true, height: 42);
        transferButton.Size = new Size(170, 42);
        transferButton.Location = new Point(292, 250);
        transferButton.Click += (_, _) => ConfirmTransfer();
        card.Controls.Add(transferButton);

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
        root.Controls.Add(card, 0, 1);
        Controls.Add(root);
        AcceptButton = transferButton;
        CancelButton = cancelButton;
    }

    private void ConfirmTransfer()
    {
        if (RecipientName.Length == 0)
        {
            MessageBox.Show(this, "Vui lòng nhập họ tên người nhận.",
                "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _nameInput.Focus();
            return;
        }

        if (RecipientEmail.Length > 0 && !RecipientEmail.Contains('@'))
        {
            MessageBox.Show(this, "Email chưa đúng định dạng hoặc hãy để trống.",
                "Email không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _emailInput.Focus();
            return;
        }

        DialogResult = DialogResult.OK;
    }
}
