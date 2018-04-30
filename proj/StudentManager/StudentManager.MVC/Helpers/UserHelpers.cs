using Restaurant.MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using Restaurant.Data.Entities;
using Restaurant.MVC.Models.Account;

namespace Restaurant.MVC.Helpers
{
    public static class UserHelpers
    {
        public static string GetCurUserID(IPrincipal pUser)
        {

            var currentUserId = pUser.Identity.GetUserId();
            

            return currentUserId;
        }

        
        public static bool CheckRoles(IPrincipal pUser, string[] roles)
        {
            bool result = false;
            foreach (string role in roles)
            {
                result |= pUser.IsInRole(role);
            }
            return result;
        }

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
    }
}