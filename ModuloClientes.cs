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
    internal static class ModuloClientes
    {
        public static void IniciarSubmenu(SistemaSoporte sistema)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesMenuClientes, 4);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        RegistrarCliente(sistema);
                        break;
                    case 2:
                        MostarInformacionCliente(sistema);
                        break;
                    case 3:
                        AbrirSubmenuModificarInformacionCliente(sistema);
                        break;
                    case 4:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 4);
        }

        public static void RegistrarCliente(SistemaSoporte sistema)
        {
            string nombreCompletoCliente = ValidarTexto("Ingrese el Nombre Completo: ", 35);
            int numeroTelefonoCliente = ValidarTelefono();
            string correoElectronicoCliente = ValidarCorreo();

            int numeroTipoCliente = ValidarNumerico(1, MenuTipoCliente, Enum.GetNames(typeof(TipoCliente)).Length);
            TipoCliente tipoCliente = (TipoCliente)numeroTipoCliente;

            Cliente cliente = new Cliente(nombreCompletoCliente, numeroTelefonoCliente, correoElectronicoCliente, tipoCliente);
            sistema.IngresarCliente(cliente);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Se Registró Correctamente al Cliente.");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void MostarInformacionCliente(SistemaSoporte sistema)
        {
            if (sistema.ListaClientes.Count == 0 || sistema.ListaClientes == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay ningún Cliente Registrado");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }
            else
            {
                int posicionCliente = ValidarNumerico(1, ListaDeClientes, sistema, sistema.ListaClientes.Count);
                Console.WriteLine();
                sistema.ListaClientes[posicionCliente - 1].MostrarInformacion();
                LimpiarConsola();
                return;
            }
        }

        public static void AbrirSubmenuModificarInformacionCliente(SistemaSoporte sistema)
        {
            if (sistema.ListaClientes.Count == 0 || sistema.ListaClientes == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay ningún Cliente Registrado");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }
            else
            {
                int posicionCliente = ValidarNumerico(1, ListaDeClientes, sistema, sistema.ListaClientes.Count);
                Console.WriteLine();
                SubmenuModificacionCliente(sistema.ListaClientes[posicionCliente - 1]);
                LimpiarConsola();
                return;
            }
        }

        public static void SubmenuModificacionCliente(Cliente cliente)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesSubMenuModificarCliente, cliente, 5);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        ModificarNumeroTelefonoCliente(cliente);
                        break;
                    case 2:
                        ModificarCorreoCliente(cliente);
                        break;
                    case 3:
                        ModificarTipoCliente(cliente);
                        break;
                    case 4:
                        ModificarNombreCliente(cliente);
                        break;
                    case 5:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 5);
        }

        public static void ModificarNumeroTelefonoCliente(Cliente cliente)
        {
            int nuevoNumeroTelefono = ValidarTelefono();
            cliente.NumeroTelefono = nuevoNumeroTelefono;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Número de teléfono modificado exitosamente!");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void ModificarCorreoCliente(Cliente cliente)
        {
            string nuevoCorreo = ValidarCorreo();
            cliente.CorreoElectronico = nuevoCorreo;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Correo electrónico modificado exitosamente!");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void ModificarTipoCliente(Cliente cliente)
        {
            int numeroTipoCliente = ValidarNumerico(1, MenuTipoCliente, Enum.GetNames(typeof(TipoCliente)).Length);
            TipoCliente nuevoTipo = (TipoCliente)numeroTipoCliente;
            cliente.TipoCliente = nuevoTipo;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Tipo de cliente modificado exitosamente!");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void ModificarNombreCliente(Cliente cliente)
        {
            string nuevoNombre = ValidarTexto("Ingrese el nuevo Nombre Completo: ", 35);
            cliente.NombreCompleto = nuevoNombre;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Nombre modificado exitosamente!");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void ListaDeClientes(SistemaSoporte sistema)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("          LISTA DE CLIENTES        ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Cliente cliente in sistema.ListaClientes)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{sistema.ListaClientes.IndexOf(cliente) + 1}. Nombre: ");
                Console.ResetColor();
                Console.Write($"{cliente.NombreCompleto} || ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Tipo: ");
                Console.ResetColor();
                Console.Write($"{cliente.TipoCliente.ObtenerDescripcion()} || ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"ID: ");
                Console.ResetColor();
                Console.Write($"{cliente.CodigoUnico}");
                Console.WriteLine();
            }
        }

        public static void OpcionesMenuClientes()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("           MÓDULO CLIENTES         ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Registrar un Cliente");
            Console.WriteLine("2. Consultar Información de Cliente");
            Console.WriteLine("3. Modificar Información de Cliente");
            Console.WriteLine("4. Volver al Menú Principal");
            Console.ResetColor();
        }

        public static void OpcionesSubMenuModificarCliente(Cliente cliente)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine($"MENÚ CLIENTE  ({cliente.CodigoUnico})");
            Console.WriteLine("=====================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Modificar el Número de Teléfono");
            Console.WriteLine("2. Modificar el Correo Electrónico");
            Console.WriteLine("3. Modificar Tipo de Cliente");
            Console.WriteLine("4. Modificar el Nombre ");
            Console.WriteLine("5. Regresar a Gestión de Clientes");
            Console.ResetColor();
        }

        public static void MenuTipoCliente()
        {
            List<string> listaTipos = Enum.GetValues(typeof(TipoCliente))
                                   .Cast<TipoCliente>()
                                   .Select(m => m.ObtenerDescripcion())
                                   .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\nSELECCIONE EL TIPO DE CLIENTE");
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