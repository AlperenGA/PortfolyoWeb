using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ThreeLayerProject.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Burada SQLite bağlantı cümleni belirt
            optionsBuilder.UseSqlite("Data Source=ThreeLayerProject.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
