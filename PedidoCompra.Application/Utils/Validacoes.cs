using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidos.Application.Utils
{
    public static class Validacoes
    {
        public static bool EhNullOuVazio(object? valor)
        {
            if (valor == null)
                return true;

            if (valor is string str)
                return string.IsNullOrWhiteSpace(str);

            if (valor is ICollection collection)
                return collection.Count == 0;

            return false;
        }

        public static void ValidarEhNullOuVazio(object? valor, string campo)
        {
            if (EhNullOuVazio(valor))
                throw new ArgumentException($"O campo obrigatório '{campo}' não foi informado.");
        }
    }
}
