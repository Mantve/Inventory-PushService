using Inventory_PushService.Data;
using Inventory_PushService.Data.Repositories;
using System;
using System.Configuration;
using System.Security.Cryptography;

namespace Inventory_PushService
{
    class Program
    {
        static void Main()
        {
            RestContext restContext = new();
            ReminderRepository reminderRepository = new(restContext);
            SubscriptionRepository subscriptionRepository = new(restContext);
            PushTimer pushTimer = new(subscriptionRepository, reminderRepository);
            /*
            var encrypted = CryptographyHelper.EncryptStringToBytes_Aes("asdf128test", System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings.Get("encryptionKey")), System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings.Get("iv")));
            var decrypted = CryptographyHelper.DecryptStringFromBytes_Aes(encrypted, System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings.Get("encryptionKey")), System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings.Get("iv")));
            Console.WriteLine(encrypted);
            Console.WriteLine(decrypted);
            */
            pushTimer.Start().Wait();
        }
    }
}
