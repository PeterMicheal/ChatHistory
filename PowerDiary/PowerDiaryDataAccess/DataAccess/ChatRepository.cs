using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.DatabaseModel;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryDataAccess.DataAccess
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        public ChatRepository(ChatDbContext powerDiaryDbContext) : base(powerDiaryDbContext)
        {
            
        }
    }
}
