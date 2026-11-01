using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XelaTechSoporte.AyudanteConsola;
using static XelaTechSoporte.Globales;

namespace XelaTechSoporte
{
    internal static class ModuloOperaciones
    {
        public static void IniciarSubmenu(SistemaSoporte sistema)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesMenuOperaciones, 4);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        sistema.AsignarTecnico();
                        LimpiarConsola();
                        break;
                    case 2:
                        SubmenuOrdenamientos(sistema);
                        break;
                    case 3:
                        SubmenuBusquedas(sistema);
                        break;
                    case 4:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 4);
        }

        public static void SubmenuOrdenamientos(SistemaSoporte sistema)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesSubMenuOrdenamientos, 5);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        EjecutarBubbleSort(sistema);
                        break;
                    case 2:
                        EjecutarSelectionSort(sistema);
                        break;
                    case 3:
                        EjecutarShellSort(sistema);
                        break;
                    case 4:
                        EjecutarQuickSort(sistema);
                        break;
                    case 5:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 5);
        }

        public static void EjecutarBubbleSort(SistemaSoporte sistema)
        {
            if (sistema.ListaSolicitudesGenerales.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay suficientes registros para ordenar.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            sistema.BubbleSortSolicitudesCostoAscendente();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==================================================");
            Console.WriteLine("  SOLICITUDES ORDENADAS POR COSTO (BUBBLE SORT)   ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Solicitud s in sistema.ListaSolicitudesGenerales)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{s.CodigoSolicitud} || ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Cliente: {s.Cliente.NombreCompleto} || ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Costo Total: Q{s.Total:F2}");
                Console.WriteLine();
            }

            Console.ResetColor();
            LimpiarConsola();
        }

        public static void EjecutarSelectionSort(SistemaSoporte sistema)
        {
            if (sistema.ListaTecnicos.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay suficientes registros para ordenar.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            sistema.SelectionSortTecnicosServiciosDescendente();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==================================================");
            Console.WriteLine(" TÉCNICOS ORDENADOS POR SERVICIOS (SELECTION SORT)");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Tecnico t in sistema.ListaTecnicos)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{t.CodigoUnico} || ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Nombre: {t.NombreCompleto} || ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Servicios Finalizados: {t.ServiciosFinalizados}");
                Console.WriteLine();
            }

            Console.ResetColor();
            LimpiarConsola();
        }

        public static void EjecutarShellSort(SistemaSoporte sistema)
        {
            if (sistema.ListaRepuestos.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay suficientes registros para ordenar.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            sistema.ShellSortRepuestosExistenciaAscendente();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==================================================");
            Console.WriteLine("  REPUESTOS ORDENADOS POR EXISTENCIA (SHELL SORT) ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Repuesto r in sistema.ListaRepuestos)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{r.Codigo} || ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Nombre: {r.Nombre} || ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Existencia: {r.Existencia}");
                Console.WriteLine();
            }

            Console.ResetColor();
            LimpiarConsola();
        }

        public static void EjecutarQuickSort(SistemaSoporte sistema)
        {
            if (sistema.ListaSolicitudesGenerales.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay suficientes registros para ordenar.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            sistema.QuickSortSolicitudesFechaAscendente();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==================================================");
            Console.WriteLine("  SOLICITUDES ORDENADAS POR FECHA (QUICK SORT)    ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Solicitud s in sistema.ListaSolicitudesGenerales)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{s.CodigoSolicitud} || ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Cliente: {s.Cliente.NombreCompleto} || ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Fecha: {s.FechaRecepcion:dd/MM/yyyy HH:mm}");
                Console.WriteLine();
            }

            Console.ResetColor();
            LimpiarConsola();
        }

        public static void SubmenuBusquedas(SistemaSoporte sistema)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesSubMenuBusquedas, 3);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        EjecutarBusquedaSecuencial(sistema);
                        break;
                    case 2:
                        EjecutarBusquedaBinaria(sistema);
                        break;
                    case 3:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 3);
        }

        public static void EjecutarBusquedaSecuencial(SistemaSoporte sistema)
        {
            if (sistema.ListaEquipos.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay equipos registrados en el sistema.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            string objetivo = ValidarTexto("Ingrese la Marca o Modelo a buscar (Parcial o Total): ", 30);

            List<EquipoBase> resultados = sistema.BusquedaSecuencialEquipos(objetivo);

            Console.WriteLine();
            if (resultados.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No se encontraron coincidencias.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("==================================================");
                Console.WriteLine($" [{resultados.Count}] COINCIDENCIAS ENCONTRADAS (SECUENCIAL)");
                Console.WriteLine("==================================================");
                Console.ResetColor();
                Console.WriteLine();

                foreach (EquipoBase eq in resultados)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{eq.CodigoInterno} || ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"Tipo: {eq.TipoEquipo.ObtenerDescripcion()} || ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Marca: {eq.Marca} | Modelo: {eq.Modelo}");
                    Console.WriteLine();
                }
            }

            LimpiarConsola();
        }

        public static void EjecutarBusquedaBinaria(SistemaSoporte sistema)
        {
            if (sistema.ListaSolicitudesGenerales.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay solicitudes registradas en el sistema.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            bool estaOrdenada = true;
            for (int i = 0; i < sistema.ListaSolicitudesGenerales.Count - 1; i++)
            {
                if (string.Compare(sistema.ListaSolicitudesGenerales[i].CodigoSolicitud, sistema.ListaSolicitudesGenerales[i + 1].CodigoSolicitud, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    estaOrdenada = false;
                    break;
                }
            }

            if (!estaOrdenada)
            {
                for (int i = 0; i < sistema.ListaSolicitudesGenerales.Count - 1; i++)
                {
                    for (int j = 0; j < sistema.ListaSolicitudesGenerales.Count - i - 1; j++)
                    {
                        if (string.Compare(sistema.ListaSolicitudesGenerales[j].CodigoSolicitud, sistema.ListaSolicitudesGenerales[j + 1].CodigoSolicitud, StringComparison.OrdinalIgnoreCase) > 0)
                        {
                            Solicitud temp = sistema.ListaSolicitudesGenerales[j];
                            sistema.ListaSolicitudesGenerales[j] = sistema.ListaSolicitudesGenerales[j + 1];
                            sistema.ListaSolicitudesGenerales[j + 1] = temp;
                        }
                    }
                }
            }

            string codigoObjetivo = ValidarAlfanumerico("Ingrese el Código de Solicitud exacto (Ej: SOL-001): ", 10);

            Solicitud encontrada = sistema.BusquedaBinariaSolicitud(sistema.ListaSolicitudesGenerales, codigoObjetivo);

            Console.WriteLine();
            if (encontrada == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("El código ingresado no existe en el sistema.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("==================================================");
                Console.WriteLine("        SOLICITUD ENCONTRADA (BINARIA)            ");
                Console.WriteLine("==================================================");
                Console.ResetColor();
                Console.WriteLine();
                encontrada.MostrarInformacion();
            }

            LimpiarConsola();
        }

        public static void OpcionesMenuOperaciones()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("         MÓDULO OPERACIONES        ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Atención de Colas (Asignación Automática)");
            Console.WriteLine("2. Módulo de Ordenamientos");
            Console.WriteLine("3. Módulo de Búsquedas");
            Console.WriteLine("4. Volver al Menú Principal");
            Console.ResetColor();
        }

        public static void OpcionesSubMenuOrdenamientos()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("       SUBMENÚ ORDENAMIENTOS       ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Ordenar Solicitudes por Costo (Bubble Sort)");
            Console.WriteLine("2. Ordenar Técnicos por Servicios (Selection Sort)");
            Console.WriteLine("3. Ordenar Repuestos por Existencia (Shell Sort)");
            Console.WriteLine("4. Ordenar Solicitudes por Fecha (Quick Sort)");
            Console.WriteLine("5. Regresar");
            Console.ResetColor();
        }

        public static void OpcionesSubMenuBusquedas()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("         SUBMENÚ BÚSQUEDAS         ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Búsqueda Secuencial (Múltiples Coincidencias)");
            Console.WriteLine("2. Búsqueda Binaria (Código de Solicitud)");
            Console.WriteLine("3. Regresar");
            Console.ResetColor();
        }
    }
}
