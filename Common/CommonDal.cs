using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Reflection; 

namespace Common
{
    public class CommonDal
    {
        public static string ConnectionString = ConfigurationManager.AppSettings["SkyWebConnection"];

        #region 插入操作
        public static string Insert(SqlParameter[] arParames, string StoredProcedure)
        {

            string msg = "数据插入操作成功";
            SqlConnection myconn = null;
            try
            {
                myconn = new SqlConnection(CommonDal.ConnectionString);
                SqlHelper.ExecuteNonQuery(myconn, CommandType.StoredProcedure, StoredProcedure, arParames);
            }
            catch (SqlException ex)
            {
                msg = "Connection error Or  Insert error:" + ex.Message;
                throw ex;

            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }

            return msg;

        }
        #endregion

        #region 更新操作全部内容
        public static string Update(SqlParameter[] arParames, string StoredProcedure)
        {

            string msg = "数据更新操作成功";
            SqlConnection myconn = null;
            try
            {
                myconn =new SqlConnection(CommonDal.ConnectionString);
                SqlHelper.ExecuteNonQuery(myconn, CommandType.StoredProcedure, StoredProcedure, arParames);
            }
            catch (SqlException ex)
            {
                msg = "Connection error Or  Update error::" + ex.Message;
                throw ex;
            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }
            return msg;

        }

        #endregion       

        #region 更新一个字段
        public static string SetFiledOneByOne(string table, string strwhere, string columnname, string columnvalue)
        {

            string msg = "";

            SqlParameter[] arParames = new SqlParameter[4];
            arParames[0] = new SqlParameter("@table ", SqlDbType.VarChar, 200);
            arParames[0].Value = table;

            arParames[1] = new SqlParameter("@Where ", SqlDbType.VarChar, 8000);
            arParames[1].Value = strwhere;

            arParames[2] = new SqlParameter("@columnname ", SqlDbType.VarChar, 200);
            arParames[2].Value = columnname;

            arParames[3] = new SqlParameter("@columnvalue ", SqlDbType.VarChar, 200);
            arParames[3].Value = columnvalue;
            SqlConnection myconn = null;
            try
            {
                myconn = new SqlConnection(CommonDal.ConnectionString);
                SqlDataReader dr = SqlHelper.ExecuteReader(myconn, CommandType.StoredProcedure, "setFiledOneByOne", arParames);
                msg = "修改成功";
            }
            catch (SqlException ex)
            {
                msg = "false";
                throw ex;
            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }
            return msg;

        }
        #endregion 

        #region 删除一个对象
        public static string Delete(string table, string strwhere)
        {

            string msg = "删除操作成功执行";
            SqlParameter[] arParames = new SqlParameter[2];
            arParames[0] = new SqlParameter("@table ", SqlDbType.VarChar, 200);
            arParames[0].Value = table;

            arParames[1] = new SqlParameter("@Where ", SqlDbType.VarChar, 8000);
            arParames[1].Value = strwhere;
            SqlConnection myconn = null;
            try
            {
                myconn = new SqlConnection(CommonDal.ConnectionString);
                SqlHelper.ExecuteNonQuery(myconn, CommandType.StoredProcedure, "deleteModelByWhere", arParames);
            }
            catch (SqlException ex)
            {
                msg = "Connection error Or  Delete error:：" + ex.Message;
                throw ex;
            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }
            return msg;

        }
        #endregion

        #region 取得单表的查询并进行分页数据
        public static Pager GetPager(Pager pager)
        {
            SqlParameter[] arParms = new SqlParameter[9];
            //@tbname   --要分页显示的表名
            arParms[0] = new SqlParameter("@tbname", SqlDbType.NVarChar, 30);
            arParms[0].Value = pager.table;

            // @FieldKey --用于定位记录的主键(惟一键)字段,可以是逗号分隔的多个字段
            arParms[1] = new SqlParameter("@FieldKey", SqlDbType.NVarChar, 40);
            arParms[1].Value = pager.FieldKey;

            //@PageCurrent --要显示的页码
            arParms[2] = new SqlParameter("@PageCurrent", SqlDbType.Int);
            arParms[2].Value = pager.PageNo ;

            // @PageSize --每页的大小(记录数)
            arParms[3] = new SqlParameter("@PageSize", SqlDbType.Int);
            arParms[3].Value = pager.PageSize;

            //@FieldShow  --以逗号分隔的要显示的字段列表,如果不指定,则显示所有字段
            arParms[4] = new SqlParameter("@FieldShow", SqlDbType.NVarChar, 500);
            arParms[4].Value = pager.FiledShow;

            //@FieldOrder --以逗号分隔的排序字段列表,可以指定在字段后面指定DESC/ASC用于指定排序顺序

            arParms[5] = new SqlParameter("@FieldOrder", SqlDbType.NVarChar, 500);
            arParms[5].Value = pager.FiledOrder;

            //@Where   --查询条件
            arParms[6] = new SqlParameter("@Where", SqlDbType.VarChar, 8000);
            arParms[6].Value = pager.strwhere;

            //@PageCount --总页数
            arParms[7] = new SqlParameter("@PageCount", SqlDbType.Int);
            arParms[7].Direction = ParameterDirection.Output;

            //@RecordCount --总记录数
            arParms[8] = new SqlParameter("@RecordCount", SqlDbType.Int);
            arParms[8].Direction = ParameterDirection.Output;
            SqlConnection myconn = new SqlConnection(CommonDal.ConnectionString);
            try
            {
              
                DataTable dt = null;
                DataSet ds = SqlHelper.ExecuteDataset(myconn, CommandType.StoredProcedure, "sp_AspNetPageView", arParms);
                dt = ds.Tables[0];
               

                var totalItems = (int)arParms[8].Value;

                pager.Amount = totalItems;
                pager.EntityDataTable = dt;
                pager.PageCount = (int)arParms[7].Value;

                return pager;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }
        }
        #endregion

        #region 获取一个对象

        public static DataTable GetOneByWhere(string table, string strwhere)
        {
         
            SqlParameter[] arParames = new SqlParameter[2];
            arParames[0] = new SqlParameter("@table ", SqlDbType.VarChar, 200);
            arParames[0].Value = table;
            arParames[1] = new SqlParameter("@Where ", SqlDbType.VarChar, 8000);
            arParames[1].Value = strwhere;
            SqlConnection myconn = null;
            try
            {
                myconn = new SqlConnection(CommonDal.ConnectionString);
                DataTable dt = null;

                DataSet ds = SqlHelper.ExecuteDataset(myconn, CommandType.StoredProcedure, "getOneByWhere", arParames);
                dt = ds.Tables[0];
               

                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }


        }
        #endregion

        #region 获取多个对象集合

        public static DataTable GetSomeByWhere(string table, string strwhere)
        {


            SqlParameter[] arParames = new SqlParameter[2];
            arParames[0] = new SqlParameter("@table ", SqlDbType.VarChar, 200);
            arParames[0].Value = table;

            arParames[1] = new SqlParameter("@Where ", SqlDbType.VarChar, 8000);
            arParames[1].Value = strwhere;
            SqlConnection myconn = null;
            try
            {
                myconn = new SqlConnection(CommonDal.ConnectionString);
                DataTable dt = null;

                DataSet ds = SqlHelper.ExecuteDataset(myconn, CommandType.StoredProcedure, "getSomeByWhere", arParames);
                dt = ds.Tables[0];


                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }


        }
        #endregion

        #region 获取一些特殊值
        public static int GetSomeValueByWhere(string table, string strwhere,string thevalue)
        {

            int count = 0;
            SqlParameter[] arParames = new SqlParameter[3];
            arParames[0] = new SqlParameter("@table ", SqlDbType.VarChar, 200);
            arParames[0].Value = table;

            arParames[1] = new SqlParameter("@Where ", SqlDbType.VarChar, 8000);
            arParames[1].Value = strwhere;

            arParames[2] = new SqlParameter("@thevalue ", SqlDbType.VarChar, 200);
            arParames[2].Value = thevalue;
           
            SqlConnection myconn = null;
            try
            {
                myconn = new SqlConnection(CommonDal.ConnectionString);
                IDataReader dr = SqlHelper.ExecuteReader(myconn, CommandType.StoredProcedure, "getSomeValueByWhere", arParames);
             

                if (dr.Read())
                {
                    string countvalue = dr.GetValue(0).ToString();
                    if (!string.IsNullOrEmpty(countvalue))
                    {
                        count = int.Parse(countvalue);
                    }
                    
                }
                return count;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }


        }
        #endregion

    }
}
