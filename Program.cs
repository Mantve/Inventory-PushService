using Inventory_PushService.Data;
using Inventory_PushService.Data.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;

namespace Inventory_PushService
{
    class Program
    {
        static void Main()
        {
            var builder = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            RestContext restContext = new(configuration);
            ReminderRepository reminderRepository = new(restContext);
            SubscriptionRepository subscriptionRepository = new(restContext);
            PushTimer pushTimer = new(subscriptionRepository, reminderRepository, configuration);
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
