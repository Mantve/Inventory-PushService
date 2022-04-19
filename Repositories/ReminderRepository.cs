using Inventory_PushService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_PushService.Data.Repositories
{
    public interface IReminderRepository : IGenericRepository<Reminder>
    {
        Task<IEnumerable<Reminder>> GetAll(string username);
        Task<IEnumerable<Reminder>> GetAllDue();
    }

    public class ReminderRepository : GenericRepository<Reminder>, IReminderRepository
    { 

        public ReminderRepository(RestContext restContext) : base(restContext)
        {
            _restContext = restContext;
        }


        public async Task<IEnumerable<Reminder>> GetAll(string username)
        {
            return await _restContext.Reminders.Include(x => x.Item).Where(x => x.Author.Username == username).ToListAsync();
        }

        public async Task<IEnumerable<Reminder>> GetAllDue()
        {
                                        
                DateTime dateTime = System.DateTime.Now;
                return await _restContext.Reminders.Include(x => x.Item).Include(x=>x.Author).Where(x => !x.Expired && x.ReminderTime <= dateTime).ToListAsync();
        }
    }
}
