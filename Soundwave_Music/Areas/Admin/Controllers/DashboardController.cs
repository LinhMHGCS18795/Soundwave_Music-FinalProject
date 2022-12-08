using Soundwave_Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Soundwave_Music.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        SoundwaveDbContext _appdb = new SoundwaveDbContext();
        public ActionResult Index()
        {
            //count active users use Soundwave Music
            ViewBag.countuser = _appdb.Users.Where(u => u.User_Status == "1").Count();
            //count active song
            ViewBag.countsong = _appdb.Songs.Where(s => s.Song_status == "1").Count();
            //count active album
            ViewBag.countalbum = _appdb.Albums.Where(a => a.Album_status == "1").Count();
            //Count active music video
            ViewBag.countmv = _appdb.Videos.Where(v => v.Video_status == "1").Count();
            //Count active news
            ViewBag.countnews = _appdb.News.Where(n => n.Status == "1").Count();
            //Count active composer
            ViewBag.countcomposer = _appdb.Composers.Where(c => c.Composer_status == "1").Count();
            //Count active singer
            ViewBag.countsinger = _appdb.Singers.Where(s => s.Singer_status == "1").Count();
            //Count all comment song
            ViewBag.countcommentsong = _appdb.Song_Comment.Count();
            //Count all comment album
            ViewBag.countcommentalbum = _appdb.Album_Comment.Count();
            //Count all comment music video
            ViewBag.countcommentmv = _appdb.Video_Comment.Count();
            return View();
        }
    }
}