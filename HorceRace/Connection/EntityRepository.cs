using Connection.Data;
using Connection.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class EntityRepository
    {
        private static EntityRepository instance;
        private BaseConnect link;
        private AccountRepository accountRepository;
        private GroupRepository groupRepository;
        private RaceRepository raceRepository;
        private ParticipantRepository participantRepository;
        private bool disposed = false;

        private EntityRepository()
        {
            link = new BaseConnect();
        }

        public static EntityRepository GetInstance() 
        {
            if (instance == null)
            {
                instance = new EntityRepository();
            }

            return instance;
        }

        public AccountRepository Accounts
        {
            get
            {
                if (accountRepository == null)
                    accountRepository = new AccountRepository(link);
                return accountRepository;
            }
        }

        public GroupRepository Groups
        {
            get
            {
                if (groupRepository == null)
                    groupRepository = new GroupRepository(link);
                return groupRepository;
            }
        }

        public RaceRepository Races
        {
            get
            {
                if (raceRepository == null)
                    raceRepository = new RaceRepository(link);
                return raceRepository;
            }
        }

        public ParticipantRepository Participants
        {
            get
            {
                if (participantRepository == null)
                    participantRepository = new ParticipantRepository(link);
                return participantRepository;
            }
        }

        public void Save()
        {
            link.SaveChanges();
        }


        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    link.Dispose();
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
