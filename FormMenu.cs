using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class FormMenu : Form
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Repos\Bewerber-Aufgabe\DB\Database.mdf;Integrated Security=True";
        private readonly string query = "SELECT CustomerID, FirstName, LastName, Email FROM Customer";

        public FormMenu()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    Customer_Table.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
