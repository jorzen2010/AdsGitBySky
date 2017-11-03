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
    public class FreeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult FreeScale()
        {
           
           return View();
              
        }
       
        #region 量表报告详情页面
        public ActionResult FreeBaogaoDetail(string score, string Dementionscore, string totalscore, string weight)
        {



            string[] sArray = Dementionscore.Split(',');
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
            ViewBag.total = totalscore;
            ViewBag.categories = chartscategories;
            ViewBag.chartsdata = chartsdata;

            return View();
        }


        #endregion


        #region 2心理服务
        public ActionResult FreeHeartList(int? page)
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

        public ActionResult SaleBabys(int? page, int id)
        {
            Pager pager = new Pager();
            pager.table = "AdsBaby";
            pager.strwhere = "SalerCode=" + id;
            pager.PageSize = 20;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "BabyId";
            pager.FiledOrder = "BabyId desc";

            pager = CommonDal.GetPager(pager);
            IList<AdsBaby> babys = DataConvertHelper<AdsBaby>.ConvertToModel(pager.EntityDataTable);
            var babysAsIPageList = new StaticPagedList<AdsBaby>(babys, pager.PageNo, pager.PageSize, pager.Amount);
            ViewBag.salercode = id;
            return View(babysAsIPageList);
        }

        //项目相关的页面
        public ActionResult FreeHeartServices(int id)
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

    }
}
