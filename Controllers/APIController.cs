using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GZTimeServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GZTimeServer.Controllers
{
    [Route("api")]
    public class APIController : Controller
    {
        private ILevelProcessRepository _levelProcessRepository;
        private IAnimeProcessRepository _animeProcessRepository;
        private ICodeKeyRepository _codeKeyRepository;
        private IMazeProcessRepository _mazeProcessRepository;
        private ILiveLikeRepository _liveLikeRepository;
        private readonly ILogger<APIController> _logger;
        private readonly IConfiguration Configuration;

        public APIController(
            IConfiguration configuration,
            ILogger<APIController> logger,
            ILevelProcessRepository levelProcessRepository,
            IAnimeProcessRepository animeProcessRepository,
            ICodeKeyRepository codeKeyRepository,
            IMazeProcessRepository mazeProcessRepository,
            ILiveLikeRepository liveLikeRepository)
        {
            _levelProcessRepository = levelProcessRepository;
            _animeProcessRepository = animeProcessRepository;
            _codeKeyRepository = codeKeyRepository;
            _mazeProcessRepository = mazeProcessRepository;
            _liveLikeRepository = liveLikeRepository;
            _logger = logger;
            Configuration = configuration;
        }

        [HttpPost("updateprocess")]
        public IActionResult UpdateProcess(string code, string level)
        {
            
            LevelProcess process = new LevelProcess();
            process.Token = Request.Cookies["token"];
            process.LevelName = level;
            process.IP = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            process.UpdateTimeUTC = DateTime.Now.ToUniversalTime();
            if (!string.IsNullOrWhiteSpace(level) && Assets.LevelDic.ContainsKey(level))
                process.LevelID = Assets.LevelDic[level];
            else
                process.LevelID = -1;

            _logger.LogInformation("====TRY====> " + process.IP + " => [" + process.LevelID + "]" + process.LevelName );

            Assets.Status Result = Assets.Status.Fail;

            switch(level)
            {
                case "Anime":
                    if (_animeProcessRepository.IsSatisfyRequiredCount(process.Token))
                        Result = _levelProcessRepository.AddOrUpdateLevelProcess(process, "d77eb394992936c977ce3f9a112f4b48");
                    break;
                case "Code":
                    if (_codeKeyRepository.CheckCodeKey(process.Token, code))
                        Result = _levelProcessRepository.AddOrUpdateLevelProcess(process, "c13367945d5d4c91047b3b50234aa7ab");
                    break;
                case "Maze":
                    if (_mazeProcessRepository.IsArriveDestination(process.Token))
                        Result = _levelProcessRepository.AddOrUpdateLevelProcess(process, "d1f47e0b0089103812481452acb680f5");
                    break;
                case "Billionaire":
                    if(_liveLikeRepository.CheckTotalCount())
                        Result = _levelProcessRepository.AddOrUpdateLevelProcess(process, "0b9cb93dff91ce2d9e504a85e6fbac54");
                    break;
                default:
                    Result = _levelProcessRepository.AddOrUpdateLevelProcess(process, code);
                    break;
            }
            
            switch (Result)
            {
                case Assets.Status.Success:
                    _logger.LogInformation("==SUCCESS==> [" + process.Token.Substring(0, 6) + "]@"
                            + process.IP + " => [" + process.LevelID + "]" + process.LevelName);
                    return (Assets.LevelMsg.ContainsKey(process.LevelID) ?
                        new JsonResult(new
                        {
                            status = "Success",
                            process.Token,
                            msg = Assets.LevelMsg[process.LevelID],
                            t = _levelProcessRepository.GetQuantityOfUser(),
                            c = _levelProcessRepository.GetNumberOfUserInCurrentLevel(process.LevelID)
                        })
                        :
                        new JsonResult(new
                        {
                            status = "Success",
                            process.Token,
                            t = _levelProcessRepository.GetQuantityOfUser(),
                            c = _levelProcessRepository.GetNumberOfUserInCurrentLevel(process.LevelID)
                        }));
                case Assets.Status.Fail:
                    return new JsonResult(new { status = "Fail", process.Token});
                case Assets.Status.NotFound:
                    return new JsonResult(new { status = "NotFound", process.Token});
                default:
                    return new JsonResult(new { status = "Fail", process.Token });
            }
        }

        [HttpPost("gettoken")]
        public IActionResult GetToken()
        {
            string token = Assets.GenRandomHEX();
            HttpContext.Response.Cookies.Append("token", token,new CookieOptions
            {
                MaxAge = new TimeSpan(9999,1,1,1,1),
            });

            _logger.LogInformation("====NEW====> [" + token.Substring(0, 6) + "]@" 
                + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

            return new JsonResult(new { token });
        }

        [HttpPost("checkanime")]
        public IActionResult CheckAnime(int order,string info)
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"],3) &&
                _animeProcessRepository.UpdateAnimeProcess(Request.Cookies["token"], order, info))
            {
                _logger.LogInformation("===ANIME===> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => ["
                    + order + "]" + info + " SUCCESS.");

                return new JsonResult(new { status = "Success" });
            }
            return new JsonResult(new { status = "Fail" });
        }

        [HttpPost("maze/step")]
        public IActionResult MazeStep(string drc)
        {
            if (!_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 10) || 
                (drc != "n" && drc != "s" && drc != "w" && drc != "e")) 
                return new JsonResult(new { status = "Fail" });

            Maze.Direction direction = Maze.Direction.Unknown;
            switch (drc)
            {
                case "n": direction = Maze.Direction.UP; break;
                case "s": direction = Maze.Direction.DOWN; break;
                case "w": direction = Maze.Direction.LEFT; break;
                case "e": direction = Maze.Direction.RIGHT; break;
            }

            Maze.Edges edges = (Maze.Edges)_mazeProcessRepository.GetCurState(Request.Cookies["token"]).walls;

            if (direction.HasFlag(Maze.Direction.Unknown) || edges.HasFlag((Maze.Edges)(int)direction))
            {
                return new JsonResult(new { status = "Fail" });
            }

            int newedges = _mazeProcessRepository.Step(Request.Cookies["token"], direction);

            return new JsonResult(new { status = "Success", newedges });
        }

        [HttpPost("maze/reset")]
        public IActionResult ResetMaze()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 10))
            {
                int edges = _mazeProcessRepository.Reset(Request.Cookies["token"]);

                return new JsonResult(new { status = "Success", edges });
            }
            else
                return new JsonResult(new { status = "Fail" });
        }

        [HttpPost("live/uploadlikes")]
        public IActionResult UploadLikes(int append)
        {
            string token = Request.Cookies["token"];
            if (_levelProcessRepository.CheckAuthority(token, 8) &&
                DateTime.UtcNow - _liveLikeRepository.GetLastUploadTime(token) 
                    > TimeSpan.FromSeconds(0.5) &&
                    _liveLikeRepository.GetIsEnabled() &&
                append <= 10000000 && append >= -5000000)
            {
                _liveLikeRepository.UpdateUserCount(token, append);
                if(_liveLikeRepository.CheckTotalCount())
                {
                    _liveLikeRepository.LockTotal();
                    Task.Factory.StartNew(async () => {
                        await Task.Delay(new TimeSpan(0, 5, 0));
                        using (var context = new AppDbContext(Configuration.GetConnectionString("DefaultConnection")))
                        {
                            var repo = new LiveLikeRepository(context);
                            repo.ResetTotal();
                        }
                    }, TaskCreationOptions.LongRunning);
                }

                return new JsonResult(new { status = "Success"});
            }
            else
            {
                return new JsonResult(new { status = "Fail" });
            }
        }

        [HttpPost("live/getlikes")]
        public IActionResult GetLikeCount()
        {
            if (_levelProcessRepository.CheckAuthority(Request.Cookies["token"], 8))
                return new JsonResult(new { status = "Success", count = _liveLikeRepository.GetTotalCount() });
            return new JsonResult(new { status = "Fail" });
        }

        [HttpPost("time")]
        public IActionResult Time()
        {
            string Time = Request.Headers["If-Unmodified-Since"];
            try
            {
                if (Time != null && DateTime.Parse(Time) > new DateTime(2150, 1, 1))
                {
                    _logger.LogInformation("===TIME====> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                        + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => "
                        + Time + " SUCCESS.");
                    return new JsonResult(new
                    {
                        msg = "<p>Welcome!</p>\n" +
                            "<p>Please get your ticket from here:</p>\n" +
                            "<p style=\"font-size:25px;\">puzzle.gztime.cc/😜</p>\n" +
                            "<p style=\"font-size:25px;\">😜.play.gztime.cc</p>\n" +
                            "<p style=\"font-weight:bold;display:none;\">TXT</p>\n"
                    });
                }

                _logger.LogInformation("===TIME====> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => "
                    + Time + " FAIL.");

                return new JsonResult(new { msg = "<p>You are too young to access this level!</p>" });
            }
            catch
            {
                _logger.LogInformation("===TIME====> [" + Request.Cookies["token"].Substring(0, 6) + "]@"
                    + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + " => "
                    + Time + " ERROR.");
                return new JsonResult(new { msg = "<p>You are too young to access this level!</p>" });
            }
        }
    }
}
