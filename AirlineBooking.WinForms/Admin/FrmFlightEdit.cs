using AirlineBooking.Core;
using System;

namespace AirlineBooking.WinForms.Admin;

public partial class FrmFlightEdit : Form
{
    // Kích thước ô ghế tính theo độ phóng to màn hình (100%, 125%, 150%...)
    private const int SeatsPerHalf = 3;
    private readonly int _seatWidth;
    private readonly int _seatHeight;
    private readonly int _seatGap;
    private readonly int _aisleWidth;
    private readonly int _rowNumberWidth;
    private readonly int _aisleStart;
    private readonly int _rightBlockStart;
    private readonly int _rowWidth;

    private readonly Flight _flight;
    private readonly List<Panel> _seatRows = new();

    public FrmFlightEdit(Flight flight)
    {
        InitializeComponent();
        float scale = DeviceDpi / 96f;
        _seatWidth = (int)(38 * scale);
        _seatHeight = (int)(26 * scale);
        _seatGap = (int)(5 * scale);
        _aisleWidth = (int)(18 * scale);
        _rowNumberWidth = (int)(26 * scale);
        _aisleStart = _rowNumberWidth + _seatGap + SeatsPerHalf * (_seatWidth + _seatGap) - _seatGap;
        _rightBlockStart = _aisleStart + _aisleWidth;
        _rowWidth = _rightBlockStart + SeatsPerHalf * (_seatWidth + _seatGap) - _seatGap;

        _flight = flight;
        AdminTheme.StylePrimaryButton(btnSave);
        AdminTheme.StyleSecondaryButton(btnClose);

        Text = $"Chỉnh sửa chuyến bay — {flight.GetFlightNumber()}";
        lblCodeValue.Text = flight.GetFlightNumber();
        lblRouteValue.Text = AdminDisplay.Route(flight);
        lblTotalValue.Text = flight.GetSeats().Count.ToString();
        lblBookedValue.Text = AdminDisplay.BookedCount(flight).ToString();
        dtpDeparture.Value = flight.GetDeparture();
        numBasePrice.Value = Math.Clamp(SeatLayout.GetBasePrice(flight), numBasePrice.Minimum, numBasePrice.Maximum);

        if (AdminDisplay.HasDeparted(flight))
        {
            dtpDeparture.Enabled = false;
            numBasePrice.Enabled = false;
            btnSave.Enabled = false;
            lblPriceNote.Text = "Chuyến đã bay, không thể chỉnh sửa.";
        }

        BuildSeatMap();
    }

    private void BuildSeatMap()
    {
        flpSeatMap.SuspendLayout();
        foreach (var (rowNumber, seats) in SeatLayout.GetRows(_flight))
        {
            AddSeatRow(rowNumber, seats);
        }
        flpSeatMap.ResumeLayout();
        CenterSeatRows();
    }

    private void AddSeatRow(int rowNumber, IReadOnlyList<Seat> seats)
    {
        var row = new Panel { Size = new Size(_rowWidth, _seatHeight), Margin = new Padding(0, 0, 0, _seatGap) };
        row.Controls.Add(new Label
        {
            Text = rowNumber.ToString(),
            Font = AdminTheme.SmallFont,
            ForeColor = AdminTheme.RowNumber,
            TextAlign = ContentAlignment.MiddleRight,
            Bounds = new Rectangle(0, 0, _rowNumberWidth, _seatHeight),
        });

        int half = (seats.Count + 1) / 2;
        for (int i = 0; i < seats.Count; i++)
        {
            Seat seat = seats[i];
            int x = i < half
                ? _aisleStart - (half - i) * (_seatWidth + _seatGap) + _seatGap   // nửa trái, sát lối đi
                : _rightBlockStart + (i - half) * (_seatWidth + _seatGap);        // nửa phải
            string seatClass = seat.GetSeatClass().GetName();
            bool premium = seatClass == "First" || seatClass == "Business";
            var label = !seat.IsAvailable()
                ? new SeatLabel(seat.GetCode(), AdminTheme.BookedSeat, AdminTheme.BookedSeat, Color.White)
                : premium
                    ? new SeatLabel(seat.GetCode(), AdminTheme.BusinessSeat, AdminTheme.BusinessSeatBorder, AdminTheme.Text)
                    : new SeatLabel(seat.GetCode(), Color.White, AdminTheme.BookedSeat, AdminTheme.Text);
            label.Bounds = new Rectangle(x, 0, _seatWidth, _seatHeight);
            row.Controls.Add(label);
        }

        _seatRows.Add(row);
        flpSeatMap.Controls.Add(row);
    }

    private void CenterSeatRows()
    {
        int left = Math.Max(0, (flpSeatMap.ClientSize.Width - _rowWidth) / 2);
        foreach (var row in _seatRows)
        {
            row.Margin = new Padding(left, 0, 0, _seatGap);
        }
    }

    private void flpSeatMap_Resize(object? sender, EventArgs e) => CenterSeatRows();

    private void btnSave_Click(object? sender, EventArgs e)
    {
        DateTime newDeparture = AdminTheme.TrimSeconds(dtpDeparture.Value);
        try
        {
            if (newDeparture != _flight.GetDeparture())
            {
                _flight.Reschedule(newDeparture, AppServices.Now);
            }
            if (numBasePrice.Value != SeatLayout.GetBasePrice(_flight))
            {
                foreach (string className in AdminDisplay.SeatClassNames(_flight))
                {
                    _flight.UpdatePrice(className, numBasePrice.Value);   // mỗi hạng tự nhân hệ số khi bán
                }
            }
        }
        catch (ArgumentException ex)
        {
            AdminTheme.Warn(this, ex.Message);
            return;
        }
        catch (InvalidOperationException ex)
        {
            AdminTheme.Warn(this, ex.Message);
            return;
        }

        DialogResult = DialogResult.OK;
    }
}
