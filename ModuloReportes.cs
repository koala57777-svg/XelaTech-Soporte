using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XelaTechSoporte.Enums;
using static XelaTechSoporte.AyudanteConsola;
using static XelaTechSoporte.Globales;

namespace XelaTechSoporte
{
    internal static class ModuloReportes
    {
        public static void IniciarSubmenu(SistemaSoporte sistema)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesMenuReportes, 9);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        ReporteSolicitudesEnEspera(sistema);
                        break;
                    case 2:
                        ReporteMatrizCargaSemanal(sistema);
                        break;
                    case 3:
                        ReporteRepuestosAlerta(sistema);
                        break;
                    case 4:
                        ReporteSolicitudesPorEstado(sistema);
                        break;
                    case 5:
                        ReporteTecnicosDisponibilidad(sistema);
                        break;
                    case 6:
                        ReporteTecnicoMasServicios(sistema);
                        break;
                    case 7:
                        ReporteEquiposPorTipo(sistema);
                        break;
                    case 8:
                        ReporteIngresosTotales(sistema);
                        break;
                    case 9:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 9);
        }

        public static void ReporteSolicitudesEnEspera(SistemaSoporte sistema)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==================================================");
            Console.WriteLine("              SOLICITUDES EN ESPERA               ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("[ PRIORIDAD URGENTE ]");
            Console.ResetColor();

            if (sistema.ColaUrgentes.Count == 0)
            {
                Console.WriteLine("No hay solicitudes urgentes en espera.");
            }
            else
            {
                int contador = 1;
                foreach (Solicitud s in sistema.ColaUrgentes)
                {
                    Console.Write($"{contador}. Solicitud: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{s.CodigoSolicitud} | ");
                    Console.ResetColor();
                    Console.Write("Cliente: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{s.Cliente.NombreCompleto} | ");
                    Console.ResetColor();
                    Console.Write("Equipo: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{s.Equipo.TipoEquipo.ObtenerDescripcion()} {s.Equipo.Marca}");
                    Console.ResetColor();
                    Console.WriteLine();
                    contador++;
                }
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("[ PRIORIDAD NORMAL ]");
            Console.ResetColor();

            if (sistema.ColaNormales.Count == 0)
            {
                Console.WriteLine("No hay solicitudes normales en espera.");
            }
            else
            {
                int contador = 1;
                foreach (Solicitud s in sistema.ColaNormales)
                {
                    Console.Write($"{contador}. Solicitud: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{s.CodigoSolicitud} | ");
                    Console.ResetColor();
                    Console.Write("Cliente: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{s.Cliente.NombreCompleto} | ");
                    Console.ResetColor();
                    Console.Write("Equipo: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{s.Equipo.TipoEquipo.ObtenerDescripcion()} {s.Equipo.Marca}");
                    Console.ResetColor();
                    Console.WriteLine();
                    contador++;
                }
            }

            LimpiarConsola();
        }

        public static void ReporteMatrizCargaSemanal(SistemaSoporte sistema)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("================================================================");
            Console.WriteLine("                   CARGA SEMANAL DE TRABAJO                     ");
            Console.WriteLine("================================================================");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(string.Format("{0,-20} | {1,-3} | {2,-3} | {3,-3} | {4,-3} | {5,-3} | {6,-3} | {7,-3} |", "Técnico", "LUN", "MAR", "MIE", "JUE", "VIE", "SAB", "DOM"));
            Console.ResetColor();
            Console.WriteLine(new string('-', 64));

            int maxCarga = -1;
            string tecnicoMayorCarga = "";
            string diaMayorCarga = "";
            string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

            for (int i = 0; i < sistema.ListaTecnicos.Count; i++)
            {
                Tecnico t = sistema.ListaTecnicos[i];
                Console.Write(string.Format("{0,-20} | ", t.CodigoUnico + " (" + t.NombreCompleto.Split(' ')[0] + ")"));

                for (int j = 0; j < 7; j++)
                {
                    int carga = sistema.MatrizCargaSemanal[i, j];

                    if (carga > 0) Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(string.Format("{0,-3}", carga));
                    Console.ResetColor();
                    Console.Write(" | ");

                    if (carga > maxCarga)
                    {
                        maxCarga = carga;
                        tecnicoMayorCarga = t.CodigoUnico;
                        diaMayorCarga = dias[j];
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine(new string('-', 64));
            Console.ForegroundColor = ConsoleColor.Magenta;
            if (maxCarga > 0)
            {
                Console.WriteLine($">> Mayor carga: {tecnicoMayorCarga} el día {diaMayorCarga} ({maxCarga} solicitudes).");
            }
            else
            {
                Console.WriteLine(">> Aún no hay cargas de trabajo registradas esta semana.");
            }
            Console.ResetColor();

            LimpiarConsola();
        }

        public static void ReporteRepuestosAlerta(SistemaSoporte sistema)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==================================================");
            Console.WriteLine("              ALERTA DE INVENTARIO                ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine();

            bool hayAlertas = false;

            foreach (Repuesto r in sistema.ListaRepuestos)
            {
                if (r.Existencia <= r.ExistenciaMinima)
                {
                    hayAlertas = true;
                    Console.Write($"{r.Codigo}: {r.Nombre} | Existencia: ");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{r.Existencia} ");
                    Console.ResetColor();

                    Console.Write($"| Min: {r.ExistenciaMinima} | ");

                    if (r.Existencia == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[ AGOTADO ]");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("[ REABASTECER ]");
                    }
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }

            if (!hayAlertas)
            {
                Console.WriteLine("Todos los repuestos tienen existencias aceptables.");
            }

            LimpiarConsola();
        }

        public static void ReporteSolicitudesPorEstado(SistemaSoporte sistema)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==================================================");
            Console.WriteLine("    SOLICITUDES ACTIVAS, FINALIZADAS Y CANCELADAS ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("--- ACTIVAS (EN PROCESO) ---");
            Console.ResetColor();
            foreach (Solicitud s in sistema.ListaSolicitudesGenerales.Where(x => x.Estado != EstadoSolicitud.Entregada && x.Estado != EstadoSolicitud.Cancelada))
            {
                Console.WriteLine($"{s.CodigoSolicitud} | Cliente: {s.Cliente.NombreCompleto} | Estado: {s.Estado.ObtenerDescripcion()}");
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("--- FINALIZADAS (ENTREGADAS) ---");
            Console.ResetColor();
            foreach (Solicitud s in sistema.ListaSolicitudesGenerales.Where(x => x.Estado == EstadoSolicitud.Entregada))
            {
                Console.WriteLine($"{s.CodigoSolicitud} | Cliente: {s.Cliente.NombreCompleto} | Estado: {s.Estado.ObtenerDescripcion()}");
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("--- CANCELADAS ---");
            Console.ResetColor();
            foreach (Solicitud s in sistema.ListaSolicitudesGenerales.Where(x => x.Estado == EstadoSolicitud.Cancelada))
            {
                Console.WriteLine($"{s.CodigoSolicitud} | Cliente: {s.Cliente.NombreCompleto} | Estado: {s.Estado.ObtenerDescripcion()}");
            }

            LimpiarConsola();
        }

        public static void ReporteTecnicosDisponibilidad(SistemaSoporte sistema)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==================================================");
            Console.WriteLine("        TÉCNICOS DISPONIBLES Y OCUPADOS           ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(string.Format("{0,-25} | {1,-20}", "DISPONIBLES", "OCUPADOS / FUERA SERVICIO"));
            Console.WriteLine(new string('-', 50));
            Console.ResetColor();

            List<Tecnico> disponibles = sistema.ListaTecnicos.Where(t => t.Estado == EstadoTecnico.Disponible).ToList();
            List<Tecnico> noDisponibles = sistema.ListaTecnicos.Where(t => t.Estado != EstadoTecnico.Disponible).ToList();

            int maxLineas = Math.Max(disponibles.Count, noDisponibles.Count);

            for (int i = 0; i < maxLineas; i++)
            {
                string col1 = i < disponibles.Count ? disponibles[i].NombreCompleto : "";
                string col2 = i < noDisponibles.Count ? $"{noDisponibles[i].NombreCompleto} ({noDisponibles[i].Estado.ObtenerDescripcion()})" : "";

                Console.WriteLine(string.Format("{0,-25} | {1,-20}", col1, col2));
            }

            LimpiarConsola();
        }

        public static void ReporteTecnicoMasServicios(SistemaSoporte sistema)
        {
            if (sistema.ListaTecnicos.Count == 0)
            {
                Console.WriteLine("No hay técnicos registrados.");
                LimpiarConsola();
                return;
            }

            int maxServicios = sistema.ListaTecnicos.Max(t => t.ServiciosFinalizados);

            if (maxServicios == 0)
            {
                Console.WriteLine("Ningún técnico ha finalizado servicios todavía.");
            }
            else
            {
                List<Tecnico> destacados = sistema.ListaTecnicos.Where(t => t.ServiciosFinalizados == maxServicios).ToList();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Magenta;
                foreach (Tecnico t in destacados)
                {
                    Console.WriteLine($"Técnico Destacado: {t.NombreCompleto} - {t.ServiciosFinalizados} servicios finalizados.");
                }
                Console.ResetColor();
            }

            LimpiarConsola();
        }

        public static void ReporteEquiposPorTipo(SistemaSoporte sistema)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==================================================");
            Console.WriteLine("           EQUIPOS ATENDIDOS POR TIPO             ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine();

            int portatiles = sistema.ListaEquipos.Count(e => e.TipoEquipo == TipoEquipo.ComputadoraPortatil);
            int escritorios = sistema.ListaEquipos.Count(e => e.TipoEquipo == TipoEquipo.ComputadoraEscritorio);
            int impresoras = sistema.ListaEquipos.Count(e => e.TipoEquipo == TipoEquipo.Impresora);

            Console.WriteLine($"Computadoras Portátiles : {portatiles}");
            Console.WriteLine($"Computadoras Escritorio : {escritorios}");
            Console.WriteLine($"Impresoras              : {impresoras}");
            Console.WriteLine($"TOTAL DE EQUIPOS        : {sistema.ListaEquipos.Count}");

            LimpiarConsola();
        }

        public static void ReporteIngresosTotales(SistemaSoporte sistema)
        {
            double sumaIngresos = sistema.ListaSolicitudesGenerales.Where(s => s.Estado == EstadoSolicitud.Entregada).Sum(s => s.Total);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==================================================");
            Console.WriteLine($" Ingresos Totales por Servicios Finalizados: Q{sumaIngresos:F2}");
            Console.WriteLine("==================================================");
            Console.ResetColor();

            LimpiarConsola();
        }

        public static void OpcionesMenuReportes()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("      MÓDULO REPORTES Y ESTAD.     ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Solicitudes en Espera (Por Prioridad)");
            Console.WriteLine("2. Matriz Semanal de Carga de Trabajo");
            Console.WriteLine("3. Repuestos con Existencia Mínima o Agotados");
            Console.WriteLine("4. Solicitudes Activas, Finalizadas y Canceladas");
            Console.WriteLine("5. Técnicos Disponibles y Ocupados");
            Console.WriteLine("6. Técnico con más Servicios Finalizados");
            Console.WriteLine("7. Equipos Atendidos por Tipo");
            Console.WriteLine("8. Ingresos Totales");
            Console.WriteLine("9. Volver al Menú Principal");
            Console.ResetColor();
        }
    }
}
