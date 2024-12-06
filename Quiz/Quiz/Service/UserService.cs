using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.interfaces.Services;
using Quiz.Repository;

namespace Quiz.Service
{
    public class UserService : IUserService
    {
        UserRepository repository=new UserRepository();
        public bool Login(string username, string password)
        {
            bool ResultOfLogin= repository.Login(username, password);
            if (!ResultOfLogin)
            {
                Console.WriteLine("There is no user with this informaton in database!! Please try againe.");
                return false;
            } 
            return true;
        }
    }
}
