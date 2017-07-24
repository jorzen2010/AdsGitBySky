using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using AdsEntity;
using AdsDal;


namespace AdsServices
{
    public class VideoServices
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();
        public static IList<AdsVideo> GetListForPageList(Pager pager)
        {
            pager = CommonDal.GetPager(pager);
            IList<AdsVideo> videoList = DataConvertHelper<AdsVideo>.ConvertToModel(pager.EntityDataTable);
            return videoList;
        }


        public static string GetVideoNameById(int id)
        {
            AdsVideo video = unitOfWork.adsVideosRepository.GetByID(id);
            return video.VideoName;
        }

       
    }
}
