using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : BasicRepository<User, int>, IUserRepository
    {
        public UserRepository(AuthDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByEmailAsync(string email) =>
            await DbSet.AnyAsync(e => e.Email == email);
    }
}
