using CursosLibres.Controllers;
using System;
using System.Windows.Forms;

namespace CursosLibres.Views
{
    public partial class FrmNuevoAlumno : Form
    {
        private AlumnosController controller = new AlumnosController();

        private TextBox txtNombre;
        private TextBox txtEmail;
        private Button btnGuardar;

        public FrmNuevoAlumno()
        {
            InitializeComponent();
            LoadControls();
        }

        private void InitializeComponent()
        {
            txtNombre = new TextBox();
            txtEmail = new TextBox();
            btnGuardar = new Button();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(12, 12);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(10, 23);
            txtNombre.TabIndex = 0;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(12, 41);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(10, 23);
            txtEmail.TabIndex = 1;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(12, 64);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // FrmNuevoAlumno
            // 
            ClientSize = new Size(284, 101);
            Controls.Add(txtNombre);
            Controls.Add(txtEmail);
            Controls.Add(btnGuardar);
            Name = "FrmNuevoAlumno";
            Text = "Nuevo Alumno";
            ResumeLayout(false);
            PerformLayout();
        }

        private void LoadControls()
        {
            this.Size = new Size(400, 200);
            this.Text = "Nuevo Alumno";
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblNombre = new Label { Text = "Nombre:", Location = new Point(10, 10), Size = new Size(80, 20) };
            this.Controls.Add(lblNombre);

            txtNombre = new TextBox { Location = new Point(100, 10), Width = 250, Name = "txtNombre" };
            this.Controls.Add(txtNombre);

            Label lblEmail = new Label { Text = "Email:", Location = new Point(10, 50), Size = new Size(80, 20) };
            this.Controls.Add(lblEmail);

            txtEmail = new TextBox { Location = new Point(100, 50), Width = 250, Name = "txtEmail" };
            this.Controls.Add(txtEmail);

            btnGuardar = new Button { Text = "Guardar", Location = new Point(150, 90), Name = "btnGuardar" };
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                controller.Crear(txtNombre.Text, txtEmail.Text);
                MessageBox.Show("Alumno creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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