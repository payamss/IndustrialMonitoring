﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Monitoring.ViewModels;

namespace Monitoring
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        private IndustrialMonitoringEntities _entities = new IndustrialMonitoringEntities();

        public IndustrialMonitoringEntities Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public UserViewModel GetUserByUserName(string userName)
        {
            UserViewModel result = null;

            User user = Entities.Users.FirstOrDefault(x => x.UserName == userName);

            result=new UserViewModel(user);

            return result;
        }

        public UserViewModel GetUserByUserId(int userId)
        {
            UserViewModel result = null;

            User user = Entities.Users.FirstOrDefault(x => x.UserId == userId);

            result = new UserViewModel(user);

            return result;
        }

        public bool Authorize(string userName, string password)
        {
            if (Entities.Users.Any(x => x.UserName == userName && x.Password == password))
            {
                return true;
            }

            return false;
        }

        public bool CheckPermission(int userId, int itemId)
        {
            if (Entities.UsersItemsPermissions.Any(x => x.UserId == userId && x.ItemId == itemId))
            {
                return true;
            }

            return false;
        }

        public bool UserHaveItemInTab(int userId, int tabId)
        {
            var userItemsPermissionQuery = Entities.UsersItemsPermissions.Where(x => x.UserId == userId);

            foreach (var u in userItemsPermissionQuery)
            {
                bool tabExist = Entities.TabsItems.Any(x => x.ItemId == u.ItemId && x.TabId==tabId);

                if (tabExist)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
