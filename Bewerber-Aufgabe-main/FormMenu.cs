using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;


namespace WinFormsApp {
    public partial class FormMenu : Form {
        private readonly string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetFullPath(@"..\..\..\DB\Database.mdf")};Integrated Security=True";
        private readonly string query = "SELECT CustomerID, FirstName, LastName, Email FROM Customer";

        public FormMenu() {
            InitializeComponent();
        }

        private void LoadData() {
            using SqlConnection conn = new(connectionString);
            try {
                conn.Open();
                SqlDataAdapter dataAdapter = new(query, conn);
                DataTable dataTable = new();
                dataAdapter.Fill(dataTable);
                Customer_Table.DataSource = dataTable;
            } catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message, "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddPetToCustomer() {
            string error = "";
            string name = PetName_TextBox.Text.Trim();
            string chipnumber = PetChipnumber_TextBox.Text.Trim();
            DateTime date = PetBirthday_DateTimePicker.Value.Date;
            if (name == "") error += "Name fehlt\n";
            if (chipnumber == "") error += "Chipnummer fehlt\n";
            if (error != "") {
                MessageBox.Show("Haustier kann aus folgenden Gründen nicht hinzugefügt werden:\n" + error, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using SqlConnection conn = new(connectionString);
            try {
                SqlCommand command = new(
                    "INSERT INTO Pet VALUES(@Name, @Birthday, @ChipNumber, @Image, @CustomerId);",
                    conn
                );
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                command.Parameters.Add("@Birthday", SqlDbType.Date).Value = date;
                command.Parameters.Add("@ChipNumber", SqlDbType.NVarChar).Value = chipnumber;
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = currentCustomerId_int;
                using (var ms = new MemoryStream()) {
                    PetImage_PictureBox.Image.Save(ms, PetImage_PictureBox.Image.RawFormat);
                    command.Parameters.Add("@Image", SqlDbType.Image).Value = ms.ToArray();
                }
                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Haustier erfolgreich hinzugefügt.");
            } catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message, "Konnte Eintrag nicht erstellen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMenu_Load(object sender, EventArgs e) {
            LoadData();
        }

        private void UpdatePetList() {
            DataGridViewSelectedRowCollection rows = Customer_Table.SelectedRows;
            if(rows.Count == 0) return;
            string customerIds = "";
            for (int i = 0; i < rows.Count; i++) {
                customerIds += rows[i].Cells["CustomerId"].Value.ToString() + ",";
            }
            customerIds = customerIds.Substring(0, customerIds.Length - 1);
            string query;
            if (rows.Count == 1) {
                query = @"SELECT PetId AS ID, Name, Birthday AS Geburtstag, ChipNumber AS Chipnummer FROM
                    PET AS P WHERE P.CustomerId = " + customerIds;
            } else {
                query = @"SELECT PetId AS ID, Name, Birthday AS Geburtstag, ChipNumber AS Chipnummer, FirstName + ' ' + LastName AS Besitzer FROM
                    Pet AS P INNER JOIN Customer AS C ON (C.CustomerId = P.CustomerId) WHERE C.CustomerId IN(" + customerIds + ");";
            }

            using SqlConnection conn = new(connectionString);
            try {
                conn.Open();
                SqlDataAdapter dataAdapter = new(query, conn);
                DataTable dataTable = new();
                dataAdapter.Fill(dataTable);
                PetList_Table.DataSource = dataTable;
            } catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message, "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadPetImage() {
            DataGridViewSelectedRowCollection rows = PetList_Table.SelectedRows;
            if(rows.Count != 1) return;
            string petId = rows[0].Cells["ID"].Value.ToString();
            string query = "SELECT Image FROM Pet WHERE PetID = " + petId;

            using SqlConnection conn = new(connectionString);
            try {
                conn.Open();
                SqlDataAdapter dataAdapter = new(query, conn);
                DataTable dataTable = new();
                dataAdapter.Fill(dataTable);
                Byte[] blob = (Byte[])dataTable.Rows[0]["Image"];
                MemoryStream ms = new(blob);
                PetPreview_PicuterBox.Image = Image.FromStream(ms);
                PetPreview_PicuterBox.Size = PetPreview_PicuterBox.Image.Size;
            } catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message, "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
