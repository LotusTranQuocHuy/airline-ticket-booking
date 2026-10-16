namespace AirlineBooking.WinForms.Admin
{
    partial class FrmRevenueReport
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            tlpMain = new TableLayoutPanel();
            pnlFilter = new Panel();
            lblFrom = new Label();
            dtpFrom = new DateTimePicker();
            lblTo = new Label();
            dtpTo = new DateTimePicker();
            btnView = new Button();
            btnExportExcel = new Button();
            tlpCards = new TableLayoutPanel();
            cardPaid = new CardPanel();
            lblPaidCaption = new Label();
            lblPaidValue = new Label();
            cardRefunded = new CardPanel();
            lblRefundedCaption = new Label();
            lblRefundedValue = new Label();
            cardNet = new CardPanel();
            lblNetCaption = new Label();
            lblNetValue = new Label();
            tlpBottom = new TableLayoutPanel();
            pnlByFlight = new Panel();
            dgvByFlight = new DataGridView();
            colFlightCode = new DataGridViewTextBoxColumn();
            colFlightRoute = new DataGridViewTextBoxColumn();
            colFlightTickets = new DataGridViewTextBoxColumn();
            colFlightPaid = new DataGridViewTextBoxColumn();
            colFlightRefunded = new DataGridViewTextBoxColumn();
            colFlightNet = new DataGridViewTextBoxColumn();
            lblByFlight = new Label();
            pnlRight = new Panel();
            chartByFlight = new BarChart();
            pnlChartGap = new Panel();
            dgvByClass = new DataGridView();
            colClassName = new DataGridViewTextBoxColumn();
            colClassTickets = new DataGridViewTextBoxColumn();
            colClassNet = new DataGridViewTextBoxColumn();
            lblByClass = new Label();
            btnBack = new Button();
            tlpMain.SuspendLayout();
            pnlFilter.SuspendLayout();
            tlpCards.SuspendLayout();
            cardPaid.SuspendLayout();
            cardRefunded.SuspendLayout();
            cardNet.SuspendLayout();
            tlpBottom.SuspendLayout();
            pnlByFlight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvByFlight).BeginInit();
            pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvByClass).BeginInit();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 1;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(pnlFilter, 0, 0);
            tlpMain.Controls.Add(tlpCards, 0, 1);
            tlpMain.Controls.Add(tlpBottom, 0, 2);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(16);
            tlpMain.RowCount = 3;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1100, 700);
            tlpMain.TabIndex = 0;
            // 
            // pnlFilter
            // 
            pnlFilter.Controls.Add(btnBack);
            pnlFilter.Controls.Add(lblFrom);
            pnlFilter.Controls.Add(dtpFrom);
            pnlFilter.Controls.Add(lblTo);
            pnlFilter.Controls.Add(dtpTo);
            pnlFilter.Controls.Add(btnView);
            pnlFilter.Controls.Add(btnExportExcel);
            pnlFilter.Dock = DockStyle.Fill;
            pnlFilter.Location = new Point(16, 16);
            pnlFilter.Margin = new Padding(0);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1068, 62);
            pnlFilter.TabIndex = 0;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(-2, 0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(55, 17);
            lblFrom.TabIndex = 0;
            lblFrom.Text = "Từ ngày";
            // 
            // dtpFrom
            // 
            dtpFrom.CustomFormat = "dd/MM/yyyy";
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(0, 22);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(160, 25);
            dtpFrom.TabIndex = 1;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(168, 0);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(63, 17);
            lblTo.TabIndex = 2;
            lblTo.Text = "Đến ngày";
            // 
            // dtpTo
            // 
            dtpTo.CustomFormat = "dd/MM/yyyy";
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(170, 22);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(160, 25);
            dtpTo.TabIndex = 3;
            // 
            // btnView
            // 
            btnView.Location = new Point(340, 20);
            btnView.Name = "btnView";
            btnView.Size = new Size(110, 28);
            btnView.TabIndex = 4;
            btnView.Text = "Xem báo cáo";
            btnView.Click += btnView_Click;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnExportExcel.Location = new Point(456, 19);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(100, 28);
            btnExportExcel.TabIndex = 5;
            btnExportExcel.Text = "Xuất Excel";
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // tlpCards
            // 
            tlpCards.ColumnCount = 3;
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpCards.Controls.Add(cardPaid, 0, 0);
            tlpCards.Controls.Add(cardRefunded, 1, 0);
            tlpCards.Controls.Add(cardNet, 2, 0);
            tlpCards.Dock = DockStyle.Fill;
            tlpCards.Location = new Point(16, 78);
            tlpCards.Margin = new Padding(0);
            tlpCards.Name = "tlpCards";
            tlpCards.RowCount = 1;
            tlpCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCards.Size = new Size(1068, 96);
            tlpCards.TabIndex = 1;
            // 
            // cardPaid
            // 
            cardPaid.BackColor = Color.White;
            cardPaid.BorderColor = Color.FromArgb(214, 214, 214);
            cardPaid.Controls.Add(lblPaidCaption);
            cardPaid.Controls.Add(lblPaidValue);
            cardPaid.Dock = DockStyle.Fill;
            cardPaid.Location = new Point(0, 0);
            cardPaid.Margin = new Padding(0, 0, 8, 14);
            cardPaid.Name = "cardPaid";
            cardPaid.Size = new Size(347, 82);
            cardPaid.TabIndex = 0;
            // 
            // lblPaidCaption
            // 
            lblPaidCaption.AutoSize = true;
            lblPaidCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblPaidCaption.Location = new Point(14, 12);
            lblPaidCaption.Name = "lblPaidCaption";
            lblPaidCaption.Size = new Size(104, 17);
            lblPaidCaption.TabIndex = 0;
            lblPaidCaption.Text = "Tổng tiền đã thu";
            // 
            // lblPaidValue
            // 
            lblPaidValue.AutoSize = true;
            lblPaidValue.Font = new Font("Segoe UI Semibold", 18F);
            lblPaidValue.Location = new Point(12, 34);
            lblPaidValue.Name = "lblPaidValue";
            lblPaidValue.Size = new Size(49, 32);
            lblPaidValue.TabIndex = 1;
            lblPaidValue.Text = "0 ₫";
            // 
            // cardRefunded
            // 
            cardRefunded.BackColor = Color.White;
            cardRefunded.BorderColor = Color.FromArgb(214, 214, 214);
            cardRefunded.Controls.Add(lblRefundedCaption);
            cardRefunded.Controls.Add(lblRefundedValue);
            cardRefunded.Dock = DockStyle.Fill;
            cardRefunded.Location = new Point(359, 0);
            cardRefunded.Margin = new Padding(4, 0, 4, 14);
            cardRefunded.Name = "cardRefunded";
            cardRefunded.Size = new Size(347, 82);
            cardRefunded.TabIndex = 1;
            // 
            // lblRefundedCaption
            // 
            lblRefundedCaption.AutoSize = true;
            lblRefundedCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblRefundedCaption.Location = new Point(14, 12);
            lblRefundedCaption.Name = "lblRefundedCaption";
            lblRefundedCaption.Size = new Size(115, 17);
            lblRefundedCaption.TabIndex = 0;
            lblRefundedCaption.Text = "Tổng tiền đã hoàn";
            // 
            // lblRefundedValue
            // 
            lblRefundedValue.AutoSize = true;
            lblRefundedValue.Font = new Font("Segoe UI Semibold", 18F);
            lblRefundedValue.ForeColor = Color.FromArgb(179, 38, 30);
            lblRefundedValue.Location = new Point(12, 34);
            lblRefundedValue.Name = "lblRefundedValue";
            lblRefundedValue.Size = new Size(49, 32);
            lblRefundedValue.TabIndex = 1;
            lblRefundedValue.Text = "0 ₫";
            // 
            // cardNet
            // 
            cardNet.BackColor = Color.White;
            cardNet.BorderColor = Color.FromArgb(35, 112, 219);
            cardNet.Controls.Add(lblNetCaption);
            cardNet.Controls.Add(lblNetValue);
            cardNet.Dock = DockStyle.Fill;
            cardNet.Location = new Point(718, 0);
            cardNet.Margin = new Padding(8, 0, 0, 14);
            cardNet.Name = "cardNet";
            cardNet.Size = new Size(350, 82);
            cardNet.TabIndex = 2;
            // 
            // lblNetCaption
            // 
            lblNetCaption.AutoSize = true;
            lblNetCaption.ForeColor = Color.FromArgb(106, 120, 141);
            lblNetCaption.Location = new Point(14, 12);
            lblNetCaption.Name = "lblNetCaption";
            lblNetCaption.Size = new Size(97, 17);
            lblNetCaption.TabIndex = 0;
            lblNetCaption.Text = "Doanh thu thực";
            // 
            // lblNetValue
            // 
            lblNetValue.AutoSize = true;
            lblNetValue.Font = new Font("Segoe UI Semibold", 18F);
            lblNetValue.ForeColor = Color.FromArgb(35, 112, 219);
            lblNetValue.Location = new Point(12, 34);
            lblNetValue.Name = "lblNetValue";
            lblNetValue.Size = new Size(49, 32);
            lblNetValue.TabIndex = 1;
            lblNetValue.Text = "0 ₫";
            // 
            // tlpBottom
            // 
            tlpBottom.ColumnCount = 2;
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56.5F));
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.5F));
            tlpBottom.Controls.Add(pnlByFlight, 0, 0);
            tlpBottom.Controls.Add(pnlRight, 1, 0);
            tlpBottom.Dock = DockStyle.Fill;
            tlpBottom.Location = new Point(16, 174);
            tlpBottom.Margin = new Padding(0);
            tlpBottom.Name = "tlpBottom";
            tlpBottom.RowCount = 1;
            tlpBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBottom.Size = new Size(1068, 510);
            tlpBottom.TabIndex = 2;
            // 
            // pnlByFlight
            // 
            pnlByFlight.Controls.Add(dgvByFlight);
            pnlByFlight.Controls.Add(lblByFlight);
            pnlByFlight.Dock = DockStyle.Fill;
            pnlByFlight.Location = new Point(0, 0);
            pnlByFlight.Margin = new Padding(0, 0, 8, 0);
            pnlByFlight.Name = "pnlByFlight";
            pnlByFlight.Size = new Size(595, 510);
            pnlByFlight.TabIndex = 0;
            // 
            // dgvByFlight
            // 
            dgvByFlight.Columns.AddRange(new DataGridViewColumn[] { colFlightCode, colFlightRoute, colFlightTickets, colFlightPaid, colFlightRefunded, colFlightNet });
            dgvByFlight.Dock = DockStyle.Fill;
            dgvByFlight.Location = new Point(0, 26);
            dgvByFlight.Name = "dgvByFlight";
            dgvByFlight.Size = new Size(595, 484);
            dgvByFlight.TabIndex = 1;
            // 
            // colFlightCode
            // 
            colFlightCode.HeaderText = "Chuyến";
            colFlightCode.Name = "colFlightCode";
            colFlightCode.Width = 70;
            // 
            // colFlightRoute
            // 
            colFlightRoute.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colFlightRoute.HeaderText = "Hành trình";
            colFlightRoute.Name = "colFlightRoute";
            // 
            // colFlightTickets
            // 
            colFlightTickets.HeaderText = "Vé";
            colFlightTickets.Name = "colFlightTickets";
            colFlightTickets.Width = 50;
            // 
            // colFlightPaid
            // 
            colFlightPaid.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colFlightPaid.HeaderText = "Đã thu";
            colFlightPaid.Name = "colFlightPaid";
            // 
            // colFlightRefunded
            // 
            colFlightRefunded.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colFlightRefunded.HeaderText = "Đã hoàn";
            colFlightRefunded.Name = "colFlightRefunded";
            // 
            // colFlightNet
            // 
            colFlightNet.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9.75F);
            colFlightNet.DefaultCellStyle = dataGridViewCellStyle1;
            colFlightNet.HeaderText = "Thực";
            colFlightNet.Name = "colFlightNet";
            // 
            // lblByFlight
            // 
            lblByFlight.Dock = DockStyle.Top;
            lblByFlight.Font = new Font("Segoe UI Semibold", 9.75F);
            lblByFlight.Location = new Point(0, 0);
            lblByFlight.Name = "lblByFlight";
            lblByFlight.Size = new Size(595, 26);
            lblByFlight.TabIndex = 0;
            lblByFlight.Text = "Theo chuyến bay";
            // 
            // pnlRight
            // 
            pnlRight.Controls.Add(chartByFlight);
            pnlRight.Controls.Add(pnlChartGap);
            pnlRight.Controls.Add(dgvByClass);
            pnlRight.Controls.Add(lblByClass);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(611, 0);
            pnlRight.Margin = new Padding(8, 0, 0, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Size = new Size(457, 510);
            pnlRight.TabIndex = 1;
            // 
            // chartByFlight
            // 
            chartByFlight.BackColor = Color.White;
            chartByFlight.Caption = "Doanh thu thực theo chuyến (triệu ₫)";
            chartByFlight.Dock = DockStyle.Fill;
            chartByFlight.Location = new Point(0, 158);
            chartByFlight.Name = "chartByFlight";
            chartByFlight.Size = new Size(457, 352);
            chartByFlight.TabIndex = 3;
            // 
            // pnlChartGap
            // 
            pnlChartGap.Dock = DockStyle.Top;
            pnlChartGap.Location = new Point(0, 146);
            pnlChartGap.Name = "pnlChartGap";
            pnlChartGap.Size = new Size(457, 12);
            pnlChartGap.TabIndex = 2;
            // 
            // dgvByClass
            // 
            dgvByClass.Columns.AddRange(new DataGridViewColumn[] { colClassName, colClassTickets, colClassNet });
            dgvByClass.Dock = DockStyle.Top;
            dgvByClass.Location = new Point(0, 26);
            dgvByClass.Name = "dgvByClass";
            dgvByClass.Size = new Size(457, 120);
            dgvByClass.TabIndex = 1;
            // 
            // colClassName
            // 
            colClassName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colClassName.HeaderText = "Hạng";
            colClassName.Name = "colClassName";
            // 
            // colClassTickets
            // 
            colClassTickets.HeaderText = "Vé";
            colClassTickets.Name = "colClassTickets";
            colClassTickets.Width = 50;
            // 
            // colClassNet
            // 
            colClassNet.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colClassNet.HeaderText = "Doanh thu thực";
            colClassNet.Name = "colClassNet";
            // 
            // lblByClass
            // 
            lblByClass.Dock = DockStyle.Top;
            lblByClass.Font = new Font("Segoe UI Semibold", 9.75F);
            lblByClass.Location = new Point(0, 0);
            lblByClass.Name = "lblByClass";
            lblByClass.Size = new Size(457, 26);
            lblByClass.TabIndex = 0;
            lblByClass.Text = "Theo hạng ghế";
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBack.Location = new Point(942, 19);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(100, 28);
            btnBack.TabIndex = 6;
            btnBack.Text = "Quay lại";
            btnBack.Click += btnBack_Click;
            // 
            // FrmRevenueReport
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            CancelButton = btnBack;
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(246, 248, 252);
            ClientSize = new Size(1100, 700);
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 9.75F);
            ForeColor = Color.FromArgb(20, 42, 78);
            MinimizeBox = false;
            MinimumSize = new Size(900, 600);
            Name = "FrmRevenueReport";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Báo cáo doanh thu";
            tlpMain.ResumeLayout(false);
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            tlpCards.ResumeLayout(false);
            cardPaid.ResumeLayout(false);
            cardPaid.PerformLayout();
            cardRefunded.ResumeLayout(false);
            cardRefunded.PerformLayout();
            cardNet.ResumeLayout(false);
            cardNet.PerformLayout();
            tlpBottom.ResumeLayout(false);
            pnlByFlight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvByFlight).EndInit();
            pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvByClass).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private Panel pnlFilter;
        private Label lblFrom;
        private DateTimePicker dtpFrom;
        private Label lblTo;
        private DateTimePicker dtpTo;
        private Button btnView;
        private Button btnExportExcel;
        private TableLayoutPanel tlpCards;
        private CardPanel cardPaid;
        private Label lblPaidCaption;
        private Label lblPaidValue;
        private CardPanel cardRefunded;
        private Label lblRefundedCaption;
        private Label lblRefundedValue;
        private CardPanel cardNet;
        private Label lblNetCaption;
        private Label lblNetValue;
        private TableLayoutPanel tlpBottom;
        private Panel pnlByFlight;
        private DataGridView dgvByFlight;
        private DataGridViewTextBoxColumn colFlightCode;
        private DataGridViewTextBoxColumn colFlightRoute;
        private DataGridViewTextBoxColumn colFlightTickets;
        private DataGridViewTextBoxColumn colFlightPaid;
        private DataGridViewTextBoxColumn colFlightRefunded;
        private DataGridViewTextBoxColumn colFlightNet;
        private Label lblByFlight;
        private Panel pnlRight;
        private BarChart chartByFlight;
        private Panel pnlChartGap;
        private DataGridView dgvByClass;
        private DataGridViewTextBoxColumn colClassName;
        private DataGridViewTextBoxColumn colClassTickets;
        private DataGridViewTextBoxColumn colClassNet;
        private Label lblByClass;
        private Button btnBack;
    }
}
