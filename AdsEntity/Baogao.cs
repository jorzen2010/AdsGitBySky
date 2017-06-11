using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsEntity
{
    public class Baogao
    {
        public int BaogaoId { get; set; }
        public int ScaleId { get; set; }
        public int CustomerId { get; set; }
        public int BabyId { get; set; }
        public string  BaogaoScore { get; set; }
        public string BaogaoDementionScore { get; set; }
        public string BaogaoWeight { get; set; }
        public string BaogaoTotalScore { get; set; }
        public DateTime BaogaoTime { get; set; }
    }

    public class BaogaoDemention
    {
        //名称
        public string demName { get; set; }
        //得分
        public int demScore { get; set; }
        //参考值
        public int demReference { get; set; }
        //序号
        public int demNumber { get; set; }
    
    }
}
