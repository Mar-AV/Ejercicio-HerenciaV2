using CursosLibres.Data;
using CursosLibres.Models;
using System;

namespace CursosLibres
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            InMemoryDb.Alumnos.AddRange(new[]
            {
                new Alumno { Nombre = "Ana", Email = "ana@ejemplo.com" },
                new Alumno { Nombre = "Luis", Email = "luis@ejemplo.com" }
            });

            var d1 = new Docente { Nombre = "Dra. Pérez", Especialidad = "Programación" };
            InMemoryDb.Docentes.Add(d1);

            var c1 = new CursoPresencial("C# desde 0", "Programación", 20, 0, d1, "Campus Central", "B-201");
            c1.Sesiones.Add(new Sesion(DateTime.Today.AddHours(18), TimeSpan.FromHours(2)));
            InMemoryDb.Cursos.Add(c1);

            var c2 = new CursoVirtual("Git y GitHub", "Herramientas", 15, 0, d1, "Teams", new Uri("https://teams.microsoft.com/l/meetup-join/19%3ameeting_abc123%40thread.v2/0"));
            c2.Sesiones.Add(new Sesion(DateTime.Today.AddHours(19), TimeSpan.FromHours(1.5)));
            InMemoryDb.Cursos.Add(c2);

            Application.Run(new MainForm());
        }
    }
}