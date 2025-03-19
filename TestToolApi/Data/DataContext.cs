using DataModel;
using Microsoft.EntityFrameworkCore;

namespace TestToolApi.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Projects> Projects { get; set; }
    public DbSet<TestCases> TestCases { get; set; }

    public DbSet<TestScripts> TestScripts { get; set; }
        
    public DbSet<TestSuites> TestSuites { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<FilesDetail>().ToTable("FilesDetail");

    }

    
}