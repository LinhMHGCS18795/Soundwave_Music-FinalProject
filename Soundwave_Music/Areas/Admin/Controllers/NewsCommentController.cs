using PagedList;
using Soundwave_Music.Common.Helper;
using Soundwave_Music.Common.NotificationLib;
using Soundwave_Music.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Soundwave_Music.Areas.Admin.Controllers
{
    public class NewsCommentController : BaseController
    {
        SoundwaveDbContext _appdb = new SoundwaveDbContext();

        //News index
        public ActionResult NewsCommentIndex(int? _sizepage, int? _pagenumber)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Create() == true || User.Identity.Permiss_Update() == true || User.Identity.Permiss_Edit() == true || User.Identity.Permiss_Delete() == true || User.Identity.Permiss_Access() == true)
                {
                    var pageSize = (_sizepage ?? 10);
                    var pageNum = (_pagenumber ?? 1);

                    var list_newscomment = from s in _appdb.News_Comment
                                           orderby s.News_comment_id descending
                                           select s;

                    return View(list_newscomment.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                return Redirect("~/User/SignIn");
            }
        }

        // Delete News
        public ActionResult NewsCommentDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News_Comment newscomment = _appdb.News_Comment.Find(id);
            if (newscomment == null)
            {
                return HttpNotFound();
            }
            return View(newscomment);
        }

        [HttpPost, ActionName("NewsCommentDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News_Comment newscomment = _appdb.News_Comment.Find(id);
            _appdb.News_Comment.Remove(newscomment);
            _appdb.SaveChanges();
            return RedirectToAction("NewsCommentIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _appdb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}