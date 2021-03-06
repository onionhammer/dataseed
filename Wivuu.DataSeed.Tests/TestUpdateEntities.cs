﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wivuu.DataSeed.Tests.Domain;

namespace Wivuu.DataSeed.Tests
{
    [TestClass]
    public class TestUpdateEntities
    {
        class TestUser
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        [TestMethod]
        public void TestDefaults()
        {
            // Scenario 1
            var item0 = new TestUser
            {
                Id   = new Guid("27a21f77-c286-4767-a23c-571ec4d11aee"),
                Name = "John Smith"
            };

            var updated0 = item0
                .Update(new { Name = "Craig Smith" })
                .Default(() => new TestUser
                {
                    Id   = item0.Id,
                    Name = "Craig Smith"
                });

            Assert.AreEqual("Craig Smith", updated0.Name);

            // Scenario 2
            var item1 = default(TestUser);

            var updated1 = item1
                .Update(new { Name = "Craig Smith" })
                .Default(c => 
                {
                    c.Id = item0.Id;
                    return c;
                });

            Assert.AreEqual("Craig Smith", updated1.Name);

            // Scenario 3
            var item2 = default(TestUser);

            var updated2 = item2
                .Update(new { Name = "Craig Smith" })
                .Update(new { Name = "Jim Smith" })
                .Default(() =>
                {
                    var c = new TestUser();
                    c.Id = item0.Id;
                    return c;
                });

            Assert.AreEqual("Jim Smith", updated2.Name);
        }
    }
}