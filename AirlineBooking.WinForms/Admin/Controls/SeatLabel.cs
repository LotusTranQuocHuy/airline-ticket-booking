namespace AirlineBooking.WinForms.Admin;

/// <summary>
/// Một ô ghế trong sơ đồ ghế chỉ xem: nền và viền theo trạng thái / hạng ghế.
/// </summary>
public class SeatLabel : Label
{
    public SeatLabel(string code, Color back, Color border, Color fore)
    {
        Text = code;
        BackColor = back;
        ForeColor = fore;
        BorderColor = border;
        Font = AdminTheme.SmallFont;
        TextAlign = ContentAlignment.MiddleCenter;
        Size = new Size(32, 26);
        Margin = Padding.Empty;
    }

    public Color BorderColor { get; }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using var pen = new Pen(BorderColor);
        e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
    }
}
