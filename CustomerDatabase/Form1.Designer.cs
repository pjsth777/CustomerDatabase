namespace CustomerDatabase
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.DataGridView dataGridCustomers;
        private System.Windows.Forms.ListBox lstLogs;
        private System.Windows.Forms.Label lblSyncInterval;
        private System.Windows.Forms.Label lblLogsList;
        private System.Windows.Forms.Label lblDataGridCustomer;

        // ------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // ------------------------------------------------------------------------------------------------------------

        #region Windows Form Designer generated code

        // ------------------------------------------------------------------------------------------------------------

        private void InitializeComponent()
        {
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.dataGridCustomers = new System.Windows.Forms.DataGridView();
            this.lstLogs = new System.Windows.Forms.ListBox();
            this.lblSyncInterval = new System.Windows.Forms.Label();
            this.lblLogsList = new System.Windows.Forms.Label();
            this.lblDataGridCustomer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCustomers)).BeginInit();
            this.SuspendLayout();

            // ********************************************************************************************************

            this.Text = "Fleet Panda Loggin System";
            this.Icon = new Icon("E:\\DOTNET projects\\CustomerDatabase\\CustomerDatabase\\Images\\fleetpanda.ico");

            // ********************************************************************************************************

            this.lblSyncInterval.AutoSize = true;
            this.lblSyncInterval.Location = new System.Drawing.Point(20, 10);
            this.lblSyncInterval.Name = "lblSyncInterval";
            this.lblSyncInterval.Size = new System.Drawing.Size(100, 23);
            this.lblSyncInterval.TabIndex = 4;
            this.lblSyncInterval.Text = "Sync Interval Time";

            // ********************************************************************************************************

            this.txtInterval.Location = new System.Drawing.Point(20, 30);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(200, 23);
            this.txtInterval.TabIndex = 0;
            this.txtInterval.Text = "5";
            this.txtInterval.TextChanged += new System.EventHandler(this.txtInterval_TextChanged);

            // ********************************************************************************************************

            this.btnSync.Location = new System.Drawing.Point(240, 30);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(100, 23);
            this.btnSync.TabIndex = 1;
            this.btnSync.Text = "Sync Now";
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);

            // ********************************************************************************************************

            this.lblDataGridCustomer.AutoSize = true;
            this.lblDataGridCustomer.Location = new System.Drawing.Point(20, 70);
            this.lblDataGridCustomer.Name = "lblDataGridCustomer";
            this.lblDataGridCustomer.Size = new System.Drawing.Size(150, 23);
            this.lblDataGridCustomer.TabIndex = 2;
            this.lblDataGridCustomer.Text = "Customer Entries";

            // ********************************************************************************************************

            this.dataGridCustomers.Location = new System.Drawing.Point(20, 90);
            this.dataGridCustomers.Name = "dataGridCustomers";
            this.dataGridCustomers.Size = new System.Drawing.Size(650, 200);
            this.dataGridCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridCustomers.ReadOnly = true;
            this.dataGridCustomers.TabIndex = 2;

            // ********************************************************************************************************

            this.lblLogsList.AutoSize = true;
            this.lblLogsList.Location = new System.Drawing.Point(20, 300);
            this.lblLogsList.Name = "lblLogList";
            this.lblLogsList.Size = new System.Drawing.Size(100, 23);
            this.lblLogsList.TabIndex = 5;
            this.lblLogsList.Text = "Logs List";

            // ********************************************************************************************************

            this.lstLogs.ItemHeight = 15;
            this.lstLogs.Location = new System.Drawing.Point(20, 320);
            this.lstLogs.Name = "List of Log Details";
            this.lstLogs.Size = new System.Drawing.Size(650, 139);
            this.lstLogs.TabIndex = 3;

            // ********************************************************************************************************

            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.lblSyncInterval);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.dataGridCustomers);
            this.Controls.Add(this.lblLogsList);
            this.Controls.Add(this.lstLogs);
            this.Controls.Add(this.lblDataGridCustomer);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            // ********************************************************************************************************

        }

        // ------------------------------------------------------------------------------------------------------------

        private void CustomizeButton()
        {
            this.btnSync.FlatAppearance.BorderSize = 0;
            this.btnSync.FlatStyle = FlatStyle.Flat;
            this.btnSync.BackColor = Color.LightBlue;

            this.btnSync.MouseEnter += (s, e) => btnSync.BackColor = Color.SkyBlue;
            this.btnSync.MouseLeave += (s, e) => btnSync.BackColor = Color.LightBlue;
        }

        // ------------------------------------------------------------------------------------------------------------

        #endregion

        // ------------------------------------------------------------------------------------------------------------
    }
}