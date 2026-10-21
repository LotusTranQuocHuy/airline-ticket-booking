using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

internal sealed class FrmBookingConfirm : Form
{
    private readonly Passenger _passenger;
    private readonly DemoFlight _flight;
    private readonly string _seatCode;
    private readonly Button _confirmButton;

    public Booking? ConfirmedBooking { get; private set; }

    public FrmBookingConfirm(
        Passenger passenger,
        DemoFlight flight,
        Seat seat)
    {
        _passenger = passenger;
        _flight = flight;
        _seatCode = seat.GetCode();
        var seatClass = seat.GetSeatClass().GetName();
        var amount = seat.GetSeatClass().CalculatePrice(seat.GetPrice());
        UiKit.ConfigureForm(this, "Xác nhận đặt vé | AirlineTicket");
        var header = UiKit.Header(
            "HOÀN TẤT",
            "Xác nhận thông tin",
            "Kiểm tra lại hành trình và ghế trước khi tiếp tục.");

        var card = UiKit.WhiteCard();
        card.Dock = DockStyle.Fill;
        card.Margin = new Padding(42);
        card.Padding = new Padding(32);
        card.Controls.Add(UiKit.At("CHI TIẾT CHUYẾN BAY", 32, 28, 9, true, UiKit.Blue));
        card.Controls.Add(UiKit.At($"{flight.Origin}  →  {flight.Destination}", 32, 64, 20, true));
        card.Controls.Add(UiKit.At($"{flight.Number}  ·  {flight.Departure:dddd, dd MMM yyyy · HH:mm}", 34, 106, 10, false, UiKit.Muted));
        card.Controls.Add(UiKit.At("Hành khách", 34, 166, 9, false, UiKit.Muted));
        card.Controls.Add(UiKit.At(passenger.GetFullName(), 34, 188, 11, true));
        card.Controls.Add(UiKit.At("Ghế", 34, 232, 9, false, UiKit.Muted));
        card.Controls.Add(UiKit.At($"{_seatCode}  ·  {seatClass}", 34, 254, 11, true));
        card.Controls.Add(UiKit.At("Tổng tiền", 34, 298, 9, false, UiKit.Muted));
        card.Controls.Add(UiKit.At(UiKit.Money(amount), 34, 322, 20, true, UiKit.Blue));
        card.Controls.Add(UiKit.At("Giá được tính theo hạng ghế của chuyến bay.", 34, 374, 9, false, UiKit.Muted));

        var cancelButton = UiKit.Button("Quay lại", height: 44);
        cancelButton.Size = new Size(150, 44);
        cancelButton.Location = new Point(34, 425);
        cancelButton.Click += (_, _) => Close();
        card.Controls.Add(cancelButton);

        _confirmButton = UiKit.Button("Đặt vé", primary: true, height: 44);
        _confirmButton.Size = new Size(190, 44);
        _confirmButton.Location = new Point(198, 425);
        _confirmButton.Click += (_, _) => ConfirmBooking();
        card.Controls.Add(_confirmButton);
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Margin = Padding.Empty,
            Padding = Padding.Empty
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 116F));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        root.Controls.Add(header, 0, 0);
        root.Controls.Add(card, 0, 1);
        Controls.Add(root);
    }

    private void ConfirmBooking()
    {
        _confirmButton.Enabled = false;
        try
        {
            ConfirmedBooking = DemoData.Book(_passenger, _flight, _seatCode);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex) when (ex is SeatUnavailableException or FlightNotFoundException or InvalidBookingStateException)
        {
            MessageBox.Show(this, ex.Message, "Không thể đặt vé",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
