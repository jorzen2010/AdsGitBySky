using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdsEntity;
using AdsDal;
using Common;

namespace AdsWeb.Controllers
{
    public class CategoryController : Controller
    {
        private SkyWebContext db = new SkyWebContext();

        private UnitOfWork unitOfWork = new UnitOfWork();


        public ActionResult Index()
        {
            ViewBag.smallTitle = "警告：请谨慎操作分类设置，不当操作可能会造成系统崩溃";

            return View();
        }


        //获取一个Category
        public JsonResult GetOneCategory(int? id)
        {
            if (id == null)
            {
                return null;
            }
            Category category = unitOfWork.categorysRepository.GetByID(id);
            category.CategoryParentName = unitOfWork.categorysRepository.GetByID(category.CategoryParentID).CategoryName;

            if (category == null)
            {
                return null;
            }
            return Json(category, JsonRequestBehavior.AllowGet);

        }
        //构建一个CategoryList的json
        public JsonResult TreeJson()
        {

            Category root = unitOfWork.categorysRepository.GetByID(1);
            bvnode rootnode = new bvnode();
            rootnode.text = root.CategoryName;
            rootnode.id = root.ID;
            rootnode.pid = root.CategoryParentID;
            LoopToAppendChildren(rootnode);
            return Json(rootnode.nodes, JsonRequestBehavior.AllowGet);
        }
        //递归出所有字典数据
        public void LoopToAppendChildren(bvnode rootnode)
        {
            var subItems = Getnodes(rootnode.id);
            if (subItems.Count > 0)
            {
                rootnode.nodes = new List<bvnode>();
                rootnode.nodes.AddRange(subItems);
                foreach (var subItem in subItems)
                {
                    LoopToAppendChildren(subItem);
                }

            }

        }

        public List<bvnode> Getnodes(int ParentID)
        {
            var categorys = from s in db.Categorys
                            orderby s.CategorySort ascending
                            where s.CategoryParentID == ParentID
                            select s;
            List<bvnode> nodes = new List<bvnode>();
            foreach (var category in categorys)
            {
                bvnode node = new bvnode();
                node.id = category.ID;
                node.pid = category.CategoryParentID;
                node.text = category.CategoryName;
                nodes.Add(node);

            }

            return nodes.ToList();

        }


        // GET: /Category/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: /Category/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryName,CategoryIcon,CategoryInfo,CategoryParentID,CategoryStatus,CategorySort")] Category category)
        {

            if (ModelState.IsValid)
            {
                db.Categorys.Add(category);
                db.SaveChanges();

                return RedirectToAction("Index", "Category", new { option = category.CategoryName });
            }
            return RedirectToAction("Index", "Category", new { option = category.CategoryName });
        }



        // POST: /Category/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryName,CategoryIcon,CategoryInfo,CategoryParentID,CategoryStatus,CategorySort")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Category", new { option = category.CategoryName });
            }
            return RedirectToAction("Index", "Category", new { option = category.CategoryName });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int? id)
        {
            Message msg = new Message();

            if (id == null)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "找不到ID";
            }
            else
            {
                var childcategorys = from s in db.Categorys
                                     orderby s.CategorySort ascending
                                     where s.CategoryParentID == id
                                     select s;

                if (childcategorys.Count() > 0)
                {
                    msg.MessageStatus = "false";
                    msg.MessageInfo = "此ID节点下有子节点，不能删除";

                }
                else
                {
                    if (id == 2)
                    {
                        msg.MessageStatus = "false";
                        msg.MessageInfo = "此节点为系统内置节点不能删除，你可以修改此节点内容！";

                    }
                    else
                    {
                        Category category = db.Categorys.Find(id);
                        db.Categorys.Remove(category);
                        db.SaveChanges();
                        msg.MessageStatus = "true";
                        msg.MessageInfo = "删除成功";
                    }
                }
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}