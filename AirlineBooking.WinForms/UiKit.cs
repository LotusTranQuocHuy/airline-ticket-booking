using System.Drawing.Drawing2D;

namespace AirlineBooking.WinForms;

internal static class UiKit
{
    public static readonly Color Navy = Color.FromArgb(20, 42, 78);
    public static readonly Color Blue = Color.FromArgb(35, 112, 219);
    public static readonly Color PaleBlue = Color.FromArgb(234, 243, 255);
    public static readonly Color Background = Color.FromArgb(246, 248, 252);
    public static readonly Color Muted = Color.FromArgb(106, 120, 141);
    public static readonly Color Border = Color.FromArgb(222, 229, 238);
    public static readonly Color Green = Color.FromArgb(40, 145, 104);
    public static readonly Color Orange = Color.FromArgb(214, 139, 46);
    public static readonly Color Purple = Color.FromArgb(130, 91, 191);

    public static void ConfigureForm(Form form, string title)
    {
        form.Text = title;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimumSize = new Size(980, 650);
        form.Size = new Size(1180, 760);
        form.WindowState = FormWindowState.Maximized;
        form.BackColor = Background;
        form.Font = new Font("Segoe UI", 9F);
        form.ForeColor = Navy;
    }

    public static Label Label(string text, int size = 10, bool bold = false, Color? color = null)
    {
        return new Label
        {
            Text = text,
            AutoSize = true,
            Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular),
            ForeColor = color ?? Navy,
            Margin = new Padding(0, 0, 0, 8)
        };
    }

    public static Label At(string text, int x, int y, int size = 10, bool bold = false, Color? color = null)
    {
        var label = Label(text, size, bold, color);
        label.Location = new Point(x, y);
        return label;
    }

    public static Button Button(string text, bool primary = false, int height = 44)
    {
        var button = new Button
        {
            Text = text,
            Height = height,
            AutoSize = false,
            FlatStyle = FlatStyle.Flat,
            BackColor = primary ? Blue : Color.White,
            ForeColor = primary ? Color.White : Navy,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Cursor = Cursors.Hand,
            UseVisualStyleBackColor = false,
            Margin = new Padding(0, 4, 10, 4),
            Padding = new Padding(12, 0, 12, 0)
        };
        button.FlatAppearance.BorderColor = primary ? Blue : Border;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.MouseOverBackColor = primary ? Color.FromArgb(26, 94, 190) : PaleBlue;
        return button;
    }

    public static TextBox TextBox(string placeholder) =>
        new()
        {
            PlaceholderText = placeholder,
            BorderStyle = BorderStyle.FixedSingle,
            Font = new Font("Segoe UI", 11F),
            Height = 38,
            Margin = new Padding(0, 2, 0, 12)
        };

    public static Panel Header(string section, string title, string subtitle)
    {
        var header = new Panel
        {
            Dock = DockStyle.Top,
            Height = 116,
            BackColor = Color.White,
            Padding = new Padding(38, 18, 38, 14)
        };
        header.Controls.Add(At(section.ToUpperInvariant(), 38, 16, 9, true, Blue));
        header.Controls.Add(At(title, 38, 36, 21, true));
        header.Controls.Add(At(subtitle, 40, 78, 9, false, Muted));
        header.Paint += (_, e) =>
        {
            using var pen = new Pen(Border);
            e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
        };
        return header;
    }

    public static void ConfigureGrid(DataGridView grid)
    {
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 251);
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Muted;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        grid.ColumnHeadersHeight = 42;
        grid.DefaultCellStyle.BackColor = Color.White;
        grid.DefaultCellStyle.ForeColor = Navy;
        grid.DefaultCellStyle.SelectionBackColor = PaleBlue;
        grid.DefaultCellStyle.SelectionForeColor = Navy;
        grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
        grid.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
        grid.RowTemplate.Height = 42;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    public static Panel WhiteCard() =>
        new()
        {
            BackColor = Color.White,
            Padding = new Padding(22),
            Margin = new Padding(0, 0, 0, 16)
        };

    public static string Money(decimal amount) => $"{amount:N0} ₫";
}

internal sealed class LoginIllustrationPanel : Panel
{
    public LoginIllustrationPanel()
    {
        DoubleBuffered = true;
        Dock = DockStyle.Fill;
        BackColor = UiKit.Navy;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        var bounds = ClientRectangle;
        using (var background = new LinearGradientBrush(
                   bounds,
                   Color.FromArgb(15, 35, 71),
                   Color.FromArgb(43, 103, 173),
                   35F))
        {
            g.FillRectangle(background, bounds);
        }

        using var orb = new SolidBrush(Color.FromArgb(34, 255, 255, 255));
        g.FillEllipse(orb, Width - 250, 75, 250, 250);
        g.FillEllipse(orb, Width - 180, 430, 150, 150);
        using var routePen = new Pen(Color.FromArgb(120, 225, 240, 255), 2F)
        {
            DashStyle = DashStyle.Dash
        };
        g.DrawArc(routePen, 42, 130, Width - 40, 320, 205, 130);
        using var planeFont = new Font("Segoe UI Symbol", 44F, FontStyle.Regular);
        using var whiteBrush = new SolidBrush(Color.White);
        g.DrawString("✈", planeFont, whiteBrush, Width - 178, 238);
        using var titleFont = new Font("Segoe UI", 22F, FontStyle.Bold);
        using var bodyFont = new Font("Segoe UI", 11F, FontStyle.Regular);
        g.DrawString("BẮT ĐẦU HÀNH TRÌNH", titleFont, whiteBrush, 44, Height - 220);
        g.DrawString("Chọn điểm đến. Chúng tôi lo phần còn lại.", bodyFont, whiteBrush, 47, Height - 166);
        g.DrawString("AIRLINE TICKET BOOKING  ·  YOUR NEXT JOURNEY", bodyFont, whiteBrush, 47, Height - 66);
    }
}
