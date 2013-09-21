﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MonitoringServiceLibrary.ViewModels;

namespace MonitoringServiceLibrary
{
    public class ProcessDataService : IProcessDataService
    {
        public ItemsLogLatestAIOViewModel GeItemsLogLatest(int itemId)
        {
            ItemsLogLatestAIOViewModel result = null;

            var Entities=new IndustrialMonitoringEntities();
            ItemsLogLatest current = Entities.ItemsLogLatests.FirstOrDefault(x => x.ItemId == itemId);

            if (current == null)
            {
                return null;
            }

            result = new ItemsLogLatestAIOViewModel(current);

            return result;
        }

        public List<ItemsAIOViewModel> GetItems()
        {
            List<ItemsAIOViewModel> result = new List<ItemsAIOViewModel>();
            var Entities = new IndustrialMonitoringEntities();
            var items = Entities.Items;

            foreach (var item in items)
            {
                result.Add(new ItemsAIOViewModel(item));
            }

            return result;
        }

        public List<Items2> GetItems2()
        {
            List<Items2> result = new List<Items2>();
            var Entities = new IndustrialMonitoringEntities();
            var items = Entities.Items;

            foreach (var item in items)
            {
                result.Add(new Items2(item));
            }

            return result;
        }

        public ItemsAIOViewModel GetItem(int itemId)
        {
            var Entities = new IndustrialMonitoringEntities();
            var item = Entities.Items.FirstOrDefault(x => x.ItemId == itemId);
            ItemsAIOViewModel result = new ItemsAIOViewModel(item);

            return result;
        }

        public List<TabsViewModel> GetTabs()
        {
            List<TabsViewModel> result = new List<TabsViewModel>();
            var Entities = new IndustrialMonitoringEntities();
            var tabs = Entities.Tabs;

            foreach (var tab in tabs)
            {
                TabsViewModel current = new TabsViewModel(tab);

                result.Add(current);
            }

            return result;
        }

        public List<TabsViewModel> GetTabs(Func<TabsViewModel, bool> predicate)
        {
            var Entities = new IndustrialMonitoringEntities();
            List<TabsViewModel> result = new List<TabsViewModel>();

            var tabs = Entities.Tabs;

            foreach (var tab in tabs)
            {
                TabsViewModel current = new TabsViewModel(tab);

                if (predicate(current))
                {
                    result.Add(current);
                }
            }

            return result;
        }

        public List<TabsItemsViewModel> GetTabItems()
        {
            var Entities = new IndustrialMonitoringEntities();
            List<TabsItemsViewModel> result = new List<TabsItemsViewModel>();

            var tabItems = Entities.TabsItems;

            foreach (var tabsItem in tabItems)
            {
                TabsItemsViewModel current = new TabsItemsViewModel(tabsItem);
                result.Add(current);
            }

            return result;
        }

        public List<TabsItemsViewModel> GetTabItems(Func<TabsItemsViewModel, bool> predicate)
        {
            var Entities = new IndustrialMonitoringEntities();
            List<TabsItemsViewModel> result = new List<TabsItemsViewModel>();

            var tabItems = Entities.TabsItems;

            foreach (var tabsItem in tabItems)
            {
                TabsItemsViewModel current = new TabsItemsViewModel(tabsItem);

                if (predicate(current))
                {
                    result.Add(current);
                }
            }

            return result;
        }

        public List<ItemsAIOViewModel> GetItems(int tabId)
        {
            var Entities = new IndustrialMonitoringEntities();
            List<ItemsAIOViewModel> result = new List<ItemsAIOViewModel>();

            var tabsItem = Entities.TabsItems.Where(x => x.TabId == tabId);

            foreach (var item in tabsItem)
            {
                Item currentItem = Entities.Items.FirstOrDefault(x => x.ItemId == item.ItemId);

                result.Add(new ItemsAIOViewModel(currentItem));
            }

            return result;
        }

        public List<ItemsLogChartHistoryViewModel> GetItemLogs(int itemId, DateTime startDate, DateTime endDate)
        {
            var Entities = new IndustrialMonitoringEntities();
            List<ItemsLogChartHistoryViewModel> result = new List<ItemsLogChartHistoryViewModel>();

            var itemsLog = Entities.ItemsLogs.Where(x => x.ItemId == itemId);

            foreach (var log in itemsLog)
            {
                if (log.Time >= startDate && log.Time <= endDate)
                {
                    result.Add(new ItemsLogChartHistoryViewModel(log));
                }

            }

            return result;
        }

        public bool AddItem2(Items2 item)
        {
            IndustrialMonitoringEntities entities=new IndustrialMonitoringEntities();
            
            Item newItem=new Item();
            newItem.ItemName = item.ItemName;
            newItem.ItemType = (int) item.ItemType;
            newItem.Location = item.Location;
            newItem.SaveInItemsLogTimeInterval = item.SaveInItemsLogTimeInterval;
            newItem.SaveInItemsLogLastesTimeInterval = item.SaveInItemsLogLastesTimeInterval;
            newItem.ShowInUITimeInterval = item.ShowInUITimeInterval;
            newItem.ScanCycle = item.ScanCycle;
            newItem.SaveInItemsLogWhen = (int) item.SaveInItemsLogWhen;
            newItem.SaveInItemsLogLastWhen = (int) item.SaveInItemsLogLastWhen;

            entities.Items.Add(newItem);

            if (entities.SaveChanges() > 0)
            {
                return true;
            }


            return false;
        }

        public bool DeleteItem(int itemId)
        {
            IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();

            bool success = false;

            using (TransactionScope transaction=new TransactionScope())
            {
                try
                {
                    var itemsLogQuery = entities.ItemsLogs.Where(x => x.ItemId == itemId);

                    foreach (var itemsLog in itemsLogQuery)
                    {
                        entities.ItemsLogs.Remove(itemsLog);
                    }

                    entities.SaveChanges();

                    var itemsLogLatestQuery = entities.ItemsLogLatests.Where(x => x.ItemId == itemId);

                    foreach (var itemsLogLatest in itemsLogLatestQuery)
                    {
                        entities.ItemsLogLatests.Remove(itemsLogLatest);
                    }

                    entities.SaveChanges();

                    var tabItemsQuery = entities.TabsItems.Where(x => x.ItemId == itemId);

                    foreach (var tabsItem in tabItemsQuery)
                    {
                        entities.TabsItems.Remove(tabsItem);
                    }

                    entities.SaveChanges();

                    var userItemsPermissionQuery = entities.UsersItemsPermissions.Where(x => x.ItemId == itemId);

                    foreach (var usersItemsPermission in userItemsPermissionQuery)
                    {
                        entities.UsersItemsPermissions.Remove(usersItemsPermission);
                    }

                    entities.SaveChanges();

                    var notificationQuery = entities.Notifications.Where(x => x.ItemId == itemId);


                    foreach (var notification in notificationQuery)
                    {
                        Notification notification1 = notification;
                        var notificationReceiversQuery =
                            entities.NotificationsReceivers.Where(x => x.NotificationId == notification1.NotificationId);

                        foreach (var notificationsReceiver in notificationReceiversQuery)
                        {
                            entities.NotificationsReceivers.Remove(notificationsReceiver);
                        }

                        entities.SaveChanges();

                        var notificationOccurQuery =
                            entities.NotificationOccurs.Where(x => x.NotificationId == notification1.NotificationId);

                        foreach (var notificationOccur in notificationOccurQuery)
                        {
                            NotificationOccur occur = notificationOccur;
                            var notificationOccurUserQuery =
                                entities.NotificationOccurUsers.Where(x => x.OccurId == occur.OccurId);

                            foreach (var notificationOccurUser in notificationOccurUserQuery)
                            {
                                entities.NotificationOccurUsers.Remove(notificationOccurUser);
                            }

                            entities.SaveChanges();

                            entities.NotificationOccurs.Remove(notificationOccur);
                            entities.SaveChanges();
                        }

                        entities.Notifications.Remove(notification);
                        entities.SaveChanges();
                    }


                    Item itemToDelete = entities.Items.FirstOrDefault(x => x.ItemId == itemId);
                    entities.Items.Remove(itemToDelete);
                    entities.SaveChanges();

                    transaction.Complete();
                    success = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return success;
        }

        public bool EditItem2(Items2 item)
        {
            IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();

            Item itemToEdit = entities.Items.FirstOrDefault(x => x.ItemId == item.ItemId);

            if (itemToEdit == null)
            {
                return false;
            }

            itemToEdit.ItemName = item.ItemName;
            itemToEdit.ItemType = (int)item.ItemType;
            itemToEdit.Location = item.Location;
            itemToEdit.SaveInItemsLogTimeInterval = item.SaveInItemsLogTimeInterval;
            itemToEdit.SaveInItemsLogLastesTimeInterval = item.SaveInItemsLogLastesTimeInterval;
            itemToEdit.ShowInUITimeInterval = item.ShowInUITimeInterval;
            itemToEdit.ScanCycle = item.ScanCycle;
            itemToEdit.SaveInItemsLogWhen = (int)item.SaveInItemsLogWhen;
            itemToEdit.SaveInItemsLogLastWhen = (int)item.SaveInItemsLogLastWhen;

            if (entities.SaveChanges() > 0)
            {
                return true;
            }


            return false;
        }
    }
}