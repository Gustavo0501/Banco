using Banco.AccountContext;
using Banco.TransactionContext.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.TransactionContext
{
    public class Deposit : Transaction
    {
        public Deposit(decimal value)
            : base(value){ }

        public override void Execute(Account account)
        {
            account.Balance += Value;
            account.AddToStatement(this, ETransactionType.Deposit, null);
            
            Console.WriteLine($"Depósito de R$ {Value} realizado com sucesso.\n");

            AddToTransactionRecord(this);
        }
    }
}
