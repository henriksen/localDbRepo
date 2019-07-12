using Microsoft.EntityFrameworkCore;
using LocalDbRepo.Pipelines.Entities;

namespace LocalDbRepo.Pipelines
{
    public class CommunicationContext : DbContext
    {
        public CommunicationContext(DbContextOptions<CommunicationContext> options)
            : base(options)
        { }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.Customer)
                .WithMany(u => u.ConversationsAsCustomer)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
