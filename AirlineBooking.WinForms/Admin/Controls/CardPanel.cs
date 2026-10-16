using System.ComponentModel;

namespace AirlineBooking.WinForms.Admin;

/// <summary>
/// Panel nền trắng có viền mảnh 1px (thay cho BorderStyle.FixedSingle vốn có viền đậm).
/// </summary>
public class CardPanel : Panel
{
    private Color _borderColor = AdminTheme.PanelBorder;

    public CardPanel()
    {
        BackColor = Color.White;
        DoubleBuffered = true;
        ResizeRedraw = true;
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            _borderColor = value;
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using var pen = new Pen(_borderColor);
        e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
    }
}
