using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Model357App.View
{
    public partial class UpdateAdmin : Form
    {
        public static string _id { get; set; } = "0";
        public UpdateAdmin()
        {
            InitializeComponent();

            int x = 800;
            int y = 500;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Size = new Size(x, y);
            this.MaximizeBox = false;

            Label labelTittle = DinamicControls.CreateLabel("labelTittle", "Actualizar registro", (x - 750) / 2, (y / 2) - 230, 300, 40);
            labelTittle.Font = new Font("Consolas", 20, FontStyle.Bold);
            DataGridView dataGridView = DinamicControls.CreateDataGridView("dataGridViewUpdate", 40, 60, x - 100, y - y / 2 + 60);
            dataGridView.SelectionChanged += DataGridView_SelectionChanged;
            dataGridView.MultiSelect = false;

            Button buttonBack = DinamicControls.CreateButton("buttonBack", "Regresar", (x - 400) / 2, (y / 2) + 150, 150, 40);
            DinamicControls.CustomButton(buttonBack);
            buttonBack.Click += (s, e) => this.Close(); // Regresa al formulario anterior

            Button buttonUpdate = DinamicControls.CreateButton("buttonUpdate", "Actualizar", x / 2, (y / 2) + 150, 150, 40);
            DinamicControls.CustomButton(buttonUpdate);
            buttonUpdate.Click += buttonUpdate_Click;

            Controls.AddRange(new Control[]
            {
                labelTittle,
                dataGridView,
                buttonBack,
                buttonUpdate
            });
        }

        private void UpdateAdmin_Load(object sender, EventArgs e)
        {
            AdminCrud admin = new AdminCrud();
            admin.ListAdmins((DataGridView)Controls["dataGridViewUpdate"]);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea actualizar la persona seleccionada?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                MenuEvent menuEvent = new MenuEvent(this);
                UpdateAdminData updateForm = new UpdateAdminData();
                updateForm.Owner = this; // Establecer el propietario explícitamente
                menuEvent.OpenForm(updateForm, 800, 500);
            }

        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = MenuEvent.ReturnCurrentRow(this, "dataGridViewUpdate");
            _id = selectedRow.Cells[0].Value?.ToString();
        }

        public void ForceRefreshGrid()
        {
            // 1. Obtener referencia al DataGridView
            DataGridView dgv = (DataGridView)this.Controls["dataGridViewUpdate"];

            // 2. Deshabilitar temporalmente el control para evitar parpadeos
            dgv.SuspendLayout();

            try
            {
                // 3. Limpiar completamente el control
                dgv.DataSource = null;
                dgv.Columns.Clear();
                dgv.Rows.Clear();

                // 4. Volver a cargar los datos
                AdminCrud admin = new AdminCrud();
                admin.ListAdmins(dgv);

                // 5. Asegurar que la vista se refresque completamente
                dgv.Refresh();
            }
            finally
            {
                // 6. Rehabilitar el layout
                dgv.ResumeLayout();
            }

            // 7. Limpiar la selección
            if (dgv.Rows.Count > 0)
            {
                dgv.ClearSelection();
            }
        }

    }

    public partial class UpdateAdminData : Form
    {
        public UpdateAdminData()
        {
            int x = 800; int y = 500;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(x, y);
            MaximizeBox = false;
            MinimizeBox = false;

            Label titleLabel = DinamicControls.CreateLabel("labelTitle", "Nuevos datos", (x / 2) - 300, (y / 2) - 200, 300, 40);
            titleLabel.Font = new Font("Consolas", 20, FontStyle.Bold);

            Label labelName = DinamicControls.CreateLabel("labelName", "Nombre: ", (x / 2) - 350, (y / 2) - 100, 100, 40);
            labelName.TextAlign = ContentAlignment.TopRight;
            TextBox name = DinamicControls.CreateTextBox("textboxName", (x / 2) - 250, (y / 2) - 100, 200, 40);

            Label labelLastName = DinamicControls.CreateLabel("labelLastName", "Apellido: ", x / 2, (y / 2) - 100, 100, 40);
            labelLastName.TextAlign = ContentAlignment.TopRight;
            TextBox lastName = DinamicControls.CreateTextBox("textboxLastName", (x / 2) + 100, (y / 2) - 100, 200, 40);

            // campo de correo
            Label labelEmail = DinamicControls.CreateLabel("labelEmail", "Correo: ", (x / 2) - 350, (y / 2) - 50, 100, 40);
            labelEmail.TextAlign = ContentAlignment.TopRight;
            TextBox email = DinamicControls.CreateTextBox("textboxEmail", (x / 2) - 250, (y / 2) - 50, 200, 40);

            // campo de teléfono
            Label labelPhone = DinamicControls.CreateLabel("labelPhone", "Teléfono: ", x / 2, (y / 2) - 50, 100, 40);
            labelPhone.TextAlign = ContentAlignment.TopRight;
            TextBox phone = DinamicControls.CreateTextBox("textboxPhone", (x / 2) + 100, (y / 2) - 50, 200, 40);

            // campo de sexo
            Label labelSex = DinamicControls.CreateLabel("labelSex", "Sexo: ", (x / 2) - 350, (y / 2), 100, 40);
            labelSex.TextAlign = ContentAlignment.TopRight;
            ComboBox sex = DinamicControls.CrearComboBox("comboboxSex", (x / 2) - 250, (y / 2), 200, 40);
            sex.Items.Add("Masculino");
            sex.Items.Add("Femenino");
            sex.DropDownStyle = ComboBoxStyle.DropDownList; // establece solo lectura en el combo box

            // campo de edad
            Label labelAge = DinamicControls.CreateLabel("labelAge", "Edad: ", (x / 2), (y / 2), 100, 40);
            labelAge.TextAlign = ContentAlignment.TopRight;
            ComboBox age = DinamicControls.CrearComboBox("comboboxAge", (x / 2) + 100, (y / 2), 200, 40);
            addItemsComboBox(age, returnArrayStrNumbers());
            age.DropDownStyle = ComboBoxStyle.DropDownList; // establece solo lectura en el combo box

            Button buttonAccept = DinamicControls.CreateButton("buttonAccept", "Confirmar cambios", (x / 2) - 250 / 2, y / 2 + 100, 250, 40);
            DinamicControls.CustomButton(buttonAccept);
            buttonAccept.Click += buttonUpdate_Click;

            Controls.AddRange(new Control[]
            {
                titleLabel,
                labelName,
                name,
                labelLastName,
                lastName,
                labelEmail,
                email,
                labelPhone,
                phone,
                labelSex,
                sex,
                labelAge,
                age,
                buttonAccept
            });
        }
        private void UpdateAdminData_Load(object sender, EventArgs e)
        {
            // Aquí puedes cargar los datos del administrador seleccionado para actualizar
            // Por ejemplo, llenar los campos de texto con los datos actuales
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateAdmin parentForm = (UpdateAdmin)this.Owner;
                AdminCrud admin = new AdminCrud();

                string name = ((TextBox)Controls["textboxName"]).Text;
                string lastName = ((TextBox)Controls["textboxLastName"]).Text;
                string phone = ((TextBox)Controls["textboxPhone"]).Text;
                string email = ((TextBox)Controls["textboxEmail"]).Text;
                string sexo = ((ComboBox)this.Controls["comboboxSex"]).SelectedItem?.ToString();
                string edad = ((ComboBox)this.Controls["comboboxAge"]).SelectedItem?.ToString();

                if (parentForm != null)
                {
                    admin.UpdateAdmin(UpdateAdmin._id, name, lastName, phone, email, sexo, edad);
                    parentForm.ForceRefreshGrid(); // actualiza el datagridview del formulario padre

                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el registro. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string[] returnArrayStrNumbers()
        {
            string[] numbers = new string[25];
            int j = 0;
            for (int i = 16; i < 41; i++)
            {
                numbers[j] = Convert.ToString(i); // convierte los números en string para agregarlos
                j++;
            }
            return numbers;
        }
        private void addItemsComboBox(ComboBox comboBox, string[] items)
        {
            try
            {
                foreach (string item in items)
                {
                    // verificar si es nulo o vacio el elemento antes de agregar
                    if (!string.IsNullOrEmpty(item)) comboBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, al cargar opciones de combobox: {ex.Message}");
                throw;
            }
        }
    }
}
