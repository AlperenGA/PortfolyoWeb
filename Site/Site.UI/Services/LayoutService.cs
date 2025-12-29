using Microsoft.EntityFrameworkCore;
using Site.UI.Models;
using ThreeLayerProject.Data;

namespace Site.UI.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LayoutViewModel> GetLayoutData()
        {
            var settings = await _context.SiteSettings.FirstOrDefaultAsync();
            var socials = await _context.SocialMedias
                                        .Where(x => x.IsActive)
                                        .OrderBy(x => x.Order)
                                        .ToListAsync();

            return new LayoutViewModel
            {
                Setting = settings,
                SocialMedias = socials
            };
        }
    }
}