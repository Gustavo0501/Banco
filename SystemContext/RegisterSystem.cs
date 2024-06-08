using Banco.AccountContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Banco.SystemContext
{
    public static class RegisterSystem
    {
        public static IList<Account> RegisteredAccounts = new List<Account>();
        public static IList<User> RegisteredUsers = new List<User>();

        public static void CreateAccount()
        {
            try
            {
                Console.WriteLine("Informe seu nome completo: ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                    throw new FormatException("O nome não pode estar em branco!");


                Console.WriteLine("Informe seu CPF: (somente os números)");
                string cpfInput = Console.ReadLine();

                if (!long.TryParse(cpfInput, out long cpfNumber))
                    throw new FormatException("O cpf deve conter apenas números!");
                
                string cpf = "";

                if (cpfNumber > 10000000000 && cpfNumber < 99999999999)
                {
                    cpf = formatCpf(cpfNumber);
                }
                else
                    throw new ArgumentOutOfRangeException("CPF inválido. Deve ter 11 dígitos.");


                Console.WriteLine("Informe sua data de nascimento: ");
                Console.Write("Ano: ");
                if(!int.TryParse(Console.ReadLine(), out int years) || years < 1900)
                    throw new FormatException("Ano inválido");

                Console.Write("Mês: ");
                if (!int.TryParse(Console.ReadLine(), out int months) || months < 1 || months > 12)
                    throw new FormatException("Mês inválido");

                Console.Write("Dia: ");
                if (!int.TryParse(Console.ReadLine(), out int days) || days < 1 || days > DateTime.DaysInMonth(years, months))
                    throw new FormatException("Dia inválido");

                DateTime birthDate = new DateTime(years, months, days);
            
                Console.WriteLine("Informe um email: ");
                string email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email) || !(email.Contains('@')))
                    throw new FormatException("Email inválido!");

                Console.WriteLine("Informe uma senha: ");
                string password = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(password))
                    throw new FormatException("A senha não pode estar em branco!");

            

                if(ElegibleUser(cpf, birthDate, email))
                {
                    var generatedSalt = Security.GenerateSalt();
                    var hashPassword = Security.GenerateHash(password, generatedSalt);

                    var user = new User(name, cpf, birthDate, email, generatedSalt, hashPassword, null);
                    var account = new Account(user);
                    user.Account = account;

                    account.AccountNumber = AccountGenerator.GenerateAccountNumber();

                    Console.WriteLine("Sua conta foi criada com sucesso!");
                    Console.WriteLine($"O número da sua conta é: {account.AccountNumber}\n");

                    RegisteredUsers.Add(user);
                    RegisteredAccounts.Add(account);
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Erro de formatação!");
                Console.WriteLine("Verifique se não deixou um campo em branco ou digitou uma informação em outro formato!");
            }

            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Erro de intervalo!");
                Console.WriteLine("Alguma informação digitada não corresponde ao número de caracteres esperado");
            }

            catch (Exception e)
            {
                Console.WriteLine("Algo deu errado!");
            }
        }

        public static User Login()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Informe o número de sua conta (incluir o dígito verificador): ");
                Console.WriteLine("(Digite apenas números)\n");
                string accountNumberInput = Console.ReadLine();
                if(string.IsNullOrEmpty(accountNumberInput) || !int.TryParse(accountNumberInput, out int accountNumber) || accountNumberInput.Length != 9)
                    throw new FormatException("A conta deve conter apenas números, e deve possuir 9 dígitos!");
                accountNumberInput = $"{accountNumberInput.Substring(0,8)}-{accountNumberInput.Substring(8,1)}";

                Console.WriteLine("Informe sua senha: ");
                string passwordString = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(passwordString))
                    throw new FormatException("A senha não pode estar em branco!");


                foreach (var user in RegisteredUsers)
                {
                    if(AccountGenerator.ValidateAccountNumber(accountNumberInput) && (accountNumberInput == user.Account.AccountNumber))
                    {
                        var password = Security.GenerateHash(passwordString, user.Salt);
                    
                        if(password.SequenceEqual(user.Password))
                        {
                            return user;
                        }
                    
                        Console.WriteLine("Senha incorreta!");
                        return null;
                    }
                }
            
                Console.WriteLine("Número de conta não encontrado!");

            }

            catch (FormatException)
            {
                Console.WriteLine("Erro de formatação!");
                Console.WriteLine("Verifique se não deixou um campo em branco ou digitou uma informação em outro formato!");
            }
            catch(Exception e)
            {
                Console.WriteLine("Algo deu errado!");
            }

            return null;
        }

        public static Account SearchAccountByAccountNumber(string accountNumber)
        {
            foreach(var account in RegisteredAccounts)
            {
                if(account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }

        private static bool ElegibleUser(string cpf, DateTime birthDate, string email)
        {
            if (cpf == "")
            {
                Console.WriteLine("Cpf inválido!");
                return false;
            }

            foreach (var registeredUser in RegisteredUsers)
            {
                if (registeredUser.Cpf == cpf)
                {
                    Console.WriteLine("Esse usuário já possui uma conta!");
                    return false;
                }
            }

            if (GetAge(birthDate) < 18)
            {
                Console.WriteLine("\nErro ao criar conta! Você precisa ter mais de 18 anos para criar uma conta em nosso banco!");
                return false;
            }

            if (!email.Contains('@'))
            {
                Console.WriteLine("Email inválido!");
                return false;
            }

            return true;
        }


        private static int GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }

        private static string formatCpf(long cpfNumber)
        {
            string cpfString = cpfNumber.ToString();
            string formatedCpf = $"{cpfString.Substring(0, 3)}.{cpfString.Substring(3, 3)}.{cpfString.Substring(6, 3)}-{cpfString.Substring(9, 2)}";
            
            return formatedCpf;

        }

    }
}

