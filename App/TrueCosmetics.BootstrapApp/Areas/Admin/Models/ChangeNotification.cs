using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Models
{
    public class ChangeNotification
    {
        public static LinkedList<ChangeNotification> AllNotifications = new LinkedList<ChangeNotification>();

        public static IDictionary<EntityState, string> StateNames = new Dictionary<EntityState, string>()
        {
            { EntityState.Added, "Добавен"},
            { EntityState.Deleted, "Изтрит"},
            { EntityState.Detached, "Откачен"},
            { EntityState.Modified, "Променен"},
            { EntityState.Unchanged, "Непроменен"},
        };

        public EntityState State { get; set; }

        public string Name { get; set; }

        public DateTime ChangeDate { get; set; }
    }
}