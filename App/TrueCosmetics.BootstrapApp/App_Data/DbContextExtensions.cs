using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace TrueCosmetics.Data
{
    public static class DbContextExtensions
    {
        public static EntityKey GetEntityKey<T>(this DbContext context, T entity) where T : class
        {
            var oc = ((IObjectContextAdapter)context).ObjectContext;
            ObjectStateEntry ose;
            if (null != entity && oc.ObjectStateManager.TryGetObjectStateEntry(entity, out ose))
            {
                return ose.EntityKey;
            }
            return null;
        }
    }
}