using CursosLibres.Enums;
using CursosLibres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLibres.Data
{
    public static class InMemoryDb
    {
        public static List<Curso> Cursos { get; } = new();
        public static List<Alumno> Alumnos { get; } = new();
        public static List<Docente> Docentes { get; } = new();
        public static List<Inscripcion> Inscripciones { get; } = new();
        public static int Confirmados(Guid cursoId)
            => Inscripciones.Count(i => i.Curso.Id == cursoId && i.Estado == EstadoInscripcion.Confirmado);
    }

}