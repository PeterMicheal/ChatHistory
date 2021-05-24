using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.DatabaseModel;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryDataAccess.DataAccess
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IPowerDiaryDbContext _powerDiaryDbContext;
        public UserRepository(PowerDiaryDbContext powerDiaryDbContext) : base(powerDiaryDbContext)
        {
            _powerDiaryDbContext = powerDiaryDbContext;
        }
    }
}
