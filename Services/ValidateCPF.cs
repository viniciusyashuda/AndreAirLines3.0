using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ValidateCPF
    {

        public static bool CpfValidator(string cpf)
        {
            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string temporaryCpf, digit;
            int sum, rest;

            if (cpf.Length != 11)
            {

                return false;

            }

            temporaryCpf = cpf.Substring(0, 9);
            sum = 0;


            for (int i = 0; i < 9; i++)
            {

                sum += int.Parse(temporaryCpf[i].ToString()) * multiplier1[i];

            }

            rest = sum % 11;
            if (rest < 2)
            {

                rest = 0;

            }
            else
            {

                rest = 11 - rest;

            }

            digit = rest.ToString();
            temporaryCpf = temporaryCpf + digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
            {

                sum += int.Parse(temporaryCpf[i].ToString()) * multiplier2[i];

            }
            rest = sum % 11;
            if (rest < 2)
            {

                rest = 0;

            }
            else
            {

                rest = 11 - rest;

            }
            digit = digit + rest.ToString();
            return cpf.EndsWith(digit);
        }

    }
}
