using CursosLibres.Controllers;
using CursosLibres.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CursosLibres.Views
{
    public partial class FrmNuevoCurso : Form
    {
        private CursosController cursosController = new CursosController();
        private DocentesController docentesController = new DocentesController();
        private List<Sesion> sesionesTemp = new List<Sesion>();

        private TextBox txtTitulo;
        private TextBox txtCategoria;
        private NumericUpDown nudCupo;
        private NumericUpDown nudCosto;
        private ComboBox cmbDocente;
        private ComboBox cmbModalidad;
        private Panel pnlPresencial;  // Variable local para paneles
        private Panel pnlVirtual;
        private Panel pnlHibrido;
        private TextBox txtCampus;
        private TextBox txtSalon;
        private TextBox txtPlataforma;
        private TextBox txtEnlace;
        private TextBox txtCampusH;
        private TextBox txtSalonH;
        private TextBox txtEnlaceH;
        private DateTimePicker dtpInicio;
        private NumericUpDown nudDuracionHoras;
        private NumericUpDown nudDuracionMin;
        private Button btnAgregarSesion;
        private DataGridView dgvSesiones;
        private Button btnGuardar;

        public FrmNuevoCurso()
        {
            InitializeComponent();  // Esto se genera en Designer.cs
            LoadControls();
            LoadData();
        }

        private void LoadControls()
        {
            this.Size = new Size(600, 700);
            this.Text = "Nuevo Curso";  // Esto se inicializa en Designer, pero se puede sobrescribir
            this.StartPosition = FormStartPosition.CenterParent;

            // Campos básicos
            Label lblTitulo = new Label { Text = "Título:", Location = new Point(10, 10), Size = new Size(80, 20) };
            this.Controls.Add(lblTitulo);
            txtTitulo = new TextBox { Location = new Point(100, 10), Width = 400, Name = "txtTitulo" };
            this.Controls.Add(txtTitulo);

            Label lblCategoria = new Label { Text = "Categoría:", Location = new Point(10, 50), Size = new Size(80, 20) };
            this.Controls.Add(lblCategoria);
            txtCategoria = new TextBox { Location = new Point(100, 50), Width = 400, Name = "txtCategoria" };
            this.Controls.Add(txtCategoria);

            Label lblCupo = new Label { Text = "Cupo:", Location = new Point(10, 90), Size = new Size(80, 20) };
            this.Controls.Add(lblCupo);
            nudCupo = new NumericUpDown { Location = new Point(100, 90), Minimum = 0, Maximum = 1000, Name = "nudCupo" };
            this.Controls.Add(nudCupo);

            Label lblCosto = new Label { Text = "Costo:", Location = new Point(10, 130), Size = new Size(80, 20) };
            this.Controls.Add(lblCosto);
            nudCosto = new NumericUpDown { Location = new Point(100, 130), Minimum = 0, DecimalPlaces = 2, Name = "nudCosto" };
            this.Controls.Add(nudCosto);

            Label lblDocente = new Label { Text = "Docente:", Location = new Point(10, 170), Size = new Size(80, 20) };
            this.Controls.Add(lblDocente);
            cmbDocente = new ComboBox { Location = new Point(100, 170), Width = 400, DropDownStyle = ComboBoxStyle.DropDownList, Name = "cmbDocente" };
            this.Controls.Add(cmbDocente);

            Label lblModalidad = new Label { Text = "Modalidad:", Location = new Point(10, 210), Size = new Size(80, 20) };
            this.Controls.Add(lblModalidad);
            cmbModalidad = new ComboBox { Location = new Point(100, 210), Width = 400, DropDownStyle = ComboBoxStyle.DropDownList, Name = "cmbModalidad" };
            cmbModalidad.Items.AddRange(new[] { "Presencial", "Virtual", "Híbrido" });
            cmbModalidad.SelectedIndexChanged += CmbModalidad_SelectedIndexChanged;
            this.Controls.Add(cmbModalidad);

            // Paneles condicionales (creados como variables locales para evitar nulls)
            pnlPresencial = new Panel { Location = new Point(10, 250), Size = new Size(550, 80), Visible = false, Name = "pnlPresencial" };
            Label lblCampus = new Label { Text = "Campus:", Location = new Point(0, 0), Size = new Size(80, 20) };
            pnlPresencial.Controls.Add(lblCampus);
            txtCampus = new TextBox { Location = new Point(90, 0), Width = 400, Name = "txtCampus" };
            pnlPresencial.Controls.Add(txtCampus);
            Label lblSalon = new Label { Text = "Salón:", Location = new Point(0, 40), Size = new Size(80, 20) };
            pnlPresencial.Controls.Add(lblSalon);
            txtSalon = new TextBox { Location = new Point(90, 40), Width = 400, Name = "txtSalon" };
            pnlPresencial.Controls.Add(txtSalon);
            this.Controls.Add(pnlPresencial);

            pnlVirtual = new Panel { Location = new Point(10, 250), Size = new Size(550, 80), Visible = false, Name = "pnlVirtual" };
            Label lblPlataforma = new Label { Text = "Plataforma:", Location = new Point(0, 0), Size = new Size(80, 20) };
            pnlVirtual.Controls.Add(lblPlataforma);
            txtPlataforma = new TextBox { Location = new Point(90, 0), Width = 400, Name = "txtPlataforma" };
            pnlVirtual.Controls.Add(txtPlataforma);
            Label lblEnlace = new Label { Text = "Enlace:", Location = new Point(0, 40), Size = new Size(80, 20) };
            pnlVirtual.Controls.Add(lblEnlace);
            txtEnlace = new TextBox { Location = new Point(90, 40), Width = 400, Name = "txtEnlace" };
            pnlVirtual.Controls.Add(txtEnlace);
            this.Controls.Add(pnlVirtual);

            pnlHibrido = new Panel { Location = new Point(10, 250), Size = new Size(550, 120), Visible = false, Name = "pnlHibrido" };
            Label lblCampusH = new Label { Text = "Campus:", Location = new Point(0, 0), Size = new Size(80, 20) };
            pnlHibrido.Controls.Add(lblCampusH);
            txtCampusH = new TextBox { Location = new Point(90, 0), Width = 400, Name = "txtCampusH" };
            pnlHibrido.Controls.Add(txtCampusH);
            Label lblSalonH = new Label { Text = "Salón:", Location = new Point(0, 40), Size = new Size(80, 20) };
            pnlHibrido.Controls.Add(lblSalonH);
            txtSalonH = new TextBox { Location = new Point(90, 40), Width = 400, Name = "txtSalonH" };
            pnlHibrido.Controls.Add(txtSalonH);
            Label lblEnlaceH = new Label { Text = "Enlace:", Location = new Point(0, 80), Size = new Size(80, 20) };
            pnlHibrido.Controls.Add(lblEnlaceH);
            txtEnlaceH = new TextBox { Location = new Point(90, 80), Width = 400, Name = "txtEnlaceH" };
            pnlHibrido.Controls.Add(txtEnlaceH);
            this.Controls.Add(pnlHibrido);

            // Sesiones
            GroupBox grpSesiones = new GroupBox { Text = "Sesiones", Location = new Point(10, 380), Size = new Size(550, 180), Name = "grpSesiones" };
            dtpInicio = new DateTimePicker { Location = new Point(10, 20), Width = 150, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm", Name = "dtpInicio" };
            grpSesiones.Controls.Add(dtpInicio);
            Label lblDuracion = new Label { Text = "Duración (H:M):", Location = new Point(170, 25), Size = new Size(100, 20) };
            grpSesiones.Controls.Add(lblDuracion);
            nudDuracionHoras = new NumericUpDown { Location = new Point(280, 20), Minimum = 0, Maximum = 24, Name = "nudDuracionHoras" };
            grpSesiones.Controls.Add(nudDuracionHoras);
            nudDuracionMin = new NumericUpDown { Location = new Point(340, 20), Minimum = 0, Maximum = 59, Name = "nudDuracionMin" };
            grpSesiones.Controls.Add(nudDuracionMin);
            btnAgregarSesion = new Button { Text = "Agregar", Location = new Point(420, 20), Name = "btnAgregarSesion" };
            btnAgregarSesion.Click += BtnAgregarSesion_Click;
            grpSesiones.Controls.Add(btnAgregarSesion);
            dgvSesiones = new DataGridView { Location = new Point(10, 60), Size = new Size(530, 110), ReadOnly = true, Name = "dgvSesiones" };
            dgvSesiones.Columns.Add("Inicio", "Inicio");
            dgvSesiones.Columns.Add("Duracion", "Duración");
            grpSesiones.Controls.Add(dgvSesiones);
            this.Controls.Add(grpSesiones);

            btnGuardar = new Button { Text = "Guardar", Location = new Point(200, 570), Name = "btnGuardar" };
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);
        }

        private void LoadData()
        {
            var docentes = docentesController.Listar();
            cmbDocente.DataSource = docentes;
            cmbDocente.DisplayMember = "Nombre";
            cmbDocente.ValueMember = "Id";
        }

        private void CmbModalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlPresencial.Visible = false;
            pnlVirtual.Visible = false;
            pnlHibrido.Visible = false;

            string mod = cmbModalidad.SelectedItem?.ToString();
            switch (mod)
            {
                case "Presencial":
                    pnlPresencial.Visible = true;
                    break;
                case "Virtual":
                    pnlVirtual.Visible = true;
                    break;
                case "Híbrido":
                    pnlHibrido.Visible = true;
                    break;
            }
        }

        private void BtnAgregarSesion_Click(object sender, EventArgs e)
        {
            DateTime inicio = dtpInicio.Value;
            TimeSpan duracion = new TimeSpan((int)nudDuracionHoras.Value, (int)nudDuracionMin.Value, 0);
            sesionesTemp.Add(new Sesion(inicio, duracion));
            dgvSesiones.Rows.Add(inicio.ToString("g"), duracion.ToString(@"hh\:mm"));
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitulo.Text) || string.IsNullOrWhiteSpace(txtCategoria.Text) || cmbDocente.SelectedItem == null || cmbModalidad.SelectedItem == null)
                {
                    throw new ArgumentException("Campos requeridos faltantes (Título, Categoría, Docente, Modalidad).");
                }

                if (nudCupo.Value < 0 || nudCosto.Value < 0)
                {
                    throw new ArgumentException("Cupo y Costo deben ser ≥ 0.");
                }

                string titulo = txtTitulo.Text.Trim();
                string cat = txtCategoria.Text.Trim();
                int cupo = (int)nudCupo.Value;
                decimal costo = nudCosto.Value;
                Docente d = (Docente)cmbDocente.SelectedItem;
                string mod = cmbModalidad.SelectedItem.ToString();

                Curso curso = null;

                if (mod == "Presencial")
                {
                    if (string.IsNullOrWhiteSpace(txtCampus.Text) || string.IsNullOrWhiteSpace(txtSalon.Text))
                        throw new ArgumentException("Campos presenciales requeridos (Campus, Salón).");
                    cursosController.CrearPresencial(titulo, cat, cupo, costo, d, txtCampus.Text, txtSalon.Text);
                    curso = cursosController.Listar().Last();
                }
                else if (mod == "Virtual")
                {
                    if (string.IsNullOrWhiteSpace(txtPlataforma.Text) || string.IsNullOrWhiteSpace(txtEnlace.Text))
                        throw new ArgumentException("Campos virtuales requeridos (Plataforma, Enlace).");
                    // Corrección CS1503: Pasa string directamente (el controlador crea el Uri internamente)
                    cursosController.CrearVirtual(titulo, cat, cupo, costo, d, txtPlataforma.Text, txtEnlace.Text);
                    curso = cursosController.Listar().Last();
                }
                else if (mod == "Híbrido")
                {
                    if (string.IsNullOrWhiteSpace(txtCampusH.Text) || string.IsNullOrWhiteSpace(txtSalonH.Text) || string.IsNullOrWhiteSpace(txtEnlaceH.Text))
                        throw new ArgumentException("Campos híbridos requeridos (Campus, Salón, Enlace).");
                    // Corrección CS1503: Pasa string directamente
                    cursosController.CrearHibrido(titulo, cat, cupo, costo, d, txtCampusH.Text, txtSalonH.Text, txtEnlaceH.Text);
                    curso = cursosController.Listar().Last();
                }

                foreach (var ses in sesionesTemp)
                {
                    cursosController.AgregarSesion(curso, ses.Inicio, ses.Duracion);
                }

                MessageBox.Show("Curso creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Solución para CS0103: El nombre 'InitializeComponent' no existe en el contexto actual
        // Agrega el siguiente método vacío si no tienes un archivo Designer.cs asociado.
        // Si tienes un archivo Designer.cs, asegúrate de que esté correctamente vinculado y que el método exista.

        private void InitializeComponent()
        {
            // Este método normalmente es generado automáticamente por el diseñador de Visual Studio.
            // Si no usas el diseñador, puedes dejarlo vacío o inicializar controles aquí si lo prefieres.
        }
    }
}