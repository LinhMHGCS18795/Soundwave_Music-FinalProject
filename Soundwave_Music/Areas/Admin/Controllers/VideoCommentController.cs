using PagedList;
using Soundwave_Music.Common.Helper;
using Soundwave_Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Soundwave_Music.Areas.Admin.Controllers
{
    public class VideoCommentController : BaseController
    {
        SoundwaveDbContext _appdb = new SoundwaveDbContext();

        //Video index
        public ActionResult VideoCommentIndex(int? _sizepage, int? _pagenumber)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Create() == true || User.Identity.Permiss_Update() == true || User.Identity.Permiss_Edit() == true || User.Identity.Permiss_Delete() == true || User.Identity.Permiss_Access() == true)
                {
                    var pageSize = (_sizepage ?? 10);
                    var pageNum = (_pagenumber ?? 1);

                    var list_videocomment = from s in _appdb.Video_Comment
                                           orderby s.Video_comment_id descending
                                           select s;

                    return View(list_videocomment.ToPagedList(pageNum, pageSize));
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

        // Delete Video
        public ActionResult VideoCommentDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video_Comment videocomment = _appdb.Video_Comment.Find(id);
            if (videocomment == null)
            {
                return HttpNotFound();
            }
            return View(videocomment);
        }

        [HttpPost, ActionName("VideoCommentDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video_Comment videocomment = _appdb.Video_Comment.Find(id);
            _appdb.Video_Comment.Remove(videocomment);
            _appdb.SaveChanges();
            return RedirectToAction("VideoCommentIndex");
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