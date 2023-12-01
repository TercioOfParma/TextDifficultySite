using Microsoft.EntityFrameworkCore;

public interface IUnitOfWork
{
    public DbSet<Language> Languages {get; set;}
    public DbSet<FrequencyWord> Words {get; set;}

    public Task<int> SaveChangesAsync();
}