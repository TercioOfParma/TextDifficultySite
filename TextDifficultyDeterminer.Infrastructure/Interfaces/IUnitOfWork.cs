using Microsoft.EntityFrameworkCore;

public interface IUnitOfWork
{
    public DbSet<Language> Languages {get; set;}
}