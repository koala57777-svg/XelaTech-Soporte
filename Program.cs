using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XelaTechSoporte.AyudanteConsola;
using static XelaTechSoporte.Globales;

namespace XelaTechSoporte
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "XelaTech Soporte";
            SistemaSoporte sistemaSoporte = new SistemaSoporte();

            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesMenuPrincipal, 9);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        ModuloClientes.IniciarSubmenu(sistemaSoporte);
                        break;
                    case 2:
                        ModuloTecnicos.IniciarSubmenu(sistemaSoporte);
                        break;
                    case 3:
                        ModuloEquipos.IniciarSubmenu(sistemaSoporte);
                        break;
                    case 4:
                        ModuloSolicitudes.IniciarSubmenu(sistemaSoporte);
                        break;
                    case 5:
                        ModuloRepuestos.IniciarSubmenu(sistemaSoporte);
                        break;
                    case 6:
                        ModuloOperaciones.IniciarSubmenu(sistemaSoporte);
                        break;
                    case 7:
                        ModuloReportes.IniciarSubmenu(sistemaSoporte);
                        break;
                    case 8:
                        GeneradorDatosPrueba.CargarDatosPrueba(sistemaSoporte);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("¡Datos de prueba generados e ingresados al sistema exitosamente!");
                        Console.ResetColor();
                        LimpiarConsola();
                        break;
                    case 9:
                        SalirMenu();
                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 9);
        }

        public static void OpcionesMenuPrincipal()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("                 XELATECH SOPORTE                 ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Gestión de clientes");
            Console.WriteLine("2. Gestión de técnicos");
            Console.WriteLine("3. Gestión de equipos");
            Console.WriteLine("4. Gestión de solicitudes");
            Console.WriteLine("5. Gestión de repuestos");
            Console.WriteLine("6. Operaciones (Asignación, Ordenamiento y Búsqueda)");
            Console.WriteLine("7. Reportes y Estadísticas");
            Console.WriteLine("8. Generar datos de prueba (Random)");
            Console.WriteLine("9. Salir");
            Console.ResetColor();
        }
    }
}