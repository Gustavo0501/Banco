using Banco.SharedContext;
using Banco.SystemContext;
using Banco.TransactionContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Banco.AccountContext
{
    public class User : Base
    {
        public User(string name, string cpf, DateTime birthDate, string email, byte[] salt, byte[] hashPassword, Account account)
        {
            
            Name = name;
            Cpf = cpf;
            BirthDate = birthDate;
            Email = email;
            Account = account;
            Salt = salt;
            Password = hashPassword;
        }

        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Password { get; set; }
        public Account Account { get; set; }

        public void ShowPessoalInfo()
        {
            Console.Clear();
            Console.WriteLine($"Nome: {Name}");
            Console.WriteLine($"CPF: {Cpf}");
            Console.WriteLine($"Data de Nascimento: {String.Format("{0:dd/MM/yyyy}", BirthDate)}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine("");
        }

        public decimal CheckBalance()
        {
            return Account.Balance;
        }

        public void Withdraw(decimal amount)
        {
            Account.WithDraw(amount);
        }

        public void Deposit(decimal amount)
        {
            Account.Deposit(amount);
        }

        public void Pix(string chavePix)
        {
            Account.Pix(chavePix);
        }

        public void ChangeEmail(string email)
        {
            Email = email;
            Console.WriteLine("Email alterado com sucesso!");
        }

        public void ShowAccountStatement()
        {
            Account.ShowStatement();
        }

        public void ChangePassword(string password)
        {
            try
            {
                var passwordHash = Security.GenerateHash(password, Salt);

                if (passwordHash.SequenceEqual(Password))
                {
                    Console.WriteLine("Informe sua nova senha: ");
                    string newPassword = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newPassword))
                        throw new FormatException("A senha não pode estar em branco!");

                    var newPasswordHashTest = Security.GenerateHash(newPassword, Salt);

                    if (!newPasswordHashTest.SequenceEqual(Password))
                    {
                        var newSalt = Security.GenerateSalt();
                        var newPasswordHash = Security.GenerateHash(newPassword, newSalt);

                        Password = newPasswordHash;
                        Console.WriteLine("Senha alterada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Você precisa informar uma senha diferente da atual!");
                    }

                }
                else
                {
                    Console.WriteLine("Senha incorreta!");
                }
            }
            catch(FormatException)
            {
                Console.WriteLine("Sua senha não pode está em branco!");
            }
            catch(Exception e)
            {
                Console.WriteLine("Algo deu errado!");
            }
        }
    }
}
