﻿using Bloggie.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AuthDbContext _authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
           var users = await _authDbContext.Users.ToListAsync();

           var superAdminUser =
               await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email == "superAdmin@bloggie.com");

           if (superAdminUser is not null)
           {
               users.Remove(superAdminUser);
           }

            return users;
        }
    }
}
