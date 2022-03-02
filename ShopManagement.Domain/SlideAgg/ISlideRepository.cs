using _0_Framework.Domain;
using ShopManagement.Application.Contract.Slide;

namespace ShopManagement.Domain.SlideAgg {
    public interface ISlideRepository : IRepository<long , Slide> {
        EditSlide? GetDetails(long id);

    }
}
