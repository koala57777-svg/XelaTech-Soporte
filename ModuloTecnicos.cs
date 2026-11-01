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
    internal static class ModuloTecnicos
    {
        public static void IniciarSubmenu(SistemaSoporte sistema)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesMenuTecnicos, 4);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        RegistrarTecnico(sistema);
                        break;
                    case 2:
                        MostarInformacionTecnico(sistema);
                        break;
                    case 3:
                        AbrirSubmenuModificarInformacionTecnico(sistema);
                        break;
                    case 4:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 4);
        }

        public static void RegistrarTecnico(SistemaSoporte sistema)
        {
            string nombreCompleto = ValidarTexto("Ingrese el Nombre Completo: ", 35);
            int numeroTelefono = ValidarTelefono();

            int numeroEspecialidad = ValidarNumerico(1, MenuEspecialidad, Enum.GetNames(typeof(EspecialidadTecnico)).Length);
            EspecialidadTecnico especialidad = (EspecialidadTecnico)numeroEspecialidad;

            int limiteTrabajos = ValidarNumerico("Ingrese el límite de trabajos simultáneos (1-99)", 2, true);

            Tecnico tecnico = new Tecnico(nombreCompleto, numeroTelefono, especialidad, limiteTrabajos);
            sistema.IngresarTecnico(tecnico);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Se Registró Correctamente al Técnico.");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void MostarInformacionTecnico(SistemaSoporte sistema)
        {
            if (sistema.ListaTecnicos.Count == 0 || sistema.ListaTecnicos == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay ningún Técnico Registrado");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }
            else
            {
                int posicion = ValidarNumerico(1, ListaDeTecnicos, sistema, sistema.ListaTecnicos.Count);
                Console.WriteLine();
                sistema.ListaTecnicos[posicion - 1].MostrarInformacion();
                LimpiarConsola();
                return;
            }
        }

        public static void AbrirSubmenuModificarInformacionTecnico(SistemaSoporte sistema)
        {
            if (sistema.ListaTecnicos.Count == 0 || sistema.ListaTecnicos == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay ningún Técnico Registrado");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }
            else
            {
                int posicion = ValidarNumerico(1, ListaDeTecnicos, sistema, sistema.ListaTecnicos.Count);
                Console.WriteLine();
                SubmenuModificacionTecnico(sistema.ListaTecnicos[posicion - 1]);
                LimpiarConsola();
                return;
            }
        }

        public static void SubmenuModificacionTecnico(Tecnico tecnico)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesSubMenuModificarTecnico, tecnico, 6);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        ModificarNumeroTelefonoTecnico(tecnico);
                        break;
                    case 2:
                        ModificarNombreTecnico(tecnico);
                        break;
                    case 3:
                        ModificarEspecialidadTecnico(tecnico);
                        break;
                    case 4:
                        ModificarLimiteTrabajos(tecnico);
                        break;
                    case 5:
                        ModificarEstadoManual(tecnico);
                        break;
                    case 6:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 6);
        }

        public static void ModificarNumeroTelefonoTecnico(Tecnico tecnico)
        {
            int nuevoNumeroTelefono = ValidarTelefono();
            tecnico.NumeroTelefono = nuevoNumeroTelefono;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Número de teléfono modificado exitosamente!");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void ModificarNombreTecnico(Tecnico tecnico)
        {
            string nuevoNombre = ValidarTexto("Ingrese el nuevo Nombre Completo: ", 35);
            tecnico.NombreCompleto = nuevoNombre;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Nombre modificado exitosamente!");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void ModificarEspecialidadTecnico(Tecnico tecnico)
        {
            int numeroEspecialidad = ValidarNumerico(1, MenuEspecialidad, Enum.GetNames(typeof(EspecialidadTecnico)).Length);
            EspecialidadTecnico nuevaEspecialidad = (EspecialidadTecnico)numeroEspecialidad;
            tecnico.Especialidad = nuevaEspecialidad;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Especialidad modificada exitosamente!");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void ModificarLimiteTrabajos(Tecnico tecnico)
        {
            int nuevoLimite = ValidarNumerico("Ingrese el nuevo límite de trabajos (1-99)", 2, true);
            tecnico.LimiteTrabajos = nuevoLimite;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Límite de trabajos modificado exitosamente!");
            Console.ResetColor();

            if (tecnico.TrabajosActivos < tecnico.LimiteTrabajos && tecnico.Estado == EstadoTecnico.Ocupado)
            {
                tecnico.Estado = EstadoTecnico.Disponible;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("El técnico ahora tiene capacidad. Estado cambiado a Disponible.");
                Console.ResetColor();
            }

            LimpiarConsola();
        }

        public static void ModificarEstadoManual(Tecnico tecnico)
        {
            if (tecnico.Estado == EstadoTecnico.Ocupado)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El técnico está Ocupado por límite de trabajos. Finalice sus solicitudes primero.");
                Console.ResetColor();
            }
            else if (tecnico.Estado == EstadoTecnico.Disponible)
            {
                tecnico.Estado = EstadoTecnico.FueraDeServicio;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Estado modificado exitosamente a: Fuera de Servicio");
                Console.ResetColor();
            }
            else
            {
                tecnico.Estado = EstadoTecnico.Disponible;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Estado modificado exitosamente a: Disponible");
                Console.ResetColor();
            }
            LimpiarConsola();
        }

        public static void ListaDeTecnicos(SistemaSoporte sistema)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("          LISTA DE TÉCNICOS        ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Tecnico tecnico in sistema.ListaTecnicos)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{sistema.ListaTecnicos.IndexOf(tecnico) + 1}. Nombre: ");
                Console.ResetColor();
                Console.Write($"{tecnico.NombreCompleto} || ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Estado: ");
                Console.ResetColor();
                Console.Write($"{tecnico.Estado.ObtenerDescripcion()} || ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"ID: ");
                Console.ResetColor();
                Console.Write($"{tecnico.CodigoUnico}");
                Console.WriteLine();
            }
        }

        public static void OpcionesMenuTecnicos()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("          MÓDULO TÉCNICOS          ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Registrar un Técnico");
            Console.WriteLine("2. Consultar Información de Técnico");
            Console.WriteLine("3. Modificar Información de Técnico");
            Console.WriteLine("4. Volver al Menú Principal");
            Console.ResetColor();
        }

        public static void OpcionesSubMenuModificarTecnico(Tecnico tecnico)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine($"MENÚ TÉCNICO  ({tecnico.CodigoUnico})");
            Console.WriteLine("=====================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Modificar el Número de Teléfono");
            Console.WriteLine("2. Modificar el Nombre");
            Console.WriteLine("3. Modificar Especialidad");
            Console.WriteLine("4. Modificar Límite de Trabajos");
            Console.WriteLine("5. Alternar Estado (Disp/Fuera de Servicio)");
            Console.WriteLine("6. Regresar a Gestión de Técnicos");
            Console.ResetColor();
        }

        public static void MenuEspecialidad()
        {
            List<string> listaEspecialidades = Enum.GetValues(typeof(EspecialidadTecnico))
                                   .Cast<EspecialidadTecnico>()
                                   .Select(m => m.ObtenerDescripcion())
                                   .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\nSELECCIONE LA ESPECIALIDAD");
            Console.ResetColor();
            Console.WriteLine();

            foreach (string especialidad in listaEspecialidades)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{listaEspecialidades.IndexOf(especialidad) + 1}. ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{especialidad}");
                Console.ResetColor();
            }
        }
    }
}
