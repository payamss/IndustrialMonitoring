﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MonitoringServiceLibrary.ViewModels;
using NationalInstruments.NetworkVariable;
using SharedLibrary;

namespace MonitoringServiceLibrary
{
    public class ProcessDataService : IProcessDataService
    {
        public ItemsLogLatestAIOViewModel GeItemsLogLatest(int itemId)
        {
            try
            {
                ItemsLogLatestAIOViewModel result = null;

                var Entities = new IndustrialMonitoringEntities();
                ItemsLogLatest current = Entities.ItemsLogLatests.FirstOrDefault(x => x.ItemId == itemId);

                if (current == null)
                {
                    return null;
                }

                result = new ItemsLogLatestAIOViewModel(current);

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return null;
            }
        }

        public List<Item1> GetItems()
        {
            try
            {
                List<Item1> result = new List<Item1>();
                var Entities = new IndustrialMonitoringEntities();
                var items = Entities.Items.OrderBy(x => x.Order);

                foreach (var item in items)
                {
                    result.Add(new Item1(item));
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return null;
            }
        }

        public List<Item2> GetItems2()
        {
            try
            {
                List<Item2> result = new List<Item2>();
                var Entities = new IndustrialMonitoringEntities();
                var items = Entities.Items.OrderBy(x => x.Order);

                foreach (var item in items)
                {
                    result.Add(new Item2(item));
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return null;
            }
        }

        public List<Item3> GetItems3()
        {
            try
            {
                List<Item3> result = new List<Item3>();
                var Entities = new IndustrialMonitoringEntities();
                var items = Entities.Items.OrderBy(x => x.Order);

                foreach (var item in items)
                {
                    result.Add(new Item3(item));
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return null;
            }
        }

        public Item1 GetItem(int itemId)
        {
            var Entities = new IndustrialMonitoringEntities();
            var item = Entities.Items.FirstOrDefault(x => x.ItemId == itemId);
            Item1 result = new Item1(item);

            return result;
        }

        public List<Tab1> GetTabs()
        {
            List<Tab1> result = new List<Tab1>();
            var Entities = new IndustrialMonitoringEntities();
            var tabs = Entities.Tabs.OrderBy(x=>x.Order);

            foreach (var tab in tabs)
            {
                Tab1 current = new Tab1(tab);

                result.Add(current);
            }

            return result;
        }

        public List<Tab1> GetTabs(Func<Tab1, bool> predicate)
        {
            var Entities = new IndustrialMonitoringEntities();
            List<Tab1> result = new List<Tab1>();

            var tabs = Entities.Tabs.OrderBy(x=>x.Order);

            foreach (var tab in tabs)
            {
                Tab1 current = new Tab1(tab);

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

            var tabItems = Entities.TabsItems.OrderBy(x=>x.Item.Order);

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

            var tabItems = Entities.TabsItems.OrderBy(x=>x.Item.Order);

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

        public List<Item1> GetItems(int tabId)
        {
            var Entities = new IndustrialMonitoringEntities();
            List<Item1> result = new List<Item1>();

            var tabsItem = Entities.TabsItems.Where(x => x.TabId == tabId).OrderBy(x=>x.Item.ItemName);

            foreach (var item in tabsItem)
            {
                Item currentItem = Entities.Items.FirstOrDefault(x => x.ItemId == item.ItemId);

                result.Add(new Item1(currentItem));
            }

            return result;
        }

        public List<ItemsLogChartHistoryViewModel> GetItemLogs(int itemId, DateTime startDate, DateTime endDate)
        {
            var Entities = new IndustrialMonitoringEntities();
            List<ItemsLogChartHistoryViewModel> result = new List<ItemsLogChartHistoryViewModel>();

            var itemsLog1 = Entities.ItemsLogs.Where(x => x.ItemId == itemId & x.Time >= startDate & x.Time <= endDate).OrderBy(x => x.ItemLogId);
            var itemsLog2 = Entities.ItemsLogArchives.Where(x => x.ItemId == itemId & x.Time>=startDate & x.Time<=endDate).OrderBy(x=>x.ItemLogId);

            List<ItemsLog> itemsLog=new List<ItemsLog>();

            itemsLog.AddRange(itemsLog1);

            foreach (ItemsLogArchive itemsLogArchive in itemsLog2)
            {
                ItemsLog log2 = new ItemsLog();
                log2.ItemId = itemsLogArchive.ItemId;
                log2.Value = itemsLogArchive.Value;
                log2.Time = itemsLogArchive.Time;
                log2.ItemLogId = itemsLogArchive.ItemLogId;

                itemsLog.Add(log2);
            }

            var itemsLogAll = itemsLog.OrderBy(x => x.Time);

            foreach (var log in itemsLogAll)
            {
                result.Add(new ItemsLogChartHistoryViewModel(log));
            }

            return result;
        }

        public bool DeleteItemLog(int itemLogId)
        {
            try
            {
                IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();
                var Item= entities.ItemsLogs.FirstOrDefault(x => x.ItemLogId == itemLogId);

                if (Item != null)
                {
                    entities.ItemsLogs.Remove(Item);
                    entities.SaveChanges();
                    return true;
                }
                else
                {
                    var Item2 = entities.ItemsLogArchives.FirstOrDefault(x => x.ItemLogId == itemLogId);

                    if (Item2 != null)
                    {
                        entities.ItemsLogArchives.Remove(Item2);
                        entities.SaveChanges();
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return false;
            }
        }

        public bool AddItem2(Item2 item)
        {
            try
            {
                IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();

                Item newItem = new Item();
                newItem.ItemName = item.ItemName;
                newItem.ItemType = (int)item.ItemType;
                newItem.Location = item.Location;
                newItem.SaveInItemsLogTimeInterval = item.SaveInItemsLogTimeInterval;
                newItem.SaveInItemsLogLastesTimeInterval = item.SaveInItemsLogLastesTimeInterval;
                newItem.ShowInUITimeInterval = item.ShowInUITimeInterval;
                newItem.ScanCycle = item.ScanCycle;
                newItem.SaveInItemsLogWhen = (int)item.SaveInItemsLogWhen;
                newItem.SaveInItemsLogLastWhen = (int)item.SaveInItemsLogLastWhen;

                entities.Items.Add(newItem);

                if (entities.SaveChanges() > 0)
                {
                    return true;
                }


                return false;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return false;
            }
        }

        public bool DeleteItem(int itemId)
        {
            try
            {
                IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();

                bool success = false;

                using (TransactionScope transaction = new TransactionScope())
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
                    Item itemToDelete = entities.Items.FirstOrDefault(x => x.ItemId == itemId);
                    entities.Items.Remove(itemToDelete);
                    entities.SaveChanges();

                    transaction.Complete();
                    success = true;
                }

                return success;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return false;
            }
        }

        public bool EditItem2(Item2 item)
        {
            try
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
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return false;
            }
        }

        public bool AddItemsToTab(string tabName, List<string> items)
        {
            try
            {
                IndustrialMonitoringEntities entities = new IndustrialMonitoringEntities();

                bool success = false;

                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        Tab currenTab = entities.Tabs.FirstOrDefault(x => x.TabName == tabName);

                        foreach (var item in items)
                        {
                            Item currentItem = entities.Items.FirstOrDefault(x => x.ItemName == item);
                            TabsItem newTabsItem = new TabsItem();
                            newTabsItem.ItemId = currentItem.ItemId;
                            newTabsItem.TabId = currenTab.TabId;

                            entities.TabsItems.Add(newTabsItem);
                            entities.SaveChanges();
                        }

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
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return false;
            }
        }

        public bool GetHorn()
        {
            try
            {
                var subscriberBool = new NetworkVariableBufferedSubscriber<bool>(@"\\localhost\Simulation\OPC\Channel1\Device1\Horn");
                subscriberBool.Connect();
                var variable = subscriberBool.ReadData();
                bool result = variable.GetValue();
                subscriberBool.Disconnect();

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return false;
            }
        }

        public bool GetHornHMI()
        {
            try
            {
                var subscriberBool = new NetworkVariableBufferedSubscriber<bool>(@"\\localhost\Simulation\OPC\Channel1\Device1\HornHMI");
                subscriberBool.Connect();
                var variable = subscriberBool.ReadData();
                bool result = variable.GetValue();
                subscriberBool.Disconnect();

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return false;
            }
        }

        public bool GetMuteHorn()
        {
            try
            {
                var subscriberBool = new NetworkVariableBufferedSubscriber<bool>(@"\\localhost\Simulation\OPC\Channel1\Device1\MuteHorn");
                subscriberBool.Connect();
                var variable = subscriberBool.ReadData();
                bool result = variable.GetValue();
                subscriberBool.Disconnect();

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
                return false;
            }
        }

        public void MuteHorn()
        {
            try
            {
                var writer = new NetworkVariableBufferedWriter<bool>(@"\\localhost\Simulation\OPC\Channel1\Device1\MuteHorn");
                writer.Connect();
                writer.WriteValue(true);

                writer.Disconnect();
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
            }
        }

        public void UnMuteHorn()
        {
            try
            {
                var writer = new NetworkVariableBufferedWriter<bool>(@"\\localhost\Simulation\OPC\Channel1\Device1\MuteHorn");
                writer.Connect();
                writer.WriteValue(false);

                writer.Disconnect();
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
            }
        }

        public bool On(string location)
        {
            try
            {
                var writer = new NetworkVariableBufferedWriter<bool>(location);
                writer.Connect();
                writer.WriteValue(true);

                writer.Disconnect();

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
            }

            return false;
        }

        public bool Off(string location)
        {
            try
            {
                var writer = new NetworkVariableBufferedWriter<bool>(location);
                writer.Connect();
                writer.WriteValue(false);

                writer.Disconnect();

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogMonitoringServiceLibrary(ex);
            }

            return false;
        }
    }
}
