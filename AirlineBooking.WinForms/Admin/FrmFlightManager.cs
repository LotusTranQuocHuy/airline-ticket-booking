using AirlineBooking.Core;

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

        cboFrom.Items.AddRange(SampleData.Airports);
        cboTo.Items.AddRange(SampleData.Airports);
        ResetInputs();
        LoadFlights();
    }

    private Flight? SelectedFlight => dgvFlights.CurrentRow?.Tag as Flight;

    private void ResetInputs()
    {
        txtFlightCode.Clear();
        cboFrom.SelectedIndex = 0;
        cboTo.SelectedIndex = 1;
        dtpDeparture.Value = DateTime.Today.AddDays(7).AddHours(9);
        numBasePrice.Value = 1_450_000;
        numFirstRows.Value = 1;
        numBusinessRows.Value = 2;
        numPremiumRows.Value = 2;
        numEconomyRows.Value = 20;
        UpdateSeatPreview();
    }

    private void UpdateSeatPreview()
    {
        int wide = (int)(numFirstRows.Value + numBusinessRows.Value + numPremiumRows.Value) * SeatLayout.WideSeatsPerRow;
        int economy = (int)numEconomyRows.Value * SeatLayout.EconomySeatsPerRow;
        lblSeatPreview.Text = $"Sẽ sinh {wide + economy} ghế: {wide} hạng cao + {economy} Economy";
    }

    private void LoadFlights(string? selectCode = null)
    {
        selectCode ??= SelectedFlight?.GetFlightNumber();
        string keyword = txtSearch.Text.Trim();

        dgvFlights.Rows.Clear();
        foreach (Flight flight in AppServices.Catalog.GetAll())
        {
            if (!Matches(flight, keyword)) continue;

            int index = dgvFlights.Rows.Add(
                flight.GetFlightNumber(),
                flight.GetOrigin(),
                flight.GetDestination(),
                AdminTheme.DateTimeText(flight.GetDeparture()),
                AdminTheme.Number(SeatLayout.GetBasePrice(flight)),
                $"{AdminDisplay.BookedCount(flight)}/{flight.GetSeats().Count}",
                AdminDisplay.StatusText(flight));
            var row = dgvFlights.Rows[index];
            row.Tag = flight;
            if (flight.GetFlightNumber() == selectCode)
            {
                dgvFlights.CurrentCell = row.Cells[0];
            }
        }
        UpdateStatus();
    }

    private static bool Matches(Flight flight, string keyword) =>
        keyword.Length == 0 ||
    flight.GetFlightNumber().Contains(keyword, StringComparison.CurrentCultureIgnoreCase) ||
    flight.GetOrigin().Contains(keyword, StringComparison.CurrentCultureIgnoreCase) ||
    flight.GetDestination().Contains(keyword, StringComparison.CurrentCultureIgnoreCase);

    private void UpdateStatus() =>
        lblStatus.Text = $"{dgvFlights.Rows.Count} chuyến bay · Đang chọn: {SelectedFlight?.GetFlightNumber() ?? "—"}";

    private void btnAddFlight_Click(object? sender, EventArgs e)
    {
        string code = txtFlightCode.Text.Trim().ToUpperInvariant();
        string from = (string)cboFrom.SelectedItem!;
        string to = (string)cboTo.SelectedItem!;
        DateTime departure = AdminTheme.TrimSeconds(dtpDeparture.Value);
        int businessRows = (int)numBusinessRows.Value;
        int economyRows = (int)numEconomyRows.Value;
        int firstRows = (int)numFirstRows.Value;
        int premiumRows = (int)numPremiumRows.Value;

        string? error =
            code.Length == 0 ? "Vui lòng nhập số hiệu chuyến." :
            from == to ? "Điểm đi và điểm đến phải khác nhau." :
            departure <= AppServices.Now ? "Giờ bay phải ở thời điểm sau hiện tại." :
            firstRows + businessRows + premiumRows + economyRows == 0 ? "Chuyến bay phải có ít nhất một hàng ghế." :
            null;
        if (error is not null)
        {
            AdminTheme.Warn(this, error);
            return;
        }

        var flight = new Flight(code, from, to, departure);
        SeatLayout.AddSeats(flight, firstRows, businessRows, premiumRows, economyRows, numBasePrice.Value);
        try
        {
            AppServices.Catalog.Add(flight);
        }
        catch (ArgumentException ex)       // ví dụ: trùng số hiệu chuyến
        {
            AdminTheme.Warn(this, ex);
            return;
        }

        txtSearch.Clear();
        LoadFlights(code);
        MessageBox.Show(this, $"Đã thêm chuyến {code} với {flight.GetSeats().Count} ghế.", "Thêm chuyến bay",
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
        if (SelectedFlight is not Flight flight)
        {
            AdminTheme.Warn(this, "Hãy chọn một chuyến bay trong danh sách.");
            return;
        }

        using var form = new FrmFlightEdit(flight);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            LoadFlights(flight.GetFlightNumber());
        }
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (SelectedFlight is not Flight flight)
        {
            AdminTheme.Warn(this, "Hãy chọn một chuyến bay trong danh sách.");
            return;
        }

        var answer = MessageBox.Show(this, $"Xóa chuyến bay {flight.GetFlightNumber()} ({AdminDisplay.Route(flight)})?", "Xóa chuyến bay",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (answer == DialogResult.Yes)
        {
            try
            {
                AppServices.Catalog.Remove(flight);
                LoadFlights();
            }
            catch (InvalidOperationException ex)   // chuyến đã có vé đặt
            {
                AdminTheme.Warn(this, ex);
            }
            LoadFlights();
        }
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        Close();
    }
}
