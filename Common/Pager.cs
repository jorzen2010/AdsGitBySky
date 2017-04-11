using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

namespace Common
{
    public class Pager
    {
        public string table;
        public string strwhere="1=1";
        public string FieldKey;
        public string FiledShow="*";
        public string FiledOrder;

        private int _pageSize;
        /// <summary>
        /// 每页数据量
        /// </summary>
        public int PageSize
        {
            get { return _pageSize == 0 ? 10 : _pageSize; }
            set { _pageSize = value; }
        }
        private int _pageCount;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return CounterPageCount(); }
            set { _pageCount = value; }
        }
        private int _pageNo;
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo
        {
          //  get { return _pageNo - 1 == -1 ? 1 : _pageNo - 1; }
            get { return _pageNo ; }
            set { _pageNo = value; }
        }
        /// <summary>
        /// 总数据量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 实体
        /// </summary>
        public DataTable EntityDataTable { get; set; }
        public IQueryable Entity { get; set; }
        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <returns></returns>
        private int CounterPageCount()
        {
            if (Amount % PageSize == 0)
            {
                return Amount / PageSize > 0 ? Amount / PageSize : 0;
            }
            else
            {
                return Amount / PageSize > 0 ? Amount / PageSize + 1 : 0;
            }
        }
    }
}
