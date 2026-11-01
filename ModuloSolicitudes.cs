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
    internal static class ModuloSolicitudes
    {
        public static void IniciarSubmenu(SistemaSoporte sistema)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesMenuSolicitudes, 4);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        RegistrarSolicitud(sistema);
                        break;
                    case 2:
                        MostarInformacionSolicitud(sistema);
                        break;
                    case 3:
                        AbrirSubmenuModificarInformacionSolicitud(sistema);
                        break;
                    case 4:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 4);
        }

        public static void RegistrarSolicitud(SistemaSoporte sistema)
        {
            if (sistema.ListaClientes.Count == 0 || sistema.ListaClientes == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No hay ningún Cliente registrado.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            if (sistema.ListaEquipos.Count == 0 || sistema.ListaEquipos == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No hay ningún Equipo registrado.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            int posicionCliente = ValidarNumerico(1, ModuloClientes.ListaDeClientes, sistema, sistema.ListaClientes.Count);
            Cliente clienteSeleccionado = sistema.ListaClientes[posicionCliente - 1];

            List<EquipoBase> equiposDelCliente = sistema.ListaEquipos.Where(e => e.Propietario == clienteSeleccionado).ToList();

            if (equiposDelCliente.Count == 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("El cliente seleccionado no tiene equipos registrados.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            int posicionEquipo = ValidarNumerico(1, ListaEquiposCliente, equiposDelCliente, equiposDelCliente.Count);
            EquipoBase equipoSeleccionado = equiposDelCliente[posicionEquipo - 1];

            bool tieneSolicitudActiva = sistema.ListaSolicitudesGenerales.Any(s => s.Equipo == equipoSeleccionado && s.Estado != EstadoSolicitud.Entregada && s.Estado != EstadoSolicitud.Cancelada);

            if (tieneSolicitudActiva)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: El equipo seleccionado ya posee una solicitud activa.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            string descripcion = ValidarTexto("Ingrese la descripción del problema", 150);

            int numeroPrioridad = ValidarNumerico(1, MenuPrioridad, Enum.GetNames(typeof(PrioridadSolicitud)).Length);
            PrioridadSolicitud prioridad = (PrioridadSolicitud)numeroPrioridad;

            Solicitud solicitud = new Solicitud(clienteSeleccionado, equipoSeleccionado, descripcion, prioridad);

            sistema.IngresarSolicitud(solicitud);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Se Registró Correctamente la Solicitud y se agregó a la cola de espera.");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void MostarInformacionSolicitud(SistemaSoporte sistema)
        {
            if (sistema.ListaSolicitudesGenerales.Count == 0 || sistema.ListaSolicitudesGenerales == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay ninguna Solicitud Registrada");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }
            else
            {
                int posicion = ValidarNumerico(1, ListaDeSolicitudes, sistema, sistema.ListaSolicitudesGenerales.Count);
                Console.WriteLine();
                sistema.ListaSolicitudesGenerales[posicion - 1].MostrarInformacion();
                LimpiarConsola();
                return;
            }
        }

        public static void AbrirSubmenuModificarInformacionSolicitud(SistemaSoporte sistema)
        {
            if (sistema.ListaSolicitudesGenerales.Count == 0 || sistema.ListaSolicitudesGenerales == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay ninguna Solicitud Registrada");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }
            else
            {
                int posicion = ValidarNumerico(1, ListaDeSolicitudes, sistema, sistema.ListaSolicitudesGenerales.Count);
                Console.WriteLine();
                SubmenuModificacionSolicitud(sistema.ListaSolicitudesGenerales[posicion - 1], sistema);
                OpcionMenuPrincipal = 0;
                LimpiarConsola();
                return;
            }
        }

        public static void SubmenuModificacionSolicitud(Solicitud solicitud, SistemaSoporte sistema)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesSubMenuModificarSolicitud, solicitud, 5);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        DiagnosticarYAsignarRepuestos(solicitud, sistema);
                        break;
                    case 2:
                        AvanzarSiguienteEstado(solicitud, sistema);
                        break;
                    case 3:
                        CancelarSolicitud(solicitud);
                        break;
                    case 4:
                        DeshacerUltimoEstado(solicitud);
                        break;
                    case 5:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 5);
        }

        public static void DiagnosticarYAsignarRepuestos(Solicitud solicitud, SistemaSoporte sistema)
        {
            if (solicitud.Estado == EstadoSolicitud.EnEspera || solicitud.Estado == EstadoSolicitud.Recibida)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("La solicitud debe ser asignada a un técnico primero (Atención de colas).");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }
            if (solicitud.Estado == EstadoSolicitud.ListaParaEntregar || solicitud.Estado == EstadoSolicitud.Entregada || solicitud.Estado == EstadoSolicitud.Cancelada)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El estado actual de la solicitud no permite agregar diagnósticos ni repuestos.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            string diagnostico = ValidarTexto("Ingrese el diagnóstico técnico", 150);
            solicitud.Diagnostico = diagnostico;

            while (true)
            {
                int deseaRepuesto = ValidarNumerico("¿Desea asignar un repuesto? (1 = Sí, 2 = No)", 1, true);
                if (deseaRepuesto == 2)
                {
                    break;
                }

                if (sistema.ListaRepuestos.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nNo hay repuestos registrados en el sistema.");
                    Console.ResetColor();
                    break;
                }

                int posicionRepuesto = ValidarNumerico(1, ModuloRepuestos.ListaDeRepuestos, sistema, sistema.ListaRepuestos.Count);
                Repuesto repuestoElegido = sistema.ListaRepuestos[posicionRepuesto - 1];

                int cantidad = ValidarNumerico("Ingrese la cantidad a utilizar (hasta 3 dígitos)", 3, true);

                if (cantidad > repuestoElegido.Existencia)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError: Inventario insuficiente para esa cantidad.");
                    Console.ResetColor();
                    Mostrar("Cancelando asignación de repuesto", 1);
                }
                else
                {
                    sistema.DescontarInventario(repuestoElegido, cantidad);
                    solicitud.RepuestosUtilizados.Add(repuestoElegido);
                    solicitud.CostoRepuestos += (repuestoElegido.PrecioVenta * cantidad);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n¡Repuesto agregado! Costo sumado: Q{repuestoElegido.PrecioVenta * cantidad:F2}");
                    Console.ResetColor();
                }
            }

            if (solicitud.Estado != EstadoSolicitud.EnReparacion)
            {
                solicitud.Estado = EstadoSolicitud.EnReparacion;
                solicitud.HistorialEstados.Push(solicitud.Estado);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDiagnóstico guardado y estado avanzado a: En Reparación.");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void AvanzarSiguienteEstado(Solicitud solicitud, SistemaSoporte sistema)
        {
            if (solicitud.Estado == EstadoSolicitud.Entregada || solicitud.Estado == EstadoSolicitud.Cancelada)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("La solicitud ya se encuentra finalizada o cancelada.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            if (solicitud.Estado == EstadoSolicitud.EnEspera || solicitud.Estado == EstadoSolicitud.Recibida)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Para asignar la solicitud debe utilizar el módulo de 'Atención de colas'.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            if (solicitud.Estado == EstadoSolicitud.EnReparacion)
            {
                double manoObra = ValidarDecimal("Ingrese el Costo de Mano de Obra para finalizar la reparación");
                solicitud.CostoManoObra = manoObra;
                solicitud.CalcularTotal();

                solicitud.Estado = EstadoSolicitud.ListaParaEntregar;
                solicitud.HistorialEstados.Push(solicitud.Estado);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSolicitud avanzada a: Lista para entregar. Total a cobrar: Q{solicitud.Total:F2}");
                Console.ResetColor();
            }
            else if (solicitud.Estado == EstadoSolicitud.ListaParaEntregar)
            {
                int calificacion = ValidarNumerico("Ingrese la calificación del servicio (1 al 5)", 1, true);

                if (calificacion < 1 || calificacion > 5)
                {
                    calificacion = 5;
                }

                sistema.CalcularPromedioTecnico(solicitud.TecnicoAsignado, calificacion);

                solicitud.TecnicoAsignado.TrabajosActivos--;
                if (solicitud.TecnicoAsignado.TrabajosActivos < solicitud.TecnicoAsignado.LimiteTrabajos)
                {
                    solicitud.TecnicoAsignado.Estado = EstadoTecnico.Disponible;
                }

                solicitud.Estado = EstadoSolicitud.Entregada;
                solicitud.HistorialEstados.Push(solicitud.Estado);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n¡Solicitud Entregada y Técnico liberado exitosamente!");
                Console.ResetColor();
            }
            else
            {
                solicitud.Estado = (EstadoSolicitud)((int)solicitud.Estado + 1);
                solicitud.HistorialEstados.Push(solicitud.Estado);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSe ha avanzado el estado a: {solicitud.Estado.ObtenerDescripcion()}");
                Console.ResetColor();
            }

            LimpiarConsola();
        }

        public static void CancelarSolicitud(Solicitud solicitud)
        {
            if (solicitud.Estado == EstadoSolicitud.Entregada || solicitud.Estado == EstadoSolicitud.Cancelada)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se puede cancelar una solicitud ya finalizada o cancelada.");
                Console.ResetColor();
            }
            else
            {
                if (solicitud.TecnicoAsignado != null)
                {
                    solicitud.TecnicoAsignado.TrabajosActivos--;
                    if (solicitud.TecnicoAsignado.TrabajosActivos < solicitud.TecnicoAsignado.LimiteTrabajos)
                    {
                        solicitud.TecnicoAsignado.Estado = EstadoTecnico.Disponible;
                    }
                }

                solicitud.Estado = EstadoSolicitud.Cancelada;
                solicitud.HistorialEstados.Push(solicitud.Estado);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Se ha cancelado la solicitud correctamente.");
                Console.ResetColor();
            }
            LimpiarConsola();
        }

        public static void DeshacerUltimoEstado(Solicitud solicitud)
        {
            if (solicitud.Estado == EstadoSolicitud.Entregada || solicitud.Estado == EstadoSolicitud.Cancelada)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: No se pueden deshacer solicitudes ya finalizadas o canceladas.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            if (solicitud.HistorialEstados.Count > 1)
            {
                solicitud.HistorialEstados.Pop();
                solicitud.Estado = solicitud.HistorialEstados.Peek();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSe ha deshecho el último cambio. El estado actual es: {solicitud.Estado.ObtenerDescripcion()}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: No hay estados anteriores en el historial para deshacer.");
                Console.ResetColor();
            }
            LimpiarConsola();
        }

        public static void ListaDeSolicitudes(SistemaSoporte sistema)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("        LISTA DE SOLICITUDES       ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Solicitud solicitud in sistema.ListaSolicitudesGenerales)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{sistema.ListaSolicitudesGenerales.IndexOf(solicitud) + 1}. Estado: ");
                Console.ResetColor();
                Console.Write($"{solicitud.Estado.ObtenerDescripcion()} || ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Prioridad: ");
                Console.ResetColor();
                Console.Write($"{solicitud.Prioridad} || ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"ID: ");
                Console.ResetColor();
                Console.Write($"{solicitud.CodigoSolicitud}");
                Console.WriteLine();
            }
        }

        public static void ListaEquiposCliente(List<EquipoBase> equipos)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("      EQUIPOS DEL CLIENTE          ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();

            foreach (EquipoBase equipo in equipos)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{equipos.IndexOf(equipo) + 1}. Tipo: ");
                Console.ResetColor();
                Console.Write($"{equipo.TipoEquipo.ObtenerDescripcion()} || ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Serie: ");
                Console.ResetColor();
                Console.Write($"{equipo.NumeroSerie} || ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"ID: ");
                Console.ResetColor();
                Console.Write($"{equipo.CodigoInterno}");
                Console.WriteLine();
            }
        }

        public static void OpcionesMenuSolicitudes()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("         MÓDULO SOLICITUDES        ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Registrar una Solicitud");
            Console.WriteLine("2. Consultar Información de Solicitud");
            Console.WriteLine("3. Modificar / Actualizar Solicitud");
            Console.WriteLine("4. Volver al Menú Principal");
            Console.ResetColor();
        }

        public static void OpcionesSubMenuModificarSolicitud(Solicitud solicitud)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine($"MENÚ SOLICITUD  ({solicitud.CodigoSolicitud})");
            Console.WriteLine("=====================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Diagnosticar y Asignar Repuestos");
            Console.WriteLine("2. Avanzar al Siguiente Estado");
            Console.WriteLine("3. Cancelar Solicitud");
            Console.WriteLine("4. Deshacer Último Estado (Ctrl+Z)");
            Console.WriteLine("5. Regresar a Gestión de Solicitudes");
            Console.ResetColor();
        }

        public static void MenuPrioridad()
        {
            List<string> listaPrio = Enum.GetValues(typeof(PrioridadSolicitud))
                                   .Cast<PrioridadSolicitud>()
                                   .Select(m => m.ObtenerDescripcion())
                                   .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\nSELECCIONE LA PRIORIDAD");
            Console.ResetColor();
            Console.WriteLine();

            foreach (string prio in listaPrio)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{listaPrio.IndexOf(prio) + 1}. ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{prio}");
                Console.ResetColor();
            }
        }
    }
}
