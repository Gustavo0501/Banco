using Banco.TransactionContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.AccountContext
{
    public class Statement
    {
        public Statement( Account account)
        {
            TransactionReceipts = new List<Receipt>();
            Account = account;
        }

        public IList<Receipt> TransactionReceipts { get; set; }
        public Account Account { get; set; }

        public void AddTransactionReceipt(Receipt receipt)
        {
            TransactionReceipts.Add(receipt);
        }

        public void ShowAll()
        {
            Console.WriteLine("Exindo todos os extratos: ");
            
            foreach (var receipt in TransactionReceipts)
            {
                FormatStatement(receipt);
            }
        }

        private void FormatStatement(Receipt receipt)
        {
            Console.WriteLine("\n=================================\n");
            Console.WriteLine($"Data: {receipt.ReceiptDate}");
            Console.WriteLine($"Descrição: {receipt.Description}");
            Console.WriteLine($"Valor: {receipt.Value}");
            Console.WriteLine("\n=================================\n");
        }
    }
}
