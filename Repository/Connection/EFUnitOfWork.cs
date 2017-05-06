using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private BookContext db;
        private BookRepository bookRepository;
        private PurchaseRepository purchaseRepository;
        private bool disposed = false;

        public EFUnitOfWork()
        {
            db = new BookContext();
        }
        public IRepository<Book> Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
        }

        public IRepository<Purchase> Purchases
        {
            get
            {
                if (purchaseRepository == null)
                    purchaseRepository = new PurchaseRepository(db);
                return purchaseRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }


        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
