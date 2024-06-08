using Banco.AccountContext;
using Banco.TransactionContext.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.TransactionContext
{
    public class Withdraw : Transaction
    {
        public Withdraw(decimal value)
            : base(value) { }

        public override void Execute(Account account)
        {
            if (account.Balance < Value)
            {
                Console.WriteLine($"Seu saldo é de R$ {account.Balance}, e é insuficiente para realizar um saque desse valor.");
            }
            else
            {
                Console.WriteLine("Saque concluído");
                Console.WriteLine($"Saldo anterior: R$ {account.Balance}");
                account.Balance -= Value;
                account.AddToStatement(this, ETransactionType.Withdraw, null);
                Console.WriteLine($"Saldo atual: R$ {account.Balance}");

                AddToTransactionRecord(this);
            }
            
        }
    }
}
