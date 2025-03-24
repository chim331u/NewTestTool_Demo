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
        //Project
        modelBuilder.Entity<Projects>()
            .HasMany(e => e.TestSuites)
            .WithOne(e => e.Project)
            .HasForeignKey(e => e.ProjectId)
            .IsRequired();
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TestSuites>()
            .HasOne(e => e.Project)
            .WithMany(e => e.TestSuites)
            .HasForeignKey(e => e.ProjectId)
            .IsRequired();
        
        //Suite
        modelBuilder.Entity<TestSuites>()
            .HasMany(e => e.TestCases)
            .WithOne(e => e.TestSuite)
            .HasForeignKey(e => e.TestSuiteId)
            .IsRequired();
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TestCases>()
            .HasOne(e => e.TestSuite)
            .WithMany(e => e.TestCases)
            .HasForeignKey(e => e.TestSuiteId)
            .IsRequired();
        
        //Cases
        modelBuilder.Entity<TestCases>()
            .HasMany(e => e.TestScripts)
            .WithOne(e => e.TestCase)
            .HasForeignKey(e => e.TestCaseId)
            .IsRequired();
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TestScripts>()
            .HasOne(e => e.TestCase)
            .WithMany(e => e.TestScripts)
            .HasForeignKey(e => e.TestCaseId)
            .IsRequired();
        
        //Script
        
        //modelBuilder.Entity<FilesDetail>().ToTable("FilesDetail");

    }

    
}