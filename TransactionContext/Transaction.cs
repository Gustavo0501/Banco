using Banco.AccountContext;
using Banco.SharedContext;
using Banco.SystemContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.TransactionContext
{
    public abstract class Transaction : Base
    {
        protected Transaction(decimal value)
        {
            Value = value;
            TransactionDate = DateTime.Now;
        }

        public decimal Value { get; set; }
        public DateTime TransactionDate { get; set; }

        public abstract void Execute(Account account);

        public void AddToTransactionRecord(Transaction transaction)
        {
            TransactionRecord.AddTransaction(transaction);
        }
        
    }
}
