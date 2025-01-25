using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Services.Interface
{
    public interface IUserServiceFactory
    {
        IUserRepository<t> GetInstance<t>() where t : class;
        void BeginTransaction();
        void RollBack();
        void CommitTransaction();
    }
}
