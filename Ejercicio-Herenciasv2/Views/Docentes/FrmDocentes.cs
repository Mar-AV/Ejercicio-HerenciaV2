using CursosLibres.Controllers;
using CursosLibres.Models;
using CursosLibres.Views.Docentes;  
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
    public partial class FrmDocentes : Form
    {
        private DocentesController controller = new DocentesController();
        private BindingList<Docente> allDocentes;
        private List<Docente> filteredDocentes;
        private int currentPage = 1;
        private int pageSize = 10;

        private TextBox txtBusqueda;
        private NumericUpDown nudPageSize;
        private Button btnAnterior;
        private Button btnSiguiente;
        private Label lblRegistros;
        private DataGridView dgvDocentes;
        private Button btnNuevo;
        private Button btnCursosPorDocente;


        private void InitializeComponent()
        {

        }

        private void LoadControls()
        {
            this.Size = new Size(800, 600);
            this.Text = "Docentes Actuales";

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

            dgvDocentes = new DataGridView { Location = new Point(10, 50), Size = new Size(760, 500), ReadOnly = true };
            dgvDocentes.Columns.Add("Nombre", "Nombre");
            dgvDocentes.Columns.Add("Especialidad", "Especialidad");
            this.Controls.Add(dgvDocentes);

            btnNuevo = new Button { Text = "Nuevo Docente", Location = new Point(10, 560) };
            btnNuevo.Click += BtnNuevo_Click;
            this.Controls.Add(btnNuevo);

            btnCursosPorDocente = new Button { Text = "Cursos por Docente", Location = new Point(150, 560) };
            btnCursosPorDocente.Click += BtnCursosPorDocente_Click;
            this.Controls.Add(btnCursosPorDocente);
        }

        private void LoadData()
        {
            allDocentes = controller.Listar();
            FilterAndPaginate();
        }

        private void FilterAndPaginate()
        {
            string search = txtBusqueda.Text.ToLower();
            filteredDocentes = allDocentes.Where(d => d.Nombre.ToLower().Contains(search) || d.Especialidad.ToLower().Contains(search)).ToList();

            int totalPages = (int)Math.Ceiling((double)filteredDocentes.Count / pageSize);
            btnAnterior.Enabled = currentPage > 1;
            btnSiguiente.Enabled = currentPage < totalPages;

            lblRegistros.Text = $"{pageSize} registros por página";

            var pageData = filteredDocentes.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            dgvDocentes.Rows.Clear();
            foreach (var docente in pageData)
            {
                dgvDocentes.Rows.Add(docente.Nombre, docente.Especialidad);
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
            new FrmNuevoDocente().ShowDialog();
            LoadData();
        }

        private void BtnCursosPorDocente_Click(object sender, EventArgs e)
        {
            try
            {
                new FrmCursosPorDocente().ShowDialog(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Cursos por Docente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}