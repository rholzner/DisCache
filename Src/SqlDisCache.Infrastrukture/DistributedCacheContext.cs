using Microsoft.EntityFrameworkCore;
using SqlDisCache.Infrastrukture.Entity;

namespace PlayboxCache;

public partial class DistributedCacheContext : DbContext
{
    public DistributedCacheContext()
    {
    }

    public DistributedCacheContext(DbContextOptions<DistributedCacheContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TestCache> DistributedCache { get; set; }
    public virtual DbSet<CacheTag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=TestCache;Trusted_Connection=True;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestCache>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DistributedCache__3214EC078136E5C6");

            entity.ToTable("DistributedCache");

            entity.HasIndex(e => e.ExpiresAtTime, "Index_ExpiresAtTime");

            entity.Property(e => e.Id)
                .HasMaxLength(449)
                .UseCollation("SQL_Latin1_General_CP1_CS_AS");
            entity.Property(e => e.Value).IsRequired();

            entity.HasMany(x => x.Tags).WithMany(x => x.CachItems);

        });

        modelBuilder.Entity<CacheTag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(x => x.CachItems).WithMany(x => x.Tags);

        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
