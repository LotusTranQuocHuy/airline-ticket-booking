using System.Globalization;
using System.Text;
using AirlineBooking.Core;

namespace AirlineBooking.WinForms.Admin;

public partial class FrmRevenueReport : Form
{
    private IReadOnlyList<RevenueLine> _byFlight = Array.Empty<RevenueLine>();
    private DateOnly _from;
    private DateOnly _to;

    public FrmRevenueReport()
    {
        InitializeComponent();
        AdminTheme.StylePrimaryButton(btnView);
        AdminTheme.StyleSecondaryButton(btnExportExcel);
        AdminTheme.StyleSecondaryButton(btnBack);
        AdminTheme.StyleGrid(dgvByFlight);
        AdminTheme.StyleGrid(dgvByClass);
        // Đủ chỗ cho 4 hạng ghế mà không cần thanh cuộn (kể cả khi màn hình phóng to chữ)
        dgvByClass.Height = dgvByClass.ColumnHeadersHeight + 4 * dgvByClass.RowTemplate.Height + 2;
        AdminTheme.AlignRight(colFlightTickets, colFlightPaid, colFlightRefunded, colFlightNet,
                              colClassTickets, colClassNet);

        dtpTo.Value = DateTime.Today;
        dtpFrom.Value = DateTime.Today.AddDays(-30);
        LoadReport();
        Shown += (_, _) => ClearGridSelection();
    }

    // Báo cáo chỉ để xem, không cần tô dòng đang chọn.
    private void ClearGridSelection()
    {
        dgvByFlight.ClearSelection();
        dgvByClass.ClearSelection();
    }

    private void LoadReport()
    {
        var from = DateOnly.FromDateTime(dtpFrom.Value);
        var to = DateOnly.FromDateTime(dtpTo.Value);
        if (from > to)
        {
            AdminTheme.Warn(this, "\"Từ ngày\" phải trước hoặc bằng \"Đến ngày\".");
            return;
        }


        decimal paid = AppServices.Revenue.GetTotalPaid(from, to);
        decimal refunded = AppServices.Revenue.GetTotalRefunded(from, to);
        lblPaidValue.Text = AdminTheme.Money(paid);
        lblRefundedValue.Text = AdminTheme.Money(refunded);
        lblNetValue.Text = AdminTheme.Money(paid - refunded);

        _byFlight = AppServices.Revenue.GetRevenueByFlight(from, to);
        dgvByFlight.Rows.Clear();
        foreach (var line in _byFlight)
        {
            dgvByFlight.Rows.Add(
                line.GetLabel(),
                AdminDisplay.RouteOf(line.GetLabel()),
                line.GetTicketCount(),
                AdminTheme.Number(line.GetTotalPaid()),
                AdminTheme.Number(line.GetTotalRefunded()),
                AdminTheme.Number(line.GetNetRevenue()));
        }

        dgvByClass.Rows.Clear();
        foreach (var line in AppServices.Revenue.GetRevenueBySeatClass(from, to))
        {
            dgvByClass.Rows.Add(line.GetLabel(), line.GetTicketCount(), AdminTheme.Number(line.GetNetRevenue()));
        }

        chartByFlight.SetData(_byFlight.Select(line => (line.GetLabel(), line.GetNetRevenue())));
        ClearGridSelection();
        _from = from;
        _to = to;
    }

    private void btnView_Click(object? sender, EventArgs e) => LoadReport();

    private void btnExportExcel_Click(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "CSV (mở bằng Excel)|*.csv",
            FileName = $"doanh-thu_{_from:yyyyMMdd}_{_to:yyyyMMdd}.csv",
        };
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var csv = new StringBuilder();
        csv.AppendLine($"Báo cáo doanh thu từ {AdminTheme.DateText(_from)} đến {AdminTheme.DateText(_to)}");
        csv.AppendLine("Chuyến,Hành trình,Số vé,Đã thu,Đã hoàn,Doanh thu thực");
        foreach (var line in _byFlight)
        {
            csv.AppendLine(string.Join(",",
                line.GetLabel(),
                AdminDisplay.RouteOf(line.GetLabel()),
                line.GetTicketCount(),
                line.GetTotalPaid().ToString(CultureInfo.InvariantCulture),
                line.GetTotalRefunded().ToString(CultureInfo.InvariantCulture),
                line.GetNetRevenue().ToString(CultureInfo.InvariantCulture)));
        }

        File.WriteAllText(dialog.FileName, csv.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
        MessageBox.Show(this, $"Đã lưu báo cáo vào {dialog.FileName}", "Xuất Excel",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        Close();
    }
}
