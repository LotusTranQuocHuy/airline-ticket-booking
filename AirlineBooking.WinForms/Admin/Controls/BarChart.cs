using System.ComponentModel;
using System.Globalization;

namespace AirlineBooking.WinForms.Admin;

/// <summary>
/// Biểu đồ cột tự vẽ (WinForms trên .NET 8 không còn sẵn control Chart).
/// Giá trị hiển thị theo đơn vị triệu đồng.
/// </summary>
public class BarChart : Control
{
    private static readonly CultureInfo Vietnamese = new("vi-VN");
    private readonly List<(string Label, decimal Value)> _items = new();
    private string _caption = "";

    public BarChart()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        BackColor = Color.White;
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Caption
    {
        get => _caption;
        set { _caption = value; Invalidate(); }
    }

    public void SetData(IEnumerable<(string Label, decimal Value)> items)
    {
        _items.Clear();
        _items.AddRange(items);
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.Clear(BackColor);
        using (var border = new Pen(AdminTheme.InputBorder))
        {
            g.DrawRectangle(border, 0, 0, Width - 1, Height - 1);
        }

        const int pad = 12;
        var smallFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPadding | TextFormatFlags.EndEllipsis;
        TextRenderer.DrawText(g, _caption, AdminTheme.BodyFont, new Point(pad, pad), AdminTheme.TextSecondary);

        int labelHeight = AdminTheme.SmallFont.Height + 4;
        int top = pad + AdminTheme.BodyFont.Height + 8;
        int baseline = Height - pad - labelHeight;
        int left = pad;
        int width = Width - pad * 2;
        if (baseline - top < 20 || width < 20)
        {
            return;
        }

        using (var axis = new Pen(AdminTheme.InputBorder))
        {
            g.DrawLine(axis, left, baseline, left + width, baseline);
        }

        if (_items.Count == 0)
        {
            TextRenderer.DrawText(g, "Không có dữ liệu trong khoảng ngày đã chọn", AdminTheme.BodyFont,
                new Rectangle(left, top, width, baseline - top), AdminTheme.TextMuted,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            return;
        }

        const int gap = 10;
        decimal max = Math.Max(1m, _items.Max(item => item.Value));
        int barWidth = Math.Max(4, (width - gap * (_items.Count - 1)) / _items.Count);
        int maxBarHeight = baseline - top - labelHeight;

        using var barBrush = new SolidBrush(AdminTheme.Primary);
        for (int i = 0; i < _items.Count; i++)
        {
            var (label, value) = _items[i];
            int x = left + i * (barWidth + gap);
            int barHeight = (int)(Math.Max(0m, value) / max * maxBarHeight);
            g.FillRectangle(barBrush, x, baseline - barHeight, barWidth, barHeight);

            string valueText = (value / 1_000_000m).ToString("0.0", Vietnamese);
            TextRenderer.DrawText(g, valueText, AdminTheme.SmallFont,
                new Rectangle(x - gap / 2, baseline - barHeight - labelHeight, barWidth + gap, labelHeight),
                AdminTheme.TextSecondary, smallFlags | TextFormatFlags.Bottom);
            TextRenderer.DrawText(g, label, AdminTheme.SmallFont,
                new Rectangle(x - gap / 2, baseline + 4, barWidth + gap, labelHeight),
                AdminTheme.TextSecondary, smallFlags);
        }
    }
}
