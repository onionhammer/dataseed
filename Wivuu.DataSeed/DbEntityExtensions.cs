﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using Wivuu.DataSeed;

namespace Wivuu.DataSeed
{
    public struct UpdateSet<T>
        where T : class
    {
        private readonly DbContext Db;
        private readonly DbEntityEntry<T> Entry;

        internal UpdateSet(DbContext db, T entity)
        {
            this.Db    = db;
            this.Entry = Db.Entry(entity);

            if (Entry.State == EntityState.Detached)
                Db.Set<T>().Attach(entity);
        }

        /// <summary>
        /// Specifically update a single property
        /// </summary>
        public UpdateSet<T> Set<K>(Expression<Func<T, K>> change, K value)
        {
            var prop = Entry.Property(change);

            prop.CurrentValue = value;
            prop.IsModified   = true;

            return this;
        }
    }
}

namespace System.Data.Entity
{
    public static class DbEntityExtensions
    {
        /// <summary>
        /// Create an UpdateSet to change one or more properties 
        /// of the input entity
        /// </summary>
        public static UpdateSet<T> UpdateSet<T>(this DbContext db, T entity)
            where T : class => 
            new UpdateSet<T>(db, entity);

        /// <summary>
        /// Specifically update a single property
        /// </summary>
        public static UpdateSet<T> Set<T, K>(this DbContext db, 
            T entity, Expression<Func<T, K>> change, K value)
            where T : class => 
            new UpdateSet<T>(db, entity).Set(change, value);
    }
}