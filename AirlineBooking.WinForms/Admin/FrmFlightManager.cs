namespace AirlineBooking.WinForms.Admin;

public partial class FrmFlightManager : Form
{
    public FrmFlightManager()
    {
        InitializeComponent();
        AdminTheme.StylePrimaryButton(btnAddFlight);
        AdminTheme.StyleSecondaryButton(btnReset);
        AdminTheme.StyleSecondaryButton(btnEdit);
        AdminTheme.StyleDangerTextButton(btnDelete);
        AdminTheme.StyleSecondaryButton(btnBack);
        AdminTheme.StyleGrid(dgvFlights);
        AdminTheme.AlignRight(colBasePrice, colBooked);

        cboFrom.Items.AddRange(AdminDemoData.Airports);
        cboTo.Items.AddRange(AdminDemoData.Airports);
        ResetInputs();
        LoadFlights();
    }

    private FlightRow? SelectedFlight => dgvFlights.CurrentRow?.Tag as FlightRow;

    private void ResetInputs()
    {
        txtFlightCode.Clear();
        cboFrom.SelectedIndex = 0;
        cboTo.SelectedIndex = 1;
        dtpDeparture.Value = DateTime.Today.AddDays(7).AddHours(9);
        numBasePrice.Value = 1_450_000;
        numBusinessRows.Value = 3;
        numEconomyRows.Value = 20;
        UpdateSeatPreview();
    }

    private void UpdateSeatPreview()
    {
        int business = (int)numBusinessRows.Value * FlightRow.BusinessSeatsPerRow;
        int economy = (int)numEconomyRows.Value * FlightRow.EconomySeatsPerRow;
        lblSeatPreview.Text = $"Hệ thống sẽ sinh {business + economy} ghế ({business} Business + {economy} Economy)";
    }

    private void LoadFlights(string? selectCode = null)
    {
        selectCode ??= SelectedFlight?.Code;
        string keyword = txtSearch.Text.Trim();

        dgvFlights.Rows.Clear();
        foreach (var flight in AdminDemoData.Flights.Where(f => Matches(f, keyword)))
        {
            int index = dgvFlights.Rows.Add(
                flight.Code,
                flight.From,
                flight.To,
                AdminTheme.DateTimeText(flight.Departure),
                AdminTheme.Number(flight.BasePrice),
                $"{AdminDemoData.CountBooked(flight.Code)}/{flight.TotalSeats}",
                flight.StatusText);
            var row = dgvFlights.Rows[index];
            row.Tag = flight;
            if (flight.Code == selectCode)
            {
                dgvFlights.CurrentCell = row.Cells[0];
            }
        }
        UpdateStatus();
    }

    private static bool Matches(FlightRow flight, string keyword) =>
        keyword.Length == 0 ||
        flight.Code.Contains(keyword, StringComparison.CurrentCultureIgnoreCase) ||
        flight.From.Contains(keyword, StringComparison.CurrentCultureIgnoreCase) ||
        flight.To.Contains(keyword, StringComparison.CurrentCultureIgnoreCase);

    private void UpdateStatus() =>
        lblStatus.Text = $"{dgvFlights.Rows.Count} chuyến bay · Đang chọn: {SelectedFlight?.Code ?? "—"}";

    private void btnAddFlight_Click(object? sender, EventArgs e)
    {
        string code = txtFlightCode.Text.Trim().ToUpperInvariant();
        string from = (string)cboFrom.SelectedItem!;
        string to = (string)cboTo.SelectedItem!;
        DateTime departure = AdminTheme.TrimSeconds(dtpDeparture.Value);
        int businessRows = (int)numBusinessRows.Value;
        int economyRows = (int)numEconomyRows.Value;

        string? error =
            code.Length == 0 ? "Vui lòng nhập số hiệu chuyến." :
            AdminDemoData.FindFlight(code) is not null ? $"Số hiệu {code} đã tồn tại." :
            from == to ? "Điểm đi và điểm đến phải khác nhau." :
            departure <= DateTime.Now ? "Giờ bay phải ở thời điểm sau hiện tại." :
            businessRows + economyRows == 0 ? "Chuyến bay phải có ít nhất một hàng ghế." :
            null;
        if (error is not null)
        {
            AdminTheme.Warn(this, error);
            return;
        }

        // Tuần 3: tạo Flight trong Core, gọi AddSeat cho từng ghế rồi FlightCatalog.Add(...)
        var flight = new FlightRow(code, from, to, departure, numBasePrice.Value, businessRows, economyRows);
        AdminDemoData.AddFlight(flight);
        txtSearch.Clear();
        LoadFlights(code);
        MessageBox.Show(this, $"Đã thêm chuyến {code} với {flight.TotalSeats} ghế.", "Thêm chuyến bay",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        ResetInputs();
    }

    private void btnReset_Click(object? sender, EventArgs e) => ResetInputs();

    private void SeatRows_ValueChanged(object? sender, EventArgs e) => UpdateSeatPreview();

    private void txtSearch_TextChanged(object? sender, EventArgs e) => LoadFlights();

    private void dgvFlights_SelectionChanged(object? sender, EventArgs e) => UpdateStatus();

    private void dgvFlights_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            EditSelectedFlight();
        }
    }

    private void btnEdit_Click(object? sender, EventArgs e) => EditSelectedFlight();

    private void EditSelectedFlight()
    {
        if (SelectedFlight is not FlightRow flight)
        {
            AdminTheme.Warn(this, "Hãy chọn một chuyến bay trong danh sách.");
            return;
        }

        using var form = new FrmFlightEdit(flight);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            LoadFlights(flight.Code);
        }
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (SelectedFlight is not FlightRow flight)
        {
            AdminTheme.Warn(this, "Hãy chọn một chuyến bay trong danh sách.");
            return;
        }
        if (AdminDemoData.HasBookings(flight.Code))
        {
            AdminTheme.Warn(this, $"Chuyến {flight.Code} đã có vé đặt nên không thể xóa.");
            return;
        }

        var answer = MessageBox.Show(this, $"Xóa chuyến bay {flight.Code} ({flight.Route})?", "Xóa chuyến bay",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (answer == DialogResult.Yes)
        {
            AdminDemoData.RemoveFlight(flight);
            LoadFlights();
        }
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        Close();
    }
}
