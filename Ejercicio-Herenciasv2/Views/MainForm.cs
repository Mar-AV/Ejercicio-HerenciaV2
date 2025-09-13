using CursosLibres.Views;
using System;
using System.Windows.Forms;

namespace CursosLibres
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            menuStrip1.Items.Clear();

            ToolStripMenuItem mnuCursos = new ToolStripMenuItem("Cursos");
            ToolStripMenuItem mnuActualesC = new ToolStripMenuItem("Actuales") { Name = "actualesCurso" };
            mnuActualesC.Click += MnuActualesC_Click;
            ToolStripMenuItem mnuNuevoC = new ToolStripMenuItem("Nuevo") { Name = "nuevoCurso" };
            mnuNuevoC.Click += MnuNuevoC_Click;
            mnuCursos.DropDownItems.Add(mnuActualesC);
            mnuCursos.DropDownItems.Add(mnuNuevoC);

            ToolStripMenuItem mnuAlumnos = new ToolStripMenuItem("Alumnos");
            ToolStripMenuItem mnuActualesA = new ToolStripMenuItem("Actuales") { Name = "actualesAlumnos" };
            mnuActualesA.Click += MnuActualesA_Click;
            ToolStripMenuItem mnuNuevoA = new ToolStripMenuItem("Nuevo") { Name = "nuevoAlumno" };
            mnuNuevoA.Click += MnuNuevoA_Click;
            mnuAlumnos.DropDownItems.Add(mnuActualesA);
            mnuAlumnos.DropDownItems.Add(mnuNuevoA);

            ToolStripMenuItem mnuDocentes = new ToolStripMenuItem("Docentes");
            ToolStripMenuItem mnuActualesD = new ToolStripMenuItem("Actuales") { Name = "actualesDocentes" };
            mnuActualesD.Click += MnuActualesD_Click;
            ToolStripMenuItem mnuNuevoD = new ToolStripMenuItem("Nuevo") { Name = "nuevoDocente" };
            mnuNuevoD.Click += MnuNuevoD_Click;
            ToolStripMenuItem mnuCursosPorD = new ToolStripMenuItem("Cursos por Docente") { Name = "cursosPorDocente" };
            mnuCursosPorD.Click += MnuCursosPorD_Click;
            mnuDocentes.DropDownItems.Add(mnuActualesD);
            mnuDocentes.DropDownItems.Add(mnuNuevoD);
            mnuDocentes.DropDownItems.Add(mnuCursosPorD);

            ToolStripMenuItem mnuInscripciones = new ToolStripMenuItem("Inscripciones");
            mnuInscripciones.Click += MnuInscripciones_Click;

            menuStrip1.Items.Add(mnuCursos);
            menuStrip1.Items.Add(mnuAlumnos);
            menuStrip1.Items.Add(mnuDocentes);
            menuStrip1.Items.Add(mnuInscripciones);
        }

        private void MnuActualesC_Click(object sender, EventArgs e)
        {
            new FrmCursos { MdiParent = this }.Show();
        }

        private void MnuNuevoC_Click(object sender, EventArgs e)
        {
            new FrmNuevoCurso { MdiParent = this }.Show();
        }

        private void MnuActualesA_Click(object sender, EventArgs e)
        {
            // Muestra el formulario de alumnos actuales como ventana 
            var frmAlumnos = new CursosLibres.Views.Alumnos.FrmAlumnos { MdiParent = this };
            frmAlumnos.Show();
        }

        private void MnuNuevoA_Click(object sender, EventArgs e)
        {
            new FrmNuevoAlumno { MdiParent = this }.Show();
        }

        private void MnuActualesD_Click(object sender, EventArgs e)
        {
            new FrmDocentes { MdiParent = this }.Show();
        }

        private void MnuNuevoD_Click(object sender, EventArgs e)
        {
            new FrmNuevoDocente { MdiParent = this }.Show();
        }

        // Agrega este método en la clase MainForm para manejar el evento Click
        private void nuevoCursoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Aquí puedes agregar la lógica que deseas ejecutar cuando se haga clic en "Nuevo Curso"
            MessageBox.Show("Nuevo Curso seleccionado.");
        }

        private void MnuInscripciones_Click(object sender, EventArgs e)
        {
            new FrmInscripciones { MdiParent = this }.Show();
        }
        // Agrega este método en la clase MainForm para manejar el evento Click de "Cursos por Docente"
        private void MnuCursosPorD_Click(object sender, EventArgs e)
        {
            new FrmCursosPorDocente { MdiParent = this }.Show();
        }
    }
    public class FrmCursosPorDocente : Form
    {
        
    }
}