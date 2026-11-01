using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XelaTechSoporte
{
    internal static class AyudanteConsola
    {
        public static string ObtenerDescripcion(this Enum valor)
        {
            FieldInfo field = valor.GetType().GetField(valor.ToString());
            DescriptionAttribute attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            return attribute != null ? attribute.Description : valor.ToString();
        }

        internal static void Mostrar(string textoMostrar, int duracionSegundos)
        {
            int ciclosTotales = duracionSegundos * 4;

            for (int i = 0; i < ciclosTotales; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                int cantidadPuntos = i % 4;
                string puntos = new string('.', cantidadPuntos);

                string espaciosBlancos = new string(' ', 3 - cantidadPuntos);

                Console.Write($"\r{textoMostrar}{puntos}{espaciosBlancos}");

                Thread.Sleep(250);
            }
            Console.ResetColor();

            Console.Write("\r" + new string(' ', 20) + "\r");
        }

        internal static string ValidarTexto(string mensajeSolicitudDeDatos, int limiteCaracteres)
        {
            bool textoConfirmado = false;
            string textoFinal = "";
            int anchoConsola = Console.WindowWidth;
            int espacios = (anchoConsola - mensajeSolicitudDeDatos.Length) / 2;
            if (espacios < 0) espacios = 0;

            while (!textoConfirmado)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(new string(' ', espacios) + mensajeSolicitudDeDatos.ToUpper());
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Presione Enter para enviar\n\n Ingresar: ");
                Console.ResetColor();

                StringBuilder sb = new StringBuilder(limiteCaracteres - 1, limiteCaracteres);

                do
                {
                    ConsoleKeyInfo teclaInfo = Console.ReadKey(intercept: true);

                    if (teclaInfo.Key == ConsoleKey.Enter && sb.Length > 0 && sb.Length <= limiteCaracteres)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"\n\nConfirmar el texto ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(sb.ToString());
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" (Yes/No): ");
                        Console.ResetColor();

                        string respuesta = Console.ReadLine().ToLower().Trim();

                        if (respuesta == "yes")
                        {
                            textoFinal = sb.ToString();
                            textoConfirmado = true;
                            Console.Clear();
                            break;
                        }
                        else if (respuesta == "no")
                        {
                            Mostrar("Reiniciando texto", 1);
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nElección inválida");
                            Console.ResetColor();
                            Mostrar("Reiniciando texto", 1);
                            break;
                        }
                    }

                    if (teclaInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        Console.Write("\b \b");
                    }

                    if (sb.Length < limiteCaracteres)
                    {
                        if (char.IsLetter(teclaInfo.KeyChar))
                        {
                            sb.Append(teclaInfo.KeyChar);
                            Console.Write(teclaInfo.KeyChar);
                        }
                        if (teclaInfo.Key == ConsoleKey.Spacebar && sb.Length > 0 && sb.Length + 1 != limiteCaracteres)
                        {
                            if (sb[sb.Length - 1] != ' ')
                            {
                                sb.Append(' ');
                                Console.Write(' ');
                            }
                        }
                    }
                } while (true);
            }
            return textoFinal;
        }

        internal static string ValidarAlfanumerico(string mensajePedirDatos, int limiteCaracteres)
        {
            bool alfanumericoConfirmado = false;
            string alfanumericoFinal="";
            int anchoConsola = Console.WindowWidth;
            int espacios = (anchoConsola - mensajePedirDatos.Length) / 2;
            if (espacios < 0) espacios = 0;

            while (!alfanumericoConfirmado)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(new string(' ', espacios) + mensajePedirDatos.ToUpper());
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\n\n Ingresar: ");
                Console.ResetColor();

                StringBuilder sb = new StringBuilder(limiteCaracteres - 1, limiteCaracteres);

                do
                {
                    ConsoleKeyInfo teclaInfo = Console.ReadKey(intercept: true);

                    if (teclaInfo.Key == ConsoleKey.Enter && sb.Length > 0 && sb.Length <= limiteCaracteres)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"\n\nConfirmar el texto ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(sb.ToString());
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" (Yes/No): ");
                        Console.ResetColor();

                        string respuesta = Console.ReadLine().ToLower().Trim();

                        if (respuesta == "yes")
                        {
                            alfanumericoFinal = sb.ToString();
                            alfanumericoConfirmado = true;
                            Console.Clear();
                            break;
                        }
                        else if (respuesta == "no")
                        {
                            Mostrar("Reiniciando texto", 1);
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nElección inválida");
                            Console.ResetColor();
                            Mostrar("Reiniciando texto", 1);
                            break;
                        }
                    }

                    if (teclaInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        Console.Write("\b \b");
                    }

                    if (sb.Length < limiteCaracteres && teclaInfo.Key != ConsoleKey.Spacebar && teclaInfo.Key != ConsoleKey.Backspace)
                    {
                        if (char.IsLetterOrDigit(teclaInfo.KeyChar) || teclaInfo.KeyChar == '-')
                        {
                            sb.Append(teclaInfo.KeyChar);
                            Console.Write(teclaInfo.KeyChar);
                        }
                    }
                } while (true);
            }
            return alfanumericoFinal;
        }

        internal static int ValidarTelefono()
        {
            bool telefonoConfirmado = false;
            int telefonoFinal = 0;
            string mensajeSolicitudDeDatos = "Ingrese su número de teléfono";
            int tamanoRequerido = 8;
            int anchoConsola = Console.WindowWidth;
            int espacios = (anchoConsola - mensajeSolicitudDeDatos.Length) / 2;
            if (espacios < 0) espacios = 0;

            while (!telefonoConfirmado)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(new string(' ', espacios) + mensajeSolicitudDeDatos.ToUpper());
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Presione Enter para enviar. \nEl número debe ser de 8 dígitos.\n\n Ingresar: ");
                Console.ResetColor();

                StringBuilder sb = new StringBuilder(tamanoRequerido, tamanoRequerido);

                do
                {
                    ConsoleKeyInfo teclaInfo = Console.ReadKey(intercept: true);

                    if (teclaInfo.Key == ConsoleKey.Enter && sb.Length == tamanoRequerido)
                    {
                        if (int.TryParse(sb.ToString(), out int numeroConvertido))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"\n\nConfirmar el número de teléfono ");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write($"{numeroConvertido.ToString("####-####")}");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(" (Yes/No): ");
                            Console.ResetColor();

                            string respuesta = Console.ReadLine().ToLower().Trim();

                            if (respuesta == "yes")
                            {
                                telefonoFinal = numeroConvertido;
                                telefonoConfirmado = true;
                                Console.Clear();
                                break;
                            }
                            else if (respuesta == "no")
                            {
                                Mostrar("Reiniciando teléfono", 1);
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nElección inválida");
                                Console.ResetColor();
                                Mostrar("Reiniciando teléfono", 1);
                                break;
                            }
                        }
                    }

                    if (teclaInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                    {
                        if (sb.Length == 4)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            Console.Write("\b \b\b \b");
                        }
                        else
                        {
                            sb.Remove(sb.Length - 1, 1);
                            Console.Write("\b \b");
                        }
                    }
                    else if (sb.Length < tamanoRequerido)
                    {
                        if (char.IsDigit(teclaInfo.KeyChar))
                        {
                            if (teclaInfo.KeyChar == '0' && sb.Length == 0)
                            {
                                continue;
                            }

                            sb.Append(teclaInfo.KeyChar);

                            if (sb.Length == 4)
                            {
                                Console.Write(teclaInfo.KeyChar + "-");
                            }
                            else
                            {
                                Console.Write(teclaInfo.KeyChar);
                            }
                        }
                    }
                } while (true);
            }
            return telefonoFinal;
        }

        internal static string ValidarCorreo()
        {
            bool correoConfirmado = false;
            string mensajeSolicitudDeDatos = "Ingrese su correo";
            int limiteCaracteres = 35;
            int anchoConsola = Console.WindowWidth;
            int espacios = (anchoConsola - mensajeSolicitudDeDatos.Length) / 2;
            if (espacios < 0) espacios = 0;
            string correoFinal = string.Empty;

            while (!correoConfirmado)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(new string(' ', espacios) + mensajeSolicitudDeDatos.ToUpper());
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Presione Enter para enviar\n\n Ingresar: ");
                Console.ResetColor();

                StringBuilder sb = new StringBuilder(limiteCaracteres - 1, limiteCaracteres);

                do
                {
                    ConsoleKeyInfo teclaInfo = Console.ReadKey(intercept: true);

                    if (teclaInfo.Key == ConsoleKey.Enter && sb.Length > 0 && sb.Length <= limiteCaracteres && sb.ToString().Contains('@') && sb.ToString().Contains('.') && sb[sb.Length - 1] != '.' && sb[sb.Length - 1] != ' ')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"\n\nConfirmar correo ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(sb.ToString());
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" (Yes/No): ");
                        Console.ResetColor();

                        string respuesta = Console.ReadLine().ToLower().Trim();

                        if (respuesta == "yes")
                        {
                            correoFinal = sb.ToString();
                            correoConfirmado = true;
                            Console.Clear();
                            break;
                        }
                        else if (respuesta == "no")
                        {
                            Mostrar("Reiniciando correo", 1);
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nElección inválida");
                            Console.ResetColor();
                            Mostrar("Reiniciando correo", 1);
                            break;
                        }
                    }

                    if (teclaInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        Console.Write("\b \b");
                    }

                    if (sb.Length < limiteCaracteres)
                    {
                        if (char.IsLetterOrDigit(teclaInfo.KeyChar))
                        {
                            sb.Append(teclaInfo.KeyChar);
                            Console.Write(teclaInfo.KeyChar);
                        }
                        if (teclaInfo.KeyChar == '.')
                        {
                            if (sb.ToString().Contains('@') && sb[sb.Length - 1] != '.' && sb[sb.Length - 1] != '@' && sb.Length + 1 != limiteCaracteres)
                            {
                                sb.Append('.');
                                Console.Write('.');
                            }
                        }
                        if (teclaInfo.KeyChar == '@')
                        {
                            if (!sb.ToString().Contains('@') && sb.Length > 0 && sb.Length < 15)
                            {
                                sb.Append('@');
                                Console.Write('@');
                            }
                        }
                        if (!sb.ToString().Contains('@') && sb.Length == 15 && teclaInfo.Key != ConsoleKey.Backspace)
                        {
                            sb.Append('@');
                            Console.Write('@');
                        }
                    }
                } while (true);
            }
            return correoFinal;
        }

        internal static double ValidarDecimal(string mensajeSolicitudDeDatos)
        {
            bool numeroConfirmado = false;
            double numeroFinal = 0;
            int anchoConsola = Console.WindowWidth;
            int espacios = (anchoConsola - mensajeSolicitudDeDatos.Length) / 2;
            if (espacios < 0) espacios = 0;

            while (!numeroConfirmado)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(new string(' ', espacios) + mensajeSolicitudDeDatos.ToUpper());
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Presione Enter para enviar.\n\n Ingresar: Q");
                Console.ResetColor();

                StringBuilder sb = new StringBuilder();

                do
                {
                    ConsoleKeyInfo teclaInfo = Console.ReadKey(intercept: true);

                    if (teclaInfo.Key == ConsoleKey.Enter && sb.Length > 0)
                    {
                        if (double.TryParse(sb.ToString(), out double numeroConvertido))
                        {
                            if (numeroConvertido >= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($"\n\nConfirmar el monto de ");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write($"Q{numeroConvertido:F2}");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(" (Yes/No): ");
                                Console.ResetColor();
                                string respuesta = Console.ReadLine().ToLower().Trim();

                                if (respuesta == "yes")
                                {
                                    numeroFinal = numeroConvertido;
                                    numeroConfirmado = true;
                                    Console.Clear();
                                    break;
                                }
                                else if (respuesta == "no")
                                {
                                    Mostrar("Reiniciando monto", 1);
                                    break;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nElección inválida");
                                    Console.ResetColor();
                                    Mostrar("Reiniciando monto", 1);
                                    break;
                                }
                            }
                        }
                    }

                    if (teclaInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                    else if (char.IsDigit(teclaInfo.KeyChar))
                    {
                        sb.Append(teclaInfo.KeyChar);
                        Console.Write(teclaInfo.KeyChar);
                    }
                    else if (teclaInfo.KeyChar == '.')
                    {
                        if (!sb.ToString().Contains(".") && sb.Length > 0)
                        {
                            sb.Append(teclaInfo.KeyChar);
                            Console.Write(teclaInfo.KeyChar);
                        }
                    }
                } while (true);
            }
            return numeroFinal;
        }

        internal static int ValidarNumerico(string mensajeSolicitudDeDatos, int limiteCaracteres, bool esLimite)
        {
            bool numeroConfirmado = false;
            int numeroFinal = 0;
            int anchoConsola = Console.WindowWidth;
            int espacios = (anchoConsola - mensajeSolicitudDeDatos.Length) / 2;
            if (espacios < 0) espacios = 0;

            while (!numeroConfirmado)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(new string(' ', espacios) + mensajeSolicitudDeDatos.ToUpper());
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Presione Enter para enviar. \nEl número puede tener hasta ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(limiteCaracteres);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" dígitos. \n\n Ingresar: ");
                Console.ResetColor();

                StringBuilder sb = new StringBuilder(limiteCaracteres);

                do
                {
                    ConsoleKeyInfo teclaInfo = Console.ReadKey(intercept: true);

                    if (teclaInfo.Key == ConsoleKey.Enter && sb.Length > 0 && sb.Length <= limiteCaracteres)
                    {
                        if (int.TryParse(sb.ToString(), out int numeroConvertido))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"\n\nConfirmar el número ");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(numeroConvertido);
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(" (Yes/No): ");
                            Console.ResetColor();
                            string respuesta = Console.ReadLine().ToLower().Trim();

                            if (respuesta == "yes")
                            {
                                numeroFinal = numeroConvertido;
                                numeroConfirmado = true;
                                Console.Clear();
                                break;
                            }
                            else if (respuesta == "no")
                            {
                                Mostrar("Reiniciando número", 1);
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nElección inválida");
                                Console.ResetColor();
                                Mostrar("Reiniciando número", 1);
                                break;
                            }
                        }
                    }

                    if (teclaInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        Console.Write("\b \b");
                    }

                    if (sb.Length < limiteCaracteres)
                    {
                        if (char.IsDigit(teclaInfo.KeyChar))
                        {
                            if (teclaInfo.KeyChar == '0' && sb.Length == 0)
                            {
                                continue;
                            }
                            else
                            {
                                sb.Append(teclaInfo.KeyChar);
                                Console.Write(teclaInfo.KeyChar);
                            }
                        }
                    }
                } while (true);
            }
            return numeroFinal;
        }

        internal static int ValidarNumerico(int limiteIgnorado, Action menuMostrar, int rangoValido)
        {
            bool numeroConfirmado = false;
            int numeroFinal = 0;
            int limiteDinamico = rangoValido.ToString().Length;

            while (!numeroConfirmado)
            {
                Console.Clear();
                menuMostrar();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n Ingresar: ");
                Console.ResetColor();

                StringBuilder sb = new StringBuilder(limiteDinamico);

                do
                {
                    ConsoleKeyInfo teclaInfo = Console.ReadKey(intercept: true);

                    if (teclaInfo.Key == ConsoleKey.Enter && sb.Length > 0)
                    {
                        if (int.TryParse(sb.ToString(), out int numeroConvertido))
                        {
                            if (numeroConvertido >= 1 && numeroConvertido <= rangoValido)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($"\n\nConfirmar el número ");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(numeroConvertido);
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(" (Yes/No): ");
                                Console.ResetColor();

                                string respuesta = Console.ReadLine().ToLower().Trim();

                                if (respuesta == "yes")
                                {
                                    numeroFinal = numeroConvertido;
                                    numeroConfirmado = true;
                                    Console.Clear();
                                    break;
                                }
                                else if (respuesta == "no")
                                {
                                    Mostrar("Reiniciando número", 1);
                                    break;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nElección inválida");
                                    Console.ResetColor();
                                    Mostrar("Reiniciando número", 1);
                                    break;
                                }
                            }
                        }
                    }

                    if (teclaInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        Console.Write("\b \b");
                    }

                    if (sb.Length < limiteDinamico)
                    {
                        if (char.IsDigit(teclaInfo.KeyChar))
                        {
                            if (teclaInfo.KeyChar == '0' && sb.Length == 0)
                            {
                                continue;
                            }
                            else
                            {
                                sb.Append(teclaInfo.KeyChar);
                                Console.Write(teclaInfo.KeyChar);
                            }
                        }
                    }
                } while (true);
            }
            return numeroFinal;
        }

        internal static int ValidarNumerico<T>(int limiteIgnorado, Action<T> menuMostrar, T parametroMenu, int rangoValido)
        {
            bool numeroConfirmado = false;
            int numeroFinal = 0;
            int limiteDinamico = rangoValido.ToString().Length;

            while (!numeroConfirmado)
            {
                Console.Clear();
                menuMostrar(parametroMenu);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n Ingresar: ");
                Console.ResetColor();

                StringBuilder sb = new StringBuilder(limiteDinamico);

                do
                {
                    ConsoleKeyInfo teclaInfo = Console.ReadKey(intercept: true);

                    if (teclaInfo.Key == ConsoleKey.Enter && sb.Length > 0)
                    {
                        if (int.TryParse(sb.ToString(), out int numeroConvertido))
                        {
                            if (numeroConvertido >= 1 && numeroConvertido <= rangoValido)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($"\n\nConfirmar el número ");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(numeroConvertido);
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(" (Yes/No): ");
                                Console.ResetColor();

                                string respuesta = Console.ReadLine().ToLower().Trim();

                                if (respuesta == "yes")
                                {
                                    numeroFinal = numeroConvertido;
                                    numeroConfirmado = true;
                                    Console.Clear();
                                    break;
                                }
                                else if (respuesta == "no")
                                {
                                    Mostrar("Reiniciando número", 1);
                                    break;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nElección inválida");
                                    Console.ResetColor();
                                    Mostrar("Reiniciando número", 1);
                                    break;
                                }
                            }
                        }
                    }

                    if (teclaInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        Console.Write("\b \b");
                    }

                    if (sb.Length < limiteDinamico)
                    {
                        if (char.IsDigit(teclaInfo.KeyChar))
                        {
                            if (teclaInfo.KeyChar == '0' && sb.Length == 0)
                            {
                                continue;
                            }
                            else
                            {
                                sb.Append(teclaInfo.KeyChar);
                                Console.Write(teclaInfo.KeyChar);
                            }
                        }
                    }
                } while (true);
            }
            return numeroFinal;
        }
    }
}
