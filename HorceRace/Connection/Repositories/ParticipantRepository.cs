using Connection.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Repositories
{
    public class ParticipantRepository : IRepository<Participant>
    {
        private BaseConnect link;
        
        public ParticipantRepository(BaseConnect context)
        {
            link = context;
        }

        public IEnumerable<Participant> GetAll()
        {
            return link.Participants;
        }

        public Participant Get(int id)
        {
            return link.Participants.Find(id);
        }

        public void Create(Participant entity)
        {
            link.Participants.Add(entity);
        }

        public void Update(Participant entity)
        {
            link.Entry(entity).State = EntityState.Modified;
        }
        public IEnumerable<Participant> Find(Func<Participant, Boolean> predicate)
        {
            return link.Participants.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Participant entity = link.Participants.Find(id);
            if (entity != null)
                link.Participants.Remove(entity);
        }

        public List<Participant> GetParticipants(int id)
        {
            List<Participant> items = null;
            try
            {
                items =
                (from item in link.Participants
                 where item.race.Id == id
                 orderby item.Run descending
                 select item).ToList();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            return items;
        }

        public List<Participant> GetParticipantsOrder(int id)
        {
            List<Participant> items = null;
            try
            {
                items =
                (from item in link.Participants
                 where item.race.Id == id
                 orderby item.Run descending
                 select item).ToList();
            }
            catch(Exception ex)
            {
                string str = ex.Message;
            }

            return items;
        }
    }
}
