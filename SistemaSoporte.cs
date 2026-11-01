using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XelaTechSoporte.Enums;

namespace XelaTechSoporte
{
    internal class SistemaSoporte
    {
        private List<Cliente> listaClientes = new List<Cliente>();

        private List<Tecnico> listaTecnicos = new List<Tecnico>();

        private List<EquipoBase> listaEquipos = new List<EquipoBase>();

        private List<Repuesto> listaRepuestos = new List<Repuesto>();

        private List<Solicitud> listaSolicitudesGenerales = new List<Solicitud>();

        private Queue<Solicitud> colaNormales = new Queue<Solicitud>();

        private Queue<Solicitud> colaUrgentes = new Queue<Solicitud>();

        private int[] conteoCalificaciones = new int[5];

        private int[,] matrizCargaSemanal;

        private double ingresosTotales;

        public SistemaSoporte()
        {
            ListaClientes = new List<Cliente>();
            ListaTecnicos = new List<Tecnico>();
            ListaEquipos = new List<EquipoBase>();
            ListaRepuestos = new List<Repuesto>();
            ListaSolicitudesGenerales = new List<Solicitud>();
            ColaNormales = new Queue<Solicitud>();
            ColaUrgentes = new Queue<Solicitud>();
            MatrizCargaSemanal = new int[0, 7];
            IngresosTotales = 0.00;
        }

        public void IngresarCliente(Cliente cliente)
        {
            if (cliente != null)
            {
                string codigoUnico = CodigoUnico.GenerarCodigoUnico(cliente.Prefijo);
                cliente.CodigoUnico = codigoUnico;
                ListaClientes.Add(cliente);
            }
        }

        public void IngresarTecnico(Tecnico tecnico)
        {
            if (tecnico != null)
            {
                string codigoUnico = CodigoUnico.GenerarCodigoUnico(tecnico.Prefijo);
                tecnico.CodigoUnico = codigoUnico;
                ListaTecnicos.Add(tecnico);
                ActualizarMatrizCargaSemanal();
            }
        }

        public void IngresarEquipo(EquipoBase equipo)
        {
            if (equipo != null)
            {
                string codigoInterno = CodigoUnico.GenerarCodigoUnico(equipo.Prefijo);
                equipo.CodigoInterno = codigoInterno;
                ListaEquipos.Add(equipo);
            }
        }

        public void IngresarRepuesto(Repuesto repuesto)
        {
            if (repuesto != null)
            {
                string codigo = CodigoUnico.GenerarCodigoUnico(repuesto.Prefijo);
                repuesto.Codigo = codigo;
                ListaRepuestos.Add(repuesto);
            }
        }

        public void IngresarSolicitud(Solicitud solicitud)
        {
            if (solicitud != null)
            {
                string codigoSolicitud = CodigoUnico.GenerarCodigoUnico(solicitud.Prefijo);
                solicitud.CodigoSolicitud = codigoSolicitud;
                ListaSolicitudesGenerales.Add(solicitud);

                if (solicitud.Prioridad == PrioridadSolicitud.Urgente)
                {
                    ColaUrgentes.Enqueue(solicitud);
                }
                else
                {
                    ColaNormales.Enqueue(solicitud);
                }

                solicitud.Cliente.CantidadSolicitudes++;
            }
        }

        public void ActualizarMatrizCargaSemanal()
        {
            int numeroTecnicos = ListaTecnicos.Count;
            int[,] nuevaMatriz = new int[numeroTecnicos, 7];

            for (int i = 0; i < MatrizCargaSemanal.GetLength(0); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    nuevaMatriz[i, j] = MatrizCargaSemanal[i, j];
                }
            }

            MatrizCargaSemanal = nuevaMatriz;
        }

        public void AsignarTecnico()
        {
            Solicitud solicitudAAtender = null;

            if (ColaUrgentes.Count > 0)
            {
                solicitudAAtender = ColaUrgentes.Dequeue();
            }
            else if (ColaNormales.Count > 0)
            {
                solicitudAAtender = ColaNormales.Dequeue();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nAviso: No hay solicitudes pendientes en las colas.");
                Console.ResetColor();
                return;
            }

            Tecnico tecnicoDisponible = null;
            int indiceTecnico = -1;

            for (int i = 0; i < ListaTecnicos.Count; i++)
            {
                Tecnico tecnico = ListaTecnicos[i];
                if (tecnico.Estado == EstadoTecnico.Disponible && (int)tecnico.Especialidad == (int)solicitudAAtender.Equipo.TipoEquipo)
                {
                    tecnicoDisponible = tecnico;
                    indiceTecnico = i;
                    break;
                }
            }

            if (tecnicoDisponible != null)
            {
                solicitudAAtender.TecnicoAsignado = tecnicoDisponible;
                solicitudAAtender.Estado = EstadoSolicitud.Asignada;
                solicitudAAtender.HistorialEstados.Push(solicitudAAtender.Estado);

                tecnicoDisponible.TrabajosActivos++;

                if (tecnicoDisponible.TrabajosActivos >= tecnicoDisponible.LimiteTrabajos)
                {
                    tecnicoDisponible.Estado = EstadoTecnico.Ocupado;
                }

                int diaActual = (int)DateTime.Now.DayOfWeek;
                int indiceDia = diaActual == 0 ? 6 : diaActual - 1;

                MatrizCargaSemanal[indiceTecnico, indiceDia]++;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n¡Solicitud {solicitudAAtender.CodigoSolicitud} asignada exitosamente al técnico {tecnicoDisponible.NombreCompleto}!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: No hay técnicos disponibles con la especialidad requerida para el equipo {solicitudAAtender.Equipo.TipoEquipo.ObtenerDescripcion()}.");
                Console.ResetColor();

                if (solicitudAAtender.Prioridad == PrioridadSolicitud.Urgente)
                {
                    ColaUrgentes.Enqueue(solicitudAAtender);
                }
                else
                {
                    ColaNormales.Enqueue(solicitudAAtender);
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("La solicitud ha sido reencolada. Intente de nuevo más tarde.");
                Console.ResetColor();
            }
        }

        public void DescontarInventario(Repuesto repuesto, int cantidad)
        {
            if (repuesto.Existencia >= cantidad)
            {
                repuesto.Existencia -= cantidad;
                repuesto.CantidadUtilizada += cantidad;

                if (repuesto.Existencia <= repuesto.ExistenciaMinima)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"\n[ ALERTA DE INVENTARIO ]: El repuesto {repuesto.Nombre} ha alcanzado su existencia mínima o menor ({repuesto.Existencia} restantes). Considere reabastecer.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError: Inventario insuficiente.");
                Console.ResetColor();
            }
        }

        public void CalcularPromedioTecnico(Tecnico tecnico, int nuevaCalificacion)
        {
            double promedioActual = tecnico.CalificacionPromedio;
            int servicios = tecnico.ServiciosFinalizados;

            double sumaAnterior = promedioActual * servicios;
            double nuevaSuma = sumaAnterior + nuevaCalificacion;

            tecnico.ServiciosFinalizados++;
            tecnico.CalificacionPromedio = nuevaSuma / tecnico.ServiciosFinalizados;

            ConteoCalificaciones[nuevaCalificacion - 1]++;
        }

        public List<EquipoBase> BusquedaSecuencialEquipos(string objetivo)
        {
            List<EquipoBase> resultados = new List<EquipoBase>();
            string objetivoMinuscula = objetivo.ToLower();

            foreach (EquipoBase equipo in ListaEquipos)
            {
                if (equipo.Marca.ToLower().Contains(objetivoMinuscula) || equipo.Modelo.ToLower().Contains(objetivoMinuscula))
                {
                    resultados.Add(equipo);
                }
            }
            return resultados;
        }

        public Solicitud BusquedaBinariaSolicitud(List<Solicitud> listaOrdenada, string codigoObjetivo)
        {
            int inicio = 0;
            int fin = listaOrdenada.Count - 1;

            while (inicio <= fin)
            {
                int medio = (inicio + fin) / 2;
                int comparacion = string.Compare(listaOrdenada[medio].CodigoSolicitud, codigoObjetivo, StringComparison.OrdinalIgnoreCase);

                if (comparacion == 0)
                {
                    return listaOrdenada[medio];
                }
                else if (comparacion < 0)
                {
                    inicio = medio + 1;
                }
                else
                {
                    fin = medio - 1;
                }
            }
            return null;
        }

        public void BubbleSortSolicitudesCostoAscendente()
        {
            int n = ListaSolicitudesGenerales.Count;
            Solicitud auxiliar;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (ListaSolicitudesGenerales[j].Total > ListaSolicitudesGenerales[j + 1].Total)
                    {
                        auxiliar = ListaSolicitudesGenerales[j];
                        ListaSolicitudesGenerales[j] = ListaSolicitudesGenerales[j + 1];
                        ListaSolicitudesGenerales[j + 1] = auxiliar;
                    }
                }
            }
        }

        public void SelectionSortTecnicosServiciosDescendente()
        {
            int n = ListaTecnicos.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int posicionMayor = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (ListaTecnicos[j].ServiciosFinalizados > ListaTecnicos[posicionMayor].ServiciosFinalizados)
                    {
                        posicionMayor = j;
                    }
                }

                Tecnico auxiliar = ListaTecnicos[i];
                ListaTecnicos[i] = ListaTecnicos[posicionMayor];
                ListaTecnicos[posicionMayor] = auxiliar;
            }
        }

        public void ShellSortRepuestosExistenciaAscendente()
        {
            int n = ListaRepuestos.Count;

            for (int salto = n / 2; salto > 0; salto = salto / 2)
            {
                for (int i = salto; i < n; i++)
                {
                    Repuesto temporal = ListaRepuestos[i];
                    int j = i;

                    while (j >= salto && ListaRepuestos[j - salto].Existencia > temporal.Existencia)
                    {
                        ListaRepuestos[j] = ListaRepuestos[j - salto];
                        j = j - salto;
                    }

                    ListaRepuestos[j] = temporal;
                }
            }
        }

        public void QuickSortSolicitudesFechaAscendente()
        {
            if (ListaSolicitudesGenerales.Count > 0)
            {
                EjecutarQuickSort(0, ListaSolicitudesGenerales.Count - 1);
            }
        }

        private void EjecutarQuickSort(int izquierda, int derecha)
        {
            int i = izquierda;
            int j = derecha;
            DateTime pivote = ListaSolicitudesGenerales[(izquierda + derecha) / 2].FechaRecepcion;

            while (i <= j)
            {
                while (ListaSolicitudesGenerales[i].FechaRecepcion < pivote) { i++; }
                while (ListaSolicitudesGenerales[j].FechaRecepcion > pivote) { j--; }

                if (i <= j)
                {
                    Solicitud auxiliar = ListaSolicitudesGenerales[i];
                    ListaSolicitudesGenerales[i] = ListaSolicitudesGenerales[j];
                    ListaSolicitudesGenerales[j] = auxiliar;
                    i++;
                    j--;
                }
            }

            if (izquierda < j) { EjecutarQuickSort(izquierda, j); }
            if (i < derecha) { EjecutarQuickSort(i, derecha); }
        }

        public double IngresosTotales
        {
            get { return ingresosTotales; }
            set { ingresosTotales = value; }
        }

        public int[,] MatrizCargaSemanal
        {
            get { return matrizCargaSemanal; }
            set { matrizCargaSemanal = value; }
        }

        public int[] ConteoCalificaciones
        {
            get { return conteoCalificaciones; }
            set { conteoCalificaciones = value; }
        }

        public Queue<Solicitud> ColaUrgentes
        {
            get { return colaUrgentes; }
            set { colaUrgentes = value; }
        }

        public Queue<Solicitud> ColaNormales
        {
            get { return colaNormales; }
            set { colaNormales = value; }
        }

        public List<Solicitud> ListaSolicitudesGenerales
        {
            get { return listaSolicitudesGenerales; }
            set { listaSolicitudesGenerales = value; }
        }

        public List<Repuesto> ListaRepuestos
        {
            get { return listaRepuestos; }
            set { listaRepuestos = value; }
        }

        public List<EquipoBase> ListaEquipos
        {
            get { return listaEquipos; }
            set { listaEquipos = value; }
        }

        public List<Tecnico> ListaTecnicos
        {
            get { return listaTecnicos; }
            set { listaTecnicos = value; }
        }

        public List<Cliente> ListaClientes
        {
            get { return listaClientes; }
            set { listaClientes = value; }
        }
    }
}