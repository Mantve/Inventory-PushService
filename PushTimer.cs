using Inventory_PushService.Data.Entities;
using Inventory_PushService.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Inventory_PushService
{
    public class PushTimer
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IReminderRepository _reminderRepository;

        public PushTimer(ISubscriptionRepository subscriptionRepository, IReminderRepository reminderRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _reminderRepository = reminderRepository;
        }

        public async Task Start()
        {
            while (true)
            {
                await OnTimedEvent();
                Task task = Task.Delay(TimeSpan.FromSeconds(60));

                try
                {
                    await task;
                }
                catch 
                {
                }
            }
        }

        public async Task<IEnumerable<Reminder>> GetAllDueReminders()
        {
            return await _reminderRepository.GetAllDue();
        }

        public async Task<IEnumerable<Subscription>> GetAllSubscriptions(string username)
        {
            return await _subscriptionRepository.GetAll(username);
        }

        public async Task UpdateReminder(Reminder reminder)
        {
            switch (reminder.RepeatFrequency)
            {
                case (RepeatFrequency.None):
                    reminder.Expired = true;
                    break;
                case (RepeatFrequency.OneDay):
                    reminder.ReminderTime.AddDays(1);
                    break;
                case (RepeatFrequency.OneMonth):
                    reminder.ReminderTime.AddMonths(1);
                    break;
                case (RepeatFrequency.OneWeek):
                    reminder.ReminderTime.AddDays(7);
                    break;
                case (RepeatFrequency.OneYear):
                    reminder.ReminderTime.AddYears(1);
                    break;
                case (RepeatFrequency.SixMonths):
                    reminder.ReminderTime.AddMonths(6);
                    break;
                case (RepeatFrequency.ThreeMonths):
                    reminder.ReminderTime.AddMonths(3);
                    break;
                case (RepeatFrequency.TwoWeeks):
                    reminder.ReminderTime.AddDays(14);
                    break;
            }
            await _reminderRepository.Put(reminder);
        }


        public async Task OnTimedEvent()
        {
            IEnumerable<Reminder> dueReminders = await GetAllDueReminders();
            foreach (var reminder in dueReminders)
            {
                IEnumerable<Subscription> subscriptions = await GetAllSubscriptions(reminder.Author.Username);
                foreach (var subscription in subscriptions)
                {
                    PushService.Push(subscription, reminder.Reason);
                }
                await UpdateReminder(reminder);
            }
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              DateTime.Now);
        }

    }
}
