﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringServiceLibrary
{
    public class NotificationsBot
    {
        private static NotificationsBot instance = null;
        private static readonly object padlock = new object();
        private Telegram.Bot.Api bot;

        private NotificationsBot()
        {
            bot = new Telegram.Bot.Api("133038323:AAFXVhA9Htj3p0a0Sl3hydt65Y7fl2AOVEI");
        }

        public static NotificationsBot Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NotificationsBot();
                    }
                    return instance;
                }
            }
        }

        public async void SendNotification(int notificationLogId)
        {
            try
            {
                IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();

                var notificationItemsLog =
                    entities.NotificationItemsLogs.FirstOrDefault(x => x.NotificationLogId == notificationLogId);

                if (notificationItemsLog == null)
                {
                    return;
                }

                int itemId = notificationItemsLog.NotificationItem.ItemId;

                var userPermissions = entities.UsersItemsPermissions.Where(x => x.ItemId == itemId);

                List<int> chatIds = new List<int>();

                foreach (UsersItemsPermission usersItemsPermission in userPermissions)
                {
                    int userId = usersItemsPermission.UserId;

                    var ids = entities.Bots.Where(x => x.UserId == userId);

                    if (!ids.Any())
                    {
                        return;
                    }

                    foreach (Bot id in ids)
                    {
                        if (id.ChatId != null)
                        {
                            if (id.ChatId > 0)
                            {
                                chatIds.Add((int)id.ChatId);
                            }
                        }

                    }
                }

                string output = string.Format(@"Alarm :
Item Name : {0}
Item Id : {1}
Description : {2}
Has Alarm : {3}
Date : {4}", notificationItemsLog.NotificationItem.Item.ItemName, notificationItemsLog.NotificationItem.ItemId
    , notificationItemsLog.NotificationItem.NotificationMsg, !notificationItemsLog.Value, notificationItemsLog.Time);

                foreach (int chatId in chatIds)
                {
                    await bot.SendTextMessage(chatId, output);
                }
            }
            catch (Exception)
            {
                
            }
            
        }
    }
}
