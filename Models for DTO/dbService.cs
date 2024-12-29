using Ancient_Coin_Generater.Data;
using Microsoft.EntityFrameworkCore;

namespace Ancient_Coin_Generater.Models_for_DTO
{
    public class dbService
    {
        private readonly IDbContextFactory<ApplicationDBContext> _dbContextFactory;

        public dbService(IDbContextFactory<ApplicationDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<ImageRecord>> GetAllImages()
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                return await _dbContext.ImageRecords.ToListAsync();
            }

        }
        public async Task SaveImageFromUrl(byte[] imageBytes)
        {
            
            var imageRecord = new ImageRecord
            {
                ImageData = imageBytes
            };
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                _dbContext.ImageRecords.Add(imageRecord);

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
