using System.Text.RegularExpressions;
using System;
using System.Linq;

namespace FI.AtividadeEntrevista.BLL.Validators
{
    public static class CpfValidador
    {
        private const int _TAMANHO_CPF = 11;
        private const int _FATOR_PRIMEIRO_DIGITO = 10;
        private const int _FATOR_SEGUNDO_DIGITO = 11;
        private const string _MENSAGEM_CPF_INVALIDO = "CPF inválido";


        public static void ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                throw new Exception("CPF não pode ser em branco ou vazio");
            }
            cpf = RemoverNaoDigitos(cpf);
            if (cpf.Length != _TAMANHO_CPF)
            {
                throw new Exception(_MENSAGEM_CPF_INVALIDO);
            }
            if (TodosOsDigitosSaoIguais(cpf))
            {
                throw new Exception(_MENSAGEM_CPF_INVALIDO);
            }
            var digitosExtraidos = ExtrairDigitos(cpf);

            var digitoVerificador1 = CalcularDigitoVerificador(cpf, _FATOR_PRIMEIRO_DIGITO);

            cpf = cpf.Substring(0,9);
            cpf = $"{cpf}{digitoVerificador1}";
                
            var digitoVerificador2 = CalcularDigitoVerificador(cpf, _FATOR_SEGUNDO_DIGITO);
            var digitosCalculados = $"{digitoVerificador1}{digitoVerificador2}";


            if (digitosExtraidos != digitosCalculados)
            {
                throw new Exception(_MENSAGEM_CPF_INVALIDO);
            }

        }

        private static string RemoverNaoDigitos(string cpf)
        {
            return Regex.Replace(cpf, @"\D", string.Empty);
        }

        private static bool TodosOsDigitosSaoIguais(string cpf)
        {
            var primeiroDigito = cpf[0];
            return cpf.All(x => x == primeiroDigito);
        }

        private static string ExtrairDigitos(string cpf)
        {
            return cpf.Substring(9);
        }

        private static int CalcularDigitoVerificador(string cpf, int fator)
        {
            var total = 0;
            var cpfEmCaracteres = cpf.ToArray();
            foreach (var caracter in cpfEmCaracteres)
            {
                if (fator > 1)
                {
                    var digito = int.Parse(caracter.ToString());
                    total += digito * fator;
                    fator--;
                }
            }
            var restante = total % 11;
            return (restante < 2) ? 0 : 11 - restante;
        }

    }
}
