using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XelaTechSoporte.Enums;

namespace XelaTechSoporte
{
    internal static class GeneradorDatosPrueba
    {
        public static void CargarDatosPrueba(SistemaSoporte sistema)
        {
            Random rnd = new Random();

            Cliente c1 = new Cliente("Ana Lopez", rnd.Next(10000000, 99999999), "ana@gmail.com", TipoCliente.Estudiante);
            Cliente c2 = new Cliente("Carlos Ruiz", rnd.Next(10000000, 99999999), "carlos@empresa.com", TipoCliente.PequenoNegocio);
            Cliente c3 = new Cliente("Maria Gomez", rnd.Next(10000000, 99999999), "maria@institucion.gob", TipoCliente.Institucion);
            Cliente c4 = new Cliente("Juan Perez", rnd.Next(10000000, 99999999), "juan@profesional.com", TipoCliente.Profesional);
            Cliente c5 = new Cliente("Luis Morales", rnd.Next(10000000, 99999999), "luis@gmail.com", TipoCliente.Estudiante);

            sistema.IngresarCliente(c1);
            sistema.IngresarCliente(c2);
            sistema.IngresarCliente(c3);
            sistema.IngresarCliente(c4);
            sistema.IngresarCliente(c5);

            Tecnico t1 = new Tecnico("Pedro Martinez", rnd.Next(10000000, 99999999), EspecialidadTecnico.Computadoras, rnd.Next(3, 6));
            Tecnico t2 = new Tecnico("Sofia Castro", rnd.Next(10000000, 99999999), EspecialidadTecnico.Impresoras, rnd.Next(3, 6));
            Tecnico t3 = new Tecnico("Diego Fernandez", rnd.Next(10000000, 99999999), EspecialidadTecnico.SoporteGeneral, rnd.Next(3, 6));

            sistema.IngresarTecnico(t1);
            sistema.IngresarTecnico(t2);
            sistema.IngresarTecnico(t3);

            Portatil e1 = new Portatil("SN" + rnd.Next(1000, 9999), "Dell", "XPS 15", c1, "Rayones leves", "Cargador original", true);
            Escritorio e2 = new Escritorio("SN" + rnd.Next(1000, 9999), "HP", "ProDesk", c2, "Polvo interno", "Cable de poder", false);
            Impresora e3 = new Impresora("SN" + rnd.Next(1000, 9999), "Epson", "L3150", c3, "Atasco de papel", "Cable USB", rnd.Next(10, 100));
            Portatil e4 = new Portatil("SN" + rnd.Next(1000, 9999), "Lenovo", "ThinkPad", c4, "Pantalla rota", "Cargador genérico", true);
            Escritorio e5 = new Escritorio("SN" + rnd.Next(1000, 9999), "Custom", "Gamer PC", c5, "No enciende", "Ninguno", true);

            sistema.IngresarEquipo(e1);
            sistema.IngresarEquipo(e2);
            sistema.IngresarEquipo(e3);
            sistema.IngresarEquipo(e4);
            sistema.IngresarEquipo(e5);

            Repuesto r1 = new Repuesto("Disco Duro 1TB", CategoriaRepuesto.Almacenamiento, rnd.Next(1, 20), rnd.Next(2, 5), rnd.Next(200, 300), rnd.Next(350, 500));
            Repuesto r2 = new Repuesto("Memoria RAM 8GB", CategoriaRepuesto.ComponentesInternos, rnd.Next(1, 20), rnd.Next(2, 5), rnd.Next(100, 150), rnd.Next(200, 250));
            Repuesto r3 = new Repuesto("Pantalla 15.6 LED", CategoriaRepuesto.Pantallas, rnd.Next(1, 20), rnd.Next(1, 3), rnd.Next(400, 600), rnd.Next(700, 900));
            Repuesto r4 = new Repuesto("Tinta Negra Epson", CategoriaRepuesto.TintasYToners, rnd.Next(1, 20), rnd.Next(3, 8), rnd.Next(40, 60), rnd.Next(80, 100));
            Repuesto r5 = new Repuesto("Teclado USB", CategoriaRepuesto.PerifericosYAccesorios, rnd.Next(1, 20), rnd.Next(3, 8), rnd.Next(30, 50), rnd.Next(70, 90));
            Repuesto r6 = new Repuesto("SSD 500GB", CategoriaRepuesto.Almacenamiento, rnd.Next(1, 20), rnd.Next(2, 5), rnd.Next(150, 250), rnd.Next(300, 400));
            Repuesto r7 = new Repuesto("Tinta Color HP", CategoriaRepuesto.TintasYToners, rnd.Next(1, 20), rnd.Next(2, 5), rnd.Next(50, 80), rnd.Next(100, 140));
            Repuesto r8 = new Repuesto("Placa Base", CategoriaRepuesto.ComponentesInternos, rnd.Next(1, 20), rnd.Next(1, 3), rnd.Next(500, 800), rnd.Next(1000, 1500));
            Repuesto r9 = new Repuesto("Mouse Óptico", CategoriaRepuesto.PerifericosYAccesorios, rnd.Next(1, 20), rnd.Next(3, 8), rnd.Next(15, 30), rnd.Next(45, 60));
            Repuesto r10 = new Repuesto("Monitor 24 pulg", CategoriaRepuesto.Pantallas, rnd.Next(1, 20), rnd.Next(1, 3), rnd.Next(600, 900), rnd.Next(1100, 1400));

            sistema.IngresarRepuesto(r1);
            sistema.IngresarRepuesto(r2);
            sistema.IngresarRepuesto(r3);
            sistema.IngresarRepuesto(r4);
            sistema.IngresarRepuesto(r5);
            sistema.IngresarRepuesto(r6);
            sistema.IngresarRepuesto(r7);
            sistema.IngresarRepuesto(r8);
            sistema.IngresarRepuesto(r9);
            sistema.IngresarRepuesto(r10);
        }
    }
}
