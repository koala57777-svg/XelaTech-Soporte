using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XelaTechSoporte.Enums;

namespace XelaTechSoporte
{
    internal class EquipoBase
    {
        private string codigoInterno;

        private string prefijo = "EQU";

        private string numeroSerie;

        private string marca;

        private string modelo;

        private Cliente propietario;

        private TipoEquipo tipoEquipo;

        private string estadoFisico;

        private string accesorios;

        public EquipoBase(string nuevoNumeroSerie, string nuevaMarca, string nuevoModelo, Cliente nuevoPropietario, TipoEquipo nuevoTipoEquipo, string nuevoEstadoFisico, string nuevosAccesorios)
        {
            NumeroSerie = nuevoNumeroSerie;
            Marca = nuevaMarca;
            Modelo = nuevoModelo;
            Propietario = nuevoPropietario;
            TipoEquipo = nuevoTipoEquipo;
            EstadoFisico = nuevoEstadoFisico;
            Accesorios = nuevosAccesorios;
        }

        internal virtual void MostrarInformacion()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ID Interno: ");
            Console.ResetColor();
            Console.Write(CodigoInterno);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Prefijo: ");
            Console.ResetColor();
            Console.Write(Prefijo);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Número de Serie: ");
            Console.ResetColor();
            Console.Write(NumeroSerie);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Marca: ");
            Console.ResetColor();
            Console.Write(Marca);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Modelo: ");
            Console.ResetColor();
            Console.Write(Modelo);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Propietario: ");
            Console.ResetColor();
            Console.Write(Propietario.NombreCompleto);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Tipo de Equipo: ");
            Console.ResetColor();
            Console.Write(TipoEquipo.ObtenerDescripcion());
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Estado Físico: ");
            Console.ResetColor();
            Console.Write(EstadoFisico);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Accesorios: ");
            Console.ResetColor();
            Console.Write(Accesorios);
        }

        internal virtual double CalcularCostoRevision()
        {
            return 50;
        }

        public string Accesorios
        {
            get { return accesorios; }
            set { accesorios = value; }
        }

        public string EstadoFisico
        {
            get { return estadoFisico; }
            set { estadoFisico = value; }
        }

        public TipoEquipo TipoEquipo
        {
            get { return tipoEquipo; }
            set { tipoEquipo = value; }
        }

        public Cliente Propietario
        {
            get { return propietario; }
            set { propietario = value; }
        }

        public string Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }

        public string Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        public string NumeroSerie
        {
            get { return numeroSerie; }
            set { numeroSerie = value; }
        }

        public string Prefijo
        {
            get { return prefijo; }
            set { prefijo = value; }
        }

        public string CodigoInterno
        {
            get { return codigoInterno; }
            set { codigoInterno = value; }
        }
    }

    internal class Portatil : EquipoBase
    {
        private bool cargador;

        public Portatil(string nuevoNumeroSerie, string nuevaMarca, string nuevoModelo, Cliente nuevoPropietario, string nuevoEstadoFisico, string nuevosAccesorios, bool poseeCargador) : base(nuevoNumeroSerie, nuevaMarca, nuevoModelo, nuevoPropietario, TipoEquipo.ComputadoraPortatil, nuevoEstadoFisico, nuevosAccesorios)
        {
            Prefijo = "POR";
            Cargador = poseeCargador;
        }

        internal override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Incluye Cargador: ");
            Console.ResetColor();
            Console.Write(Cargador ? "Sí" : "No");
        }

        internal override double CalcularCostoRevision()
        {
            return base.CalcularCostoRevision() + 25.00;
        }

        public bool Cargador
        {
            get { return cargador; }
            set { cargador = value; }
        }
    }

    internal class Escritorio : EquipoBase
    {
        private bool monitor;

        public Escritorio(string nuevoNumeroSerie, string nuevaMarca, string nuevoModelo, Cliente nuevoPropietario, string nuevoEstadoFisico, string nuevosAccesorios, bool poseeMonitor) : base(nuevoNumeroSerie, nuevaMarca, nuevoModelo, nuevoPropietario, TipoEquipo.ComputadoraEscritorio, nuevoEstadoFisico, nuevosAccesorios)
        {
            Prefijo = "ESC";
            Monitor = poseeMonitor;
        }

        internal override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Incluye Monitor: ");
            Console.ResetColor();
            Console.Write(Monitor ? "Sí" : "No");
        }

        internal override double CalcularCostoRevision()
        {
            return base.CalcularCostoRevision();
        }

        public bool Monitor
        {
            get { return monitor; }
            set { monitor = value; }
        }
    }

    internal class Impresora : EquipoBase
    {
        private int nivelTintaEstimado;

        public Impresora(string nuevoNumeroSerie, string nuevaMarca, string nuevoModelo, Cliente nuevoPropietario, string nuevoEstadoFisico, string nuevosAccesorios, int nuevoNivelTintaEstimado) : base(nuevoNumeroSerie, nuevaMarca, nuevoModelo, nuevoPropietario, TipoEquipo.Impresora, nuevoEstadoFisico, nuevosAccesorios)
        {
            Prefijo = "IMP";
            NivelTintaEstimado = nuevoNivelTintaEstimado;
        }

        internal override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Nivel de Tinta Estimado: ");
            Console.ResetColor();
            Console.Write($"{NivelTintaEstimado}%");
        }

        internal override double CalcularCostoRevision()
        {
            return base.CalcularCostoRevision() + 35.00;
        }

        public int NivelTintaEstimado
        {
            get { return nivelTintaEstimado; }
            set { nivelTintaEstimado = value; }
        }
    }
}
