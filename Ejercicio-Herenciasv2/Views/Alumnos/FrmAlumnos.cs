using CursosLibres.Controllers;
using CursosLibres.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CursosLibres.Views.Alumnos
{
    public partial class FrmAlumnos : Form
    {
        private AlumnosController controller = new AlumnosController();
        private DocentesController docentesController = new DocentesController();
        private CursosController cursosController = new CursosController();
        private DataGridView dgvAlumnos;
        private DataGridView dgvDocentes;
        private DataGridView dgvCursosPorDocente;

        public FrmAlumnos()
        {
            InitializeComponent();
            LoadControls();
            LoadData();
        }

        private void InitializeComponent()
        {

        }

        private void LoadControls()
        {
            this.Size = new Size(1200, 500);
            this.Text = "Alumnos, Docentes y Cursos por Docente";

            dgvAlumnos = new DataGridView { Location = new Point(10, 10), Size = new Size(360, 400), ReadOnly = true };
            dgvAlumnos.Columns.Add("Nombre", "Nombre");
            dgvAlumnos.Columns.Add("Email", "Email");
            dgvAlumnos.Columns.Add("FechaRegistro", "Fecha de Registro");
            this.Controls.Add(dgvAlumnos);

            dgvDocentes = new DataGridView { Location = new Point(390, 10), Size = new Size(360, 400), ReadOnly = true };
            dgvDocentes.Columns.Add("Nombre", "Nombre");
            dgvDocentes.Columns.Add("Email", "Email");
            this.Controls.Add(dgvDocentes);

            dgvCursosPorDocente = new DataGridView { Location = new Point(770, 10), Size = new Size(400, 400), ReadOnly = true };
            dgvCursosPorDocente.Columns.Add("Docente", "Docente");
            dgvCursosPorDocente.Columns.Add("Curso", "Curso");
            dgvCursosPorDocente.Columns.Add("Tipo", "Tipo");
            this.Controls.Add(dgvCursosPorDocente);
        }

        private void LoadData()
        {
            // Alumnos
            BindingList<Alumno> alumnosActuales = controller.Listar();
            dgvAlumnos.Rows.Clear();
            foreach (var alumno in alumnosActuales)
            {
                dgvAlumnos.Rows.Add(alumno.Nombre, alumno.Email, "");
            }

            // Docentes
            BindingList<Docente> docentesActuales = docentesController.Listar();
            dgvDocentes.Rows.Clear();
            foreach (var docente in docentesActuales)
            {
                dgvDocentes.Rows.Add(docente.Nombre, docente.Especialidad);
            }

            // Cursos por docente
            dgvCursosPorDocente.Rows.Clear();
            foreach (var docente in docentesActuales)
            {

                var cursos = cursosController.ListarPorDocente(docente);
                foreach (var curso in cursos)
                {
                    dgvCursosPorDocente.Rows.Add(docente.Nombre, curso.Nombre, curso.GetType().Name);
                }
            }
        }
    }
}