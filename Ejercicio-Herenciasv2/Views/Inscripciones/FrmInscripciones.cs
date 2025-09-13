using CursosLibres.Controllers;
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
    public partial class FrmInscripciones : Form
    {
        private AlumnosController alumnosController = new AlumnosController();
        private CursosController cursosController = new CursosController();
        private InscripcionesController inscripcionesController = new InscripcionesController();

        private ComboBox cmbAlumno;
        private ComboBox cmbCurso;
        private Button btnAsignar;
        private Label lblRespuesta;
        private DataGridView dgvInscripciones;

        public FrmInscripciones()
        {
            InitializeComponent();
            LoadControls();
            LoadData();
        }

        private void LoadControls()
        {
            this.Size = new Size(800, 600);
            this.Text = "Inscripciones";

            cmbAlumno = new ComboBox { Location = new Point(10, 10), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            this.Controls.Add(cmbAlumno);

            cmbCurso = new ComboBox { Location = new Point(220, 10), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCurso.SelectedIndexChanged += CmbCurso_SelectedIndexChanged;
            this.Controls.Add(cmbCurso);

            btnAsignar = new Button { Text = "Asignar", Location = new Point(430, 10) };
            btnAsignar.Click += BtnAsignar_Click;
            this.Controls.Add(btnAsignar);

            lblRespuesta = new Label { Location = new Point(10, 50), Width = 760, Height = 50, BorderStyle = BorderStyle.FixedSingle };
            this.Controls.Add(lblRespuesta);

            dgvInscripciones = new DataGridView { Location = new Point(10, 110), Size = new Size(760, 450), ReadOnly = true };
            dgvInscripciones.Columns.Add("Alumno", "Alumno");
            dgvInscripciones.Columns.Add("Estado", "Estado");
            this.Controls.Add(dgvInscripciones);
        }

        private void LoadData()
        {
            var alumnos = alumnosController.Listar();
            cmbAlumno.DataSource = alumnos;
            cmbAlumno.DisplayMember = "Nombre";

            var cursos = cursosController.Listar();
            cmbCurso.DataSource = cursos;
            cmbCurso.DisplayMember = "Titulo";
        }

        private void BtnAsignar_Click(object sender, EventArgs e)
        {
            if (cmbAlumno.SelectedItem is Alumno alumno && cmbCurso.SelectedItem is Curso curso)
            {
                var (insc, msg) = inscripcionesController.Inscribir(alumno, curso);
                lblRespuesta.Text = msg;
                UpdateDgv(curso);
            }
            else
            {
                lblRespuesta.Text = "Seleccione alumno y curso.";
            }
        }

        private void CmbCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCurso.SelectedItem is Curso curso)
            {
                UpdateDgv(curso);
            }
        }

        private void UpdateDgv(Curso curso)
        {
            var inscripciones = inscripcionesController.ListarPorCurso(curso);
            dgvInscripciones.Rows.Clear();
            foreach (var insc in inscripciones)
            {
                dgvInscripciones.Rows.Add(insc.Alumno.Nombre, insc.Estado);
            }
        }
    }
}