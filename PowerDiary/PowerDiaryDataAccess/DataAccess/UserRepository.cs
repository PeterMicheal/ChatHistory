using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.DatabaseModel;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryDataAccess.DataAccess
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ChatDbContext powerDiaryDbContext) : base(powerDiaryDbContext)
        {
            
        }
    }
}
