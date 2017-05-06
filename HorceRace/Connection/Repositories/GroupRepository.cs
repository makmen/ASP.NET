using Connection.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Repositories
{
    public class GroupRepository : IRepository<Group>
    {
        private BaseConnect link;

        public GroupRepository(BaseConnect context)
        {
            link = context;
        }

        public IEnumerable<Group> GetAll()
        {
            return link.Groups;
        }

        public Group Get(int id)
        {
            return link.Groups.Find(id);
        }

        public void Create(Group entity)
        {
            link.Groups.Add(entity);
        }

        public void Update(Group entity)
        {
            link.Entry(entity).State = EntityState.Modified;
        }
        public IEnumerable<Group> Find(Func<Group, Boolean> predicate)
        {
            return link.Groups.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Group entity = link.Groups.Find(id);
            if (entity != null)
                link.Groups.Remove(entity);
        }
    }
}
