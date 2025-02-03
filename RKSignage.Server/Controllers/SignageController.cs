using Microsoft.AspNetCore.Mvc;
using RKSignage.Server;
using RKSignage.Server.Models;

namespace RKSignage.Server.Controllers
{
    [ApiController]
    public class SignageController : Controller
    {
        private readonly ILogger<SignageController> _logger;

        public SignageController(ILogger<SignageController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        [Route("GetSlides")]
        public IEnumerable<Slide> GetSlidesData(string playListName)
        {
            List<Slide> slides = new List<Slide>();

            slides = DBManager.ViewTable(playListName);

            return slides.ToArray();
        }

        [HttpGet()]
        [Route("GetPlayLists")]
        public IEnumerable<Playlist> GetPlayListsData()
        {
            List<Playlist> playLists = new List<Playlist>();

            playLists = DBManager.ViewTables();

            return playLists.ToArray();
        }
    }
}
