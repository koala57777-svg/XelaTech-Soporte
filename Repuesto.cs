using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XelaTechSoporte.Enums;

namespace XelaTechSoporte
{
    internal class Repuesto
    {
        private string codigo;

        private string prefijo = "REP";

        private string nombre;

        private CategoriaRepuesto categoria;

        private int existencia;

        private int existenciaMinima;

        private double costoUnitario;

        private double precioVenta;

        private int cantidadUtilizada;

        public Repuesto(string nuevoNombre, CategoriaRepuesto nuevaCategoria, int nuevaExistencia, int nuevaExistenciaMinima, double nuevoCostoUnitario, double nuevoPrecioVenta)
        {
            Nombre = nuevoNombre;
            Categoria = nuevaCategoria;
            Existencia = nuevaExistencia;
            ExistenciaMinima = nuevaExistenciaMinima;
            CostoUnitario = nuevoCostoUnitario;
            PrecioVenta = nuevoPrecioVenta;
            CantidadUtilizada = 0;
        }

        internal void MostrarInformacion()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ID: ");
            Console.ResetColor();
            Console.Write(Codigo);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Prefijo: ");
            Console.ResetColor();
            Console.Write(Prefijo);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Nombre: ");
            Console.ResetColor();
            Console.Write(Nombre);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Categoría: ");
            Console.ResetColor();
            Console.Write(Categoria.ObtenerDescripcion());
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Existencia Actual: ");
            Console.ResetColor();
            Console.Write(Existencia);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Existencia Mínima: ");
            Console.ResetColor();
            Console.Write(ExistenciaMinima);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Costo Unitario: ");
            Console.ResetColor();
            Console.Write($"Q{CostoUnitario:F2}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Precio de Venta: ");
            Console.ResetColor();
            Console.Write($"Q{PrecioVenta:F2}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Cantidad Utilizada (Histórico): ");
            Console.ResetColor();
            Console.Write(CantidadUtilizada);
        }

        public int CantidadUtilizada
        {
            get { return cantidadUtilizada; }
            set { cantidadUtilizada = value; }
        }

        public double PrecioVenta
        {
            get { return precioVenta; }
            set { precioVenta = value; }
        }

        public double CostoUnitario
        {
            get { return costoUnitario; }
            set { costoUnitario = value; }
        }

        public int ExistenciaMinima
        {
            get { return existenciaMinima; }
            set { existenciaMinima = value; }
        }

        public int Existencia
        {
            get { return existencia; }
            set { existencia = value; }
        }

        public CategoriaRepuesto Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Prefijo
        {
            get { return prefijo; }
            set { prefijo = value; }
        }

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
    }
}
