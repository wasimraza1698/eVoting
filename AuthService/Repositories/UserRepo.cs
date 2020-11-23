using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Data;

namespace AuthService.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly AuthDbContext _context;
        public UserRepo(AuthDbContext context)
        {
            _context = context;
        }
        /*public bool Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }*/

        public User Get(User user)
        {
            var valUser =
                _context.Users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            return valUser;
        }
    }
}
