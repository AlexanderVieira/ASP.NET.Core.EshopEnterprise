﻿using ESE.Core.DomainObjects.Exceptions;
using ESE.Core.Properties;
using ESE.Core.Utils;

namespace ESE.Core.DomainObjects
{
    public class Cpf
    {
        public const int CPF_MAX_LENGTH = 11;
        public string Number { get; private set; }

        protected Cpf()
        {
        }

        public Cpf(string number)
        {
            if (!CpfValid(number)) throw new DomainException(Resources.MSG_ERRO_CPF_INVALIDO);
            Number = number;
        }

        public static bool CpfValid(string cpf)
        {
            cpf = cpf.OnlyNumbers(cpf);
            
            if (cpf.Length != 11) return false;

            //while (cpf.Length != 11)
            //    cpf = '0' + cpf;

            var igual = true;
            for (var i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            var numeros = new int[11];

            for (var i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            var resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }
    }
}