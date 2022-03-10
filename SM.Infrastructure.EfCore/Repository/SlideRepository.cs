using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository {
    public class SlideRepository: RepositoryBase<long, Slide>, ISlideRepository {
        private readonly ShopContext _context;
        public SlideRepository (ShopContext context) : base(context) {
            _context = context;
        }

        public EditSlide? GetDetails (long id) {
            return _context.Slides.Select(x => new EditSlide {
                BtnText = x.BtnText,
                Heading = x.Heading,
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text,
                Title = x.Title,
                Link = x.Link
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}
