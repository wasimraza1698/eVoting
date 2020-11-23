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
        VoteRepo voteRepo;
        [SetUp]
        public void Setup()
        {
            user = new List<Vote>()
            {
                new Vote{VoteID=1,VoterID = 1,ContenderID = 2}

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
            voteRepo = new VoteRepo(votecontextmock.Object);
        }

        [Test]
        public void AddVotePass()
        {
            var vote = new Vote() {VoteID = 2, VoterID = 3, ContenderID = 4};
            var added = voteRepo.CastVote(vote);
            Assert.AreEqual(true,added);
        }
    }
}