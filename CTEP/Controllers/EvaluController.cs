using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMsListEP.Models;
using CTEP.Models;
using Newtonsoft.Json;

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
        public ActionResult SearchForms(string key)
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
                    _F = db.EvalutionForms.Where(x => (x.author.Contains(key) || x.title.Contains(key)) && x.FormStatus == 0) as IQueryable<EvalutionForms>;
                }
            }
            catch (Exception)
            {
                return Json(false);
            }


            return Json(_F.ToList());
        }





        /// <summary>
        /// 获取评价表
        /// </summary>
        /// <param name="id">评价表id</param>
        /// <returns>评价表Json对象</returns>
        [HttpPost]
        public ActionResult Form(int? id)
        {
            IQueryable<EvalutionForms> ListEvalutionForms = db.EvalutionForms.Where(x => x.id == id) as IQueryable<EvalutionForms>;
            EvalutionForms ef = ListEvalutionForms.FirstOrDefault();
            return Json(ef);
        }




        /// <summary>
        /// 获取课程模板表
        /// </summary>
        /// <param name="id">评价表id</param>
        /// <returns>评价表Json对象</returns>
        [HttpPost]
        public ActionResult CourseTemp(int? id)
        { 
            return Json(db.CourseTemplates.Where(x=>x.id==id));
        }

        /// <summary>
        /// 获取评价表评论 
        /// </summary>
        /// <param name="id">评价表id</param>
        /// <returns>评论记录表Json对象</returns>
        [HttpPost]
        public ActionResult FormResult(int? id)
        {
            return Json(db.Assessment.Where(x => x.EvaTabId == id).ToList());
        }

        /// <summary>
        /// 获取评价表列表
        /// </summary>
        /// <param name="ids">评价表id数组</param>
        /// <returns>评价表列表Json对象</returns>
        [HttpPost]
        public ActionResult Forms(string ids)
        {
            List<EvalutionForms> efs = new List<EvalutionForms>();
            int[] Ids = ToObject<int[]>(ids);
            foreach (int id in Ids)
            {
                EvalutionForms eForm = ToObject<EvalutionForms>(ExecuteController<EvaluController>().Form(id).ToString());
                efs.Add(eForm);
            }
            return Json(efs);
        }


        [HttpPost]
        public ActionResult PutForm([Bind(Include = "id,CreateId,CourseId,author,score,FormStatus,title,body,img,CreateTime,EStartTime,EEndTime,Eweek,BandiId")] EvalutionForms eForms)
        {
            try
            {
                int FormsNum = EFormsNumForId(eForms.id);
                if (FormsNum > 0)
                {
                    db.Entry(eForms).State = EntityState.Modified;
                }
                else 
                {

                    db.EvalutionForms.Add(new EvalutionForms
                    {


                        CreateId = eForms.CreateId,
                        CourseId = eForms.CourseId,
                        author = eForms.author,
                        score = eForms.score,
                        FormStatus = eForms.FormStatus,
                        title = eForms.title,
                        body = eForms.body,
                        img = eForms.img,
                        CreateTime = eForms.CreateTime,
                        EStartTime = eForms.EStartTime,
                        EEndTime = eForms.EEndTime,
                        Eweek = eForms.Eweek,
                        BandiId = eForms.BandiId
                    });

                    if (CTNumForId(eForms.CourseId) < 0)
                    {
                        AddData<CourseTemplates>(new CourseTemplates
                        {
                            CreaterId = eForms.id,
                            body = eForms.body,
                            title = eForms.title,
                            img = eForms.img,
                            CourseScore = eForms.score,
                            CourseStatus = eForms.FormStatus
                        });
                    }

                }

                
            }
            catch (Exception)
            {

                return Json(false);
            }
           

            db.SaveChanges();

            return Json(true);

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
            // 测试队列
            //MsList ms = new MsList();

            //ms.Add(new MsItem("Post过来的数据", assessment)); // 测试完成，数据到达！

            try
            {

                if (ModelState.IsValid)
                {
                    //准备查找评价表 为修改评价表中分值做准备

                    IQueryable<EvalutionForms> _ef = null;
                    //准备查找课程模板表 为修课程模板表中分值做准备
                    IQueryable<CourseTemplates> _ct = null;

                    //1）查看是否有记录 用户是都已经评价过此评价表
                    IQueryable<Assessment> _al = db.Assessment.Where(x => x.EvaTabId == assessment.EvaTabId && x.id == assessment.id) as IQueryable<Assessment>;

                    if (_al.Count() > 0)
                    {//如果记录中有匹配的记录 1>0的  那就修改
                        db.Entry(assessment).State = EntityState.Modified;

                        //测试数据 有匹配的记录 评价表
                        //ms.Add(new MsItem("有匹配的记录评价表", assessment));
                    }
                    else
                    {//如果记录中没有匹配的记录 0!>0的 
                        db.Assessment.Add(assessment);
                        //测试数据 没有匹配的记录 评价表
                        //ms.Add(new MsItem("没有匹配的记录的记录评价表", assessment));
                    }



                    //修改评价表中的分值
                    //查找评价表
                    _ef = db.EvalutionForms.Where(x => x.id == assessment.EvaTabId).Take(1) as IQueryable<EvalutionForms>;


                    //取出用户评价的评价表对象
                    EvalutionForms ef = _ef.FirstOrDefault();

                    //测试数据查找评价表
                    //ms.Add(new MsItem("测试数据 查找的评价表", ef));

                    //执行操作 存入评价
                    db.SaveChanges();



                    //2） 修改分数  如果不能保存 若没有人评价 则没有评价记录就不用查找了 算分等。。

                    //查找出多少人提交了对ef 此评价表的 评价
                    IQueryable<Assessment> _ae = db.Assessment.Where(x => x.EvaTabId == ef.id) as IQueryable<Assessment>;
                    if (_ae.Count() > 0)
                    {
                        //记录人数
                        int eNum = _ae.Count();

                        //TSET: 记录人数
                        //ms.Add(new MsItem("TSET:提交了对ef 此评价表的人数", eNum));

                        //TSET: 所有给评价表评价的评价记录
                        //ms.Add(new MsItem("TSET:所有给评价表评价的评价记录", _ae.ToList()));
                        //清空从新计算
                        ef.score = 0;
                        //遍历 所有给评价表评价的评价记录
                        foreach (Assessment a in _ae.ToList())
                        {
                            ef.score += a.AllScore;
                        }


                        //取平均
                        ef.score = (ef.score / eNum);
                        //TSET: 取平均
                        //ms.Add(new MsItem("TSET: 评价表取平均", ef.score));
                    }




                    _ct = db.CourseTemplates.Where(x => x.id == assessment.CourseId).Take(1) as IQueryable<CourseTemplates>;
                    //取出用户评价的 课程模板表对象
                    CourseTemplates ct = _ct.FirstOrDefault();

                    //TSET: 取出课程模板表对象
                    //ms.Add(new MsItem("TSET:取出课程模板表对象", ct));

                    //查找出多少人提交了对ed 此对象 评价表的 评价
                    IQueryable<Assessment> _ac = db.Assessment.Where(x => x.CourseId == ct.id) as IQueryable<Assessment>;
                    //记录人数
                    if (_ct.Count() > 0)
                    {
                        int cNum = _ac.Count();


                        //TSET: cNum
                        //ms.Add(new MsItem("TSET:cNum", cNum));

                        //清空从新计算
                        ct.CourseScore = 0;
                        foreach (Assessment a in _ac.ToList())
                        {
                            ct.CourseScore += a.AllScore;
                        }
                        //取平均
                        ct.CourseScore = (ct.CourseScore / cNum);
                        //TSET: 取平均
                        //ms.Add(new MsItem("TSET:ct取平均", ct.CourseScore));
                    }


                    //修改评价表分数
                    db.Entry(ef).State = EntityState.Modified;
                    //修改课程模板表分数
                    db.Entry(ct).State = EntityState.Modified;

                    db.SaveChanges();

                    //return Json(ms);
                }
            }
            catch (Exception ex)
            {

                return Json(ex.ToString());
            }


            return Json(true);
        }



        /// <summary>
        /// 查看用户评价信息，返回评价列表与相对应的表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Assessments(int? id)
        {

            // 测试队列
            MsList ms = new MsList();

            //ms.Add(new MsJson("Post过来的ID", id)); // 测试完成，数据到达！
            IQueryable<EvalutionForms> _el = null;

            IQueryable<Assessment> _al = db.Assessment.Where(x => x.id == id) as IQueryable<Assessment>;
            foreach (Assessment a in _al.ToList())
            {
                _el = db.EvalutionForms.Where(x => x.id == a.EvaTabId);
                EvalutionForms ef = _el.FirstOrDefault();
                ms.Add(new MsJson(ef.id,ef.title, a));
            }
            
            return Json(ms);
        }
        /// <summary>
        /// 添加评价的内容或模板
        /// </summary>
        /// <param name="Comtemp"></param>
        /// <returns></returns>
        public JsonResult PutComTemp([Bind(Include = "id,CreateId,CreteTime,EContent")] CommentTemplates Comtemp)
        {
            try
            {

                IQueryable<CommentTemplates> _cl = db.CommentTemplates.Where(x => x.id == Comtemp.id);
                if (ModelState.IsValid)
                {
                    if (_cl.Count() > 0)
                    {
                        //如果记录中有匹配的记录 1>0的  那就修改
                        db.Entry(Comtemp).State = EntityState.Modified;
                        //测试数据 有匹配的记录 评价表
                        //ms.Add(new MsItem("有匹配的记录评价表", assessment));
                    }
                    else
                    {//如果记录中没有匹配的记录 0!>0的 
                        //Comtemp.CreteTime = TimeNow; 数据库设计错 需要修改!!!!
                        db.CommentTemplates.Add(new CommentTemplates() { CreateId = Comtemp.CreateId, CreteTime = Comtemp.CreteTime, EContent = Comtemp.EContent });
                        //测试数据 没有匹配的记录 评价表
                        //ms.Add(new MsItem("没有匹配的记录的记录评价表", assessment));
                    }
                    db.SaveChanges();

                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {

                return Json(false);
            }

            return Json(true);
        }






        //------------------------------------ !!!!


    }
}
