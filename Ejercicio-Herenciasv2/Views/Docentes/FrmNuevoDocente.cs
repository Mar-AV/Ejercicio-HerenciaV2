using CursosLibres.Controllers;
using System;
using System.Windows.Forms;

namespace CursosLibres.Views
{
    public partial class FrmNuevoDocente : Form
    {
        private DocentesController controller = new DocentesController();

        private TextBox txtNombre;
        private TextBox txtEspecialidad;
        private Button btnGuardar;


        public FrmNuevoDocente()
        {

            LoadControls();
        }

        private void LoadControls()
        {
            this.Size = new Size(400, 200);
            this.Text = "Nuevo Docente";
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblNombre = new Label { Text = "Nombre:", Location = new Point(10, 10), Size = new Size(80, 20) };
            this.Controls.Add(lblNombre);

            txtNombre = new TextBox { Location = new Point(100, 10), Width = 250, Name = "txtNombre" };
            this.Controls.Add(txtNombre);

            Label lblEspecialidad = new Label { Text = "Especialidad:", Location = new Point(10, 50), Size = new Size(80, 20) };
            this.Controls.Add(lblEspecialidad);

            txtEspecialidad = new TextBox { Location = new Point(100, 50), Width = 250, Name = "txtEspecialidad" };
            this.Controls.Add(txtEspecialidad);

            btnGuardar = new Button { Text = "Guardar", Location = new Point(150, 90), Name = "btnGuardar" };
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                controller.Crear(txtNombre.Text, txtEspecialidad.Text);
                MessageBox.Show("Docente creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}