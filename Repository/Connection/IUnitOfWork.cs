using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books{ get; }
        IRepository<Purchase> Purchases { get; }
        void Save();
    }
}
