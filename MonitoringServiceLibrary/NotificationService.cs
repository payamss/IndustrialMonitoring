﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MonitoringServiceLibrary.ViewModels;

namespace MonitoringServiceLibrary
{
    public class NotificationService : INotificationService
    {
        public bool HasNotification(int itemId)
        {
            int count = 0;
            IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();

            List<NotificationItem> notificationItems = entities.NotificationItems.Where(x => x.ItemId == itemId).ToList();

            if (notificationItems == null || notificationItems.Count == 0)
            {
                return false;
            }

            foreach (var notificationItem in notificationItems)
            {
                NotificationItemsLogLatest notificationItemsLogLatest =
                    entities.NotificationItemsLogLatests.FirstOrDefault(
                        x => x.NotificationId == notificationItem.NotificationId);

                if (notificationItemsLogLatest.Value)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                return true;
            }

            return false;
        }

        public List<Notification1> GetNotifications(int itemId)
        {
            List<Notification1> resultList=new List<Notification1>();

            IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();

            List<NotificationItem> notificationItems = entities.NotificationItems.Where(x => x.ItemId == itemId).ToList();

            if (notificationItems == null || notificationItems.Count == 0)
            {
                return null;
            }

            foreach (var notificationItem in notificationItems)
            {
                NotificationItemsLogLatest notificationItemsLogLatest =
                    entities.NotificationItemsLogLatests.FirstOrDefault(
                        x => x.NotificationId == notificationItem.NotificationId);

                if (notificationItemsLogLatest.Value)
                {
                    Notification1 notification1=new Notification1(notificationItem);
                    resultList.Add(notification1);
                }
            }

            return resultList;
        }

        public bool SystemHasNotification(int userId)
        {
            IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();

            var notifications = entities.NotificationItemsLogLatests.ToList();

            if (notifications == null || notifications.Count == 0)
            {
                return false;
            }

            List<int> ItemIds=new List<int>();

            foreach (NotificationItemsLogLatest notification in notifications)
            {
                if (!notification.Value)
                {
                    ItemIds.Add(notification.NotificationItem.ItemId);    
                }
            }

            List<int> ItemIdsForCurrentUser=new List<int>();

            foreach (int itemId in ItemIds)
            {
                if (entities.UsersItemsPermissions.Any(x => x.UserId == userId && x.ItemId == itemId))
                {
                    ItemIdsForCurrentUser.Add(itemId);
                }
            }

            if (ItemIdsForCurrentUser.Count > 0)
            {
                return true;
            }

            return false;
        }

        public List<NotificationLog> GetNotificationLogs(int userId, DateTime startTime, DateTime endTime)
        {
            IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();

            List<NotificationItemsLog> notificationItemsLogs =
                entities.NotificationItemsLogs.Where(x => x.Time >= startTime && x.Time <= endTime).ToList();

            List<UsersItemsPermission> usersItemsPermissions =
                entities.UsersItemsPermissions.Where(x => x.UserId == userId).ToList();

            List<NotificationLog> result=new List<NotificationLog>();


            foreach (NotificationItemsLog log in notificationItemsLogs)
            {
                if (usersItemsPermissions.Any(x => x.ItemId == log.NotificationItem.ItemId))
                {
                    int itemId = log.NotificationItem.ItemId;
                    string itemName = log.NotificationItem.Item.ItemName;
                    string notificationMsg = log.NotificationItem.NotificationMsg;
                    DateTime dateTime = log.Time;
                    bool hasFault = !log.Value;

                    NotificationLog notificationLog=new NotificationLog(itemId,itemName,notificationMsg,dateTime,hasFault);

                    result.Add(notificationLog);
                }
                
            }

            return result;
        }

        public List<NotificationLog> GetNotificationLog(int userId, int itemId, DateTime startTime, DateTime endTime)
        {
            IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();

            List<NotificationItemsLog> notificationItemsLogs =
                entities.NotificationItemsLogs.Where(x => x.Time >= startTime && x.Time <= endTime).ToList();

            List<UsersItemsPermission> usersItemsPermissions =
                entities.UsersItemsPermissions.Where(x => x.UserId == userId).ToList();

            List<NotificationLog> result = new List<NotificationLog>();


            foreach (NotificationItemsLog log in notificationItemsLogs)
            {
                if (log.NotificationItem.ItemId == itemId)
                {
                    if (usersItemsPermissions.Any(x => x.ItemId == log.NotificationItem.ItemId))
                    {
                        int itemId2 = log.NotificationItem.ItemId;
                        string itemName = log.NotificationItem.Item.ItemName;
                        string notificationMsg = log.NotificationItem.NotificationMsg;
                        DateTime dateTime = log.Time;
                        bool hasFault = !log.Value;

                        NotificationLog notificationLog = new NotificationLog(itemId2, itemName, notificationMsg, dateTime, hasFault);

                        result.Add(notificationLog);
                    }
                }

            }

            return result;
        }
    }
}
