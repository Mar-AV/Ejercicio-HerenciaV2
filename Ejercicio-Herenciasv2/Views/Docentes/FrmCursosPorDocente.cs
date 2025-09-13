using CursosLibres.Controllers;
using CursosLibres.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CursosLibres.Views.Docentes
{
    public partial class FrmCursosPorDocente : Form
    {
        private DocentesController docentesController = new DocentesController();
        private CursosController cursosController = new CursosController();

        private ComboBox cmbDocente;
        private Button btnBuscar;
        private FlowLayoutPanel flpContenido;

        public FrmCursosPorDocente()
        {
            InitializeComponent();  
            LoadControls();
            LoadData();
        }

        private void LoadControls()
        {
            this.Size = new Size(800, 600);
            this.Text = "Cursos por Docente";  
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblDocente = new Label { Text = "Docente:", Location = new Point(10, 10), Size = new Size(80, 20) };
            this.Controls.Add(lblDocente);

            cmbDocente = new ComboBox { Location = new Point(100, 10), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList, Name = "cmbDocente" };
            this.Controls.Add(cmbDocente);

            btnBuscar = new Button { Text = "Buscar", Location = new Point(310, 10), Name = "btnBuscar" };
            btnBuscar.Click += BtnBuscar_Click;
            this.Controls.Add(btnBuscar);

            flpContenido = new FlowLayoutPanel { Location = new Point(10, 50), Size = new Size(760, 500), AutoScroll = true, FlowDirection = FlowDirection.TopDown, WrapContents = false, Name = "flpContenido" };
            this.Controls.Add(flpContenido);
        }

        private void LoadData()
        {
            var docentes = docentesController.Listar();
            cmbDocente.DataSource = docentes;
            cmbDocente.DisplayMember = "Nombre";
            cmbDocente.ValueMember = "Id";
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            flpContenido.Controls.Clear();

            if (cmbDocente.SelectedItem is Docente docente)
            {
                var cursos = cursosController.ListarPorDocente(docente);

                if (cursos.Count == 0)
                {
                    Label lblNoCursos = new Label { Text = "Sin cursos asignados", Size = new Size(760, 50), TextAlign = ContentAlignment.MiddleCenter, Font = new Font(Font, FontStyle.Bold) };
                    flpContenido.Controls.Add(lblNoCursos);
                    return;
                }

                foreach (var curso in cursos)
                {
                    // Encabezado del curso
                    Label header = new Label
                    {
                        Text = $"----------------------------------------------\n{curso.Titulo}\n----------------------------------------------",
                        Size = new Size(760, 60),
                        TextAlign = ContentAlignment.MiddleCenter,
                        BorderStyle = BorderStyle.FixedSingle,
                        Font = new Font(Font, FontStyle.Bold)
                    };
                    flpContenido.Controls.Add(header);

                    // Rejilla de sesiones (3 por fila)
                    int numRows = (int)Math.Ceiling((double)curso.Sesiones.Count / 3);
                    TableLayoutPanel tblSesiones = new TableLayoutPanel
                    {
                        ColumnCount = 3,
                        RowCount = numRows,
                        Size = new Size(760, 100 + (numRows * 30)),  // Altura dinámica
                        AutoSize = true,
                        Padding = new Padding(10)
                    };
                    tblSesiones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                    tblSesiones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                    tblSesiones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));

                    for (int i = 0; i < curso.Sesiones.Count; i++)
                    {
                        int row = i / 3;
                        tblSesiones.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        Label lblSes = new Label
                        {
                            Text = $"Sesión #{i + 1}",
                            BorderStyle = BorderStyle.FixedSingle,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Margin = new Padding(5),
                            Size = new Size(200, 25)
                        };
                        tblSesiones.Controls.Add(lblSes, i % 3, row);
                    }
                    flpContenido.Controls.Add(tblSesiones);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un docente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}