using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.interfaces.Repositories
{
    public interface IUserRepository
    {
        bool Login(string username, string password);
        //void GetListOfUserCards(string username);
    }
}
