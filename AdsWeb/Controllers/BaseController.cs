using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using AdsEntity;
using AdsDal;

namespace AdsWeb.Controllers
{
    public class BaseController : Controller
    {
        private UnitOfWork un = new UnitOfWork();
        //从webconfig中获取相应配置
        public  string SkyConnection = ConfigurationManager.AppSettings["SkyWebConnection"];
        public  int    SkyPageSize = int.Parse(ConfigurationManager.AppSettings["SkyPageSize"]);
        public int SkyCustomerRootId = int.Parse(ConfigurationManager.AppSettings["SkyCustomerRootId"]);
        public int SkyVideoRootId = int.Parse(ConfigurationManager.AppSettings["SkyVideoRootId"]);

        //从数据库中获取相应配置
        public  Setting SkySet;

        public void GetSeting()
        {
            Setting setting = un.settingsRepository.GetByID(1);
            this.SkySet = setting;
        }


	}
}