using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework;

public class TodoContext:DbContext
{
    public TodoContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Todo> Todos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(builder =>
        {
            builder.ToTable("items");
            builder.HasKey(todo => todo.Id);
            builder.Property(todo => todo.Id).HasColumnName("id");
            builder.Property(todo => todo.Title).HasColumnName("title");
            builder.Property(todo => todo.IsDone).HasColumnName("is_done");
        });
    }
}