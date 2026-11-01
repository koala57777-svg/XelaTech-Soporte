using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XelaTechSoporte
{
    public static class CodigoUnico
    {
        private static HashSet<string> baseDeCodigosUnicos = new HashSet<string>();

        private static Dictionary<string, int> ultimosNumeros = new Dictionary<string, int>();

        private static readonly object bloqueo = new object();

        public static string GenerarCodigoUnico(string prefijo)
        {
            lock (bloqueo)
            {
                if (!ultimosNumeros.TryGetValue(prefijo, out int numeroActual))
                {
                    numeroActual = 0;
                }

                string codigoCandidato;

                do
                {
                    numeroActual++;
                    codigoCandidato = $"{prefijo}-{numeroActual:D3}";
                }
                while (ExisteEnBaseDeDatos(codigoCandidato));

                ultimosNumeros[prefijo] = numeroActual;
                baseDeCodigosUnicos.Add(codigoCandidato);

                return codigoCandidato;
            }
        }

        private static bool ExisteEnBaseDeDatos(string codigo)
        {
            return baseDeCodigosUnicos.Contains(codigo);
        }
    }
}
