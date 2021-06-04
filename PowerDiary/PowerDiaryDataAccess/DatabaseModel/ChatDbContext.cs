using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PowerDiaryDataAccess.DatabaseModel;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryDataAccess.DatabaseModel
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>()
                .HasOne(p => p.User)
                .WithMany(t => t.Chats)
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Chat>()
                .HasOne(p => p.UserTo)
                .WithMany(t => t.ChatsTo)
                .HasForeignKey(m => m.UserToId)
                .IsRequired(false);
        }

        public DbSet<Chat> Chat { get; set; }
        public DbSet<User> User { get; set; }
    }
}
