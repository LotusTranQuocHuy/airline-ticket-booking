using System.Globalization;

namespace AirlineBooking.WinForms.Admin;

/// <summary>
/// Màu, font và định dạng dùng chung cho các form Admin (theo bản thiết kế design_handoff_winforms_admin).
/// </summary>
public static class AdminTheme
{
    public static readonly Color Primary = UiKit.Blue;
    public static readonly Color PrimaryHover = Color.FromArgb(26, 94, 190);
    public static readonly Color PrimaryPressed = UiKit.Navy;
    public static readonly Color PrimaryTint = UiKit.PaleBlue;
    public static readonly Color Selection = UiKit.PaleBlue;

    public static readonly Color Text = UiKit.Navy;
    public static readonly Color TextSecondary = UiKit.Muted;
    public static readonly Color TextMuted = UiKit.Muted;
    public static readonly Color RowNumber = UiKit.Muted;

    public static readonly Color FormBack = UiKit.Background;
    public static readonly Color StripBack = Color.White;
    public static readonly Color AlternateRow = Color.FromArgb(248, 250, 253);
    public static readonly Color Separator = UiKit.Border;
    public static readonly Color PanelBorder = UiKit.Border;
    public static readonly Color InputBorder = UiKit.Border;
    public static readonly Color HoverBack = UiKit.PaleBlue;
    public static readonly Color DisabledBack = Color.FromArgb(229, 229, 229);

    public static readonly Color Danger = Color.FromArgb(0xB3, 0x26, 0x1E);
    public static readonly Color DangerHover = Color.FromArgb(0xC4, 0x39, 0x2F);
    public static readonly Color DangerTint = Color.FromArgb(0xFD, 0xEC, 0xEA);
    public static readonly Color Success = Color.FromArgb(0x0F, 0x7B, 0x0F);

    public static readonly Color BusinessSeat = Color.FromArgb(0xFF, 0xF6, 0xDC);
    public static readonly Color BusinessSeatBorder = Color.FromArgb(0xC7, 0x9A, 0x2E);
    public static readonly Color BookedSeat = Color.FromArgb(0x9A, 0x9A, 0x9A);

    public static readonly Font BodyFont = new("Segoe UI", 9.75F);
    public static readonly Font BodyBoldFont = new("Segoe UI Semibold", 9.75F);
    public static readonly Font SmallFont = new("Segoe UI", 8F);

    private static readonly CultureInfo Vietnamese = new("vi-VN");

    public static string Money(decimal value) => Number(value) + " ₫";

    public static string Number(decimal value) => value.ToString("N0", Vietnamese);

    public static string DateTimeText(DateTime value) =>
        value.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

    public static string DateText(DateOnly value) =>
        value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

    public static DateTime TrimSeconds(DateTime value) =>
        new(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);

    public static void StylePrimaryButton(Button button) =>
        StyleFilledButton(button, Primary, PrimaryHover, PrimaryPressed);

    public static void StyleDangerButton(Button button) =>
        StyleFilledButton(button, Danger, DangerHover, DangerHover);

    public static void StyleSecondaryButton(Button button)
    {
        StyleOutlinedButton(button);
        button.ForeColor = Text;
        button.FlatAppearance.MouseOverBackColor = AlternateRow;
    }

    public static void StyleDangerTextButton(Button button)
    {
        StyleOutlinedButton(button);
        button.ForeColor = Danger;
        button.FlatAppearance.MouseOverBackColor = DangerTint;
    }

    public static void StyleGrid(DataGridView grid)
    {
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.RowHeadersVisible = false;
        grid.EnableHeadersVisualStyles = false;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;
        grid.GridColor = Separator;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.ColumnHeadersHeight = 30;
        grid.ColumnHeadersDefaultCellStyle.BackColor = FormBack;
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = FormBack;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Text;
        grid.ColumnHeadersDefaultCellStyle.Font = BodyBoldFont;
        grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(4, 0, 4, 0);

        grid.RowTemplate.Height = 28;
        grid.DefaultCellStyle.ForeColor = Text;
        grid.DefaultCellStyle.SelectionBackColor = Selection;
        grid.DefaultCellStyle.SelectionForeColor = Text;
        grid.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
        grid.AlternatingRowsDefaultCellStyle.BackColor = AlternateRow;

        foreach (DataGridViewColumn column in grid.Columns)
        {
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }

    public static void AlignRight(params DataGridViewColumn[] columns)
    {
        foreach (var column in columns)
        {
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }

    public static void Warn(IWin32Window owner, string message) =>
        MessageBox.Show(owner, message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    private static void StyleFilledButton(Button button, Color back, Color hover, Color pressed)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseOverBackColor = hover;
        button.FlatAppearance.MouseDownBackColor = pressed;
        button.UseVisualStyleBackColor = false;
        button.ForeColor = Color.White;
        button.Cursor = Cursors.Hand;

        void ApplyEnabledLook() => button.BackColor = button.Enabled ? back : DisabledBack;
        ApplyEnabledLook();
        button.EnabledChanged += (_, _) => ApplyEnabledLook();
    }

    private static void StyleOutlinedButton(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.BorderColor = InputBorder;
        button.FlatAppearance.MouseDownBackColor = Separator;
        button.UseVisualStyleBackColor = false;
        button.BackColor = Color.White;
        button.Cursor = Cursors.Hand;
    }
}
