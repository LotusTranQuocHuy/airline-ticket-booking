using System.ComponentModel;

namespace AirlineBooking.WinForms.Admin;

/// <summary>
/// Nút lớn trên màn hình chính Admin: ô số thứ tự, tiêu đề và mô tả; đổi viền xanh khi rê chuột.
/// </summary>
public class MenuCard : Control
{
    private static readonly Font BadgeFont = new("Segoe UI", 11.25F, FontStyle.Bold);
    private static readonly Font TitleFont = new("Segoe UI Semibold", 13.5F);

    private string _badge = "1";
    private string _title = "Tiêu đề";
    private string _description = "Mô tả";
    private bool _hover;

    public MenuCard()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                 ControlStyles.Selectable, true);
        BackColor = Color.White;
        Cursor = Cursors.Hand;
        TabStop = true;
        Size = new Size(260, 300);
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Badge
    {
        get => _badge;
        set { _badge = value; Invalidate(); }
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Title
    {
        get => _title;
        set { _title = value; AccessibleName = value; Invalidate(); }
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Description
    {
        get => _description;
        set { _description = value; Invalidate(); }
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        _hover = true;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _hover = false;
        Invalidate();
    }

    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        Invalidate();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (e.KeyCode is Keys.Enter or Keys.Space)
        {
            OnClick(EventArgs.Empty);
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        bool highlighted = _hover || Focused;
        g.Clear(highlighted ? AdminTheme.HoverBack : BackColor);
        using (var border = new Pen(highlighted ? AdminTheme.Primary : AdminTheme.PanelBorder))
        {
            g.DrawRectangle(border, 0, 0, Width - 1, Height - 1);
        }

        const int pad = 24;
        var badgeRect = new Rectangle(pad, pad, 40, 40);
        using (var badgeBack = new SolidBrush(AdminTheme.PrimaryTint))
        {
            g.FillRectangle(badgeBack, badgeRect);
        }
        TextRenderer.DrawText(g, _badge, BadgeFont, badgeRect, AdminTheme.Primary,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

        int textWidth = Math.Max(1, Width - pad * 2);
        var wrap = TextFormatFlags.WordBreak | TextFormatFlags.NoPadding;
        Size descSize = TextRenderer.MeasureText(g, _description, AdminTheme.BodyFont, new Size(textWidth, 0), wrap);
        Size titleSize = TextRenderer.MeasureText(g, _title, TitleFont, new Size(textWidth, 0), wrap);
        int descTop = Height - pad - descSize.Height;
        int titleTop = descTop - 6 - titleSize.Height;
        TextRenderer.DrawText(g, _title, TitleFont,
            new Rectangle(pad, titleTop, textWidth, titleSize.Height), AdminTheme.Text, wrap);
        TextRenderer.DrawText(g, _description, AdminTheme.BodyFont,
            new Rectangle(pad, descTop, textWidth, descSize.Height), AdminTheme.TextSecondary, wrap);
    }
}
