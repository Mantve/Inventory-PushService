using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_PushService.Entities
{
    public class Payload
    {
        public Notification notification { get; set; }
        public Payload(string title)
        {
            notification = new()
            {
                title = title,
                icon = "assets/micon-72x72.png",
                vibrate = new() { 100, 50, 100 },
                data = new()
                {
                    dateOfArrival = "aaa",
                    primaryKey = 1
                },
                actions = new()
                {
                    new()
                    {
                        action = "explore",
                        title = "Go to reminders"
                    }
                }
            };
        }

        public class Data
        {
            public string dateOfArrival { get; set; }
            public int primaryKey { get; set; }
        }

        public class Action
        {
            public string action { get; set; }
            public string title { get; set; }
        }

        public class Notification
        {
            public string title { get; set; }
            public string body { get; set; }
            public string icon { get; set; }
            public List<int> vibrate { get; set; }
            public Data data { get; set; }
            public List<Action> actions { get; set; }
        }


    }
}
