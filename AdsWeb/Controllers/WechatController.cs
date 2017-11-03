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
using AdsWeb.AliyunVideo;
using System.Globalization;
using PsyCoderWechat.WechatPay;



namespace AdsWeb.Controllers
{
     [AllowAnonymous]
    public class WechatController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SkyWebContext db = new SkyWebContext();
        JsApiPay jsApiPay = new JsApiPay();

        //登录相关所有操作


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

        #region 微信登录跳转页面
        public ActionResult Index()
        {
             string userAgent = Request.UserAgent;

            string CODE = Request["code"];

            string STATE = Request["state"];

            if (string.IsNullOrEmpty(CODE))
            {
                return RedirectToAction("Login");
            }
            else
            {
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
                Session["openid"] = customer.CustomerOpenid;
                Session["CustomerId"] = customer.CustomerId;

             //   return Redirect(STATE);
                return RedirectToAction("Calendar");
            
            }

            
                

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


        //底部导航5个页面



        #region 1量表测评列表
        public ActionResult Ceping(int? page)
        {

            if (Session["CustomerId"] != null)
            {

                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id &&u.Babystatus==true);
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

        #region 2心理服务
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


        //项目相关的页面
        public ActionResult HeartServices(int id)
        {
            string ApiUrl = AliyunCommonParaConfig.ApiUrl;
            // 注意这里需要使用UTC时间，比北京时间少8小时。
            string Timestamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
            string Action = "GetVideoPlayAuth";
            string SignatureNonce = CommonTools.EncryptToSHA1(CommonTools.GenerateRandomNumber(8));


            AdsVideo video = unitOfWork.adsVideosRepository.GetByID(id);
            string VideoId = video.VideoUrl;
            ViewBag.VideoId = VideoId;

            ViewBag.PlayAuth = AliyunVideoServices.GetVideoInfo(ApiUrl, VideoId, Timestamp, Action, SignatureNonce).PlayAuth;

            return View(video);
        }




        #region 3训练计划日历
        public ActionResult Calendar(int? page, int ?cid ,int? orderid)
        {
            int oid = orderid ?? 1;
        
            AdsBaby baby = new AdsBaby();
            if (Session["CustomerId"] != null)
            {
                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id && u.Babystatus == true);
                int count = babys.Count();
                if (count > 0)
                {
                    baby = babys.First() as AdsBaby;
                    ViewData["videocat"] = CategoryServices.GetCategoryListByParentID(2);
                    ViewBag.babyName = baby.BabyName;
                    ViewBag.babyId = baby.BabyId;
                    ViewBag.cepingcount = StatisticsServices.GetCepingCountByBabyId(baby.BabyId);
                    ViewBag.pingjiacount = StatisticsServices.GetPingjiaCountByBabyId(baby.BabyId, 0);
                    ViewBag.days = StatisticsServices.GetDaysByCustomerId(baby.BabyRegTime);

                    List<BaogaoDemention> demlist=PlanServices.PlanCategory(baby.BabyId);
                    if (demlist.Count() == 0)
                    {
                        return RedirectToAction("NoScale", new { name = baby.BabyName });
                    }

                    int categoryid = cid??demlist[0].demcategoryid;

                    Pager pager = new Pager();
                    pager.table = "AdsVideo";
                    pager.strwhere = "VideoCategory=" + categoryid;
                    pager.PageSize = 15;
                    pager.PageNo = page ?? 1;
                    pager.FieldKey = "VideoId";
                    pager.FiledOrder = "VideoId desc";

                
                    pager = CommonDal.GetPager(pager);
                    IList<AdsVideo> videos = DataConvertHelper<AdsVideo>.ConvertToModel(pager.EntityDataTable);
                    var videosAsIPageList = new StaticPagedList<AdsVideo>(videos, pager.PageNo, pager.PageSize, pager.Amount);

                    ViewBag.orderid = oid;

                    if (oid == 1 || oid == 2)
                    {
                        ViewBag.ctitle = "必修任务";
                        ViewBag.cminTitle = "每次需训练30分钟";
                    }
                    if (oid == 3|| oid == 4)
                    {
                        ViewBag.ctitle = "选修任务";
                        ViewBag.cminTitle = "每次需训练20分钟";
                    }
                    if (oid == 5)
                    {
                        ViewBag.ctitle = "一般任务";
                        ViewBag.cminTitle = "自行安排训练";
                    }
                    ViewData["dem"] = demlist;

                    

                    return View(videosAsIPageList);
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

        #region 4项目评价
        public ActionResult Pingjia(int? page, float? sid)
        {

            float status = sid ?? 0;
            if (Session["CustomerId"] != null)
            {
                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id && u.Babystatus == true);
                int count = babys.Count();
                if (count > 0)
                {
                    AdsBaby baby = babys.First() as AdsBaby;
                    Pager pager = new Pager();
                    pager.table = "Pingjia";
                    pager.strwhere = "BabyId=" + baby.BabyId;
                    if (status != 0)
                    {
                        pager.strwhere = pager.strwhere + " and PingjiaValue=" + status;

                    }
                    pager.PageSize = 20;
                    pager.PageNo = page ?? 1;
                    pager.FieldKey = "PingjiaId";
                    pager.FiledOrder = "PingjiaId desc";

                    pager = CommonDal.GetPager(pager);
                    IList<Pingjia> pingjias = DataConvertHelper<Pingjia>.ConvertToModel(pager.EntityDataTable);
                    var pingjiasAsIPageList = new StaticPagedList<Pingjia>(pingjias, pager.PageNo, pager.PageSize, pager.Amount);
                    ViewBag.babyId = baby.BabyId; 
                    ViewBag.babyName = baby.BabyName;
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

        #region 5个人中心
        public ActionResult MemberCenter()
        {
            if (Session["CustomerId"] != null)
            {


                int id = int.Parse(Session["CustomerId"].ToString());
                AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(id);
                AdsBaby baby = new AdsBaby();
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id && u.Babystatus == true);
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


        //测量相关的页面


        #region 心理测评页面
        public ActionResult Scale()
        {
            AdsBaby baby = new AdsBaby();
            if (Session["CustomerId"] != null)
            {
                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id && u.Babystatus == true);
                int count = babys.Count();
                if (count > 0)
                {
                    baby = babys.First() as AdsBaby;
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

        #region 保存测评结果
        [HttpPost]

        public JsonResult SaveScaleResult(string score, string Dementionscore, string totalscore, string weight)
        {
            int customerId = int.Parse(Session["CustomerId"].ToString());
            int babyid = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == customerId && u.Babystatus == true).First().BabyId;


            Message msg = new Message();

            Baogao baogao = new Baogao();
            baogao.BaogaoScore = score;
            baogao.BaogaoDementionScore = Dementionscore;

            baogao.BaogaoTotalScore = totalscore;
            baogao.BaogaoWeight = PlanServices.MakePlan(weight);

            baogao.CustomerId = customerId;
            baogao.BabyId = babyid;
            baogao.ScaleId = 1;
            baogao.BaogaoTime = System.DateTime.Now;


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
                if (dem.demName == "感觉能力")
                {
                    dem.demReference = 30;
                }
                if (dem.demName == "交往能力")
                {
                    dem.demReference = 44;
                }
                if (dem.demName == "运动能力")
                {
                    dem.demReference = 29;
                }
                if (dem.demName == "语言能力")
                {
                    dem.demReference = 31;
                }
                if (dem.demName == "自理能力")
                {
                    dem.demReference = 25;
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
            ViewBag.babyage = DateTime.Now.Year - Convert.ToDateTime(baby.BabyBirthday).Year;
            return View(baogao);
        }


        #endregion


        //项目相关的页面
        public ActionResult Program(int id, int bid)
        {
              if (Session["CustomerId"] != null)
            {
            string ApiUrl = AliyunCommonParaConfig.ApiUrl;
            // 注意这里需要使用UTC时间，比北京时间少8小时。
            string Timestamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
            string Action = "GetVideoPlayAuth";
            string SignatureNonce = CommonTools.EncryptToSHA1(CommonTools.GenerateRandomNumber(8));
            

            AdsVideo video = unitOfWork.adsVideosRepository.GetByID(id);
            string VideoId = video.VideoUrl;
            ViewBag.babyId = bid;
            ViewBag.VideoId = VideoId;

            ViewBag.PlayAuth = AliyunVideoServices.GetVideoInfo(ApiUrl, VideoId, Timestamp, Action, SignatureNonce).PlayAuth;

            return View(video);
            }
              else
              {
                  return RedirectToAction("Login");
              }
        }



        #region 项目评价统计
        public ActionResult PingjiaTongji(int babyid)
        {
             if (Session["CustomerId"] != null)
            {
            AdsBaby baby = unitOfWork.adsBabysRepository.GetByID(babyid);

            ViewBag.pingjiaA = StatisticsServices.GetPingjiaCountByBabyId(baby.BabyId, 0.3f);
            ViewBag.pingjiaB = StatisticsServices.GetPingjiaCountByBabyId(baby.BabyId, 0.5f);
            ViewBag.pingjiaC = StatisticsServices.GetPingjiaCountByBabyId(baby.BabyId, 0.8f);
            ViewBag.babyId = baby.BabyId;
            ViewBag.babyName = baby.BabyName;

            string A1 = "", B1 = "", C1 = "", catName = "";


            //这里需要循环Category
            List<Category> listcat = CategoryServices.GetCategoryListByParentID(3);

            foreach (Category cat in listcat)
            {
                catName = catName + "'" + cat.CategoryName + "'" + ",";

                A1 = A1 + unitOfWork.pingjiasRepository.Get(filter: u => u.VideoCategory == cat.ID && u.BabyId == babyid && u.PingjiaValue == 0.3f).Count().ToString() + ",";
                B1 = B1 + unitOfWork.pingjiasRepository.Get(filter: u => u.VideoCategory == cat.ID && u.BabyId == babyid && u.PingjiaValue == 0.5f).Count().ToString() + ",";
                C1 = C1 + unitOfWork.pingjiasRepository.Get(filter: u => u.VideoCategory == cat.ID && u.BabyId == babyid && u.PingjiaValue == 0.8f).Count().ToString() + ",";


            }
            catName = catName.TrimEnd(',');
            A1 = A1.TrimEnd(',');
            B1 = B1.TrimEnd(',');
            C1 = C1.TrimEnd(',');

            ViewBag.A = A1;
            ViewBag.B = B1;
            ViewBag.C = C1;
            ViewBag.catName = catName;

            return View();
            }
             else
             {
                 return RedirectToAction("Login");
             }
          

        }

        #endregion

        //评价相关的页面

        #region 评价记录

        [HttpPost]
        public JsonResult PingjiaCreate(int babyId, float pingjiaValue, int videoId, int videoCategory)
        {
            //检查一下是否有评论

            Message msg = new Message();
            Pingjia pingjia = new Pingjia();
            pingjia.BabyId = babyId;
            pingjia.VideoId = videoId;
            pingjia.PingjiaValue = pingjiaValue;
            pingjia.VideoCategory = videoCategory;
            pingjia.PingjiaTime = DateTime.Now;

            bool ifsave = PingjiaServices.GetPingjiaCount(pingjia.VideoId, pingjia.BabyId, pingjia.PingjiaTime);

            if (!ifsave)
            {
                msg.MessageStatus = "true";
                msg.MessageInfo = "此项目6小时内不能重复评价。";
            }
            else
            { 
                    
                if (ModelState.IsValid)
                {
                    unitOfWork.pingjiasRepository.Insert(pingjia);
                    unitOfWork.Save();
                    msg.MessageStatus = "true";
                    msg.MessageInfo = "评估项目完成";
                }
                else
                {
                    msg.MessageStatus = "false";
                    msg.MessageInfo = "评估项目失败";
                }

            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
        #endregion



     
        //错误页面导航
        #region 尚未申请计划
        public ActionResult NoBaby()
        {
            return View();
        
        }
        #endregion

        #region 尚未进行测评
        public ActionResult NoScale()
        {
            return View();

        }
        #endregion




        #region 申请加入计划
        public ActionResult StarBaby()
        {
            if (Session["CustomerId"] != null)
            {
                int cid = int.Parse(Session["CustomerId"].ToString());
               
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == cid);
                if (babys.Count() > 0)
                {
                    AdsBaby baby = babys.First() as AdsBaby;
                    return RedirectToAction("StarBabyEdit", "Wechat", new { id=baby.BabyId});

                }
                else
                {
                    ViewData["customerid"] = Session["CustomerId"].ToString();
                    ViewBag.cusid = Session["CustomerId"].ToString();
                    return View();
                }
               

                
            }
            else {
                return RedirectToAction("Login");
            
            }
           
        }
        #endregion


        #region 申请加入计划
        public ActionResult StarBabyEdit(int id)
        {
            if (Session["CustomerId"] != null)
            {
                AdsBaby baby = unitOfWork.adsBabysRepository.GetByID(id);
                ViewData["customerid"] = Session["CustomerId"].ToString();
                return View(baby);
            }
            else
            {
                return RedirectToAction("Login");

            }

        }
        #endregion




        #region 注册页面
        public ActionResult Reg()
        {
            return View();
        }



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

        #region 个人账号设置
        public ActionResult Setting()
        {
             if (Session["CustomerId"] != null)
            {
            int id = int.Parse(Session["CustomerId"].ToString());
            AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(id);
            return View(customer);
            }
             else
             {
                 return RedirectToAction("Login");

             }
        }
        #endregion


        #region 个人账号设置保存
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setting(AdsCustomer customer)
        {

            if (ModelState.IsValid)
            {
                AdsCustomer cus = unitOfWork.adsCustomersRepository.GetByID(customer.CustomerId);
                cus.CustomerBirthdayType = "公历";
                cus.CustomerBirthday = DateTime.Parse(customer.CustomerBirthday.ToString());
                cus.CustomerEmail = customer.CustomerEmail;
                cus.CustomerNickName = customer.CustomerNickName;

                unitOfWork.adsCustomersRepository.Update(cus);
                unitOfWork.Save();

                return RedirectToAction("MemberCenter");
            }

            return View(customer);
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
                if (baby.Babystatus)
                {
                    return RedirectToAction("Calendar");
                }
                else
                {
                    return RedirectToAction("NoPay");
                }               
               
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        #endregion

        #region 付费Action页面
        public ActionResult StarBabyPay(int bid)
        {
             if (Session["CustomerId"] != null)
            {
                  AdsBaby baby = unitOfWork.adsBabysRepository.GetByID(bid);
                    
             return View(baby);
             }
             else
            {
                return RedirectToAction("Login");
            }

        }

       


        #endregion



        #region 创建计划新增星宝
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BabyCreate(AdsBaby baby)
        {
            if (ModelState.IsValid)
            {
                
                baby.BabyAvatar = "~/wechat/img/star.jpg";
                baby.BabyRegTime = System.DateTime.Now;
                baby.BabyExpiredTime = System.DateTime.Now.AddYears(1);

                unitOfWork.adsBabysRepository.Insert(baby);
                unitOfWork.Save();

                Session["babyId"] = baby.BabyId;
                //这里大小写敏感需要与微信支付目录统一一致
              //  return Redirect("/Wechat/StarBabyPay/?bid="+baby.BabyId);
                return Redirect("/wechatpay/pay/");
            }

            return View(baby);
        }

        #endregion


        #region 创建计划新增星宝
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StarBabyEdit(AdsBaby baby)
        {
            if (ModelState.IsValid)
            {

                baby.BabyAvatar = "~/wechat/img/star.jpg";
                baby.BabyRegTime = System.DateTime.Now;
                baby.BabyExpiredTime = System.DateTime.Now.AddYears(5);

                unitOfWork.adsBabysRepository.Update(baby);
                unitOfWork.Save();
                Session["babyId"] = baby.BabyId;
                //注意这里大小写敏感需要与微信支付目录统一一致
               // return Redirect("/Wechat/StarBabyPay/?bid=" + baby.BabyId);
                return Redirect("/wechatpay/pay/");
            }

            return View(baby);
        }

        #endregion




        


    }
}
