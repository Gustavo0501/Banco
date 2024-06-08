using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.TransactionContext
{
    public class Receipt
    {
        public Receipt(DateTime receiptDate, string value, string description)
        {
            ReceiptDate = receiptDate;
            Value = value;
            Description = description;
        }

        public DateTime ReceiptDate { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
