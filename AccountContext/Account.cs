using Banco.SharedContext;
using Banco.SystemContext;
using Banco.TransactionContext;
using Banco.TransactionContext.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Banco.AccountContext
{
    public class Account : Base
    {
        public Account(User user)
        {
            User = user;
            Balance = 0;
            Statement = new Statement(this);
        }

        public User User { get; set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
        public Statement Statement { get; set; }

        public void WithDraw(decimal amount)
        {
            var withdraw = new Withdraw(amount);
            withdraw.Execute(this);
        }

        public void Deposit(decimal amount)
        {
            Console.WriteLine($"Saldo anterior: R$ {Balance}");

            var deposit = new Deposit(amount);
            deposit.Execute(this);

            Console.WriteLine($"Saldo atual: R$ {Balance}");
        }

        public void Pix(string chavePix)
        {
            var account = RegisterSystem.SearchAccountByAccountNumber(chavePix);

            if (account == null)
            {
                Console.WriteLine("O destinatário não foi encontrado!");
                Console.Write("Digite uma chave válida!\n");
            }
            else
            {
                try
                {
                    Console.WriteLine($"Destinatário: {account.User.Name}");
                    Console.WriteLine("Digite o valor que deseja enviar: ");
                    decimal value = decimal.Parse(Console.ReadLine());

                    var pixTransaction = new PixTransaction(account, value);
                    pixTransaction.Execute(this);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Você precisa digitar um número!");
                    Console.WriteLine("Utilize . para separar a casa decimal");
                }
            }
        }

        public void AddToStatement(Transaction transaction, ETransactionType transactionType, Account account)
        {  
            switch ((int)transactionType)
            {
                case 1: //Withdraw
                    Statement.AddTransactionReceipt(new Receipt(transaction.TransactionDate, $"- {transaction.Value}", "Saque"));
                    break;
                case 2: //Deposit
                    Statement.AddTransactionReceipt(new Receipt(transaction.TransactionDate, $"+ {transaction.Value}", "Depósito"));
                    break;
                case 3: //PixSent
                    Statement.AddTransactionReceipt(new Receipt(transaction.TransactionDate, $"- {transaction.Value}", $"Pix enviado para {account.User.Name}"));
                    break;
                case 4: //PixReceived
                    Statement.AddTransactionReceipt(new Receipt(transaction.TransactionDate, $"+ {transaction.Value}", $"Pix recebido de {account.User.Name}"));
                    break;
                default: 
                    Console.WriteLine("Esse tipo de transação não existe!");
                    break;
            }

        }

        public void ShowStatement()
        {
            Statement.ShowAll();
        }
    }
}
