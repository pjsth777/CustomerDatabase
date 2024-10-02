using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Timers;
using CustomerDatabase.Models;
using System.Data;

namespace CustomerDatabase
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer _syncTimer;

        // ------------------------------------------------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
            CustomizeButton();
            CreateSQLiteTables();

            int defaultInterval = 5;
            if (int.TryParse(txtInterval.Text, out int intervalMinutes))
            {
                defaultInterval = intervalMinutes;
            }
            StartSyncTimer(defaultInterval);
        }


        // ------------------------------------------------------------------------------------------------------------

        private void btnSync_Click(object sender, EventArgs e)
        {
            SyncData();
        }

        // ------------------------------------------------------------------------------------------------------------

        private void txtInterval_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtInterval.Text, out int intervalMinutes))
            {
                StartSyncTimer(intervalMinutes);
            }
            else
            {
                MessageBox.Show("Please enter a valid number for the sync interval");
            }
        }

        // ------------------------------------------------------------------------------------------------------------

        private void StartSyncTimer(int intervalMinutes)
        {

            if (_syncTimer != null)
            {
                _syncTimer.Stop();
            }

            _syncTimer = new System.Windows.Forms.Timer();
            _syncTimer.Interval = intervalMinutes * 60000;
            _syncTimer.Tick += (sender, e) => SyncData();
            _syncTimer.Start();
        }

        // ------------------------------------------------------------------------------------------------------------

        private void CreateSQLiteTables()
        {
            using (var connection = new SQLiteConnection("Data Source=syncapp.sqlite"))
            {
                connection.Open();
                var command = new SQLiteCommand(@"
                    CREATE TABLE IF NOT EXISTS Customer (
                        CustomerID INTEGER PRIMARY KEY,
                        Name TEXT,
                        Email TEXT,
                        Phone TEXT
                    );
                    CREATE TABLE IF NOT EXISTS Location (
                        LocationID INTEGER PRIMARY KEY,
                        CustomerID INTEGER,
                        Address TEXT
                    );
                    CREATE TABLE IF NOT EXISTS SyncLog (
                        LogID INTEGER PRIMARY KEY AUTOINCREMENT,
                        SyncTime DATETIME,
                        customerChangeDetails TEXT
                );", connection);
                command.ExecuteNonQuery();
            }
        }

        // ------------------------------------------------------------------------------------------------------------

        private List<Customer> FetchFromMSSQL()
        {
            var customers = new List<Customer>();

            using (var connection = new SqlConnection("Data Source=DESKTOP-QTVAU89\\SQLEXPRESS;Initial Catalog=CustomerDatabase;Integrated Security=True;MultipleActiveResultSets=True;"))
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM Customer", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customer = new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"],
                            Phone = (string)reader["Phone"],
                            Locations = new List<Location>()
                        };

                        using (var locationCommand = new SqlCommand("SELECT * FROM Location WHERE CustomerID = @CustomerID", connection))
                        {
                            locationCommand.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

                            using (var locationReader = locationCommand.ExecuteReader())
                            {
                                while (locationReader.Read())
                                {
                                    var location = new Location
                                    {
                                        LocationID = (int)locationReader["LocationID"],
                                        CustomerID = (int)locationReader["CustomerID"],
                                        Address = (string)locationReader["Address"]
                                    };

                                    customer.Locations.Add(location);
                                }
                            }
                        }

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }

        // ------------------------------------------------------------------------------------------------------------

        private void SyncData()
        {
            var mssqlCustomers = FetchFromMSSQL();
            InsertOrUpdateCustomers(mssqlCustomers);
            lstLogs.Items.Add($"{DateTime.Now}: Data synchronized.");

            var customerData = FetchCustomersFromSQLite();
            dataGridCustomers.DataSource = customerData;

            dataGridCustomers.Columns["CustomerID"].HeaderText = "Customer ID";
            dataGridCustomers.Columns["Name"].HeaderText = "Name";
            dataGridCustomers.Columns["Email"].HeaderText = "Email";
            dataGridCustomers.Columns["Phone"].HeaderText = "Phone";
            dataGridCustomers.Columns["Locations"].HeaderText = "Locations";
            
        }

        // ------------------------------------------------------------------------------------------------------------

        private void InsertOrUpdateCustomers(List<Customer> customers)
        {
            using (var connection = new SQLiteConnection("Data Source=syncapp.sqlite;"))
            {
                connection.Open();

                foreach (var customer in customers)
                {
                    var existingCommand = new SQLiteCommand("SELECT * FROM Customer WHERE CustomerID = @CustomerID", connection);
                    existingCommand.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

                    // ************************************************************************************************

                    string customerChangeDetails = "";

                    // ************************************************************************************************

                    using (var existingCustomer = existingCommand.ExecuteReader())
                    {
                        if (existingCustomer.Read())
                        {
                            var existingName = existingCustomer["Name"].ToString();
                            var existingEmail = existingCustomer["Email"].ToString();
                            var existingPhone = existingCustomer["Phone"].ToString();

                            if (existingName != customer.Name)
                            {
                                customerChangeDetails += $"CustomerID {customer.CustomerID}: Name changed from '{existingName}' to '{customer.Name}';";
                            }
                            if (existingEmail != customer.Email)
                            {
                                customerChangeDetails += $"CustomerID {customer.CustomerID}: Email changed from '{existingEmail}' to '{customer.Email}'; ";
                            }
                            if (existingPhone != customer.Phone)
                            {
                                customerChangeDetails += $"CustomerID {customer.CustomerID}: Phone changed from '{existingPhone}' to '{customer.Phone}'; ";
                            }
                        }
                    }

                    // ************************************************************************************************

                    using (var command = new SQLiteCommand(@"
                            INSERT OR REPLACE INTO CUSTOMER (CustomerID, Name, Email, Phone)
                            VALUES (@CustomerID, @Name, @Email, @Phone)
                        ", connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        command.Parameters.AddWithValue("@Name", customer.Name);
                        command.Parameters.AddWithValue("@Email", customer.Email);
                        command.Parameters.AddWithValue("@Phone", customer.Phone);
                        command.ExecuteNonQuery();
                    }

                    // ************************************************************************************************

                    if (!string.IsNullOrEmpty(customerChangeDetails))
                    {
                        using (var logCommand = new SQLiteCommand("INSERT INTO SyncLog (SyncTime, ChangeDetails) VALUES (@SyncTime, @ChangeDetails)", connection))
                        {
                            logCommand.Parameters.AddWithValue("@SyncTime", DateTime.Now);
                            logCommand.Parameters.AddWithValue("@ChangeDetails", customerChangeDetails);
                            logCommand.ExecuteNonQuery();
                        }
                    }

                    // ************************************************************************************************

                    foreach (var location in customer.Locations)
                    {
                        using (var existingLocationCommand = new SQLiteCommand("SELECT * FROM Location WHERE LocationID = @LocationID", connection))
                        {
                            existingLocationCommand.Parameters.AddWithValue("@LocationID", location.LocationID);
                            string locationChangeDetails = "";

                            // ****************************************************************************************

                            using (var existingLocation = existingLocationCommand.ExecuteReader())
                            {
                                // Compare and log changes in location data
                                if (existingLocation.Read())
                                {
                                    var existingAddress = existingLocation["Address"].ToString();
                                    if (existingAddress != location.Address)
                                    {
                                        locationChangeDetails += $"LocationID {location.LocationID}: Address changed from '{existingAddress}' to '{location.Address}';";
                                    }
                                }
                            }

                            // ************************************************************************************************

                            using (var locationCommand = new SQLiteCommand(@"
                                INSERT OR REPLACE INTO Location (LocationID, CustomerID, Address)
                                VALUES (@LocationID, @CustomerID, @Address)
                            ", connection))
                            {
                                locationCommand.Parameters.AddWithValue("@LocationID", location.LocationID);
                                locationCommand.Parameters.AddWithValue("@CustomerID", location.CustomerID);
                                locationCommand.Parameters.AddWithValue("@Address", location.Address);
                                locationCommand.ExecuteNonQuery();
                            }

                            // ************************************************************************************************

                            if (!string.IsNullOrEmpty(locationChangeDetails))
                            {
                                using (var logCommand = new SQLiteCommand("INSERT INTO SyncLog (SyncTime, ChangeDetails) VALUES (@SyncTime, @ChangeDetails)", connection))
                                {
                                    logCommand.Parameters.AddWithValue("@SyncTime", DateTime.Now);
                                    logCommand.Parameters.AddWithValue("@ChangeDetails", locationChangeDetails);
                                    logCommand.ExecuteNonQuery();
                                }
                            }

                            // ************************************************************************************************
                        }
                    }
                }
            }
        }

        // ------------------------------------------------------------------------------------------------------------
        
        private DataTable FetchCustomersFromSQLite()
        {
            var customerTable = new DataTable();
 
            using (var connection = new SQLiteConnection("Data Source=syncapp.sqlite;"))
            {
                connection.Open();

                using (var command = new SQLiteCommand(@"
                    SELECT 
                        c.CustomerID, 
                        c.Name, 
                        c.Email, 
                        c.Phone,
                        GROUP_CONCAT(l.Address, ', ') AS locations
                    FROM Customer c
                    LEFT JOIN Location l ON c.CustomerID = l.CustomerID
                    GROUP BY c.CustomerID
                ", connection))
                {
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(customerTable);
                    }
                }
            }

            return customerTable;
        }
        
        // ------------------------------------------------------------------------------------------------------------

    }
}