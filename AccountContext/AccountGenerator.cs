using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.AccountContext
{
    public static class AccountGenerator
    {
        static IList<string> accountNumbersGenerated = new List<string>();

        public static string GenerateAccountNumber()
        {
            string accountNumber = GenerateNumber();
            int digitoVerificador = CalculateDigitalChecker(accountNumber);

            string completeNumber = $"{accountNumber}-{digitoVerificador}";
            accountNumbersGenerated.Add(completeNumber);

            return completeNumber;
        }
        
        private static string GenerateNumber()
        {
            Random random = new Random();
            string accountNumber;

            do
            {
                accountNumber = "";
                // Gerar um número de conta de 8 dígitos
                for (int i = 0; i < 8; i++)
                {
                    accountNumber += random.Next(0, 10);
                }
            } while (accountNumbersGenerated.Contains($"{accountNumber}-{CalculateDigitalChecker(accountNumber)}")); // Verificar se o número já foi gerado anteriormente

            return accountNumber;
        }

        private static int CalculateDigitalChecker(string accountNumber)
        {
            int sum = 0;
            int weight = 2;

            // Percorrer o número da conta da direita para a esquerda
            for (int i = accountNumber.Length - 1; i >= 0; i--)
            {
                sum += (accountNumber[i] - '0') * weight;
                weight++;

                // O peso reinicia em 2 após alcançar 9
                if (weight > 9)
                {
                    weight = 2;
                }
            }

            int rest = sum % 11;
            int checkDigit = 11 - rest;

            // Se o resultado for 10 ou 11, o dígito verificador é 0
            if (checkDigit == 10 || checkDigit == 11)
            {
                checkDigit = 0;
            }

            return checkDigit;
        }

        private static bool ValidateCheckDigit(string completeAccountNumber)
        {
            if (string.IsNullOrWhiteSpace(completeAccountNumber) || !completeAccountNumber.Contains("-"))
            {
                return false;
            }

            string[] pieces = completeAccountNumber.Split('-');
            if (pieces.Length != 2)
            {
                return false;
            }

            string accountNumber = pieces[0];
            int originalCheckDigit;
            if (!int.TryParse(pieces[1], out originalCheckDigit))
            {
                return false;
            }

            int calculatedCheckDigit = CalculateDigitalChecker(accountNumber);

            return originalCheckDigit == calculatedCheckDigit;
        }

        private static bool ExistsInSystem(string completeAccountNumber)
        {
            return accountNumbersGenerated.Contains(completeAccountNumber);
        }

        public static bool ValidateAccountNumber(string completeAccountNumber)
        {
            return ValidateCheckDigit(completeAccountNumber) && ExistsInSystem(completeAccountNumber);
        }

    }
}

