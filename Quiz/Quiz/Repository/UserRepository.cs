using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.DB;
using Quiz.interfaces.Repositories;

namespace Quiz.Repository
{
    public class UserRepository : IUserRepository
    {
        AppDbContext _context=new AppDbContext();
        public bool Login(string username, string password)
        {
            return _context.Users.Any(u=>u.UserName == username && u.Password == password);
        }
    }
}
