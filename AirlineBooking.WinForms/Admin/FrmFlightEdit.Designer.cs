namespace AirlineBooking.WinForms.Admin
{
    partial class FrmFlightEdit
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tlpMain = new TableLayoutPanel();
            pnlInfo = new CardPanel();
            lblInfoTitle = new Label();
            lblCodeCaption = new Label();
            lblCodeValue = new Label();
            lblRouteCaption = new Label();
            lblRouteValue = new Label();
            lblTotalCaption = new Label();
            lblTotalValue = new Label();
            lblBookedCaption = new Label();
            lblBookedValue = new Label();
            sepInfo = new Label();
            lblDeparture = new Label();
            dtpDeparture = new DateTimePicker();
            lblBasePrice = new Label();
            numBasePrice = new NumericUpDown();
            lblPriceNote = new Label();
            btnSave = new Button();
            btnClose = new Button();
            pnlSeats = new CardPanel();
            lblSeatTitle = new Label();
            lblLegendEmptyBox = new Label();
            lblLegendEmpty = new Label();
            lblLegendBookedBox = new Label();
            lblLegendBooked = new Label();
            lblLegendBusinessBox = new Label();
            lblLegendBusiness = new Label();
            flpSeatMap = new FlowLayoutPanel();
            tlpMain.SuspendLayout();
            pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numBasePrice).BeginInit();
            pnlSeats.SuspendLayout();
            SuspendLayout();
            //
            // tlpMain
            //
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 296F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(pnlInfo, 0, 0);
            tlpMain.Controls.Add(pnlSeats, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(16);
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(900, 640);
            tlpMain.TabIndex = 0;
            //
            // pnlInfo
            //
            pnlInfo.Controls.Add(lblInfoTitle);
            pnlInfo.Controls.Add(lblCodeCaption);
            pnlInfo.Controls.Add(lblCodeValue);
            pnlInfo.Controls.Add(lblRouteCaption);
            pnlInfo.Controls.Add(lblRouteValue);
            pnlInfo.Controls.Add(lblTotalCaption);
            pnlInfo.Controls.Add(lblTotalValue);
            pnlInfo.Controls.Add(lblBookedCaption);
            pnlInfo.Controls.Add(lblBookedValue);
            pnlInfo.Controls.Add(sepInfo);
            pnlInfo.Controls.Add(lblDeparture);
            pnlInfo.Controls.Add(dtpDeparture);
            pnlInfo.Controls.Add(lblBasePrice);
            pnlInfo.Controls.Add(numBasePrice);
            pnlInfo.Controls.Add(lblPriceNote);
            pnlInfo.Controls.Add(btnSave);
            pnlInfo.Controls.Add(btnClose);
            pnlInfo.Dock = DockStyle.Fill;
            pnlInfo.Location = new Point(16, 16);
            pnlInfo.Margin = new Padding(0, 0, 16, 0);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new Size(280, 608);
            pnlInfo.TabIndex = 0;
            //
            // lblInfoTitle
            //
            lblInfoTitle.AutoSize = true;
            lblInfoTitle.Font = new Font("Segoe UI Semibold", 11.25F);
            lblInfoTitle.Location = new Point(14, 14);
            lblInfoTitle.Name = "lblInfoTitle";
            lblInfoTitle.TabIndex = 0;
            lblInfoTitle.Text = "Thông tin chuyến";
            //
            // lblCodeCaption
            //
            lblCodeCaption.AutoSize = true;
            lblCodeCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblCodeCaption.Location = new Point(14, 50);
            lblCodeCaption.Name = "lblCodeCaption";
            lblCodeCaption.TabIndex = 1;
            lblCodeCaption.Text = "Số hiệu";
            //
            // lblCodeValue
            //
            lblCodeValue.AutoSize = true;
            lblCodeValue.Font = new Font("Segoe UI Semibold", 9.75F);
            lblCodeValue.Location = new Point(104, 50);
            lblCodeValue.Name = "lblCodeValue";
            lblCodeValue.TabIndex = 2;
            lblCodeValue.Text = "VN201";
            //
            // lblRouteCaption
            //
            lblRouteCaption.AutoSize = true;
            lblRouteCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblRouteCaption.Location = new Point(14, 76);
            lblRouteCaption.Name = "lblRouteCaption";
            lblRouteCaption.TabIndex = 3;
            lblRouteCaption.Text = "Hành trình";
            //
            // lblRouteValue
            //
            lblRouteValue.AutoSize = true;
            lblRouteValue.Location = new Point(104, 76);
            lblRouteValue.Name = "lblRouteValue";
            lblRouteValue.TabIndex = 4;
            lblRouteValue.Text = "HAN → SGN";
            //
            // lblTotalCaption
            //
            lblTotalCaption.AutoSize = true;
            lblTotalCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblTotalCaption.Location = new Point(14, 102);
            lblTotalCaption.Name = "lblTotalCaption";
            lblTotalCaption.TabIndex = 5;
            lblTotalCaption.Text = "Tổng ghế";
            //
            // lblTotalValue
            //
            lblTotalValue.AutoSize = true;
            lblTotalValue.Location = new Point(104, 102);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.TabIndex = 6;
            lblTotalValue.Text = "132";
            //
            // lblBookedCaption
            //
            lblBookedCaption.AutoSize = true;
            lblBookedCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblBookedCaption.Location = new Point(14, 128);
            lblBookedCaption.Name = "lblBookedCaption";
            lblBookedCaption.TabIndex = 7;
            lblBookedCaption.Text = "Đã đặt";
            //
            // lblBookedValue
            //
            lblBookedValue.AutoSize = true;
            lblBookedValue.Location = new Point(104, 128);
            lblBookedValue.Name = "lblBookedValue";
            lblBookedValue.TabIndex = 8;
            lblBookedValue.Text = "0";
            //
            // sepInfo
            //
            sepInfo.BackColor = Color.FromArgb(222, 229, 238);
            sepInfo.Location = new Point(16, 160);
            sepInfo.Name = "sepInfo";
            sepInfo.Size = new Size(248, 1);
            sepInfo.TabIndex = 9;
            //
            // lblDeparture
            //
            lblDeparture.AutoSize = true;
            lblDeparture.Location = new Point(14, 172);
            lblDeparture.Name = "lblDeparture";
            lblDeparture.TabIndex = 10;
            lblDeparture.Text = "Giờ bay";
            //
            // dtpDeparture
            //
            dtpDeparture.CustomFormat = "dd/MM/yyyy  HH:mm";
            dtpDeparture.Format = DateTimePickerFormat.Custom;
            dtpDeparture.Location = new Point(16, 192);
            dtpDeparture.Name = "dtpDeparture";
            dtpDeparture.Size = new Size(248, 25);
            dtpDeparture.TabIndex = 11;
            //
            // lblBasePrice
            //
            lblBasePrice.AutoSize = true;
            lblBasePrice.Location = new Point(14, 228);
            lblBasePrice.Name = "lblBasePrice";
            lblBasePrice.TabIndex = 12;
            lblBasePrice.Text = "Giá gốc (₫)";
            //
            // numBasePrice
            //
            numBasePrice.Increment = new decimal(new int[] { 50000, 0, 0, 0 });
            numBasePrice.Location = new Point(16, 248);
            numBasePrice.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numBasePrice.Minimum = new decimal(new int[] { 100000, 0, 0, 0 });
            numBasePrice.Name = "numBasePrice";
            numBasePrice.Size = new Size(248, 25);
            numBasePrice.TabIndex = 13;
            numBasePrice.ThousandsSeparator = true;
            numBasePrice.Value = new decimal(new int[] { 1850000, 0, 0, 0 });
            //
            // lblPriceNote
            //
            lblPriceNote.Font = new Font("Segoe UI", 9F);
            lblPriceNote.ForeColor = Color.FromArgb(106, 120, 141);
            lblPriceNote.Location = new Point(14, 284);
            lblPriceNote.Name = "lblPriceNote";
            lblPriceNote.Size = new Size(250, 36);
            lblPriceNote.TabIndex = 14;
            lblPriceNote.Text = "Giá mới chỉ áp dụng cho vé đặt sau khi lưu.";
            //
            // btnSave
            //
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.Location = new Point(16, 560);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(160, 32);
            btnSave.TabIndex = 15;
            btnSave.Text = "Lưu thay đổi";
            btnSave.Click += btnSave_Click;
            //
            // btnClose
            //
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(184, 560);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 32);
            btnClose.TabIndex = 16;
            btnClose.Text = "Đóng";
            //
            // pnlSeats
            //
            pnlSeats.Controls.Add(lblSeatTitle);
            pnlSeats.Controls.Add(lblLegendEmptyBox);
            pnlSeats.Controls.Add(lblLegendEmpty);
            pnlSeats.Controls.Add(lblLegendBookedBox);
            pnlSeats.Controls.Add(lblLegendBooked);
            pnlSeats.Controls.Add(lblLegendBusinessBox);
            pnlSeats.Controls.Add(lblLegendBusiness);
            pnlSeats.Controls.Add(flpSeatMap);
            pnlSeats.Dock = DockStyle.Fill;
            pnlSeats.Location = new Point(312, 16);
            pnlSeats.Margin = new Padding(0);
            pnlSeats.Name = "pnlSeats";
            pnlSeats.Size = new Size(572, 608);
            pnlSeats.TabIndex = 1;
            //
            // lblSeatTitle
            //
            lblSeatTitle.AutoSize = true;
            lblSeatTitle.Font = new Font("Segoe UI Semibold", 11.25F);
            lblSeatTitle.Location = new Point(14, 14);
            lblSeatTitle.Name = "lblSeatTitle";
            lblSeatTitle.TabIndex = 0;
            lblSeatTitle.Text = "Sơ đồ ghế (chỉ xem)";
            //
            // lblLegendEmptyBox
            //
            lblLegendEmptyBox.BackColor = Color.White;
            lblLegendEmptyBox.BorderStyle = BorderStyle.FixedSingle;
            lblLegendEmptyBox.Location = new Point(16, 51);
            lblLegendEmptyBox.Name = "lblLegendEmptyBox";
            lblLegendEmptyBox.Size = new Size(14, 14);
            lblLegendEmptyBox.TabIndex = 1;
            //
            // lblLegendEmpty
            //
            lblLegendEmpty.AutoSize = true;
            lblLegendEmpty.Font = new Font("Segoe UI", 9F);
            lblLegendEmpty.ForeColor = Color.FromArgb(106, 120, 141);
            lblLegendEmpty.Location = new Point(34, 49);
            lblLegendEmpty.Name = "lblLegendEmpty";
            lblLegendEmpty.TabIndex = 2;
            lblLegendEmpty.Text = "Trống";
            //
            // lblLegendBookedBox
            //
            lblLegendBookedBox.BackColor = Color.FromArgb(154, 154, 154);
            lblLegendBookedBox.Location = new Point(96, 51);
            lblLegendBookedBox.Name = "lblLegendBookedBox";
            lblLegendBookedBox.Size = new Size(14, 14);
            lblLegendBookedBox.TabIndex = 3;
            //
            // lblLegendBooked
            //
            lblLegendBooked.AutoSize = true;
            lblLegendBooked.Font = new Font("Segoe UI", 9F);
            lblLegendBooked.ForeColor = Color.FromArgb(106, 120, 141);
            lblLegendBooked.Location = new Point(114, 49);
            lblLegendBooked.Name = "lblLegendBooked";
            lblLegendBooked.TabIndex = 4;
            lblLegendBooked.Text = "Đã đặt";
            //
            // lblLegendBusinessBox
            //
            lblLegendBusinessBox.BackColor = Color.FromArgb(255, 246, 220);
            lblLegendBusinessBox.BorderStyle = BorderStyle.FixedSingle;
            lblLegendBusinessBox.Location = new Point(182, 51);
            lblLegendBusinessBox.Name = "lblLegendBusinessBox";
            lblLegendBusinessBox.Size = new Size(14, 14);
            lblLegendBusinessBox.TabIndex = 5;
            //
            // lblLegendBusiness
            //
            lblLegendBusiness.AutoSize = true;
            lblLegendBusiness.Font = new Font("Segoe UI", 9F);
            lblLegendBusiness.ForeColor = Color.FromArgb(106, 120, 141);
            lblLegendBusiness.Location = new Point(200, 49);
            lblLegendBusiness.Name = "lblLegendBusiness";
            lblLegendBusiness.TabIndex = 6;
            lblLegendBusiness.Text = "First / Business";
            //
            // flpSeatMap
            //
            flpSeatMap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpSeatMap.AutoScroll = true;
            flpSeatMap.FlowDirection = FlowDirection.TopDown;
            flpSeatMap.Location = new Point(16, 80);
            flpSeatMap.Name = "flpSeatMap";
            flpSeatMap.Size = new Size(540, 512);
            flpSeatMap.TabIndex = 7;
            flpSeatMap.WrapContents = false;
            flpSeatMap.Resize += flpSeatMap_Resize;
            //
            // FrmFlightEdit
            //
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(246, 248, 252);
            CancelButton = btnClose;
            ClientSize = new Size(900, 640);
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 9.75F);
            ForeColor = Color.FromArgb(20, 42, 78);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmFlightEdit";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chỉnh sửa chuyến bay";
            tlpMain.ResumeLayout(false);
            pnlInfo.ResumeLayout(false);
            pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBasePrice).EndInit();
            pnlSeats.ResumeLayout(false);
            pnlSeats.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private CardPanel pnlInfo;
        private Label lblInfoTitle;
        private Label lblCodeCaption;
        private Label lblCodeValue;
        private Label lblRouteCaption;
        private Label lblRouteValue;
        private Label lblTotalCaption;
        private Label lblTotalValue;
        private Label lblBookedCaption;
        private Label lblBookedValue;
        private Label sepInfo;
        private Label lblDeparture;
        private DateTimePicker dtpDeparture;
        private Label lblBasePrice;
        private NumericUpDown numBasePrice;
        private Label lblPriceNote;
        private Button btnSave;
        private Button btnClose;
        private CardPanel pnlSeats;
        private Label lblSeatTitle;
        private Label lblLegendEmptyBox;
        private Label lblLegendEmpty;
        private Label lblLegendBookedBox;
        private Label lblLegendBooked;
        private Label lblLegendBusinessBox;
        private Label lblLegendBusiness;
        private FlowLayoutPanel flpSeatMap;
    }
}
