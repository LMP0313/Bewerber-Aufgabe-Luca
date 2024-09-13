using System;
using System.Collections;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.SqlServer.Server;

namespace WinFormsApp {
    public partial class FormMenu : Form {
        private System.Windows.Forms.DataGridView Customer_Table;
        private System.Windows.Forms.Label Dashboard_Label;
        private System.Windows.Forms.Label Kunden_Label;
        private Button AddPet_Button;
        private Label PetList_Label;
        private DataGridView PetList_Table;
        private PictureBox PetPreview_PicuterBox;


        private Form PetPopup_Form;
        private Label PetPopupTitle_Label;
        private Label PetName_Label;
        private Label PetBirthday_Label;
        private Label PetChipnumber_Label;
        private Label PetImage_Label;
        private TextBox PetName_TextBox;
        private DateTimePicker PetBirthday_DateTimePicker;
        private TextBox PetChipnumber_TextBox;
        private PictureBox PetImage_PictureBox;
        private Button PetImageGetRandom_Button;
        private Button PetRegister_Button;
        private ComboBox BreedList_ComboBox;
        private Button PetNextImage_Button;

        private string currentImageUrl_String = "";
        private int currentCustomerId_int = 0;

        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            Dashboard_Label = new Label();
            Kunden_Label = new Label();
            Customer_Table = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)Customer_Table).BeginInit();
            SuspendLayout();
            // 
            // Dashboard_Label
            // 
            Dashboard_Label.AutoSize = true;
            Dashboard_Label.FlatStyle = FlatStyle.Flat;
            Dashboard_Label.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            Dashboard_Label.Location = new Point(12, 9);
            Dashboard_Label.Name = "Dashboard_Label";
            Dashboard_Label.Size = new Size(119, 30);
            Dashboard_Label.TabIndex = 0;
            Dashboard_Label.Text = "Dashboard";
            // 
            // Kunden_Label
            // 
            Kunden_Label.AutoSize = true;
            Kunden_Label.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            Kunden_Label.Location = new Point(12, 50);
            Kunden_Label.Name = "Kunden_Label";
            Kunden_Label.Size = new Size(66, 21);
            Kunden_Label.TabIndex = 1;
            Kunden_Label.Text = "Kunden";
            // 
            // Customer_Table
            // 
            Customer_Table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Customer_Table.Location = new Point(12, 74);
            Customer_Table.Name = "Customer_Table";
            Customer_Table.Size = new Size(461, 338);
            Customer_Table.TabIndex = 2;
            Customer_Table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Customer_Table.SelectionChanged += CustomerTableSelectionChanged;


            PetList_Label = new Label();
            PetList_Label.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            PetList_Label.Location = new Point(500, 50);
            PetList_Label.Size = new Size(100, 21);
            PetList_Label.Text = "Haustiere";

            PetList_Table = new DataGridView();
            PetList_Table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PetList_Table.Location = new Point(500, 74);
            PetList_Table.Size = new Size(500, 338);
            PetList_Table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PetList_Table.SelectionChanged += PetListSelectionChanged;

            PetPreview_PicuterBox = new PictureBox();
            PetPreview_PicuterBox.Location = new Point(500, 430);

            AddPet_Button = new Button();
            AddPet_Button.Text = "Haustier\nhinzufügen";
            AddPet_Button.Location = new Point(12, 430);
            AddPet_Button.Size = new Size(100, 100);
            AddPet_Button.Click += AddPetClick;

            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 700);
            Controls.Add(Customer_Table);
            Controls.Add(Kunden_Label);
            Controls.Add(Dashboard_Label);

            Controls.Add(PetList_Table);
            Controls.Add(PetList_Label);
            Controls.Add(PetPreview_PicuterBox);
            Controls.Add(AddPet_Button);


            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMenu";
            Text = "TecPets GmbH";
            Load += FormMenu_Load;
            ((System.ComponentModel.ISupportInitialize)Customer_Table).EndInit();
            ResumeLayout(false);
            PerformLayout();

            InitPopUp();
        }
        private void InitPopUp() {

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));

            PetPopupTitle_Label = new Label();
            PetPopupTitle_Label.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            PetPopupTitle_Label.Location = new Point(10, 10);
            PetPopupTitle_Label.Size = new Size(500, 30);

            PetName_Label = new Label();
            PetName_Label.Location = new Point(10, 40);
            PetName_Label.Size = new Size(100, 20);
            PetName_Label.Text = "Name:";

            PetBirthday_Label = new Label();
            PetBirthday_Label.Location = new Point(10, 70);
            PetBirthday_Label.Size = new Size(100, 20);
            PetBirthday_Label.Text = "Geburtstag:";

            PetChipnumber_Label = new Label();
            PetChipnumber_Label.Location = new Point(10, 100);
            PetChipnumber_Label.Size = new Size(100, 20);
            PetChipnumber_Label.Text = "Chipnummer:";

            PetImage_Label = new Label();
            PetImage_Label.Location = new Point(10, 130);
            PetImage_Label.Size = new Size(100, 20);
            PetImage_Label.Text = "Bild:";

            PetName_TextBox = new TextBox();
            PetName_TextBox.Location = new Point(120, 40);
            PetName_TextBox.Size = new Size(200, 1);

            PetBirthday_DateTimePicker = new DateTimePicker();
            PetBirthday_DateTimePicker.Location = new Point(120, 70);
            PetBirthday_DateTimePicker.Format = DateTimePickerFormat.Short;
            PetBirthday_DateTimePicker.Size = new Size(200, 1);
            PetBirthday_DateTimePicker.MaxDate = DateTime.Today;

            PetChipnumber_TextBox = new TextBox();
            PetChipnumber_TextBox.Location = new Point(120, 100);
            PetChipnumber_TextBox.Size = new Size(200, 1);

            PetImage_PictureBox = new PictureBox();
            PetImage_PictureBox.Location = new Point(10, 160);
            PetImage_PictureBox.LoadCompleted += PetImageLoadComplete;

            PetImageGetRandom_Button = new Button();
            PetImageGetRandom_Button.Location = new Point(450, 130);
            PetImageGetRandom_Button.Size = new Size(100, 23);
            PetImageGetRandom_Button.Text = "Zufälliges Bild laden";
            PetImageGetRandom_Button.Click += PetImageGetRandomClick;

            PetRegister_Button = new Button();
            PetRegister_Button.Location = new Point(350, 40);
            PetRegister_Button.Size = new Size(80, 80);
            PetRegister_Button.Text = "Haustier\nhinzufügen";
            PetRegister_Button.Click += PetRegisterClick;

            BreedList_ComboBox = new();
            BreedList_ComboBox.Location = new Point(120, 130);
            BreedList_ComboBox.Size = new Size(200, 23);
            BreedList_ComboBox.SelectedValueChanged += BreedListSelectionValueChanged;
            ListAllBreeds();

            PetNextImage_Button = new Button();
            PetNextImage_Button.Location = new Point(350, 130);
            PetNextImage_Button.Size = new Size(100, 23);
            PetNextImage_Button.Text = "Anderes Bild";
            PetNextImage_Button.Click += PetNextImageClick;


            PetPopup_Form = new Form();
            PetPopup_Form.ClientSize = new Size(800, 450);
            PetPopup_Form.Icon = (Icon)resources.GetObject("$this.Icon");
            PetPopup_Form.Text = "TecPets GmbH";
            PetPopup_Form.Controls.Add(PetName_Label);
            PetPopup_Form.Controls.Add(PetBirthday_Label);
            PetPopup_Form.Controls.Add(PetChipnumber_Label);
            PetPopup_Form.Controls.Add(PetImage_Label);
            PetPopup_Form.Controls.Add(PetPopupTitle_Label);
            PetPopup_Form.Controls.Add(PetName_TextBox);
            PetPopup_Form.Controls.Add(PetBirthday_DateTimePicker);
            PetPopup_Form.Controls.Add(PetChipnumber_TextBox);
            PetPopup_Form.Controls.Add(PetImage_PictureBox);
            PetPopup_Form.Controls.Add(PetImageGetRandom_Button);
            PetPopup_Form.Controls.Add(PetRegister_Button);
            PetPopup_Form.Controls.Add(BreedList_ComboBox);
            PetPopup_Form.Controls.Add(PetNextImage_Button);
        }
        private void CustomerTableSelectionChanged(object sender, EventArgs a) {
            DataGridViewSelectedRowCollection rows = Customer_Table.SelectedRows;
            AddPet_Button.Enabled = rows.Count == 1;
            if (rows.Count > 0) UpdatePetList();
        }
        private void PetListSelectionChanged(object sender, EventArgs a) {
            DataGridViewSelectedRowCollection rows = PetList_Table.SelectedRows;
            if (rows.Count == 1) {
                LoadPetImage();
            }
        }
        private void AddPetClick(object sender, EventArgs a) {
            DataGridViewSelectedRowCollection rows = Customer_Table.SelectedRows;
            currentCustomerId_int = Int32.Parse(rows[0].Cells["CustomerId"].Value.ToString());
            string name = rows[0].Cells["FirstName"].Value.ToString();
            name += " " + rows[0].Cells["LastName"].Value.ToString();
            PetPopupTitle_Label.Text = $"Haustier für {name} hinzufügen";
            PetPopup_Form.ShowDialog();
        }
        private void PetImageLoadComplete(object sender, AsyncCompletedEventArgs a) {
            PetImage_PictureBox.Size = PetImage_PictureBox.Image.Size;
        }
        private async void PetImageGetRandomClick(object sender, EventArgs a) {
            PetImageGetRandom_Button.Enabled = false;
            
            try {
                currentImageUrl_String = await GetImageURL(@"https://dog.ceo/api/breeds/image/random/1");
                PetImage_PictureBox.LoadAsync(currentImageUrl_String);
                PetImageGetRandom_Button.Enabled = true;
            } catch (Exception e) {
                MessageBox.Show("Error: " + e.Message, "HTTP Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PetImageGetRandom_Button.Enabled = true;
            }
        }
        private void PetRegisterClick(object sender, EventArgs a) {
            AddPetToCustomer();
        }
        private async void ListAllBreeds() {
            HttpClient client = new HttpClient();
            try {
                string response = await client.GetStringAsync(@"https://dog.ceo/api/breeds/list/all");
                JsonObject json = JsonObject.Parse(response).AsObject();
                if (json["status"] != null && json["status"].AsValue().ToString() == "success") {
                    JsonObject breeds = json["message"].AsObject();
                    IEnumerator breedEnumerator =  breeds.GetEnumerator();
                    KeyValuePair<string, JsonNode> breedValues;
                    JsonArray subBreed;
                    while(breedEnumerator.MoveNext()) {
                        breedValues = (KeyValuePair<string, JsonNode>)breedEnumerator.Current;
                        BreedList_ComboBox.Items.Add(breedValues.Key);
                        subBreed = breedValues.Value.AsArray();
                        for(int i = 0; i < subBreed.Count; i++) {
                            BreedList_ComboBox.Items.Add(breedValues.Key + "-" + subBreed[i]);
                        }
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("Error: " + e.Message, "HTTP Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private async void LoadNewBreedImage() {
            string breed = BreedList_ComboBox.GetItemText(BreedList_ComboBox.SelectedItem);
            if(breed == "") {
                MessageBox.Show("Bitte Hundeart auswählen.");
                return;
            }
            currentImageUrl_String = await GetImageURL($@"https://dog.ceo/api/breed/{breed}/images/random/1");
            PetImage_PictureBox.LoadAsync(currentImageUrl_String);
        }
        private void BreedListSelectionValueChanged(object sender, EventArgs a) {
            LoadNewBreedImage();
        }
        private void PetNextImageClick(object sender, EventArgs a) {
            LoadNewBreedImage();
        }
        private async Task<string> GetImageURL(string baseURL) {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(baseURL);
            JsonObject json = JsonObject.Parse(response).AsObject();
            if (json["status"] != null && json["status"].AsValue().ToString() == "success") {
                JsonArray urls = json["message"].AsArray();
                if (urls.Count > 0 && urls[0].AsValue().ToString() != "") {
                    return urls[0].AsValue().ToString();
                }
            }
            return "";
        }
    }
}
