using Connection.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Repositories
{
    public class RaceRepository : IRepository<Race>
    {
        private BaseConnect link;

        public RaceRepository(BaseConnect context)
        {
            link = context;
        }

        public IEnumerable<Race> GetAll()
        {
            return link.Races;
        }

        public Race Get(int id)
        {
            return link.Races.Find(id);
        }

        public void Create(Race entity)
        {
            link.Races.Add(entity);
        }

        public void Update(Race entity)
        {
            link.Entry(entity).State = EntityState.Modified;
        }
        public IEnumerable<Race> Find(Func<Race, Boolean> predicate)
        {
            return link.Races.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Race entity = link.Races.Find(id);
            if (entity != null)
                link.Races.Remove(entity);
        }

        public List<Race> GetRacesLimit(int limit, int page)
        {
            int offset = (page - 1) * limit;
            List<Race> items = link.Races.OrderByDescending(races => races.Created).Skip(offset).Take(limit).ToList();

            return items;
        }

        public int GetTotalNews()
        {
            int total;
            try
            {
                total = link.Races.Count();
            }
            catch
            {
                total = -1;
            }

            return total;
        }


    }
}
