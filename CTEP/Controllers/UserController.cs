using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTEP.Models;
namespace CTEP.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Login([Bind(Include = "id,email,pw,role")] Users user)
        {
            Users u = new Users() { id = -1,email="",pw="",role=-1 };
            IQueryable<Users> _users = db.Users.Where(x => x.id == user.id && x.pw == user.pw && x.role ==user.role&&x.status==user.status ).Take(1) as IQueryable<Users>;
            return Json(u);
        }
    }
}