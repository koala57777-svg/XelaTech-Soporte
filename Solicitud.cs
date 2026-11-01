using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XelaTechSoporte.Enums;

namespace XelaTechSoporte
{
    internal class Solicitud
    {
        private string codigoSolicitud;

        private string prefijo = "SOL";

        private string descripcionProblema;

        private DateTime fechaRecepcion;

        private PrioridadSolicitud prioridad;

        private EstadoSolicitud estado;

        private string diagnostico;

        private double costoManoObra;

        private double costoRepuestos;

        private double total;

        private Cliente cliente;

        private EquipoBase equipo;

        private Tecnico tecnicoAsignado;

        private List<Repuesto> repuestosUtilizados;

        private Stack<EstadoSolicitud> historialEstados;

        public Solicitud(Cliente nuevoCliente, EquipoBase nuevoEquipo, string nuevaDescripcionProblema, PrioridadSolicitud nuevaPrioridad)
        {
            Cliente = nuevoCliente;
            Equipo = nuevoEquipo;
            DescripcionProblema = nuevaDescripcionProblema;
            Prioridad = nuevaPrioridad;

            FechaRecepcion = DateTime.Now;
            Estado = EstadoSolicitud.EnEspera;
            Diagnostico = "Pendiente de revisión";
            CostoManoObra = 0.00;
            CostoRepuestos = 0.00;
            Total = 0.00;

            RepuestosUtilizados = new List<Repuesto>();
            HistorialEstados = new Stack<EstadoSolicitud>();

            HistorialEstados.Push(Estado);
        }

        internal void MostrarInformacion()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ID Solicitud: ");
            Console.ResetColor();
            Console.Write(CodigoSolicitud);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Prefijo: ");
            Console.ResetColor();
            Console.Write(Prefijo);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Fecha de Recepción: ");
            Console.ResetColor();
            Console.Write(FechaRecepcion.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Cliente: ");
            Console.ResetColor();
            Console.Write(Cliente.NombreCompleto);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Equipo: ");
            Console.ResetColor();
            Console.Write($"{Equipo.TipoEquipo.ObtenerDescripcion()} - {Equipo.Marca} {Equipo.Modelo} (SN: {Equipo.NumeroSerie})");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Descripción del Problema: ");
            Console.ResetColor();
            Console.Write(DescripcionProblema);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Prioridad: ");
            Console.ResetColor();
            Console.Write(Prioridad);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Estado Actual: ");
            Console.ResetColor();
            Console.Write(Estado.ObtenerDescripcion());
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Técnico Asignado: ");
            Console.ResetColor();
            if (TecnicoAsignado != null)
            {
                Console.Write($"{TecnicoAsignado.NombreCompleto} ({TecnicoAsignado.CodigoUnico})");
            }
            else
            {
                Console.Write("Ninguno (En Cola)");
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Diagnóstico: ");
            Console.ResetColor();
            Console.Write(Diagnostico);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Repuestos Utilizados: ");
            Console.ResetColor();
            Console.Write(RepuestosUtilizados.Count);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Costo Mano de Obra: ");
            Console.ResetColor();
            Console.Write($"Q{CostoManoObra:F2}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Costo Repuestos: ");
            Console.ResetColor();
            Console.Write($"Q{CostoRepuestos:F2}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Total a Pagar (Incluye Revisión): ");
            Console.ResetColor();
            Console.Write($"Q{Total:F2}");
        }

        internal void CalcularTotal()
        {
            Total = CostoManoObra + CostoRepuestos + Equipo.CalcularCostoRevision();
        }

        public Stack<EstadoSolicitud> HistorialEstados
        {
            get { return historialEstados; }
            set { historialEstados = value; }
        }

        public List<Repuesto> RepuestosUtilizados
        {
            get { return repuestosUtilizados; }
            set { repuestosUtilizados = value; }
        }

        public Tecnico TecnicoAsignado
        {
            get { return tecnicoAsignado; }
            set { tecnicoAsignado = value; }
        }

        public EquipoBase Equipo
        {
            get { return equipo; }
            set { equipo = value; }
        }

        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        public double CostoRepuestos
        {
            get { return costoRepuestos; }
            set { costoRepuestos = value; }
        }

        public double CostoManoObra
        {
            get { return costoManoObra; }
            set { costoManoObra = value; }
        }

        public string Diagnostico
        {
            get { return diagnostico; }
            set { diagnostico = value; }
        }

        public EstadoSolicitud Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public PrioridadSolicitud Prioridad
        {
            get { return prioridad; }
            set { prioridad = value; }
        }

        public DateTime FechaRecepcion
        {
            get { return fechaRecepcion; }
            set { fechaRecepcion = value; }
        }

        public string DescripcionProblema
        {
            get { return descripcionProblema; }
            set { descripcionProblema = value; }
        }

        public string Prefijo
        {
            get { return prefijo; }
            set { prefijo = value; }
        }

        public string CodigoSolicitud
        {
            get { return codigoSolicitud; }
            set { codigoSolicitud = value; }
        }
    }
}
