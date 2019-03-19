using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTEP.Models;
namespace CTEP.Controllers
{
    public class BandisSchoolController : BaseController
    {




        // POST: BandisSchool/School
        [HttpPost]
        public ActionResult School(string uName)
        {

            //如果参数为空 或者空字符串则返回 所有列表学校
            if (string.IsNullOrEmpty(uName) || uName.Trim().Length < 1)
            {
                return Json(db.University.ToList());
            }
            //如果不为空则 模糊查询
            IQueryable<University> _un = db.University.Where(x => x.name.Contains(uName.Trim())) as IQueryable<University>;

            return Json(_un.ToList());
        }





        // POST: BandisSchool/Academy
        [HttpPost]
        public ActionResult Academy(string uName, int? uId)
        {
            //查找准备
            IQueryable<Academys> _un = null;
            //如果参数为空 或者空字符串则返回 学校所对应的所有学院列表
            if (string.IsNullOrEmpty(uName) || uName.Trim().Length < 1)
            {
                return Json(db.Academys.Where(x => x.upid == uId) as IQueryable<Academys>);
            }

            //如果不为空则 模糊查询
            _un = db.Academys.Where(x => x.name.Contains(uName.Trim()) && x.upid == uId) as IQueryable<Academys>;
            return Json(_un.ToList());
        }


        // POST: BandisSchool/Grad
        [HttpPost]
        public ActionResult Grad(string uName, int? uId)
        {
            //查找准备
            IQueryable<Grads> _un = null;
            //如果参数为空 或者空字符串则返回 学校所对应的所有年级列表
            if (string.IsNullOrEmpty(uName) || uName.Trim().Length < 1)
            {
                return Json(db.Grads.Where(x => x.upid == uId) as IQueryable<Grads>);
            }

            //如果不为空则 模糊查询
            _un = db.Grads.Where(x => x.name.Contains(uName.Trim()) && x.upid == uId) as IQueryable<Grads>;

            return Json(_un.ToList());
        }

        //获取班级
        // POST: BandisSchool/ClassNum
        [HttpPost]
        public ActionResult ClassNum(string uName, int? uId)
        {
            //查找准备
            IQueryable<ClassNum> _un = null;
            //如果参数为空 或者空字符串则返回 年级所对应的所有班级列表
            if (string.IsNullOrEmpty(uName) || uName.Trim().Length < 1)
            {
                return Json(db.ClassNum.Where(x => x.upid == uId) as IQueryable<ClassNum>);
            }

            //如果不为空则 模糊查询
            _un = db.ClassNum.Where(x => x.name.Contains(uName.Trim()) && x.upid == uId) as IQueryable<ClassNum>;

            return Json(_un.ToList());
        }

        //[HttpPost]
        //public ActionResult BandsiSchool(int? uId)
        //{
        //    //查找准备
        //    IQueryable<Grads> _un = null;

        //    //如果参数为空 或者空字符串则返回 学校所对应的所有学院列表
        //    if (string.IsNullOrEmpty(uName) || uName.Trim().Length < 1)
        //    {
        //        return Json(db.Grads.Where(x => x.upid == uId) as IQueryable<Grads>);
        //    }

        //    //如果不为空则 模糊查询
        //    _un = db.Grads.Where(x => x.name.Contains(uName.Trim()) && x.upid == uId) as IQueryable<Grads>;

        //    return Json(_un.ToList());
        //}


        // POST: SchoolBandisTabs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        /// <summary>
        /// 绑定学校信息
        /// </summary>
        /// <param name="sB"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SetBandsiSchool0([Bind(Include = "id,UniversityName,AcademyName,GradeNum,ClassNum")] SchoolBandisTabs sB)
        {
            //查找准备
            IQueryable<SchoolBandisTabs> _un = null;
            _un = db.SchoolBandisTabs.Where(x => x.UniversityName == sB.UniversityName && x.AcademyName == sB.AcademyName && x.GradeNum == sB.GradeNum && x.ClassNum == sB.ClassNum).Take(1) as IQueryable<SchoolBandisTabs>;

            try
            {
                //如果此绑定值存在 
                if (_un.Count() > 0)
                {

                    // sbt 记录下查找出的课堂绑定值
                    SchoolBandisTabs sbt = _un.FirstOrDefault();
                    //此时为用户写入绑定表 post 过来的 SchoolBandisTabs sB 的ID 为 用户Id 
                    db.BandTabs.Add(new BandTabs() { type = 1, BandiId = sbt.id, UserId = sB.id });
                    db.SaveChanges();
                }
                //如果此绑定值不存在 
                else
                {
                    //创建绑定值
                    db.SchoolBandisTabs.Add(new SchoolBandisTabs() { UniversityName = sB.UniversityName, AcademyName = sB.AcademyName, GradeNum = sB.GradeNum, ClassNum = sB.ClassNum });
                    db.SaveChanges();

                    var Controller = DependencyResolver.Current.GetService<BandisSchoolController>();
                    var result = Controller.SetBandsiSchool0(sB);
                    return result;

                }
                
            }
            catch (Exception)
            {

                return Json(false);
            }
            return Json(true);
        }


        // POST: SchoolBandisTabs/PutBandsiSchool
        /// <summary>
        /// 此方法用于添加学校绑定使用 
        /// </summary>
        /// <param name="schoolBandisTabs"></param>
        /// <returns></returns>
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PutBandsiSchool([Bind(Include = "id,UniversityName,AcademyName,GradeNum,ClassNum")] SchoolBandisTabs schoolBandisTabs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.SchoolBandisTabs.Add(schoolBandisTabs);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return Json(false);
            }

            return Json(true);
        }






    }
}
