using System.Collections.Generic;
using System.Linq;
using AuthService.Controllers;
using AuthService.Data;
using AuthService.Models;
using AuthService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;

namespace AuthServiceTest
{
    public class Tests
    {
        List<User> user = new List<User>();
        IQueryable<User> userdata;
        Mock<DbSet<User>> mockSet;
        Mock<AuthDbContext> usercontextmock;
        Mock<IConfiguration> config;
        [SetUp]
        public void Setup()
        {
            user = new List<User>()
            {
                new User{UserID=1,UserName="abc",Password="abc123"}

            };
            userdata = user.AsQueryable();
            mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userdata.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userdata.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userdata.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userdata.GetEnumerator());
            var p = new DbContextOptions<AuthDbContext>();
            usercontextmock = new Mock<AuthDbContext>(p);
            usercontextmock.Setup(x => x.Users).Returns(mockSet.Object);
            config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("SecretKeyForEncryption");
        }


        [Test]
        public void LoginTest()
        {
            var userRepo = new UserRepo(usercontextmock.Object);
            var controller = new UserController(userRepo, config.Object);
            var auth = controller.Login(new User { UserID = 1, UserName = "abc", Password = "abc123" }) as OkObjectResult;
            Assert.AreEqual(200, auth.StatusCode);
        }
        [Test]
        public void LoginTestFail()
        {
            var userRepo = new UserRepo(usercontextmock.Object);
            var controller = new UserController(userRepo, config.Object);
            var auth = controller.Login(new User { UserID = 1, UserName = "abc", Password = "c123" }) as OkObjectResult;
            Assert.IsNull(auth);
        }
    }
}