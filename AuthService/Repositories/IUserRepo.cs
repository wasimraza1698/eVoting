﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Models;

namespace AuthService.Repositories
{
    public interface IUserRepo
    {
        //public bool Add(User user);
        public User Get(User user);
    }
}
