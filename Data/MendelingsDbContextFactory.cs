using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Mendelings.Data;

//класс фабрика для объективного создания БД
public class MendelingsDbContextFactory
    : IDesignTimeDbContextFactory<MendelingsDbContext>
{
    public MendelingsDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString =
            configuration.GetConnectionString("MendelingsDb");

        var optionsBuilder =
            new DbContextOptionsBuilder<MendelingsDbContext>();

        optionsBuilder.UseSqlServer(connectionString);

        return new MendelingsDbContext(optionsBuilder.Options);
    }
}