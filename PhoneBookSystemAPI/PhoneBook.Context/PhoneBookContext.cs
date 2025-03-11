using Microsoft.EntityFrameworkCore;
using PhoneBook.Model;

namespace PhoneBook.Context;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public PhoneBookContext(DbContextOptions<PhoneBookContext> dbContext) : base(dbContext)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id); // Primary Key

            entity.Property(e => e.Id)
            .UseIdentityAlwaysColumn();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(40);

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(40);

            // This Prop Has Non Clustered Index For InCrease Searching Performance
            entity.HasIndex(e => e.Name)
                .HasDatabaseName("IX_Contact_Name_GIN")
                .HasMethod("gin")
                .HasOperators("gin_trgm_ops");

            // This Prop Has Non Clustered Index For InCrease Searching Performance
            entity.HasIndex(e => e.PhoneNumber)
                .HasDatabaseName("IX_Contact_Phone_GIN")
                .HasMethod("gin")
                .HasOperators("gin_trgm_ops");

            // This Prop Has Non Clustered Index For InCrease Searching Performance
            entity.HasIndex(e => e.Email)
                .HasDatabaseName("IX_Contact_Email_GIN")
                .HasMethod("gin")
                .HasOperators("gin_trgm_ops");
        });




        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        // set value for base entity data 
        var entities = ChangeTracker.Entries<Contact>();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
