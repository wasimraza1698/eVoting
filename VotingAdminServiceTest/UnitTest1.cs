using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using VotingAdminService.Controllers;
using VotingAdminService.Data;
using VotingAdminService.Models;
using VotingAdminService.Repositories;

namespace VotingAdminServiceTest
{
    public class Tests
    {
        List<Contender> contenders= new List<Contender>();
        IQueryable<Contender> contdata;
        Mock<DbSet<Contender>> mockSet;
        Mock<AdminDbContext> contcontextmock;
        ContenderRepo contRepo;
        [SetUp]
        public void Setup()
        {
            contenders = new List<Contender>()
            {
                new Contender(){ContenderID=1,ContenderName="abc"},
                new Contender(){ContenderID =2,ContenderName = "def"},
                new Contender(){ContenderID = 3,ContenderName = "ghi"}
            };
            contdata = contenders.AsQueryable();
            mockSet = new Mock<DbSet<Contender>>();
            mockSet.As<IQueryable<Contender>>().Setup(m => m.Provider).Returns(contdata.Provider);
            mockSet.As<IQueryable<Contender>>().Setup(m => m.Expression).Returns(contdata.Expression);
            mockSet.As<IQueryable<Contender>>().Setup(m => m.ElementType).Returns(contdata.ElementType);
            mockSet.As<IQueryable<Contender>>().Setup(m => m.GetEnumerator()).Returns(contdata.GetEnumerator());
            var p = new DbContextOptions<AdminDbContext>();
            contcontextmock = new Mock<AdminDbContext>(p);
            contcontextmock.Setup(x => x.Contenders).Returns(mockSet.Object);
            contRepo = new ContenderRepo(contcontextmock.Object);
        }

        [Test]
        public void GetAllTest()
        {
            var conts = contRepo.GetAll();
            Assert.AreEqual(contenders.Count, conts.Count());
        }

        [Test]
        public void GetById()
        {
            var cont = contRepo.GetByID(2);
            Assert.IsNotNull(cont);
        }

        [Test]
        public void GetByIdFail()
        {
            var cont = contRepo.GetByID(17);
            Assert.IsNull(cont);
        }
        [Test]
        public void Addtest()
        {
            var cont = new Contender(){ContenderID = 17,ContenderName = "KDB"};
            var added = contRepo.Add(cont);
            Assert.AreEqual(true,added);
        }

    }
}