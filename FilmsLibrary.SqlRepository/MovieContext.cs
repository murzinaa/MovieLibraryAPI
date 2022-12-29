using FilmsLibrary.Models.Sql;
using FilmsLibrary.SqlRepository.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FilmsLibrary.SqlRepository
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<ActorMovie> ActorMovies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Title).HasMaxLength(100).IsRequired();
                entity.Property(m => m.Description).HasMaxLength(1000);
                entity.Property(m => m.GenreId).HasDefaultValue(1);
                entity.HasMany(m => m.Comments).WithOne(m => m.Movie);
                entity.HasMany(m => m.ActorMovies).WithOne(m => m.Movie);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Text).HasMaxLength(2000).IsRequired();
                entity.HasOne(c => c.Movie).WithMany(c => c.Comments).IsRequired();
            });

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).HasMaxLength(30).IsRequired();
                entity.Property(a => a.Surname).HasMaxLength(30).IsRequired();
                entity.HasMany(a => a.ActorMovies).WithOne(a => a.Actor);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Value).HasMaxLength(30).IsRequired();
            });

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
