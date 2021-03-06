﻿using System;
using System.Threading.Tasks;
using Sample.Wivuu.Domain;

namespace Sample.Wivuu.Business
{
    public sealed class BusinessContext : IDisposable
    {
        internal MyDbContext Db { get; }

        public UserFormService UserForms { get; }

        public BusinessContext()
        {
            Db        = new MyDbContext();
            UserForms = new UserFormService(this);
        }

        public Task Save() => Db.SaveChangesAsync();

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}