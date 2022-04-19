using Inventory_PushService.Data.Entities;
using Inventory_PushService.Entities;
using System.Collections.Generic;
using System.Text.Json;
using WebPush;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Inventory_PushService
{
    public static class PushService
    {

        public static void Push(Subscription subscription, string reason, IConfigurationRoot configuration)
        {
            WebPushClient webPushClient = new();
            var encryptionKey = System.Text.Encoding.UTF8.GetBytes(configuration["encryptionKey"]);
            var iv = System.Text.Encoding.UTF8.GetBytes(configuration["iv"]);
            PushSubscription pushSubscription = new(
                CryptographyHelper.DecryptStringFromBytes_Aes(subscription.Endpoint, encryptionKey, iv),
                CryptographyHelper.DecryptStringFromBytes_Aes(subscription.P256dh, encryptionKey, iv),
                CryptographyHelper.DecryptStringFromBytes_Aes(subscription.Auth, encryptionKey, iv)
              );
            VapidDetails vapidDetails = new(ConfigurationManager.AppSettings.Get("subject"), ConfigurationManager.AppSettings.Get("publicKey"), ConfigurationManager.AppSettings.Get("privateKey"));
            Payload payload = new(reason);
            string stringified = JsonSerializer.Serialize(payload);
            try
            {
                webPushClient.SendNotification(pushSubscription, stringified, vapidDetails);
            }
            catch
            {
            }
        }
    }
}
