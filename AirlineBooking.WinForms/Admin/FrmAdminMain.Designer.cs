namespace AirlineBooking.WinForms.Admin
{
    partial class FrmAdminMain
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
            menuStrip = new MenuStrip();
            mnuSystem = new ToolStripMenuItem();
            mnuLogout = new ToolStripMenuItem();
            mnuExit = new ToolStripMenuItem();
            mnuFlights = new ToolStripMenuItem();
            mnuFlightManager = new ToolStripMenuItem();
            mnuBookings = new ToolStripMenuItem();
            mnuBookingManager = new ToolStripMenuItem();
            mnuReports = new ToolStripMenuItem();
            mnuRevenueReport = new ToolStripMenuItem();
            mnuHelp = new ToolStripMenuItem();
            mnuAbout = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            lblReady = new ToolStripStatusLabel();
            lblOpenFlights = new ToolStripStatusLabel();
            lblTodayBookings = new ToolStripStatusLabel();
            lblWelcome = new Label();
            lblRole = new Label();
            btnLogout = new Button();
            tlpCards = new TableLayoutPanel();
            cardFlights = new MenuCard();
            cardBookings = new MenuCard();
            cardRevenue = new MenuCard();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            tlpCards.SuspendLayout();
            SuspendLayout();
            //
            // menuStrip
            //
            menuStrip.BackColor = Color.White;
            menuStrip.Items.AddRange(new ToolStripItem[] { mnuSystem, mnuFlights, mnuBookings, mnuReports, mnuHelp });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(900, 25);
            menuStrip.TabIndex = 0;
            //
            // mnuSystem
            //
            mnuSystem.DropDownItems.AddRange(new ToolStripItem[] { mnuLogout, mnuExit });
            mnuSystem.Name = "mnuSystem";
            mnuSystem.Text = "Hệ thống";
            //
            // mnuLogout
            //
            mnuLogout.Name = "mnuLogout";
            mnuLogout.Text = "Đăng xuất";
            mnuLogout.Click += Logout;
            //
            // mnuExit
            //
            mnuExit.Name = "mnuExit";
            mnuExit.Text = "Thoát";
            mnuExit.Click += mnuExit_Click;
            //
            // mnuFlights
            //
            mnuFlights.DropDownItems.AddRange(new ToolStripItem[] { mnuFlightManager });
            mnuFlights.Name = "mnuFlights";
            mnuFlights.Text = "Chuyến bay";
            //
            // mnuFlightManager
            //
            mnuFlightManager.Name = "mnuFlightManager";
            mnuFlightManager.Text = "Quản lý chuyến bay";
            mnuFlightManager.Click += OpenFlightManager;
            //
            // mnuBookings
            //
            mnuBookings.DropDownItems.AddRange(new ToolStripItem[] { mnuBookingManager });
            mnuBookings.Name = "mnuBookings";
            mnuBookings.Text = "Vé";
            //
            // mnuBookingManager
            //
            mnuBookingManager.Name = "mnuBookingManager";
            mnuBookingManager.Text = "Quản lý vé";
            mnuBookingManager.Click += OpenBookingManager;
            //
            // mnuReports
            //
            mnuReports.DropDownItems.AddRange(new ToolStripItem[] { mnuRevenueReport });
            mnuReports.Name = "mnuReports";
            mnuReports.Text = "Báo cáo";
            //
            // mnuRevenueReport
            //
            mnuRevenueReport.Name = "mnuRevenueReport";
            mnuRevenueReport.Text = "Báo cáo doanh thu";
            mnuRevenueReport.Click += OpenRevenueReport;
            //
            // mnuHelp
            //
            mnuHelp.DropDownItems.AddRange(new ToolStripItem[] { mnuAbout });
            mnuHelp.Name = "mnuHelp";
            mnuHelp.Text = "Trợ giúp";
            //
            // mnuAbout
            //
            mnuAbout.Name = "mnuAbout";
            mnuAbout.Text = "Giới thiệu";
            mnuAbout.Click += mnuAbout_Click;
            //
            // statusStrip
            //
            statusStrip.BackColor = Color.White;
            statusStrip.Items.AddRange(new ToolStripItem[] { lblReady, lblOpenFlights, lblTodayBookings });
            statusStrip.Location = new Point(0, 578);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(900, 22);
            statusStrip.SizingGrip = false;
            statusStrip.TabIndex = 5;
            //
            // lblReady
            //
            lblReady.ForeColor = Color.FromArgb(106, 120, 141);
            lblReady.Margin = new Padding(4, 3, 24, 2);
            lblReady.Name = "lblReady";
            lblReady.Text = "Sẵn sàng";
            //
            // lblOpenFlights
            //
            lblOpenFlights.ForeColor = Color.FromArgb(106, 120, 141);
            lblOpenFlights.Margin = new Padding(0, 3, 24, 2);
            lblOpenFlights.Name = "lblOpenFlights";
            lblOpenFlights.Text = "Chuyến đang mở bán: 0";
            //
            // lblTodayBookings
            //
            lblTodayBookings.ForeColor = Color.FromArgb(106, 120, 141);
            lblTodayBookings.Name = "lblTodayBookings";
            lblTodayBookings.Text = "Vé hôm nay: 0";
            //
            // lblWelcome
            //
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI Semibold", 18F);
            lblWelcome.ForeColor = Color.FromArgb(20, 42, 78);
            lblWelcome.Location = new Point(36, 52);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(278, 32);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Xin chào, Quản trị viên";
            //
            // lblRole
            //
            lblRole.AutoSize = true;
            lblRole.ForeColor = Color.FromArgb(106, 120, 141);
            lblRole.Location = new Point(40, 92);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(150, 17);
            lblRole.TabIndex = 2;
            lblRole.Text = "Vai trò: Quản trị viên";
            //
            // btnLogout
            //
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Location = new Point(750, 70);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(110, 32);
            btnLogout.TabIndex = 3;
            btnLogout.Text = "Đăng xuất";
            btnLogout.Click += Logout;
            //
            // tlpCards
            //
            tlpCards.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tlpCards.ColumnCount = 3;
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpCards.Controls.Add(cardFlights, 0, 0);
            tlpCards.Controls.Add(cardBookings, 1, 0);
            tlpCards.Controls.Add(cardRevenue, 2, 0);
            tlpCards.Location = new Point(40, 140);
            tlpCards.Name = "tlpCards";
            tlpCards.RowCount = 1;
            tlpCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCards.Size = new Size(820, 406);
            tlpCards.TabIndex = 4;
            //
            // cardFlights
            //
            cardFlights.Badge = "1";
            cardFlights.Description = "Tạo chuyến, sửa giờ bay, giá, xem sơ đồ ghế";
            cardFlights.Dock = DockStyle.Fill;
            cardFlights.Margin = new Padding(0, 0, 14, 0);
            cardFlights.Name = "cardFlights";
            cardFlights.TabIndex = 0;
            cardFlights.Title = "Quản lý chuyến bay";
            cardFlights.Click += OpenFlightManager;
            //
            // cardBookings
            //
            cardBookings.Badge = "2";
            cardBookings.Description = "Tra cứu, xem chi tiết, hủy vé và hoàn tiền";
            cardBookings.Dock = DockStyle.Fill;
            cardBookings.Margin = new Padding(7, 0, 7, 0);
            cardBookings.Name = "cardBookings";
            cardBookings.TabIndex = 1;
            cardBookings.Title = "Quản lý vé";
            cardBookings.Click += OpenBookingManager;
            //
            // cardRevenue
            //
            cardRevenue.Badge = "3";
            cardRevenue.Description = "Đã thu, đã hoàn, doanh thu thực theo khoảng ngày";
            cardRevenue.Dock = DockStyle.Fill;
            cardRevenue.Margin = new Padding(14, 0, 0, 0);
            cardRevenue.Name = "cardRevenue";
            cardRevenue.TabIndex = 2;
            cardRevenue.Title = "Báo cáo doanh thu";
            cardRevenue.Click += OpenRevenueReport;
            //
            // FrmAdminMain
            //
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(246, 248, 252);
            ClientSize = new Size(900, 600);
            Controls.Add(tlpCards);
            Controls.Add(btnLogout);
            Controls.Add(lblRole);
            Controls.Add(lblWelcome);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            Font = new Font("Segoe UI", 9.75F);
            ForeColor = Color.FromArgb(20, 42, 78);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            Name = "FrmAdminMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Airline Booking — Quản trị";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            tlpCards.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem mnuSystem;
        private ToolStripMenuItem mnuLogout;
        private ToolStripMenuItem mnuExit;
        private ToolStripMenuItem mnuFlights;
        private ToolStripMenuItem mnuFlightManager;
        private ToolStripMenuItem mnuBookings;
        private ToolStripMenuItem mnuBookingManager;
        private ToolStripMenuItem mnuReports;
        private ToolStripMenuItem mnuRevenueReport;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuAbout;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblReady;
        private ToolStripStatusLabel lblOpenFlights;
        private ToolStripStatusLabel lblTodayBookings;
        private Label lblWelcome;
        private Label lblRole;
        private Button btnLogout;
        private TableLayoutPanel tlpCards;
        private MenuCard cardFlights;
        private MenuCard cardBookings;
        private MenuCard cardRevenue;
    }
}
