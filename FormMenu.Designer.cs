using System;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class FormMenu : Form
    {
        private System.Windows.Forms.DataGridView Customer_Table;
        private System.Windows.Forms.Label Dashboard_Label;
        private System.Windows.Forms.Label Kunden_Label;

        private void InitializeComponent()
        {
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
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Customer_Table);
            Controls.Add(Kunden_Label);
            Controls.Add(Dashboard_Label);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMenu";
            Text = "TecPets GmbH";
            Load += FormMenu_Load;
            ((System.ComponentModel.ISupportInitialize)Customer_Table).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
