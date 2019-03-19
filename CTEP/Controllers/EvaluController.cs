using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTEP.Models;

namespace CTEP.Controllers
{
    public class EvaluController : BaseController
    {

        //------------------------------------ !!!!



        /// <summary>
        /// 搜索待评价的课堂 POST: Evalu/SearchEvaluForms
        /// </summary>
        /// <param name="key">关键词 评价表的创建者码/评价表的课程名字/老师名</param>
        /// <returns>true查找成功| 但是没有值 false 查找失败 |查找出符合的 EvalutionFormsList</returns>
        [HttpPost]
        public ActionResult SearchEvaluForms(string key)
        {
            //查找准备
            IQueryable<EvalutionForms> _F = null;
            // 关键词去空字符
            key = key.Trim();
            //尝试匹配 评价表的创建者码


            //如果key参数为空 或者空字符串则返回查找成功
            if (string.IsNullOrEmpty(key) || key.Length < 1)
            {
                return Json(true);
            }

            try
            {
                //初始值为-1 用户ID不可能为负值
                int uFormkey = -1;
                //尝试转化为INT 
                if (int.TryParse(key, out uFormkey))
                {
                    //如果转化成功  Formkey 变化了   准备查找表
                    //根据创建者用户ID来查找表
                    _F = db.EvalutionForms.Where(x => x.CreateId == uFormkey) as IQueryable<EvalutionForms>;

                }
                else
                {
                    //转化为INT 失败 则尝试匹配评价表的课程 名字或老师名  是否隐藏评价表的发布 评价 0 false 1 true
                    _F = db.EvalutionForms.Where(x => (x.author.Contains(key) || x.title.Contains(key))&&x.FormStatus==0) as IQueryable<EvalutionForms>;
                }
            }
            catch (Exception)
            {
                return Json(false);
            }
            

            return Json(_F.ToList());
        }



        /// <summary>
        /// post assessment 提交评论
        /// </summary>
        /// <param name="assessment">评价对象</param>
        /// <returns>ex 错误信息 ，true 评价提交成功 </returns>
        // POST: Evalu/PutAssessment
        [HttpPost]
        public ActionResult PutAssessment([Bind(Include = "id,EvaTabId,CourseId,AllScore,suggest,TeachObj,TeachMethod,TeachAbility,TeachAttitude,StudentRelation,BandiId")] Assessment assessment)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //准备查找评价表 为修改评价表中分值做准备
                    //准备查找课程模板表 为修课程模板表中分值做准备
                    IQueryable<EvalutionForms> _ef = null;
                    IQueryable<CourseTemplates> _ct = null;
                    //查看是否有记录 用户是都已经评价过此评价表
                    IQueryable<Assessment> _al = db.Assessment.Where(x => x.EvaTabId == assessment.EvaTabId && x.id == assessment.id) as IQueryable<Assessment>;
                    if (_al.Count() > 0)
                    {//如果记录中有匹配的记录 1>0的  那就修改
                        db.Entry(assessment).State = EntityState.Modified;
                    }
                    else
                    {//如果记录中没有匹配的记录 0!>0的 
                        db.Assessment.Add(assessment);
                    }

                    //修改评价表中的分值
                    //查找评价表
                    _ef = db.EvalutionForms.Where(x => x.id == assessment.EvaTabId).Take(1) as IQueryable<EvalutionForms>;
                    //取出用户评价的 评价表对象
                    EvalutionForms ef = _ef.FirstOrDefault();
                    //修改分数
                    //查找出多少人提交了对ed 此对象 评价表的 评价
                    IQueryable<Assessment> _ae = db.Assessment.Where(x => x.EvaTabId == ef.id) as IQueryable<Assessment>;
                    //记录人数
                    int eNum = _ae.Count();
                    //遍历所有评价分数给评价表
                    foreach (Assessment a in _ae.ToList())
                    {
                        ef.score += a.AllScore;
                    }
                    //取平均
                    ef.score = ef.score / eNum;

                    _ct = db.CommentTemplates.Where(x => x.id == assessment.CourseId).Take(1) as IQueryable<CourseTemplates>;
                    //取出用户评价的 课程模板表对象
                    CourseTemplates ct = _ct.FirstOrDefault();

                    //查找出多少人提交了对ed 此对象 评价表的 评价
                    IQueryable<Assessment> _ac = db.Assessment.Where(x => x.CourseId == ct.id) as IQueryable<Assessment>;
                    //记录人数
                    int cNum = _ac.Count();
                    foreach (Assessment a in _ac.ToList())
                    {
                        ct.CourseScore += a.AllScore;
                    }
                    //取平均
                    ct.CourseScore = ct.CourseScore / cNum;

                    //修改评价表分数
                    db.Entry(ef).State = EntityState.Modified;
                    //修改课程模板表分数
                    db.Entry(ct).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
            catch (Exception ex )
            {
                return Json(ex.ToString());
            }
            

            return Json(true);
        }



        [HttpPost]
        public JsonResult Assessments(int? id) {

            List<JsonResult> dbs = new List<JsonResult>();
            IQueryable<EvalutionForms> _el = null;

           IQueryable <Assessment> _al = db.Assessment.Where(x => x.id == id);
            foreach (Assessment a in _al.ToList())
            {
                _el = db.EvalutionForms.Where(x => x.id == a.EvaTabId);
                dbs.Add(Json(a));
                dbs.Add(Json(_el.FirstOrDefault()));
            }
            return Json(dbs) ;
        }
            












        //------------------------------------ !!!!


    }
}
