using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.interfaces.Services
{
    public interface IUserService
    {
        bool Login(string username, string password);
    }
}
