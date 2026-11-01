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
    internal static class ModuloEquipos
    {
        public static void IniciarSubmenu(SistemaSoporte sistema)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesMenuEquipos, 4);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        RegistrarEquipo(sistema);
                        break;
                    case 2:
                        MostarInformacionEquipo(sistema);
                        break;
                    case 3:
                        AbrirSubmenuModificarInformacionEquipo(sistema);
                        break;
                    case 4:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 4);
        }

        public static void RegistrarEquipo(SistemaSoporte sistema)
        {
            if (sistema.ListaClientes.Count == 0 || sistema.ListaClientes == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No hay ningún Cliente registrado en el sistema.");
                Console.WriteLine("Debe registrar al menos un Cliente antes de ingresar un Equipo.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            string numeroSerie = ValidarAlfanumerico("Ingrese el Número de Serie", 15);

            bool serieExiste = sistema.ListaEquipos.Any(e => e.NumeroSerie.Equals(numeroSerie, StringComparison.OrdinalIgnoreCase));
            if (serieExiste)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: El número de serie ya está registrado en el sistema.");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }

            string marca = ValidarTexto("Ingrese la Marca del equipo", 20);
            string modelo = ValidarAlfanumerico("Ingrese el Modelo", 20);

            int posicionCliente = ValidarNumerico(1, ModuloClientes.ListaDeClientes, sistema, sistema.ListaClientes.Count);
            Cliente propietario = sistema.ListaClientes[posicionCliente - 1];

            int numeroTipoEquipo = ValidarNumerico(1, MenuTipoEquipos, Enum.GetNames(typeof(TipoEquipo)).Length);
            TipoEquipo tipoEquipo = (TipoEquipo)numeroTipoEquipo;

            string estadoFisico = ValidarTexto("Ingrese el estado físico recibido", 40);
            string accesorios = ValidarTexto("Ingrese los accesorios entregados", 50);

            EquipoBase equipo;

            if (tipoEquipo == TipoEquipo.ComputadoraPortatil)
            {
                int respuestaCargador = ValidarNumerico("¿Incluye cargador original? (1 = Sí, 2 = No)", 1, true);
                bool poseeCargador = (respuestaCargador == 1);
                equipo = new Portatil(numeroSerie, marca, modelo, propietario, estadoFisico, accesorios, poseeCargador);
            }
            else if (tipoEquipo == TipoEquipo.ComputadoraEscritorio)
            {
                int respuestaMonitor = ValidarNumerico("¿Incluye monitor? (1 = Sí, 2 = No)", 1, true);
                bool poseeMonitor = (respuestaMonitor == 1);
                equipo = new Escritorio(numeroSerie, marca, modelo, propietario, estadoFisico, accesorios, poseeMonitor);
            }
            else
            {
                int nivelTinta = ValidarNumerico("Ingrese el nivel de tinta estimado (1 a 100)", 3, true);
                equipo = new Impresora(numeroSerie, marca, modelo, propietario, estadoFisico, accesorios, nivelTinta);
            }

            sistema.IngresarEquipo(equipo);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Se Registró Correctamente el Equipo.");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void MostarInformacionEquipo(SistemaSoporte sistema)
        {
            if (sistema.ListaEquipos.Count == 0 || sistema.ListaEquipos == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay ningún Equipo Registrado");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }
            else
            {
                int posicionEquipo = ValidarNumerico(1, ListaDeEquipos, sistema, sistema.ListaEquipos.Count);
                Console.WriteLine();
                sistema.ListaEquipos[posicionEquipo - 1].MostrarInformacion();
                LimpiarConsola();
                return;
            }
        }

        public static void AbrirSubmenuModificarInformacionEquipo(SistemaSoporte sistema)
        {
            if (sistema.ListaEquipos.Count == 0 || sistema.ListaEquipos == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay ningún Equipo Registrado");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }
            else
            {
                int posicionEquipo = ValidarNumerico(1, ListaDeEquipos, sistema, sistema.ListaEquipos.Count);
                Console.WriteLine();
                SubmenuModificacionEquipo(sistema.ListaEquipos[posicionEquipo - 1]);
                OpcionMenuPrincipal = 0;
                LimpiarConsola();
                return;
            }
        }

        public static void SubmenuModificacionEquipo(EquipoBase equipo)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesSubMenuModificarEquipo, equipo, 3);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        ModificarEstadoFisico(equipo);
                        break;
                    case 2:
                        ModificarAccesorios(equipo);
                        break;
                    case 3:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 3);
        }

        public static void ModificarEstadoFisico(EquipoBase equipo)
        {
            string nuevoEstado = ValidarTexto("Ingrese el nuevo estado físico: ", 40);
            equipo.EstadoFisico = nuevoEstado;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Estado físico modificado exitosamente!");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void ModificarAccesorios(EquipoBase equipo)
        {
            string nuevosAccesorios = ValidarTexto("Ingrese la actualización de accesorios: ", 50);
            equipo.Accesorios = nuevosAccesorios;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Accesorios modificados exitosamente!");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void ListaDeEquipos(SistemaSoporte sistema)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("          LISTA DE EQUIPOS         ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();

            foreach (EquipoBase equipo in sistema.ListaEquipos)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{sistema.ListaEquipos.IndexOf(equipo) + 1}. Tipo: ");
                Console.ResetColor();
                Console.Write($"{equipo.TipoEquipo.ObtenerDescripcion()} || ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Serie: ");
                Console.ResetColor();
                Console.Write($"{equipo.NumeroSerie} || ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Propietario: ");
                Console.ResetColor();
                Console.Write($"{equipo.Propietario.NombreCompleto}");
                Console.WriteLine();
            }
        }

        public static void OpcionesMenuEquipos()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("           MÓDULO EQUIPOS          ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Registrar un Equipo");
            Console.WriteLine("2. Consultar Información de Equipo");
            Console.WriteLine("3. Modificar Información de Equipo");
            Console.WriteLine("4. Volver al Menú Principal");
            Console.ResetColor();
        }

        public static void OpcionesSubMenuModificarEquipo(EquipoBase equipo)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine($"MENÚ EQUIPO  ({equipo.CodigoInterno})");
            Console.WriteLine("=====================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Modificar el Estado Físico");
            Console.WriteLine("2. Modificar los Accesorios");
            Console.WriteLine("3. Regresar a Gestión de Equipos");
            Console.ResetColor();
        }

        public static void MenuTipoEquipos()
        {
            List<string> listaTipos = Enum.GetValues(typeof(TipoEquipo))
                                   .Cast<TipoEquipo>()
                                   .Select(m => m.ObtenerDescripcion())
                                   .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\nSELECCIONE EL TIPO DE EQUIPO");
            Console.ResetColor();
            Console.WriteLine();

            foreach (string tipo in listaTipos)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{listaTipos.IndexOf(tipo) + 1}. ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{tipo}");
                Console.ResetColor();
            }
        }
    }
}
