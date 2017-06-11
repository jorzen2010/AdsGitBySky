using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using AdsEntity;
using AdsEntity.WechatEntity;
using AdsWeb.WechatServices;
using AdsDal;
using PagedList;
using PagedList.Mvc;
using AdsServices;

namespace AdsWeb.Controllers
{
    public class WechatController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
      

        #region 微信登录跳转页面
        public ActionResult Index()
        {
             string userAgent = Request.UserAgent;

            string CODE = Request["code"];

            string STATE = Request["state"];

            WebchatJsUserinfo userinfo = WechatJsServices.GetUserInfo(userAgent, CODE);

            var cus = unitOfWork.adsCustomersRepository.Get(filter: u => u.CustomerOpenid == userinfo.openid);
            int aa = cus.Count();
            AdsCustomer customer = new AdsCustomer();
            if (aa == 0)
            {

                customer.CustomerOpenid = userinfo.openid;
                customer.CustomerNickName = userinfo.nickname;
                if (userinfo.sex == 0)
                {
                    customer.CustomerSex = "未知";
                }
                if (userinfo.sex == 1)
                {
                    customer.CustomerSex = "男";
                }
                if (userinfo.sex == 2)
                {
                    customer.CustomerSex = "女";
                }
                customer.CustomerUnionid = userinfo.unionid;
                customer.CustomerIdentity = AdsCustomer.IdentiyStatus.未申请计划;
                customer.CustomerLastLoginTime = System.DateTime.Now;
                customer.CustomerRegTime = System.DateTime.Now;

                customer.CustomerAvatar = userinfo.headimgurl;
                customer.CustomerUserName = userinfo.openid;
                customer.CustomerProvince = userinfo.province;
                customer.CustomerCity = userinfo.city;

                unitOfWork.adsCustomersRepository.Insert(customer);
                unitOfWork.Save();
            }
            else
            {
                customer = cus.First() as AdsCustomer;
            }

            Session["CustomerNickName"] = customer.CustomerNickName;
            Session["CustomerOpenid"] = customer.CustomerOpenid;
            Session["CustomerId"] = customer.CustomerId;

            return Redirect(STATE);
                

           // return View();
        }
        #endregion 

        #region 微信登录
        public ActionResult WechatLogin()
        {
                string sourceUrl = Request.UrlReferrer.ToString();

                string userAgent = Request.UserAgent;
         
                WechatConfig wechatconfig = AccessTokenService.GetWechatConfig();

                string REDIRECT_URI = System.Web.HttpUtility.UrlEncode("http://wx.zzd123.com/Wechat/Index");

                string SCOPE = "snsapi_userinfo";
                string STATE = sourceUrl;

                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + wechatconfig.Appid + "&redirect_uri=" + REDIRECT_URI + "&response_type=code&scope=" + SCOPE + "&state="+STATE+"#wechat_redirect";

                return Redirect(url);
           


        }
        #endregion
        
        
        #region 训练计划
        public ActionResult Calendar()
        {
            AdsBaby baby = new AdsBaby();
            if (Session["CustomerId"] != null)
            {
                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id);
                int count = babys.Count();
                if (count > 0)
                {
                    baby = babys.First() as AdsBaby;
                    ViewData["videocat"] = CategoryServices.GetCategoryListByParentID(2);
                    return View(baby);
                }
                else
                {


                    return RedirectToAction("NoBaby");

                }

            }
            else
            {
                return RedirectToAction("Login");
            
            }
        }
        #endregion

        #region 登录页面
        public ActionResult Login()
        {
         //   string nickname = Session["nickname"].ToString();
            if (Session["CustomerNickName"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("MemberCenter", "Wechat");
            }
        
        }
        #endregion

        

        #region 量表测评
        public ActionResult Ceping(int? page)
        {

            if (Session["CustomerId"] != null)
            {

                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id);
                int count = babys.Count();
                if (count > 0)
                {
                    AdsBaby baby = babys.First() as AdsBaby;

                    Pager pager = new Pager();
                    pager.table = "Baogao";
                    pager.strwhere = "BabyId=" + baby.BabyId;
                    pager.PageSize = 20;
                    pager.PageNo = page ?? 1;
                    pager.FieldKey = "BaogaoId";
                    pager.FiledOrder = "BaogaoId desc";

                    pager = CommonDal.GetPager(pager);
                    IList<Baogao> baogaos = DataConvertHelper<Baogao>.ConvertToModel(pager.EntityDataTable);
                    var baogaosAsIPageList = new StaticPagedList<Baogao>(baogaos, pager.PageNo, pager.PageSize, pager.Amount);
                    ViewBag.babyname = baby.BabyName;
                    return View(baogaosAsIPageList);
                }
                else
                {
                    return RedirectToAction("NoBaby");
                }

            }
            else
            {
                return RedirectToAction("Login");

            }
        }
        #endregion

        public ActionResult Program()
        {
            return View();
        }

        #region 心理服务
        public ActionResult HeartList(int?page)
        {


            string sqlpara = CategoryServices.GetChildIdLisForSql(4);

            Pager pager = new Pager();
            pager.table = "AdsVideo";
            pager.strwhere = "VideoCategory in" + sqlpara;
            pager.PageSize = 50;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "VideoId";
            pager.FiledOrder = "VideoId desc";

            pager = CommonDal.GetPager(pager);
            IList<AdsVideo> videos = DataConvertHelper<AdsVideo>.ConvertToModel(pager.EntityDataTable);
            var videosAsIPageList = new StaticPagedList<AdsVideo>(videos, pager.PageNo, pager.PageSize, pager.Amount);
            return View(videosAsIPageList);        


           
        }
        #endregion

        #region 训练项目
        public ActionResult ProgrameList(int? page, int? cid)
        {

            int categoryid = cid ?? 8;

            Pager pager = new Pager();
            pager.table = "AdsVideo";
            pager.strwhere = "VideoCategory=" + categoryid;
            pager.PageSize = 50;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "VideoId";
            pager.FiledOrder = "VideoId desc";

            pager = CommonDal.GetPager(pager);
            IList<AdsVideo> videos = DataConvertHelper<AdsVideo>.ConvertToModel(pager.EntityDataTable);
            var videosAsIPageList = new StaticPagedList<AdsVideo>(videos, pager.PageNo, pager.PageSize, pager.Amount);
            ViewBag.cid = categoryid;
            //    ViewBag.cname = CategoryServices.GetCategoryNameById(categoryid);
            return View(videosAsIPageList);



        }
        #endregion

        #region 个人中心
        public ActionResult MemberCenter()
        {
            if (Session["CustomerId"] != null)
            {


                int id = int.Parse(Session["CustomerId"].ToString());
                AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(id);
                AdsBaby baby = new AdsBaby();
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id);
                int count = babys.Count();
                if (count > 0)
                {
                    baby = babys.First() as AdsBaby;
                    ViewBag.baby = true;
                }
                else
                {
                    ViewBag.baby = false;
                
                }
                return View(customer);

            }

            else
            {
                return RedirectToAction("Login");
            
            }
           
               
            

        }
        #endregion

        #region 测评页面
        public ActionResult Baogao(int? page)
        {
            
            if (Session["CustomerId"] != null)
            {

                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id);
                int count = babys.Count();
                if (count > 0)
                {
                    AdsBaby baby = babys.First() as AdsBaby;
                    return View(baby);
                }
                else
                {  
                    return RedirectToAction("NoBaby");

                }


               
               

            }
            else
            {
                return RedirectToAction("NoBaby");

            }
        }
        #endregion

        #region 尚未申请计划
        public ActionResult NoBaby()
        {
            return View();
        
        }
        #endregion

        #region 心理测评
        public ActionResult Scale()
        {
            AdsBaby baby = new AdsBaby();
            if (Session["CustomerId"] != null)
            {
                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id);
                int count = babys.Count();
                if (count > 0)
                {
                    baby = babys.First() as AdsBaby;
                    return View(baby);
                }
                else
                {
                    ViewBag.info = "您尚未开通训练计划";
                    return View(baby);

                }

            }
            else
            {
                ViewBag.info = "您尚未登录";
                return View(baby);

            }
        }
        #endregion

        #region 注册页面
        public ActionResult Reg()
        {
            return View();
        }

        //public ActionResult GetUserInfo()
        //{
        //    WechatConfig wechatconfig = AccessTokenService.GetWechatConfig();

        //    string REDIRECT_URI = System.Web.HttpUtility.UrlEncode("http://wx.zzd123.com/Wechat/GetUserId");

        //    string SCOPE = "snsapi_userinfo";

        //    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + wechatconfig.Appid + "&redirect_uri=" + REDIRECT_URI + "&response_type=code&scope=" + SCOPE + "&state=STATE#wechat_redirect";

        //    return Redirect(url);

        //}
        //public ActionResult GetUserId()
        //{

        //    string CODE = Request["code"];
        //    string STATE = Request["state"];

        //    string userAgent = Request.UserAgent;

        //    WebchatJsUserinfo userinfo = WechatJsServices.GetUserInfo(userAgent, CODE);

        //    return View(userinfo);


        //}

        #endregion

        #region 退出
        public ActionResult Logout()
        {
            Session["CustomerNickName"] = null;
            Session["CustomerOpenid"] = null;
            Session["CustomerId"] = null;
            return RedirectToAction("Login");
        }

        #endregion

        #region 账号设置
        public ActionResult Setting()
        {
            return View();
        }
        #endregion

        #region 申请加入计划
        public ActionResult StarBaby()
        {
            if (Session["CustomerId"] != null)
            {
                ViewData["customerid"] = Session["CustomerId"].ToString();
                return View();
            }
            else {
                return RedirectToAction("Login");
            
            }
           
        }
        #endregion

        #region 未付费
        public ActionResult NoPay()
        {
            if (Session["CustomerId"] != null)
            {
               
                return View();
            }
            else
            {
                return RedirectToAction("Login");

            }

        }
        #endregion

        #region 付费Action
        public ActionResult Pay(int bid)
        {
            if (Session["CustomerId"] != null)
            {
                AdsBaby baby = unitOfWork.adsBabysRepository.GetByID(bid);
                baby.Babystatus = true;

                return RedirectToAction("Calendar");
               
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        #endregion





        #region 项目评价
        public ActionResult Pingjia(int? page,int?sid) 
        {

            int status = sid ?? 0;
            if (Session["CustomerId"] != null)
            {
                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id);
                int count = babys.Count();
                if (count > 0)
                {
                    AdsBaby baby = babys.First() as AdsBaby;
                    Pager pager = new Pager();
                    pager.table = "Pingjia";
                    pager.strwhere = "BabyId=" + baby.BabyId;
                    if (status != 0)
                    {
                        pager.strwhere = pager.strwhere + " and Status=" + status;
                    
                    }
                    pager.PageSize = 20;
                    pager.PageNo = page ?? 1;
                    pager.FieldKey = "PingjiaId";
                    pager.FiledOrder = "PingjiaId desc";

                    pager = CommonDal.GetPager(pager);
                    IList<Pingjia> pingjias = DataConvertHelper<Pingjia>.ConvertToModel(pager.EntityDataTable);
                    var pingjiasAsIPageList = new StaticPagedList<Pingjia>(pingjias, pager.PageNo, pager.PageSize, pager.Amount);
                    ViewBag.babyId = baby.BabyId;
                    return View(pingjiasAsIPageList);
                }
                else
                {
                    return RedirectToAction("NoBaby");

                }

            }
            else
            {
                return RedirectToAction("Login");

            }
        
        }

        #endregion

        #region 评价记录
        public ActionResult PingjiaCreate(int babyId, int status, int videoId)
        {
            Pingjia pingjia = new Pingjia();
            pingjia.BabyId = babyId;
            pingjia.VideoId =videoId ;
            if (status == 1)
            {
                pingjia.Status = PingjiaStatus.熟练完成;
            }
            if (status == 2)
            {
                pingjia.Status = PingjiaStatus.基本完成;
            }
            if (status == 3)
            {
                pingjia.Status = PingjiaStatus.不能完成;
            }
            pingjia.PingjiaTime = DateTime.Now;

            unitOfWork.pingjiasRepository.Insert(pingjia);
            unitOfWork.Save();

            return View();
        
        }
        #endregion

        #region 项目详情
        public ActionResult VideoDetail()
        {
            AdsBaby baby = new AdsBaby();
            if (Session["CustomerId"] != null)
            {
                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id);
                int count = babys.Count();
                if (count > 0)
                {
                    baby = babys.First() as AdsBaby;
                    ViewBag.babyId = baby.BabyId;
                    return View();
                }
                else
                {
                    return RedirectToAction("NoBaby");

                }

            }
            else
            {
                return RedirectToAction("Login");

            }
        
        }

        #endregion

        #region 创建计划
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BabyCreate(AdsBaby baby)
        {
            if (ModelState.IsValid)
            {
                
                baby.BabyAvatar = "~/wechat/img/star.jpg";
                baby.BabyRegTime = System.DateTime.Now;
                baby.BabyExpiredTime = System.DateTime.Now.AddYears(5);

                unitOfWork.adsBabysRepository.Insert(baby);
                unitOfWork.Save();

                return RedirectToAction("Calendar");
            }

            return View(baby);
        }

        #endregion


        public ActionResult test()
        {
            string x = "感觉能力:99,交往能力:56,运动能力:46,语言能力:76,自理能力:90";
            string y = "感觉能力:0.9,交往能力:0.8,运动能力:0.4,语言能力:0.7,自理能力:0.6";

            ViewBag.test = PlanServices.MakePlan(x);
            ViewBag.test2 = PlanServices.MakePlan(y);

            string[] sArray = ViewBag.test.Split(',');
            List<BaogaoDemention> demlist = new List<BaogaoDemention>();
            int number = 0;
            foreach (string s in sArray)
            {
                number++;
                // string  dem=s.ToString();
                BaogaoDemention dem = new BaogaoDemention();
                dem.demName = s.Substring(0, s.IndexOf(":"));
                dem.demScore = int.Parse(s.Substring(s.IndexOf(":") + 1));
                dem.demNumber=number;

                demlist.Add(dem);
                
            }

            ViewData["dem"] = demlist;
            ViewBag.current = demlist[0].demName;

            List<AdsVideo> videolist = new List<AdsVideo>();
            return View();
        
        }


        #region 量表报告详情页面
        public ActionResult BaogaoDetail(int id)
        {   
            Baogao baogao = unitOfWork.baogaoRepository.GetByID(id);
            AdsBaby baby = unitOfWork.adsBabysRepository.GetByID(baogao.BabyId);
        //    string babyName = unitOfWork.adsBabysRepository.GetByID(baogao.BabyId).BabyName;
            string str = baogao.BaogaoDementionScore;

            string[] sArray = str.Split(',');
            List<BaogaoDemention> demlist = new List<BaogaoDemention>();
            string chartscategories = "[";
            string chartsdata = "[";

            foreach (string s in sArray)
            {
                // string  dem=s.ToString();
                BaogaoDemention dem = new BaogaoDemention();
                dem.demName = s.Substring(0, s.IndexOf(":"));
                dem.demScore = int.Parse(s.Substring(s.IndexOf(":") + 1));
                if (dem.demName == "交往能力" || dem.demName == "运动能力")
                {
                    dem.demReference = 8;
                }
                else
                {
                    dem.demReference = 5;
                }
                demlist.Add(dem);
                chartscategories = chartscategories + "\"" + dem.demName + "\"" + ",";
                chartsdata = chartsdata + dem.demScore + ",";
            }
            chartscategories = chartscategories.TrimEnd(',') + "]";
            chartsdata = chartsdata.TrimEnd(',') + "]";
            ViewData["dem"] = demlist;
            ViewBag.categories = chartscategories;
            ViewBag.chartsdata = chartsdata;
            ViewBag.babyName = baby.BabyName;
            ViewBag.babysex = baby.BabySex;
            ViewBag.babyage = DateTime.Now.Year -  Convert.ToDateTime(baby.BabyBirthday).Year;
            return View(baogao);
        }


        #endregion

        #region 保存测评结果
        [HttpPost]

        public JsonResult SaveScaleResult(string score, string Dementionscore, string totalscore,string weight)
        {
            int customerId = int.Parse(Session["CustomerId"].ToString());
            int babyid = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == customerId).First().BabyId;
          
            
            Message msg = new Message();

            Baogao baogao = new Baogao();
            baogao.BaogaoScore = score;
            baogao.BaogaoDementionScore = Dementionscore;
            baogao.BaogaoTotalScore = totalscore;
            baogao.BaogaoWeight = weight;
            baogao.CustomerId = customerId;
            baogao.BabyId = babyid;
            baogao.ScaleId = 1;
            baogao.BaogaoTime = System.DateTime.Now;

            Plan plan = new Plan();


            try
            {
                unitOfWork.baogaoRepository.Insert(baogao);
                unitOfWork.Save();
                msg.MessageStatus = "true";
                msg.MessageInfo = baogao.BaogaoId.ToString();
            }
            catch
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "保存失败";
            
            }
            
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}
