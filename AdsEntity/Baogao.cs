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
        public string BaogaoTotalScore { get; set; }
        public DateTime BaogaoTime { get; set; }
    }

    public class BaogaoDemention
    {
        public string demName { get; set; }
        public int demScore { get; set; }
    
    }
}
