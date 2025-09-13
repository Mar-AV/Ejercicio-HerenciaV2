using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLibres.Models
{
    public sealed class CursoVirtual : Curso
    {
        public string Plataforma { get; }
        public Uri Enlace { get; }
        public CursoVirtual(string t, string c, int cupo, decimal costo, Docente d, string plataforma, Uri enlace)
            : base(t, c, cupo, costo, d) { Plataforma = plataforma; Enlace = enlace; }
        public override string GenerarAcceso() => $"Conéctese vía {Plataforma}: {Enlace}";
    }
}