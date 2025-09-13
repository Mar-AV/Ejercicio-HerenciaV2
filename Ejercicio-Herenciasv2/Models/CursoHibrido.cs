using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLibres.Models
{
    public sealed class CursoHibrido : Curso
    {
        public string Campus { get; }
        public string Salon { get; }
        public Uri Enlace { get; }
        public CursoHibrido(string t, string c, int cupo, decimal costo, Docente d, string campus, string salon, Uri enlace)
            : base(t, c, cupo, costo, d) { Campus = campus; Salon = salon; Enlace = enlace; }
        public override string GenerarAcceso() => $"Presencial: {Campus}/{Salon} o Virtual: {Enlace}";
    }
}