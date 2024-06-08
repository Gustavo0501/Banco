using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banco.TransactionContext;

namespace Banco.SystemContext
{
    public static class TransactionRecord
    {
        private static IList<Transaction> Transactions = new List<Transaction>();

        public static void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }
    }
}
