using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GZTimeServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GZTimeServer.Controllers
{
    public class DataViewController : Controller
    {
        private ILevelProcessRepository _levelProcessRepository;
        private ILiveLikeRepository _liveLikeRepository;

        public DataViewController(ILevelProcessRepository levelProcessRepository,
            ILiveLikeRepository liveLikeRepository)
        {
            _levelProcessRepository = levelProcessRepository;
            _liveLikeRepository = liveLikeRepository;
        }

        [Route("livelikes")]
        public IActionResult LikesData()
        {
            var list = from likes in _liveLikeRepository.GetLiveLikes()
                       orderby likes.Count descending
                       select likes;

            ViewBag.down = new List<LiveLike>();
            ViewBag.up = new List<LiveLike>();

            int count = 0;
            foreach(var item in list)
            {
                if (item.Token == "[Total]")
                    continue;
                if (++count > 10)
                    break;
                ViewBag.up.Add(item);
            }

            list = from likes in _liveLikeRepository.GetLiveLikes()
                   orderby likes.DCount
                   select likes;

            count = 0;
            foreach (var item in list)
            {
                if (item.Token == "[Total]")
                    continue;
                if (++count > 10)
                    break;
                ViewBag.down.Add(item);
            }

            return View();
        }

        [Route("Leveldata")]
        public IActionResult LevelData()
        {

            ViewBag.Counts = new List<int>();
            ViewBag.Levels = new Dictionary<int, string>
            {
                { 0, "Start" },     // Done
                { 1, "A Letter"},   // Done
                { 2, "Memory"},     // Done
                { 3, "Anime"},     // Done
                { 4, "Math"},      // Done
                { 5, "Fans"},      // Done
                { 6, "Excel"},     // Done
                { 7, "Code"},      // Done
                { 8, "Billionaire"}, // Done
                { 9, "Trees"},     // Done
                { 10, "Emoji"},     // Done
                { 11, "Maze"},     // Done
                { 12, "NO.12"},    // Done
                { 13, "Ending?"},  // Done
                { 14, "Ending."}    // Done
            };
            for (int i = 0; i < 15; i++)
                ViewBag.Counts.Add(_levelProcessRepository.GetNumberOfUserInCurrentLevel(i));
            ViewBag.TotalCount = _levelProcessRepository.GetQuantityOfUser();
            return View();
        }
    }
}
