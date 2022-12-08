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
    public class AlbumCommentController : BaseController
    {
        SoundwaveDbContext _appdb = new SoundwaveDbContext();

        //Album index
        public ActionResult AlbumCommentIndex(int? _sizepage, int? _pagenumber)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Create() == true || User.Identity.Permiss_Update() == true || User.Identity.Permiss_Edit() == true || User.Identity.Permiss_Delete() == true || User.Identity.Permiss_Access() == true)
                {
                    var pageSize = (_sizepage ?? 10);
                    var pageNum = (_pagenumber ?? 1);

                    var list_albumcomment = from s in _appdb.Album_Comment
                                           orderby s.Album_comment_id descending
                                           select s;

                    return View(list_albumcomment.ToPagedList(pageNum, pageSize));
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

        // Delete Album
        public ActionResult AlbumCommentDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album_Comment albumcomment = _appdb.Album_Comment.Find(id);
            if (albumcomment == null)
            {
                return HttpNotFound();
            }
            return View(albumcomment);
        }

        [HttpPost, ActionName("AlbumCommentDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album_Comment albumcomment = _appdb.Album_Comment.Find(id);
            _appdb.Album_Comment.Remove(albumcomment);
            _appdb.SaveChanges();
            return RedirectToAction("AlbumCommentIndex");
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
