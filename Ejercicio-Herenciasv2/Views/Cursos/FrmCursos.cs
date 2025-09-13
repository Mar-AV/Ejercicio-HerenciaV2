using CursosLibres.Controllers;
using CursosLibres.Data;
using CursosLibres.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CursosLibres.Views
{
    public partial class FrmCursos : Form
    {
        private CursosController controller = new CursosController();
        private BindingList<Curso> allCursos;
        private List<Curso> filteredCursos;
        private int currentPage = 1;
        private int pageSize = 10;

        private TextBox txtBusqueda;
        private NumericUpDown nudPageSize;
        private Button btnAnterior;
        private Button btnSiguiente;
        private Label lblRegistros;
        private DataGridView dgvCursos;
        private Button btnNuevo;

        public FrmCursos()
        {
            InitializeComponent();
            LoadControls();
            LoadData();
        }

        private void LoadControls()
        {
            this.Size = new Size(800, 600);
            this.Text = "Cursos Actuales";

            txtBusqueda = new TextBox { Location = new Point(10, 10), Width = 200 };
            txtBusqueda.TextChanged += TxtBusqueda_TextChanged;
            this.Controls.Add(txtBusqueda);

            nudPageSize = new NumericUpDown { Location = new Point(220, 10), Value = 10, Minimum = 1, Maximum = 50 };
            nudPageSize.ValueChanged += NudPageSize_ValueChanged;
            this.Controls.Add(nudPageSize);

            btnAnterior = new Button { Text = "Anterior", Location = new Point(330, 10) };
            btnAnterior.Click += BtnAnterior_Click;
            this.Controls.Add(btnAnterior);

            btnSiguiente = new Button { Text = "Siguiente", Location = new Point(420, 10) };
            btnSiguiente.Click += BtnSiguiente_Click;
            this.Controls.Add(btnSiguiente);

            lblRegistros = new Label { Text = "10 registros por página", Location = new Point(520, 15) };
            this.Controls.Add(lblRegistros);

            dgvCursos = new DataGridView { Location = new Point(10, 50), Size = new Size(760, 500), ReadOnly = true };

            dgvCursos.Columns.Add("Modalidad", "Modalidad");
            dgvCursos.Columns.Add("Docente", "Docente");
            dgvCursos.Columns.Add("CupoDisponible", "Cupo Disponible");
            dgvCursos.Columns.Add("ProximaSesion", "Próxima Sesión");
            this.Controls.Add(dgvCursos);

            btnNuevo = new Button { Text = "Nuevo Curso", Location = new Point(10, 560) };
            btnNuevo.Click += BtnNuevo_Click;
            this.Controls.Add(btnNuevo);
        }

        private void LoadData()
        {
            allCursos = controller.Listar();
            FilterAndPaginate();
        }

        private void FilterAndPaginate()
        {
            string search = txtBusqueda.Text.ToLower();
            filteredCursos = allCursos.Where(c => c.Titulo.ToLower().Contains(search) || c.Categoria.ToLower().Contains(search) || c.Docente.Nombre.ToLower().Contains(search)).ToList();

            int totalPages = (int)Math.Ceiling((double)filteredCursos.Count / pageSize);
            btnAnterior.Enabled = currentPage > 1;
            btnSiguiente.Enabled = currentPage < totalPages;

            lblRegistros.Text = $"{pageSize} registros por página";

            var pageData = filteredCursos.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            dgvCursos.Rows.Clear();
            foreach (var curso in pageData)
            {
                int cupoDisp = curso.CupoDisponible(InMemoryDb.Confirmados(curso.Id));
                string proxSes = curso.ProximaSesion?.ToString("g") ?? "Ninguna";

                dgvCursos.Rows.Add(curso.Modalidad, curso.Docente.Nombre, cupoDisp, proxSes);
            }
        }

        private void TxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            FilterAndPaginate();
        }

        private void NudPageSize_ValueChanged(object sender, EventArgs e)
        {
            pageSize = (int)nudPageSize.Value;
            currentPage = 1;
            FilterAndPaginate();
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            currentPage--;
            FilterAndPaginate();
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            currentPage++;
            FilterAndPaginate();
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            new FrmNuevoCurso().ShowDialog();
            LoadData();
        }

        private void frmCursos_Load(object sender, EventArgs e)
        {

            CargarCursosActuales();
        }

        private void CargarCursosActuales()
        {

            List<Curso> cursosActuales = CursosController.ObtenerCursosActuales();
            dgvCursos.DataSource = cursosActuales;
        }
    }
}