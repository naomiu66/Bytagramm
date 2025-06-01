using BytagrammAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BytagrammAPI.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Community> Communities { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
    .HasMany(u => u.Posts)
    .WithOne(p => p.Author)
    .HasForeignKey(p => p.AuthorId)
    .OnDelete(DeleteBehavior.Cascade);

            // 👇 Используем отдельное свойство OwnedCommunities
            builder.Entity<User>()
                .HasMany(u => u.OwnedCommunities)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Community>()
                .HasMany(c => c.Posts)
                .WithOne(p => p.Community)
                .HasForeignKey(p => p.CommunityId)
                .OnDelete(DeleteBehavior.Cascade);

            // 👇 SubscribedCommunities участвуют в many-to-many через промежуточную таблицу
            builder.Entity<Community>()
                .HasMany(c => c.Subscribers)
                .WithMany(u => u.SubscribedCommunities)
                .UsingEntity<Dictionary<string, object>>(
                    "Subscribers",
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Community>()
                        .WithMany()
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                );


        }
    }
}
