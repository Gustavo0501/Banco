using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.SharedContext
{
    public abstract class Base
    {
        protected Base()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

    }
}
