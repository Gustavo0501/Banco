using Banco.AccountContext;
using Banco.TransactionContext.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.TransactionContext
{
    public class PixTransaction : Transaction
    {
        public PixTransaction(Account destinyAccount, decimal value)
            : base (value)
        {
            DestinyAccount = destinyAccount;
        }

        public Account DestinyAccount { get; set; }

        public override void Execute(Account account)
        {
            if (account.Balance < Value)
            {
                Console.WriteLine($"Seu saldo é de R$ {account.Balance}, e é insuficiente para realizar um pix desse valor.");
            }
            else
            {
                account.Balance -= Value;
                DestinyAccount.Balance += Value;

                account.AddToStatement(this, ETransactionType.PixSent, DestinyAccount);
                DestinyAccount.AddToStatement(this, ETransactionType.PixReceived, account);

                Console.WriteLine("Pix concluído");

                AddToTransactionRecord(this);
            }
        }
    }
}
