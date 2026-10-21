namespace AirlineBooking.WinForms.Admin
{
    partial class FrmFlightManager
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
            pnlAddFlight = new CardPanel();
            lblAddTitle = new Label();
            lblFlightCode = new Label();
            txtFlightCode = new TextBox();
            lblFrom = new Label();
            cboFrom = new ComboBox();
            lblTo = new Label();
            cboTo = new ComboBox();
            lblDeparture = new Label();
            dtpDeparture = new DateTimePicker();
            lblBasePrice = new Label();
            numBasePrice = new NumericUpDown();
            sepSeats = new Label();
            lblSeatSection = new Label();
            lblFirstRows = new Label();
            numFirstRows = new NumericUpDown();
            lblBusinessRows = new Label();
            numBusinessRows = new NumericUpDown();
            lblEconomyRows = new Label();
            numEconomyRows = new NumericUpDown();
            lblPremiumRows = new Label();
            numPremiumRows = new NumericUpDown();
            lblSeatPreview = new Label();
            btnAddFlight = new Button();
            btnReset = new Button();
            pnlList = new Panel();
            btnBack = new Button();
            txtSearch = new TextBox();
            btnEdit = new Button();
            btnDelete = new Button();
            dgvFlights = new DataGridView();
            colCode = new DataGridViewTextBoxColumn();
            colFrom = new DataGridViewTextBoxColumn();
            colTo = new DataGridViewTextBoxColumn();
            colDeparture = new DataGridViewTextBoxColumn();
            colBasePrice = new DataGridViewTextBoxColumn();
            colBooked = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            tlpMain.SuspendLayout();
            pnlAddFlight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numBasePrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFirstRows).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBusinessRows).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPremiumRows).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEconomyRows).BeginInit();
            pnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFlights).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 346F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(pnlAddFlight, 0, 0);
            tlpMain.Controls.Add(pnlList, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(16);
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1100, 658);
            tlpMain.TabIndex = 0;
            // 
            // pnlAddFlight
            // 
            pnlAddFlight.BackColor = Color.White;
            pnlAddFlight.BorderColor = Color.FromArgb(214, 214, 214);
            pnlAddFlight.Controls.Add(lblAddTitle);
            pnlAddFlight.Controls.Add(lblFlightCode);
            pnlAddFlight.Controls.Add(txtFlightCode);
            pnlAddFlight.Controls.Add(lblFrom);
            pnlAddFlight.Controls.Add(cboFrom);
            pnlAddFlight.Controls.Add(lblTo);
            pnlAddFlight.Controls.Add(cboTo);
            pnlAddFlight.Controls.Add(lblDeparture);
            pnlAddFlight.Controls.Add(dtpDeparture);
            pnlAddFlight.Controls.Add(lblBasePrice);
            pnlAddFlight.Controls.Add(numBasePrice);
            pnlAddFlight.Controls.Add(sepSeats);
            pnlAddFlight.Controls.Add(lblSeatSection);
            pnlAddFlight.Controls.Add(lblFirstRows);
            pnlAddFlight.Controls.Add(numFirstRows);
            pnlAddFlight.Controls.Add(lblBusinessRows);
            pnlAddFlight.Controls.Add(numBusinessRows);
            pnlAddFlight.Controls.Add(lblPremiumRows);
            pnlAddFlight.Controls.Add(numPremiumRows);
            pnlAddFlight.Controls.Add(lblEconomyRows);
            pnlAddFlight.Controls.Add(numEconomyRows);
            pnlAddFlight.Controls.Add(lblSeatPreview);
            pnlAddFlight.Controls.Add(btnAddFlight);
            pnlAddFlight.Controls.Add(btnReset);
            pnlAddFlight.Dock = DockStyle.Fill;
            pnlAddFlight.Location = new Point(16, 16);
            pnlAddFlight.Margin = new Padding(0, 0, 16, 0);
            pnlAddFlight.Name = "pnlAddFlight";
            pnlAddFlight.Size = new Size(330, 626);
            pnlAddFlight.TabIndex = 0;
            // 
            // lblAddTitle
            // 
            lblAddTitle.AutoSize = true;
            lblAddTitle.Font = new Font("Segoe UI Semibold", 11.25F);
            lblAddTitle.Location = new Point(14, 14);
            lblAddTitle.Name = "lblAddTitle";
            lblAddTitle.Size = new Size(160, 20);
            lblAddTitle.TabIndex = 0;
            lblAddTitle.Text = "Thêm chuyến bay mới";
            // 
            // lblFlightCode
            // 
            lblFlightCode.AutoSize = true;
            lblFlightCode.Location = new Point(14, 52);
            lblFlightCode.Name = "lblFlightCode";
            lblFlightCode.Size = new Size(95, 17);
            lblFlightCode.TabIndex = 1;
            lblFlightCode.Text = "Số hiệu chuyến";
            // 
            // txtFlightCode
            // 
            txtFlightCode.CharacterCasing = CharacterCasing.Upper;
            txtFlightCode.Location = new Point(16, 72);
            txtFlightCode.MaxLength = 10;
            txtFlightCode.Name = "txtFlightCode";
            txtFlightCode.PlaceholderText = "VD: VN245";
            txtFlightCode.Size = new Size(298, 25);
            txtFlightCode.TabIndex = 2;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(14, 108);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(53, 17);
            lblFrom.TabIndex = 3;
            lblFrom.Text = "Điểm đi";
            // 
            // cboFrom
            // 
            cboFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFrom.Location = new Point(16, 128);
            cboFrom.Name = "cboFrom";
            cboFrom.Size = new Size(144, 25);
            cboFrom.TabIndex = 4;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(168, 108);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(64, 17);
            lblTo.TabIndex = 5;
            lblTo.Text = "Điểm đến";
            // 
            // cboTo
            // 
            cboTo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTo.Location = new Point(170, 128);
            cboTo.Name = "cboTo";
            cboTo.Size = new Size(144, 25);
            cboTo.TabIndex = 6;
            // 
            // lblDeparture
            // 
            lblDeparture.AutoSize = true;
            lblDeparture.Location = new Point(14, 164);
            lblDeparture.Name = "lblDeparture";
            lblDeparture.Size = new Size(53, 17);
            lblDeparture.TabIndex = 7;
            lblDeparture.Text = "Giờ bay";
            // 
            // dtpDeparture
            // 
            dtpDeparture.CustomFormat = "dd/MM/yyyy  HH:mm";
            dtpDeparture.Format = DateTimePickerFormat.Custom;
            dtpDeparture.Location = new Point(16, 184);
            dtpDeparture.Name = "dtpDeparture";
            dtpDeparture.Size = new Size(298, 25);
            dtpDeparture.TabIndex = 8;
            // 
            // lblBasePrice
            // 
            lblBasePrice.AutoSize = true;
            lblBasePrice.Location = new Point(14, 220);
            lblBasePrice.Name = "lblBasePrice";
            lblBasePrice.Size = new Size(73, 17);
            lblBasePrice.TabIndex = 9;
            lblBasePrice.Text = "Giá gốc (₫)";
            // 
            // numBasePrice
            // 
            numBasePrice.Increment = new decimal(new int[] { 50000, 0, 0, 0 });
            numBasePrice.Location = new Point(16, 240);
            numBasePrice.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numBasePrice.Minimum = new decimal(new int[] { 100000, 0, 0, 0 });
            numBasePrice.Name = "numBasePrice";
            numBasePrice.Size = new Size(298, 25);
            numBasePrice.TabIndex = 10;
            numBasePrice.ThousandsSeparator = true;
            numBasePrice.Value = new decimal(new int[] { 1450000, 0, 0, 0 });
            // 
            // sepSeats
            // 
            sepSeats.BackColor = Color.FromArgb(222, 229, 238);
            sepSeats.Location = new Point(16, 282);
            sepSeats.Name = "sepSeats";
            sepSeats.Size = new Size(298, 1);
            sepSeats.TabIndex = 11;
            // 
            // lblSeatSection
            // 
            lblSeatSection.AutoSize = true;
            lblSeatSection.Font = new Font("Segoe UI Semibold", 9.75F);
            lblSeatSection.Location = new Point(14, 294);
            lblSeatSection.Name = "lblSeatSection";
            lblSeatSection.Size = new Size(147, 17);
            lblSeatSection.TabIndex = 12;
            lblSeatSection.Text = "Số hàng ghế mỗi hạng";
            // 
            // lblFirstRows
            // 
            lblFirstRows.AutoSize = true;
            lblFirstRows.Location = new Point(14, 330);
            lblFirstRows.Name = "lblFirstRows";
            lblFirstRows.TabIndex = 24;
            lblFirstRows.Text = "First (4 ghế/hàng)";
            // 
            // numFirstRows
            // 
            numFirstRows.Location = new Point(204, 326);
            numFirstRows.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            numFirstRows.Name = "numFirstRows";
            numFirstRows.Size = new Size(110, 25);
            numFirstRows.TabIndex = 25;
            numFirstRows.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numFirstRows.ValueChanged += SeatRows_ValueChanged;
            // 
            // lblBusinessRows
            // 
            lblBusinessRows.AutoSize = true;
            lblBusinessRows.Location = new Point(14, 366);
            lblBusinessRows.Name = "lblBusinessRows";
            lblBusinessRows.Size = new Size(136, 17);
            lblBusinessRows.TabIndex = 13;
            lblBusinessRows.Text = "Business (4 ghế/hàng)";
            // 
            // numBusinessRows
            // 
            numBusinessRows.Location = new Point(204, 362);
            numBusinessRows.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            numBusinessRows.Name = "numBusinessRows";
            numBusinessRows.Size = new Size(110, 25);
            numBusinessRows.TabIndex = 14;
            numBusinessRows.Value = new decimal(new int[] { 2, 0, 0, 0 });
            numBusinessRows.ValueChanged += SeatRows_ValueChanged;
            // 
            // lblPremiumRows
            // 
            lblPremiumRows.AutoSize = true;
            lblPremiumRows.Location = new Point(14, 402);
            lblPremiumRows.Name = "lblPremiumRows";
            lblPremiumRows.TabIndex = 26;
            lblPremiumRows.Text = "Premium (4 ghế/hàng)";
            // 
            // numPremiumRows
            // 
            numPremiumRows.Location = new Point(204, 398);
            numPremiumRows.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            numPremiumRows.Name = "numPremiumRows";
            numPremiumRows.Size = new Size(110, 25);
            numPremiumRows.TabIndex = 27;
            numPremiumRows.Value = new decimal(new int[] { 2, 0, 0, 0 });
            numPremiumRows.ValueChanged += SeatRows_ValueChanged;
            // 
            // lblEconomyRows
            // 
            lblEconomyRows.AutoSize = true;
            lblEconomyRows.Location = new Point(14, 438);
            lblEconomyRows.Name = "lblEconomyRows";
            lblEconomyRows.Size = new Size(140, 17);
            lblEconomyRows.TabIndex = 15;
            lblEconomyRows.Text = "Economy (6 ghế/hàng)";
            // 
            // numEconomyRows
            // 
            numEconomyRows.Location = new Point(204, 434);
            numEconomyRows.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            numEconomyRows.Name = "numEconomyRows";
            numEconomyRows.Size = new Size(110, 25);
            numEconomyRows.TabIndex = 16;
            numEconomyRows.Value = new decimal(new int[] { 20, 0, 0, 0 });
            numEconomyRows.ValueChanged += SeatRows_ValueChanged;
            // 
            // lblSeatPreview
            // 
            lblSeatPreview.ForeColor = Color.FromArgb(106, 120, 141);
            lblSeatPreview.Location = new Point(14, 474);
            lblSeatPreview.Name = "lblSeatPreview";
            lblSeatPreview.Size = new Size(290, 40);
            lblSeatPreview.TabIndex = 17;
            lblSeatPreview.Text = "Sẽ sinh 140 ghế: 20 hạng cao + 120 Economy";
            // 
            // btnAddFlight
            // 
            btnAddFlight.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddFlight.Location = new Point(16, 578);
            btnAddFlight.Name = "btnAddFlight";
            btnAddFlight.Size = new Size(200, 32);
            btnAddFlight.TabIndex = 18;
            btnAddFlight.Text = "Thêm && sinh sơ đồ ghế";
            btnAddFlight.Click += btnAddFlight_Click;
            // 
            // btnReset
            // 
            btnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReset.Location = new Point(224, 578);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(90, 32);
            btnReset.TabIndex = 19;
            btnReset.Text = "Làm mới";
            btnReset.Click += btnReset_Click;
            // 
            // pnlList
            // 
            pnlList.Controls.Add(btnBack);
            pnlList.Controls.Add(txtSearch);
            pnlList.Controls.Add(btnEdit);
            pnlList.Controls.Add(btnDelete);
            pnlList.Controls.Add(dgvFlights);
            pnlList.Dock = DockStyle.Fill;
            pnlList.Location = new Point(362, 16);
            pnlList.Margin = new Padding(0);
            pnlList.Name = "pnlList";
            pnlList.Size = new Size(722, 626);
            pnlList.TabIndex = 1;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBack.Location = new Point(642, 0);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(80, 28);
            btnBack.TabIndex = 4;
            btnBack.Text = "Quay lại";
            btnBack.Click += btnBack_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(0, 1);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm số hiệu, điểm đi/đến…";
            txtSearch.Size = new Size(260, 25);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEdit.Location = new Point(474, 0);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 28);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Sửa…";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.Location = new Point(562, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(72, 28);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Xóa";
            btnDelete.Click += btnDelete_Click;
            // 
            // dgvFlights
            // 
            dgvFlights.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvFlights.Columns.AddRange(new DataGridViewColumn[] { colCode, colFrom, colTo, colDeparture, colBasePrice, colBooked, colStatus });
            dgvFlights.Location = new Point(0, 38);
            dgvFlights.Name = "dgvFlights";
            dgvFlights.Size = new Size(722, 588);
            dgvFlights.TabIndex = 3;
            dgvFlights.CellDoubleClick += dgvFlights_CellDoubleClick;
            dgvFlights.SelectionChanged += dgvFlights_SelectionChanged;
            // 
            // colCode
            // 
            colCode.HeaderText = "Số hiệu";
            colCode.Name = "colCode";
            colCode.Width = 70;
            // 
            // colFrom
            // 
            colFrom.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colFrom.HeaderText = "Điểm đi";
            colFrom.Name = "colFrom";
            // 
            // colTo
            // 
            colTo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTo.HeaderText = "Điểm đến";
            colTo.Name = "colTo";
            // 
            // colDeparture
            // 
            colDeparture.HeaderText = "Giờ bay";
            colDeparture.Name = "colDeparture";
            colDeparture.Width = 130;
            // 
            // colBasePrice
            // 
            colBasePrice.HeaderText = "Giá gốc";
            colBasePrice.Name = "colBasePrice";
            // 
            // colBooked
            // 
            colBooked.HeaderText = "Đã đặt";
            colBooked.Name = "colBooked";
            colBooked.Width = 70;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Trạng thái";
            colStatus.Name = "colStatus";
            colStatus.Width = 90;
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.White;
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 658);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1100, 22);
            statusStrip.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.ForeColor = Color.FromArgb(106, 120, 141);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(77, 17);
            lblStatus.Text = "0 chuyến bay";
            // 
            // FrmFlightManager
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            CancelButton = btnBack;
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(246, 248, 252);
            ClientSize = new Size(1100, 680);
            Controls.Add(tlpMain);
            Controls.Add(statusStrip);
            Font = new Font("Segoe UI", 9.75F);
            ForeColor = Color.FromArgb(20, 42, 78);
            MinimizeBox = false;
            MinimumSize = new Size(900, 600);
            Name = "FrmFlightManager";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tạo vé — Quản lý chuyến bay";
            tlpMain.ResumeLayout(false);
            pnlAddFlight.ResumeLayout(false);
            pnlAddFlight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBasePrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFirstRows).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBusinessRows).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPremiumRows).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEconomyRows).EndInit();
            pnlList.ResumeLayout(false);
            pnlList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFlights).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private CardPanel pnlAddFlight;
        private Label lblAddTitle;
        private Label lblFlightCode;
        private TextBox txtFlightCode;
        private Label lblFrom;
        private ComboBox cboFrom;
        private Label lblTo;
        private ComboBox cboTo;
        private Label lblDeparture;
        private DateTimePicker dtpDeparture;
        private Label lblBasePrice;
        private NumericUpDown numBasePrice;
        private Label sepSeats;
        private Label lblSeatSection;
        private Label lblFirstRows;
        private NumericUpDown numFirstRows;
        private Label lblBusinessRows;
        private NumericUpDown numBusinessRows;
        private Label lblEconomyRows;
        private NumericUpDown numEconomyRows;
        private Label lblPremiumRows;
        private NumericUpDown numPremiumRows;
        private Label lblSeatPreview;
        private Button btnAddFlight;
        private Button btnReset;
        private Panel pnlList;
        private TextBox txtSearch;
        private Button btnEdit;
        private Button btnDelete;
        private DataGridView dgvFlights;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewTextBoxColumn colFrom;
        private DataGridViewTextBoxColumn colTo;
        private DataGridViewTextBoxColumn colDeparture;
        private DataGridViewTextBoxColumn colBasePrice;
        private DataGridViewTextBoxColumn colBooked;
        private DataGridViewTextBoxColumn colStatus;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private Button btnBack;
    }
}
