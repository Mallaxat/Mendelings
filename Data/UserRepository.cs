using Mendelings.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mendelings.Data
{
    public class UserRepository
    {
        private readonly MendelingsDbContext _context;

        public UserRepository(MendelingsDbContext context)
        {
            _context = context;
        }
        //Поиск по почте
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
        //по логину
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetByEmailOrUsernameAsync(string value)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == value || x.Username == value);
        }
    }
}
