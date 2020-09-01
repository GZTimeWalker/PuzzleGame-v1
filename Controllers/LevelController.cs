using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GZTimeServer.Models;

namespace GZTimeServer.Controllers
{
    public class LevelController : Controller
    {
        private ILevelProcessRepository _levelProcessRepository;
        private ICodeKeyRepository _codeKeyRepository;
        private IMazeProcessRepository _mazeProcessRepository;
        private IRankRepository _rankRepository;
        private readonly ILogger<LevelController> _logger;

        public LevelController(ILevelProcessRepository levelProcessRepository,
            IRankRepository rankRepository,
            ILogger<LevelController> logger,
            ICodeKeyRepository codeKeyRepository,
            IMazeProcessRepository mazeProcessRepository)
        {
            _levelProcessRepository = levelProcessRepository;
            _codeKeyRepository = codeKeyRepository;
            _mazeProcessRepository = mazeProcessRepository;
            _rankRepository = rankRepository;
            _logger = logger;
        }

        [Route("")]
        public IActionResult Start()
        {
            _logger.LogInformation("===LEVEL===> [######]@"
                + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Start] SUCCESS.");
            return View();
        }

        [Route("Letter")]
        public IActionResult Letter()
        {
            if(_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 1))
            {
                _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Letter] SUCCESS.");
                return View();
            }
            return new NotFoundResult();
        }

        [Route("Memory")]
        public IActionResult Memory()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 2))
            {
                _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Memory] SUCCESS.");
                return View();
            }
            return new NotFoundResult();
        }

        [Route("Anime")]
        public IActionResult Anime()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 3))
            {
                _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Anime] SUCCESS.");
                ViewData["is_iPad"] = Request.Headers["User-Agent"].ToString().Contains("iPad") ||
                    Request.Headers["User-Agent"].ToString().Contains("iPhone") ||
                    Request.Headers["User-Agent"].ToString().Contains("Mac OS");
                return View();
            }
            return new NotFoundResult();
        }

        [Route("Math")]
        public IActionResult Math()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 4))
            {
                _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Math] SUCCESS.");
                return View();
            }
            return new NotFoundResult();
        }

        [Route("Unknown/{flag?}")]
        public IActionResult Unknown(string flag)
        {
            switch (flag)
            {
                case "friends":
                    if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 5))
                    {
                        _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                            + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Fans] SUCCESS.");
                        return View("Fans");
                    }
                    return new NotFoundResult();
                case "what":
                    if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 6))
                    {
                        _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                            + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Excel] SUCCESS.");
                        return View("Excel");
                    }
                    return new NotFoundResult();
                case "next":
                    if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 7))
                    {
                        string token = Request.Cookies["token"];
                        string key = Assets.GenOriginalCodeKey(token);
                        string userstr = "Your Key is: [" + key + "]";
                        string codedstr = Assets.GenCodedString(userstr);
                        _codeKeyRepository.SaveCodeKey(token, Coding.MD5(key));
                        ViewData["code"] = codedstr;
                        _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                            + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Code] SUCCESS.");
                        return View("Code");
                    }
                    return new NotFoundResult();
                default:
                    return new NotFoundResult();
            }
        }

        [Route("Youth/Dingtalk")]
        public IActionResult Billionaire()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 8))
            {
                _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Billionaire] SUCCESS.");
                ViewData["Count"] = _levelProcessRepository.GetQuantityOfUser();
                return View();
            }
            return new NotFoundResult();
        }

        [Route("Trees")]
        public IActionResult Trees()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 9))
            {
                _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Trees] SUCCESS.");
                return View();
            }
            return new NotFoundResult();
        }

        [Route("Through/Time")]
        public IActionResult Time()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 10))
            {
                _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Time] SUCCESS.");
                return View();
            }
            return new NotFoundResult();
        }

        [Route("😜")]
        public IActionResult Emoji()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 10))
            {
                _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Emoji] SUCCESS.");
                return View();
            }
            return new NotFoundResult();
        }

        [Route("Maze")]
        public IActionResult Maze()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 11))
            {
                _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Maze] SUCCESS.");
                ViewBag.state = _mazeProcessRepository.GetCurState(Request.Cookies["token"]);
                return View();
            }
            return new NotFoundResult();
        }

        [Route("NO12")]
        public IActionResult NO12()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 12))
            {
                _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#NO.12] SUCCESS.");
                return View();
            }
            return new NotFoundResult();
        }

        [Route("Zubeneschamali")]
        public IActionResult FakeEnding([FromQuery]string k)
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 13)
                && string.Equals(k,"a803c4-b1c!2f1100"))
            {
                _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#Fake END] SUCCESS.");
                return View();
            }
            return new NotFoundResult();
        }

        [Route("GZTime")]
        public IActionResult Ending()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 14))
            {
                LevelProcess pro = _levelProcessRepository.GetLevelProcessByToken(Request.Cookies["token"]);
                if (pro.LevelID >= 14)
                {
                    _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                        + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#END] SUCCESS.");

                    ViewBag.Token = Request.Cookies["token"];
                    ViewBag.Count = _levelProcessRepository.GetQuantityOfUser();
                    int rank = _rankRepository.GetRankByToken(Request.Cookies["token"]);
                    ViewBag.WinCount = rank == -1 ? _levelProcessRepository.GetNumberOfUserInCurrentLevel(14) : rank;

                    return View();
                }
                else if(pro.LevelID == 13)
                {
                    LevelProcess process = new LevelProcess();
                    process.Token = Request.Cookies["token"];
                    process.LevelName = "Ending.";
                    process.IP = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    process.UpdateTimeUTC = DateTime.Now.ToUniversalTime();
                    process.LevelID = 14;
                    _levelProcessRepository.AddOrUpdateLevelProcess(process, "f342f928be56c2ab3e6d63e3eff9d677");

                    _rankRepository.AddPlayer(Request.Cookies["token"], HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

                    ViewBag.Token = Request.Cookies["token"];
                    ViewBag.Count = _levelProcessRepository.GetQuantityOfUser();
                    ViewBag.WinCount = _levelProcessRepository.GetNumberOfUserInCurrentLevel(14);

                    _logger.LogInformation("===LEVEL===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                        + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => [LEVEL#END] SUCCESS. \n"
                        + "\t\t\t\t\t\t\t\t ========> Pos: ["
                        + _levelProcessRepository.GetNumberOfUserInCurrentLevel(14) + "/"
                        + _levelProcessRepository.GetQuantityOfUser() + "] <========");

                    return View();
                }
            }
            return new NotFoundResult();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
