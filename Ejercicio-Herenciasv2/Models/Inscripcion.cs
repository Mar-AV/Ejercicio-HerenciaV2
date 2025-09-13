using CursosLibres.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLibres.Models
{
    public sealed class Inscripcion
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Alumno Alumno { get; }
        public Curso Curso { get; }
        public EstadoInscripcion Estado { get; private set; } = EstadoInscripcion.Preinscrito;
        public DateTime Fecha { get; } = DateTime.Now;
        public Inscripcion(Alumno a, Curso c) { Alumno = a; Curso = c; }
        public void Confirmar() => Estado = EstadoInscripcion.Confirmado;
        public void Espera() => Estado = EstadoInscripcion.ListaEspera;
        public void Cancelar() => Estado = EstadoInscripcion.Cancelado;
    }
}