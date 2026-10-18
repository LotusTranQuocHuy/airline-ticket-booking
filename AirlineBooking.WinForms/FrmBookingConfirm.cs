using AirlineBooking.Core;

namespace AirlineBooking.WinForms;

internal sealed class FrmBookingConfirm : Form
{
    private readonly Passenger _passenger;
    private readonly DemoFlight _flight;
    private readonly string _seatCode;
    private readonly string _seatClass;
    private readonly decimal _amount;

    public FrmBookingConfirm(
        Passenger passenger,
        DemoFlight flight,
        string seatCode,
        string seatClass,
        decimal amount)
    {
        _passenger = passenger;
        _flight = flight;
        _seatCode = seatCode;
        _seatClass = seatClass;
        _amount = amount;
        UiKit.ConfigureForm(this, "Xác nhận đặt vé | AirlineTicket");
        Controls.Add(UiKit.Header(
            "HOÀN TẤT",
            "Xác nhận thông tin",
            "Kiểm tra lại hành trình và ghế trước khi tiếp tục."));

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
        card.Controls.Add(UiKit.At($"{seatCode}  ·  {seatClass}", 34, 254, 11, true));
        card.Controls.Add(UiKit.At("Tổng tiền (mẫu)", 34, 298, 9, false, UiKit.Muted));
        card.Controls.Add(UiKit.At(UiKit.Money(amount), 34, 322, 20, true, UiKit.Blue));
        card.Controls.Add(UiKit.At("Đây là luồng giao diện demo; dịch vụ đặt vé sẽ được kết nối ở Tuần 3.", 34, 374, 9, false, UiKit.Muted));

        var cancelButton = UiKit.Button("Quay lại", height: 44);
        cancelButton.Size = new Size(150, 44);
        cancelButton.Location = new Point(34, 425);
        cancelButton.Click += (_, _) => Close();
        card.Controls.Add(cancelButton);

        var confirmButton = UiKit.Button("Đặt vé ", primary: true, height: 44);
        confirmButton.Size = new Size(190, 44);
        confirmButton.Location = new Point(198, 425);
        confirmButton.Click += (_, _) => ConfirmDemoBooking();
        card.Controls.Add(confirmButton);
        Controls.Add(card);
    }

    private void ConfirmDemoBooking()
    {
        try
        {
            var booking = DemoData.AddBooking(
                _passenger,
                _flight,
                _seatCode,
                _seatClass,
                _amount);
            MessageBox.Show(
                this,
                $"Đã thêm vé mẫu {booking.Id} vào mục Vé của tôi.",
                "Hoàn tất ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(this, ex.Message, "Không thể chọn ghế",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
