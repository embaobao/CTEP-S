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
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="user">绑定用户对象</param>
        /// <returns>用户对象 id小于0 则登录失败</returns>
        [HttpPost]
        public ActionResult Login([Bind(Include = "id,email,pw,role,status")] Users user)
        {


            Users u = new Users() { id = -1, email = "", pw = "", role = 0, status = 0 };
            IQueryable<Users> _users = db.Users.Where(x => x.email == user.email && x.pw == user.pw && x.role == user.role && x.status == 1).Take(1) as IQueryable<Users>;

            if (_users.Count() > 0)
            {
                Users uu = _users.FirstOrDefault();
                u = uu;
            }

            //user.email = user.email + DateTime.Now;
            return Json(u);
        }
        [HttpPost]
        public ActionResult LoadInfo([Bind(Include = "id,email,pw,role,status")] Users user)
        {

            IQueryable<Users> _users = db.Users.Where(x => x.email == user.email && x.pw == user.pw && x.role == user.role && x.status == 1).Take(1) as IQueryable<Users>;
            UserInfo ui = null;
            if (_users.Count() > 0)
            {
                Users uu = _users.FirstOrDefault();
                ui = new UserInfo() { id = uu.id, name = uu.email };
                IQueryable<UserInfo> _ui = db.UserInfo.Where(x => x.id == uu.id).Take(1) as IQueryable<UserInfo>;
                if (_ui.Count() > 0)
                {
                    ui = _ui.FirstOrDefault();
                }

            }

            return Json(ui);
        }

        [HttpPost]
        public ActionResult Set([Bind(Include = "id,email,pw,role,status")] Users user)
        {

            IQueryable<Users> _users = db.Users.Where(x => x.email == user.email && x.pw == user.pw && x.role == user.role && x.status == 1).Take(1) as IQueryable<Users>;
            UserSetTabs us = null;
            if (_users.Count() > 0)
            {
                Users uu = _users.FirstOrDefault();
                us = new UserSetTabs() { };
                IQueryable<UserSetTabs> _ui = db.UserSetTabs.Where(x => x.id == uu.id).Take(1) as IQueryable<UserSetTabs>;
                if (_ui.Count() > 0)
                {
                    us = _ui.FirstOrDefault();
                }

            }

            return Json(us);
        }

        [HttpPost]
        public ActionResult Account([Bind(Include = "id,email,pw,role,status")] Users user)
        {

            IQueryable<Users> _users = db.Users.Where(x => x.email == user.email && x.pw == user.pw && x.role == user.role && x.status == 1).Take(1) as IQueryable<Users>;
            AccountInfo us = null;
            if (_users.Count() > 0)
            {
                Users uu = _users.FirstOrDefault();
                us = new AccountInfo() { };
                IQueryable<AccountInfo> _ui = db.AccountInfo.Where(x => x.id == uu.id).Take(1) as IQueryable<AccountInfo>;
                if (_ui.Count() > 0)
                {
                    us = _ui.FirstOrDefault();
                }

            }

            return Json(us);
        }

        [HttpPost]
        public ActionResult EvaluForm0([Bind(Include = "id,email,pw,role,status")] Users user)
        {
            //验证用户
            IQueryable<Users> _users = db.Users.Where(x => x.email == user.email && x.pw == user.pw && x.role == user.role && x.status == 1).Take(1) as IQueryable<Users>;
            //存储绑定表
            List<BandTabs> us = null;
            //存储评价表
            List<EvalutionForms> ef = new List<EvalutionForms>();
            //如果用户验证通过
            if (_users.Count() > 0)
            {
                //当前提交的用户
                Users uu = _users.FirstOrDefault();
                us = new List<BandTabs>();
                //查出用户所有绑定信息
                IQueryable<BandTabs> _ui = db.BandTabs.Where(x => x.UserId == uu.id) as IQueryable<BandTabs>;
                //如果用户有绑定信息
                if (_ui.Count() > 0)
                {
                    //建立查找评价表准备
                    IQueryable<EvalutionForms> _ef = null;

                    us = _ui.ToList();
                    //遍历绑定表查找评价
                    foreach (BandTabs band in us)
                    {
                        //如果绑定类型为 课程 即 评价表ID 绑定？
                        if (band.type == 0)
                        {
                            // 绑定的评价表
                            _ef = db.EvalutionForms.Where(x => x.id == band.BandiId) as IQueryable<EvalutionForms>;
                            //查找到的绑定表添加到表列表中
                            ef.Add(_ef.FirstOrDefault());
                        }
                        //如果不是ID绑定 是其他 就是班级绑定 那么就搜素评价表 中绑定值
                        else {
                            //否则 则搜索评价表中 绑定值与用户绑定的 课程
                            _ef = db.EvalutionForms.Where(x => x.BandiId == band.BandiId) as IQueryable<EvalutionForms>;
                            //遍历根据绑定值查出的评价表 添加到表列表中
                            foreach (EvalutionForms form in _ef.ToList())
                            {
                                ef.Add(form);
                            }
                        }
                        
                    }
                }

            }





            return Json(ef);
        }

        //[HttpPost]
        //public ActionResult EvalutonForms(int? id)
        //{
        //    List<EvalutionForms> result = null;
        //    IQueryable<EvalutionForms> _ui = db.EvalutionForms.Where(x => x.id == id) as IQueryable<EvalutionForms>;
        //    return Json(result);
        //}





    }
}