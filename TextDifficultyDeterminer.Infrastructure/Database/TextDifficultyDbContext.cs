using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


public class TextDifficultyDbContext : DbContext, IUnitOfWork
{
    public DbSet<FrequencyWord> Words {get; set;}
    public DbSet<Language> Languages {get; set;}
    public string DbPath {get; set;} = "TextCorpus.db";

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}