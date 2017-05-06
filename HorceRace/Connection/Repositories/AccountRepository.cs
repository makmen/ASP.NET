using Connection.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        private BaseConnect link;

        public AccountRepository(BaseConnect context)
        {
            link = context;
        }

        public IEnumerable<Account> GetAll()
        {
            return link.Accounts;
        }

        public Account Get(int id)
        {
            return link.Accounts.Find(id);
        }

        public void Create(Account entity)
        {
            link.Accounts.Add(entity);
        }

        public void Update(Account entity)
        {
            link.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<Account> Find(Func<Account, Boolean> predicate)
        {
            return link.Accounts.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Account entity = link.Accounts.Find(id);
            if (entity != null)
                link.Accounts.Remove(entity);
        }


        public bool Authorization(Account account)
        {
            bool done = true;
            Account findAccount = (
                from acbd in link.Accounts
                where acbd.Login == account.Login &&
                acbd.Password == account.Password
                select acbd).FirstOrDefault();
            if (findAccount != null)
            {
                account.Id = findAccount.Id;
                account.Name = findAccount.Name;
                account.MiddleName = findAccount.MiddleName;
                account.LastName = findAccount.LastName;
                account.group = findAccount.group;
            }
            else
            {
                done = false;
            }

            return done;
        }

    }
}
