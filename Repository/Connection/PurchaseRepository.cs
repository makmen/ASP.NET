using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class PurchaseRepository : IRepository<Purchase>
    {
        private BookContext db;

        public PurchaseRepository(BookContext context)
        {
            this.db = context;
        }

        public IEnumerable<Purchase> GetAll()
        {
            return db.Purchases;
        }

        public Purchase Get(int id)
        {
            return db.Purchases.Find(id);
        }

        public void Create(Purchase order)
        {
            db.Purchases.Add(order);
        }

        public void Update(Purchase order)
        {
            //db.Entry(order).State = EntityState.Modified;
        }
        public IEnumerable<Purchase> Find(Func<Purchase, Boolean> predicate)
        {
            return db.Purchases.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Purchase order = db.Purchases.Find(id);
            if (order != null)
                db.Purchases.Remove(order);
        }

    }
}
