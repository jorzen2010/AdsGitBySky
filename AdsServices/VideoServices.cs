using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using AdsEntity;


namespace AdsServices
{
    public class VideoServices
    {
        public static IList<AdsVideo> GetListForPageList(Pager pager)
        {
            pager = CommonDal.GetPager(pager);
            IList<AdsVideo> videoList = DataConvertHelper<AdsVideo>.ConvertToModel(pager.EntityDataTable);
            return videoList;
        }

       
    }
}
