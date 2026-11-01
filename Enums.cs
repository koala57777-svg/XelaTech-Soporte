using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XelaTechSoporte
{
    internal class Enums
    {
        public enum TipoCliente
        {
            Estudiante = 1,
            Profesional = 2,
            [Description("Pequeño Negocio")]
            PequenoNegocio = 3,
            [Description("Institución")]
            Institucion = 4
        }

        public enum EstadoTecnico
        {
            Disponible = 1,
            Ocupado = 2,
            [Description("Fuera de Servicio")]
            FueraDeServicio = 3
        }

        public enum EspecialidadTecnico
        {
            Computadoras = 1,
            Impresoras = 2,
            [Description("Soporte General")]
            SoporteGeneral = 3
        }

        public enum TipoEquipo
        {
            [Description("Computadora Portátil")]
            ComputadoraPortatil = 1,
            [Description("Computadora de Escritorio")]
            ComputadoraEscritorio = 2,
            Impresora = 3
        }

        public enum PrioridadSolicitud
        {
            Urgente = 1,
            Normal = 2
        }

        public enum EstadoSolicitud
        {
            Recibida = 1,
            [Description("En espera")]
            EnEspera = 2,
            Asignada = 3,
            [Description("En diagnóstico")]
            EnDiagnostico = 4,
            [Description("En reparación")]
            EnReparacion = 5,
            [Description("Lista para entregar")]
            ListaParaEntregar = 6,
            Entregada = 7,
            Cancelada = 8
        }

        public enum CategoriaRepuesto
        {
            Almacenamiento = 1,
            Pantallas = 2,
            [Description("Periféricos y Accesorios")]
            PerifericosYAccesorios = 3,
            [Description("Tintas y Tóners")]
            TintasYToners = 4,
            [Description("Componentes Internos")]
            ComponentesInternos = 5
        }
    }
}
