using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PsyCoderWechat.WechatPay;

namespace AdsWeb.Controllers
{
    public class WechatPayController : Controller
    {
        JsApiPay jsApiPay = new JsApiPay();

        [HttpPost]
        public ActionResult Index()
        {
            return View();
        
        }
       
        /// <summary>
        /// 充值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MeterRecharge()
        {
            object objResult = "";
            string strTotal_fee = Request.Form["totalfee"];
            string strFee = (double.Parse(strTotal_fee) * 100).ToString();

            //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
            jsApiPay.openid = Session["openid"].ToString();
            jsApiPay.total_fee = int.Parse(strFee);

            //JSAPI支付预处理
            try
            {
                string strBody = "自闭症家庭训练会员";//商品描述
                string attach = "帮助每一个自闭症患者，更有尊严的生活";//附件说明
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(strBody, attach);
                WxPayData wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数

                ModelForOrder aOrder = new ModelForOrder()
                {
                    appId = wxJsApiParam.GetValue("appId").ToString(),
                    nonceStr = wxJsApiParam.GetValue("nonceStr").ToString(),
                    packageValue = wxJsApiParam.GetValue("package").ToString(),
                    paySign = wxJsApiParam.GetValue("paySign").ToString(),
                    timeStamp = wxJsApiParam.GetValue("timeStamp").ToString(),
                    msg = "成功下单,正在接入微信支付."
                };
                objResult = aOrder;
            }
            catch (Exception ex)
            {
                ModelForOrder aOrder = new ModelForOrder()
                {
                    appId = "",
                    nonceStr = "",
                    packageValue = "",
                    paySign = "",
                    timeStamp = "",
                    msg = ex.ToString() + "下单失败，请重试,多次失败,请联系管理员."
                };
                objResult = aOrder;
            }
            return Json(objResult);
        }
    }

}