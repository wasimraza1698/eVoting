using System.Collections.Generic;
using System.Linq;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using VoterService.Controllers;
using VoterService.Data;
using VoterService.Models;
using VoterService.Repositories;

namespace VoterServiceTest
{
    public class Tests
    {
        List<Vote> user = new List<Vote>();
        IQueryable<Vote> votedata;
        Mock<DbSet<Vote>> mockSet;
        Mock<VoterDbContext> votecontextmock;
        [SetUp]
        public void Setup()
        {
            user = new List<Vote>()
            {
                new Vote{VoteID=1,VoterID = 1,ContenderID = 2},
                new Vote{VoteID=2,VoterID = 2,ContenderID = 4}

            };
            votedata = user.AsQueryable();
            mockSet = new Mock<DbSet<Vote>>();
            mockSet.As<IQueryable<Vote>>().Setup(m => m.Provider).Returns(votedata.Provider);
            mockSet.As<IQueryable<Vote>>().Setup(m => m.Expression).Returns(votedata.Expression);
            mockSet.As<IQueryable<Vote>>().Setup(m => m.ElementType).Returns(votedata.ElementType);
            mockSet.As<IQueryable<Vote>>().Setup(m => m.GetEnumerator()).Returns(votedata.GetEnumerator());
            var p = new DbContextOptions<VoterDbContext>();
            votecontextmock = new Mock<VoterDbContext>(p);
            votecontextmock.Setup(x => x.Votes).Returns(mockSet.Object);
        }

        [Test]
        public void AddVotePass()
        {
            VoteRepo voteRepo = new VoteRepo(votecontextmock.Object);
            var controller = new VoteController(voteRepo);
            var response = controller.Add(new Vote() {VoteID = 3, VoterID = 3, ContenderID = 2}) as StatusCodeResult;
            Assert.AreEqual(201,response.StatusCode);
        }

        /*[Test]
        public void AddVoteFail()
        {
            VoteRepo voteRepo = new VoteRepo(votecontextmock.Object);
            var controller = new VoteController(voteRepo);
            var response = controller.Add(new Vote() {VoterID = 3, ContenderID = 2 }) as StatusCodeResult;
            Assert.AreEqual(400, response.StatusCode);
        }*/
    }
}