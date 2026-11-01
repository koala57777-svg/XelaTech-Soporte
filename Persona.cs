using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XelaTechSoporte.Enums;

namespace XelaTechSoporte
{
    internal class Persona
    {
        private string codigoUnico;

        private string prefijo = "PER";

        private string nombreCompleto;

        private int numeroTelefono;

        public Persona(string nuevoNombreCompleto, int nuevoNumeroTelefono)
        {
            NombreCompleto = nuevoNombreCompleto;
            NumeroTelefono = nuevoNumeroTelefono;
        }

        internal virtual void MostrarInformacion()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ID: ");
            Console.ResetColor();
            Console.Write(CodigoUnico);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Prefijo: ");
            Console.ResetColor();
            Console.Write(Prefijo);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Nombre Completo: ");
            Console.ResetColor();
            Console.Write(NombreCompleto);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Número de Teléfono: ");
            Console.ResetColor();
            Console.Write(NumeroTelefono);
        }

        public int NumeroTelefono
        {
            get { return numeroTelefono; }
            set { numeroTelefono = value; }
        }

        public string NombreCompleto
        {
            get { return nombreCompleto; }
            set { nombreCompleto = value; }
        }

        public string Prefijo
        {
            get { return prefijo; }
            set { prefijo = value; }
        }

        public string CodigoUnico
        {
            get { return codigoUnico; }
            set { codigoUnico = value; }
        }
    }

    internal class Cliente : Persona
    {
        private string correoElectronico;

        private TipoCliente tipoCliente;

        private int cantidadSolicitudes;

        public Cliente(string nuevoNombreCompleto, int nuevoNumeroTelefono, string nuevoCorreoElectronico, TipoCliente nuevoTipoCliente) : base(nuevoNombreCompleto, nuevoNumeroTelefono)
        {
            Prefijo = "CLI";
            CorreoElectronico = nuevoCorreoElectronico;
            TipoCliente = nuevoTipoCliente;
            CantidadSolicitudes = 0;
        }

        internal override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Correo Electrónico: ");
            Console.ResetColor();
            Console.Write(CorreoElectronico);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Tipo de Cliente: ");
            Console.ResetColor();
            Console.Write(TipoCliente.ObtenerDescripcion());
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Cantidad de Solicitudes: ");
            Console.ResetColor();
            Console.Write(CantidadSolicitudes);
        }

        public int CantidadSolicitudes
        {
            get { return cantidadSolicitudes; }
            set { cantidadSolicitudes = value; }
        }

        public TipoCliente TipoCliente
        {
            get { return tipoCliente; }
            set { tipoCliente = value; }
        }

        public string CorreoElectronico
        {
            get { return correoElectronico; }
            set { correoElectronico = value; }
        }
    }

    internal class Tecnico : Persona
    {
        private EspecialidadTecnico especialidad;

        private EstadoTecnico estado;

        private int trabajosActivos;

        private int serviciosFinalizados;

        private double calificacionPromedio;

        private int limiteTrabajos;

        public Tecnico(string nuevoNombreCompleto, int nuevoNumeroTelefono, EspecialidadTecnico nuevaEspecialidad, int nuevoLimiteTrabajos) : base(nuevoNombreCompleto, nuevoNumeroTelefono)
        {
            Prefijo = "TEC";
            Especialidad = nuevaEspecialidad;
            Estado = EstadoTecnico.Disponible;
            TrabajosActivos = 0;
            ServiciosFinalizados = 0;
            CalificacionPromedio = 0;
            LimiteTrabajos = nuevoLimiteTrabajos;
        }

        internal override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Especialidad: ");
            Console.ResetColor();
            Console.Write(Especialidad.ObtenerDescripcion());
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Estado: ");
            Console.ResetColor();
            Console.Write(Estado.ObtenerDescripcion());
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Trabajos Activos: ");
            Console.ResetColor();
            Console.Write(TrabajosActivos);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Límite de Trabajos: ");
            Console.ResetColor();
            Console.Write(LimiteTrabajos);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Servicios Finalizados: ");
            Console.ResetColor();
            Console.Write(ServiciosFinalizados);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Calificación Promedio: ");
            Console.ResetColor();
            Console.Write(CalificacionPromedio);
        }

        public int LimiteTrabajos
        {
            get { return limiteTrabajos; }
            set { limiteTrabajos = value; }
        }

        public double CalificacionPromedio
        {
            get { return calificacionPromedio; }
            set { calificacionPromedio = value; }
        }

        public int ServiciosFinalizados
        {
            get { return serviciosFinalizados; }
            set { serviciosFinalizados = value; }
        }

        public int TrabajosActivos
        {
            get { return trabajosActivos; }
            set { trabajosActivos = value; }
        }

        public EstadoTecnico Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public EspecialidadTecnico Especialidad
        {
            get { return especialidad; }
            set { especialidad = value; }
        }
    }
}
