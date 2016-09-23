using System.Web.Http;
using WebApiResource.Services;

namespace WebApiResource.Controllers
{
    public class HomeController : ApiController
    {
        private readonly IAlwaysTheSame _alwaysTheSame;
        private readonly ISameInARequest _sameInARequest;
        private readonly ISameInARequest _sameInARequest2;
        private readonly IAlwaysDifferent _alwaysDifferent;
        private readonly IAlwaysDifferent _alwaysDifferent2;

        public HomeController(
            IAlwaysTheSame alwaysTheSame,
            ISameInARequest sameInARequest,
            ISameInARequest sameInARequest2,
            IAlwaysDifferent alwaysDifferent,
            IAlwaysDifferent alwaysDifferent2
        )
        {
            _alwaysTheSame = alwaysTheSame;
            _sameInARequest = sameInARequest;
            _sameInARequest2 = sameInARequest2;
            _alwaysDifferent = alwaysDifferent;
            _alwaysDifferent2 = alwaysDifferent2;
        }

        public GetResponse Get()
        {
            var response = new GetResponse
            {
                AlwaysTheSame = _alwaysTheSame.GetHashCode(),
                SameInARequest = _sameInARequest.GetHashCode(),
                AlwaysDifferent = _alwaysDifferent.GetHashCode(),
                SameInARequest2 = _sameInARequest2.GetHashCode(),
                AlwaysDifferent2 = _alwaysDifferent2.GetHashCode(),
            };

            return response;
        }
    }

    public class GetResponse
    {
        public int AlwaysTheSame { get; set; }
        public int SameInARequest { get; set; }
        public int AlwaysDifferent { get; set; }
        public int SameInARequest2 { get; set; }
        public int AlwaysDifferent2 { get; set; }
    }
}