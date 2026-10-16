namespace AirlineBooking.WinForms.Admin
{
    partial class FrmBookingManager
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
            pnlFilter = new Panel();
            lblFlight = new Label();
            cboFlight = new ComboBox();
            lblStatusFilter = new Label();
            cboStatus = new ComboBox();
            lblPassenger = new Label();
            txtPassenger = new TextBox();
            btnSearch = new Button();
            btnClearFilter = new Button();
            dgvBookings = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colPassenger = new DataGridViewTextBoxColumn();
            colFlight = new DataGridViewTextBoxColumn();
            colSeat = new DataGridViewTextBoxColumn();
            colClass = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            pnlDetail = new CardPanel();
            lblDetailTitle = new Label();
            lblPassengerCaption = new Label();
            lblPassengerValue = new Label();
            lblEmailCaption = new Label();
            lblEmailValue = new Label();
            lblFlightCaption = new Label();
            lblFlightValue = new Label();
            lblDepartureCaption = new Label();
            lblDepartureValue = new Label();
            lblSeatCaption = new Label();
            lblSeatValue = new Label();
            lblBookedAtCaption = new Label();
            lblBookedAtValue = new Label();
            lblPaidCaption = new Label();
            lblPaidValue = new Label();
            lblStatusCaption = new Label();
            lblStatusValue = new Label();
            sepRefund = new Label();
            lblRefundCaption = new Label();
            lblRefundAmount = new Label();
            lblRefundNote = new Label();
            btnCancelBooking = new Button();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            btnBack = new Button();
            tlpMain.SuspendLayout();
            pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBookings).BeginInit();
            pnlDetail.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 290F));
            tlpMain.Controls.Add(pnlFilter, 0, 0);
            tlpMain.Controls.Add(dgvBookings, 0, 1);
            tlpMain.Controls.Add(pnlDetail, 1, 1);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(16);
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1116, 640);
            tlpMain.TabIndex = 0;
            // 
            // pnlFilter
            // 
            tlpMain.SetColumnSpan(pnlFilter, 2);
            pnlFilter.Controls.Add(btnBack);
            pnlFilter.Controls.Add(lblFlight);
            pnlFilter.Controls.Add(cboFlight);
            pnlFilter.Controls.Add(lblStatusFilter);
            pnlFilter.Controls.Add(cboStatus);
            pnlFilter.Controls.Add(lblPassenger);
            pnlFilter.Controls.Add(txtPassenger);
            pnlFilter.Controls.Add(btnSearch);
            pnlFilter.Controls.Add(btnClearFilter);
            pnlFilter.Dock = DockStyle.Fill;
            pnlFilter.Location = new Point(16, 16);
            pnlFilter.Margin = new Padding(0);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1084, 62);
            pnlFilter.TabIndex = 0;
            // 
            // lblFlight
            // 
            lblFlight.AutoSize = true;
            lblFlight.Location = new Point(-2, 0);
            lblFlight.Name = "lblFlight";
            lblFlight.Size = new Size(75, 17);
            lblFlight.TabIndex = 0;
            lblFlight.Text = "Chuyến bay";
            // 
            // cboFlight
            // 
            cboFlight.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFlight.Location = new Point(0, 22);
            cboFlight.Name = "cboFlight";
            cboFlight.Size = new Size(170, 25);
            cboFlight.TabIndex = 1;
            cboFlight.SelectedIndexChanged += Filter_Changed;
            // 
            // lblStatusFilter
            // 
            lblStatusFilter.AutoSize = true;
            lblStatusFilter.Location = new Point(178, 0);
            lblStatusFilter.Name = "lblStatusFilter";
            lblStatusFilter.Size = new Size(66, 17);
            lblStatusFilter.TabIndex = 2;
            lblStatusFilter.Text = "Trạng thái";
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Location = new Point(180, 22);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(170, 25);
            cboStatus.TabIndex = 3;
            cboStatus.SelectedIndexChanged += Filter_Changed;
            // 
            // lblPassenger
            // 
            lblPassenger.AutoSize = true;
            lblPassenger.Location = new Point(358, 0);
            lblPassenger.Name = "lblPassenger";
            lblPassenger.Size = new Size(65, 17);
            lblPassenger.TabIndex = 4;
            lblPassenger.Text = "Tên khách";
            // 
            // txtPassenger
            // 
            txtPassenger.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassenger.Location = new Point(360, 22);
            txtPassenger.Name = "txtPassenger";
            txtPassenger.PlaceholderText = "Nhập tên hành khách…";
            txtPassenger.Size = new Size(420, 25);
            txtPassenger.TabIndex = 5;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(796, 22);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(78, 28);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Tìm";
            btnSearch.Click += btnSearch_Click;
            // 
            // btnClearFilter
            // 
            btnClearFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearFilter.Location = new Point(880, 22);
            btnClearFilter.Name = "btnClearFilter";
            btnClearFilter.Size = new Size(80, 28);
            btnClearFilter.TabIndex = 7;
            btnClearFilter.Text = "Xóa lọc";
            btnClearFilter.Click += btnClearFilter_Click;
            // 
            // dgvBookings
            // 
            dgvBookings.Columns.AddRange(new DataGridViewColumn[] { colId, colPassenger, colFlight, colSeat, colClass, colPrice, colStatus });
            dgvBookings.Dock = DockStyle.Fill;
            dgvBookings.Location = new Point(16, 78);
            dgvBookings.Margin = new Padding(0, 0, 16, 0);
            dgvBookings.Name = "dgvBookings";
            dgvBookings.Size = new Size(778, 546);
            dgvBookings.TabIndex = 1;
            dgvBookings.CellFormatting += dgvBookings_CellFormatting;
            dgvBookings.SelectionChanged += dgvBookings_SelectionChanged;
            // 
            // colId
            // 
            colId.HeaderText = "Mã vé";
            colId.Name = "colId";
            colId.Width = 80;
            // 
            // colPassenger
            // 
            colPassenger.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colPassenger.HeaderText = "Hành khách";
            colPassenger.Name = "colPassenger";
            // 
            // colFlight
            // 
            colFlight.HeaderText = "Chuyến";
            colFlight.Name = "colFlight";
            colFlight.Width = 70;
            // 
            // colSeat
            // 
            colSeat.HeaderText = "Ghế";
            colSeat.Name = "colSeat";
            colSeat.Width = 50;
            // 
            // colClass
            // 
            colClass.HeaderText = "Hạng";
            colClass.Name = "colClass";
            colClass.Width = 90;
            // 
            // colPrice
            // 
            colPrice.HeaderText = "Giá";
            colPrice.Name = "colPrice";
            colPrice.Width = 110;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Trạng thái";
            colStatus.Name = "colStatus";
            colStatus.Width = 110;
            // 
            // pnlDetail
            // 
            pnlDetail.BackColor = Color.White;
            pnlDetail.BorderColor = Color.FromArgb(214, 214, 214);
            pnlDetail.Controls.Add(lblDetailTitle);
            pnlDetail.Controls.Add(lblPassengerCaption);
            pnlDetail.Controls.Add(lblPassengerValue);
            pnlDetail.Controls.Add(lblEmailCaption);
            pnlDetail.Controls.Add(lblEmailValue);
            pnlDetail.Controls.Add(lblFlightCaption);
            pnlDetail.Controls.Add(lblFlightValue);
            pnlDetail.Controls.Add(lblDepartureCaption);
            pnlDetail.Controls.Add(lblDepartureValue);
            pnlDetail.Controls.Add(lblSeatCaption);
            pnlDetail.Controls.Add(lblSeatValue);
            pnlDetail.Controls.Add(lblBookedAtCaption);
            pnlDetail.Controls.Add(lblBookedAtValue);
            pnlDetail.Controls.Add(lblPaidCaption);
            pnlDetail.Controls.Add(lblPaidValue);
            pnlDetail.Controls.Add(lblStatusCaption);
            pnlDetail.Controls.Add(lblStatusValue);
            pnlDetail.Controls.Add(sepRefund);
            pnlDetail.Controls.Add(lblRefundCaption);
            pnlDetail.Controls.Add(lblRefundAmount);
            pnlDetail.Controls.Add(lblRefundNote);
            pnlDetail.Controls.Add(btnCancelBooking);
            pnlDetail.Dock = DockStyle.Fill;
            pnlDetail.Location = new Point(810, 78);
            pnlDetail.Margin = new Padding(0);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Size = new Size(290, 546);
            pnlDetail.TabIndex = 2;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.AutoSize = true;
            lblDetailTitle.Font = new Font("Segoe UI Semibold", 11.25F);
            lblDetailTitle.Location = new Point(14, 14);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new Size(77, 20);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "Chi tiết vé";
            // 
            // lblPassengerCaption
            // 
            lblPassengerCaption.AutoSize = true;
            lblPassengerCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblPassengerCaption.Location = new Point(14, 50);
            lblPassengerCaption.Name = "lblPassengerCaption";
            lblPassengerCaption.Size = new Size(75, 17);
            lblPassengerCaption.TabIndex = 1;
            lblPassengerCaption.Text = "Hành khách";
            // 
            // lblPassengerValue
            // 
            lblPassengerValue.AutoEllipsis = true;
            lblPassengerValue.Location = new Point(104, 50);
            lblPassengerValue.Name = "lblPassengerValue";
            lblPassengerValue.Size = new Size(172, 20);
            lblPassengerValue.TabIndex = 2;
            lblPassengerValue.Text = "—";
            // 
            // lblEmailCaption
            // 
            lblEmailCaption.AutoSize = true;
            lblEmailCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblEmailCaption.Location = new Point(14, 76);
            lblEmailCaption.Name = "lblEmailCaption";
            lblEmailCaption.Size = new Size(39, 17);
            lblEmailCaption.TabIndex = 3;
            lblEmailCaption.Text = "Email";
            // 
            // lblEmailValue
            // 
            lblEmailValue.AutoEllipsis = true;
            lblEmailValue.Location = new Point(104, 76);
            lblEmailValue.Name = "lblEmailValue";
            lblEmailValue.Size = new Size(172, 20);
            lblEmailValue.TabIndex = 4;
            lblEmailValue.Text = "—";
            // 
            // lblFlightCaption
            // 
            lblFlightCaption.AutoSize = true;
            lblFlightCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblFlightCaption.Location = new Point(14, 102);
            lblFlightCaption.Name = "lblFlightCaption";
            lblFlightCaption.Size = new Size(50, 17);
            lblFlightCaption.TabIndex = 5;
            lblFlightCaption.Text = "Chuyến";
            // 
            // lblFlightValue
            // 
            lblFlightValue.AutoEllipsis = true;
            lblFlightValue.Location = new Point(104, 102);
            lblFlightValue.Name = "lblFlightValue";
            lblFlightValue.Size = new Size(172, 20);
            lblFlightValue.TabIndex = 6;
            lblFlightValue.Text = "—";
            // 
            // lblDepartureCaption
            // 
            lblDepartureCaption.AutoSize = true;
            lblDepartureCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblDepartureCaption.Location = new Point(14, 128);
            lblDepartureCaption.Name = "lblDepartureCaption";
            lblDepartureCaption.Size = new Size(53, 17);
            lblDepartureCaption.TabIndex = 7;
            lblDepartureCaption.Text = "Giờ bay";
            // 
            // lblDepartureValue
            // 
            lblDepartureValue.AutoEllipsis = true;
            lblDepartureValue.Location = new Point(104, 128);
            lblDepartureValue.Name = "lblDepartureValue";
            lblDepartureValue.Size = new Size(172, 20);
            lblDepartureValue.TabIndex = 8;
            lblDepartureValue.Text = "—";
            // 
            // lblSeatCaption
            // 
            lblSeatCaption.AutoSize = true;
            lblSeatCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblSeatCaption.Location = new Point(14, 154);
            lblSeatCaption.Name = "lblSeatCaption";
            lblSeatCaption.Size = new Size(75, 17);
            lblSeatCaption.TabIndex = 9;
            lblSeatCaption.Text = "Ghế / Hạng";
            // 
            // lblSeatValue
            // 
            lblSeatValue.AutoEllipsis = true;
            lblSeatValue.Location = new Point(104, 154);
            lblSeatValue.Name = "lblSeatValue";
            lblSeatValue.Size = new Size(172, 20);
            lblSeatValue.TabIndex = 10;
            lblSeatValue.Text = "—";
            // 
            // lblBookedAtCaption
            // 
            lblBookedAtCaption.AutoSize = true;
            lblBookedAtCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblBookedAtCaption.Location = new Point(14, 180);
            lblBookedAtCaption.Name = "lblBookedAtCaption";
            lblBookedAtCaption.Size = new Size(62, 17);
            lblBookedAtCaption.TabIndex = 11;
            lblBookedAtCaption.Text = "Ngày đặt";
            // 
            // lblBookedAtValue
            // 
            lblBookedAtValue.AutoEllipsis = true;
            lblBookedAtValue.Location = new Point(104, 180);
            lblBookedAtValue.Name = "lblBookedAtValue";
            lblBookedAtValue.Size = new Size(172, 20);
            lblBookedAtValue.TabIndex = 12;
            lblBookedAtValue.Text = "—";
            // 
            // lblPaidCaption
            // 
            lblPaidCaption.AutoSize = true;
            lblPaidCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblPaidCaption.Location = new Point(14, 206);
            lblPaidCaption.Name = "lblPaidCaption";
            lblPaidCaption.Size = new Size(44, 17);
            lblPaidCaption.TabIndex = 13;
            lblPaidCaption.Text = "Đã trả";
            // 
            // lblPaidValue
            // 
            lblPaidValue.AutoEllipsis = true;
            lblPaidValue.Location = new Point(104, 206);
            lblPaidValue.Name = "lblPaidValue";
            lblPaidValue.Size = new Size(172, 20);
            lblPaidValue.TabIndex = 14;
            lblPaidValue.Text = "—";
            // 
            // lblStatusCaption
            // 
            lblStatusCaption.AutoSize = true;
            lblStatusCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblStatusCaption.Location = new Point(14, 232);
            lblStatusCaption.Name = "lblStatusCaption";
            lblStatusCaption.Size = new Size(66, 17);
            lblStatusCaption.TabIndex = 15;
            lblStatusCaption.Text = "Trạng thái";
            // 
            // lblStatusValue
            // 
            lblStatusValue.AutoEllipsis = true;
            lblStatusValue.Location = new Point(104, 232);
            lblStatusValue.Name = "lblStatusValue";
            lblStatusValue.Size = new Size(172, 20);
            lblStatusValue.TabIndex = 16;
            lblStatusValue.Text = "—";
            // 
            // sepRefund
            // 
            sepRefund.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            sepRefund.BackColor = Color.FromArgb(222, 229, 238);
            sepRefund.Location = new Point(16, 408);
            sepRefund.Name = "sepRefund";
            sepRefund.Size = new Size(258, 1);
            sepRefund.TabIndex = 17;
            // 
            // lblRefundCaption
            // 
            lblRefundCaption.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblRefundCaption.AutoSize = true;
            lblRefundCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblRefundCaption.Location = new Point(14, 420);
            lblRefundCaption.Name = "lblRefundCaption";
            lblRefundCaption.Size = new Size(81, 17);
            lblRefundCaption.TabIndex = 18;
            lblRefundCaption.Text = "Số tiền hoàn";
            // 
            // lblRefundAmount
            // 
            lblRefundAmount.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblRefundAmount.Font = new Font("Segoe UI Semibold", 9.75F);
            lblRefundAmount.Location = new Point(150, 420);
            lblRefundAmount.Name = "lblRefundAmount";
            lblRefundAmount.Size = new Size(124, 20);
            lblRefundAmount.TabIndex = 19;
            lblRefundAmount.Text = "—";
            lblRefundAmount.TextAlign = ContentAlignment.TopRight;
            // 
            // lblRefundNote
            // 
            lblRefundNote.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblRefundNote.Font = new Font("Segoe UI", 9F);
            lblRefundNote.ForeColor = Color.FromArgb(106, 120, 141);
            lblRefundNote.Location = new Point(14, 446);
            lblRefundNote.Name = "lblRefundNote";
            lblRefundNote.Size = new Size(260, 46);
            lblRefundNote.TabIndex = 20;
            lblRefundNote.Text = "Chọn một vé trong danh sách để xem chi tiết.";
            // 
            // btnCancelBooking
            // 
            btnCancelBooking.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnCancelBooking.Location = new Point(16, 498);
            btnCancelBooking.Name = "btnCancelBooking";
            btnCancelBooking.Size = new Size(258, 32);
            btnCancelBooking.TabIndex = 21;
            btnCancelBooking.Text = "Hủy vé thay khách";
            btnCancelBooking.Click += btnCancelBooking_Click;
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.White;
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 640);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1116, 22);
            statusStrip.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.ForeColor = Color.FromArgb(106, 120, 141);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(28, 17);
            lblStatus.Text = "0 vé";
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBack.Location = new Point(966, 22);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(80, 28);
            btnBack.TabIndex = 8;
            btnBack.Text = "Quay lại";
            btnBack.Click += btnBack_Click;
            // 
            // FrmBookingManager
            // 
            AcceptButton = btnSearch;
            AutoScaleDimensions = new SizeF(7F, 17F);
            CancelButton = btnBack;
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(246, 248, 252);
            ClientSize = new Size(1116, 662);
            Controls.Add(tlpMain);
            Controls.Add(statusStrip);
            Font = new Font("Segoe UI", 9.75F);
            ForeColor = Color.FromArgb(20, 42, 78);
            MinimizeBox = false;
            MinimumSize = new Size(900, 560);
            Name = "FrmBookingManager";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý vé";
            tlpMain.ResumeLayout(false);
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBookings).EndInit();
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private Panel pnlFilter;
        private Label lblFlight;
        private ComboBox cboFlight;
        private Label lblStatusFilter;
        private ComboBox cboStatus;
        private Label lblPassenger;
        private TextBox txtPassenger;
        private Button btnSearch;
        private Button btnClearFilter;
        private DataGridView dgvBookings;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colPassenger;
        private DataGridViewTextBoxColumn colFlight;
        private DataGridViewTextBoxColumn colSeat;
        private DataGridViewTextBoxColumn colClass;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colStatus;
        private CardPanel pnlDetail;
        private Label lblDetailTitle;
        private Label lblPassengerCaption;
        private Label lblPassengerValue;
        private Label lblEmailCaption;
        private Label lblEmailValue;
        private Label lblFlightCaption;
        private Label lblFlightValue;
        private Label lblDepartureCaption;
        private Label lblDepartureValue;
        private Label lblSeatCaption;
        private Label lblSeatValue;
        private Label lblBookedAtCaption;
        private Label lblBookedAtValue;
        private Label lblPaidCaption;
        private Label lblPaidValue;
        private Label lblStatusCaption;
        private Label lblStatusValue;
        private Label sepRefund;
        private Label lblRefundCaption;
        private Label lblRefundAmount;
        private Label lblRefundNote;
        private Button btnCancelBooking;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private Button btnBack;
    }
}
