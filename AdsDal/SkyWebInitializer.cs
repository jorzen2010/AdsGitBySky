using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AdsEntity;

namespace AdsDal
{
    public class SkyWebInitializer : DropCreateDatabaseIfModelChanges<SkyWebContext>
    {
        protected override void Seed(SkyWebContext context)
        {
            base.Seed(context);

            #region 用户初始化
            var sysUsers = new List<AdsSysUser>
            {
                new AdsSysUser{AdsSysUserName="Tom",AdsSysPassword="1"},
                new AdsSysUser{AdsSysUserName="Jerry",AdsSysPassword="2"},
                new AdsSysUser{AdsSysUserName="Jeem",AdsSysPassword="3"}
            };
            sysUsers.ForEach(s => context.AdsSysUsers.Add(s));
            context.SaveChanges();

            #endregion



        }
    }
}
