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
    internal static class ModuloRepuestos
    {
        public static void IniciarSubmenu(SistemaSoporte sistema)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesMenuRepuestos, 4);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        RegistrarRepuesto(sistema);
                        break;
                    case 2:
                        MostarInformacionRepuesto(sistema);
                        break;
                    case 3:
                        AbrirSubmenuModificarInformacionRepuesto(sistema);
                        break;
                    case 4:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 4);
        }

        public static void RegistrarRepuesto(SistemaSoporte sistema)
        {
            string nombre = ValidarTexto("Ingrese el Nombre del repuesto", 40);

            int numeroCategoria = ValidarNumerico(1, MenuCategoriaRepuesto, Enum.GetNames(typeof(CategoriaRepuesto)).Length);
            CategoriaRepuesto categoria = (CategoriaRepuesto)numeroCategoria;

            int existencia = ValidarNumerico("Ingrese la Existencia Inicial (hasta 4 dígitos)", 4, true);

            int existenciaMinima = ValidarNumerico("Ingrese la Existencia Mínima permitida (hasta 4 dígitos)", 4, true);

            double costoUnitario = ValidarDecimal("Ingrese el Costo Unitario");

            double precioVenta = ValidarDecimal("Ingrese el Precio de Venta al Público");

            Repuesto repuesto = new Repuesto(nombre, categoria, existencia, existenciaMinima, costoUnitario, precioVenta);
            sistema.IngresarRepuesto(repuesto);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Se Registró Correctamente el Repuesto.");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void MostarInformacionRepuesto(SistemaSoporte sistema)
        {
            if (sistema.ListaRepuestos.Count == 0 || sistema.ListaRepuestos == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay ningún Repuesto Registrado");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }
            else
            {
                int posicionRepuesto = ValidarNumerico(1, ListaDeRepuestos, sistema, sistema.ListaRepuestos.Count);
                Console.WriteLine();
                sistema.ListaRepuestos[posicionRepuesto - 1].MostrarInformacion();
                LimpiarConsola();
                return;
            }
        }

        public static void AbrirSubmenuModificarInformacionRepuesto(SistemaSoporte sistema)
        {
            if (sistema.ListaRepuestos.Count == 0 || sistema.ListaRepuestos == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("No hay ningún Repuesto Registrado");
                Console.ResetColor();
                LimpiarConsola();
                return;
            }
            else
            {
                int posicionRepuesto = ValidarNumerico(1, ListaDeRepuestos, sistema, sistema.ListaRepuestos.Count);
                Console.WriteLine();
                SubmenuModificacionRepuesto(sistema.ListaRepuestos[posicionRepuesto - 1]);
                OpcionMenuPrincipal = 0;
                LimpiarConsola();
                return;
            }
        }

        public static void SubmenuModificacionRepuesto(Repuesto repuesto)
        {
            OpcionMenuPrincipal = 0;
            do
            {
                OpcionMenuPrincipal = ValidarNumerico(1, OpcionesSubMenuModificarRepuesto, repuesto, 3);
                Console.Clear();
                switch (OpcionMenuPrincipal)
                {
                    case 1:
                        ModificarExistencia(repuesto);
                        break;
                    case 2:
                        ModificarPrecioVenta(repuesto);
                        break;
                    case 3:

                        break;
                    default:
                        ErroOpcionNoValida();
                        break;
                }
            } while (OpcionMenuPrincipal != 3);
        }

        public static void ModificarExistencia(Repuesto repuesto)
        {
            int nuevaExistencia = ValidarNumerico("Ingrese la nueva cantidad de existencia total", 4, true);
            repuesto.Existencia = nuevaExistencia;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Existencia actualizada exitosamente a {repuesto.Existencia} unidades!");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void ModificarPrecioVenta(Repuesto repuesto)
        {
            double nuevoPrecio = ValidarDecimal("Ingrese el nuevo Precio de Venta al Público");
            repuesto.PrecioVenta = nuevoPrecio;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n¡Precio de Venta actualizado exitosamente a Q{repuesto.PrecioVenta:F2}!");
            Console.ResetColor();
            LimpiarConsola();
        }

        public static void ListaDeRepuestos(SistemaSoporte sistema)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("        LISTA DE REPUESTOS         ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Repuesto repuesto in sistema.ListaRepuestos)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{sistema.ListaRepuestos.IndexOf(repuesto) + 1}. Nombre: ");
                Console.ResetColor();
                Console.Write($"{repuesto.Nombre} || ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Categoría: ");
                Console.ResetColor();
                Console.Write($"{repuesto.Categoria.ObtenerDescripcion()} || ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"ID: ");
                Console.ResetColor();
                Console.Write($"{repuesto.Codigo}");
                Console.WriteLine();
            }
        }

        public static void OpcionesMenuRepuestos()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("          MÓDULO REPUESTOS         ");
            Console.WriteLine("===================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Registrar un Repuesto");
            Console.WriteLine("2. Consultar Información de Repuesto");
            Console.WriteLine("3. Modificar Información de Repuesto");
            Console.WriteLine("4. Volver al Menú Principal");
            Console.ResetColor();
        }

        public static void OpcionesSubMenuModificarRepuesto(Repuesto repuesto)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine($"MENÚ REPUESTO  ({repuesto.Codigo})");
            Console.WriteLine("=====================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Actualizar Existencia Total");
            Console.WriteLine("2. Actualizar Precio de Venta");
            Console.WriteLine("3. Regresar a Gestión de Repuestos");
            Console.ResetColor();
        }

        public static void MenuCategoriaRepuesto()
        {
            List<string> listaCategorias = Enum.GetValues(typeof(CategoriaRepuesto))
                                   .Cast<CategoriaRepuesto>()
                                   .Select(m => m.ObtenerDescripcion())
                                   .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\nSELECCIONE LA CATEGORÍA");
            Console.ResetColor();
            Console.WriteLine();

            foreach (string categoria in listaCategorias)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{listaCategorias.IndexOf(categoria) + 1}. ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{categoria}");
                Console.ResetColor();
            }
        }
    }
}
