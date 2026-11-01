using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XelaTechSoporte
{
    internal static class Globales
    {
        private static int opcionMenuPrincipal;

        public static int OpcionMenuPrincipal
        {
            get { return opcionMenuPrincipal; }
            set { opcionMenuPrincipal = value; }
        }

        public static void LimpiarConsola()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Presione CUALQUIER tecla para continuar");
            Console.ResetColor();
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
        }

        public static void SalirMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Gracias por usar XelaTech Soporte :)");
            Console.WriteLine();
            Console.ReadKey();
            Console.ResetColor();
        }

        public static void ErroOpcionNoValida()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: Opción no válida");
            Console.WriteLine();
            Console.ResetColor();
            LimpiarConsola();
        }
    }
}
