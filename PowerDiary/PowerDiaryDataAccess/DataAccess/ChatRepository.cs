using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.DatabaseModel;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryDataAccess.DataAccess
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        private readonly IPowerDiaryDbContext _powerDiaryDbContext;
        public ChatRepository(PowerDiaryDbContext powerDiaryDbContext) : base(powerDiaryDbContext)
        {
            _powerDiaryDbContext = powerDiaryDbContext;
        }
    }
}
