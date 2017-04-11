using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{

    public class Setting
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 网站名
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 网站域名
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 版权
        /// </summary>
        public string Copyright { get; set; }
        /// <summary>
        /// 统计js
        /// </summary>
        public string Statistics { get; set; }

        /// <summary>
        /// 网站协议
        /// </summary>
        public string Protocol { get; set; }






        /// <summary>
        /// SEO Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// SEO Keywords
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// SEO Description
        /// </summary>
        public string Description { get; set; }





        /// <summary>
        /// 文件上传地址
        /// </summary>
        public string FileUploadUrl { get; set; }
        /// <summary>
        /// 编辑器上传地址
        /// </summary>
        public string EditorUploadUrl { get; set; }
        /// <summary>
        /// 图片上传地址
        /// </summary>
        public string ImgUploadUrl { get; set; }
        /// <summary>
        /// 头像上传地址
        /// </summary>
        public string AvatarUploadUrl { get; set; }
        /// <summary>
        /// 路径前缀
        /// </summary>
        public string BaseUrl { get; set; }



        /// <summary>
        /// 微信应用ID
        /// </summary>
        public string WXAppID { get; set; }
        /// <summary>
        /// 微信应用密钥
        /// </summary>
        public string WXAppSecret { get; set; }


        /// <summary>
        /// 微博应用ID
        /// </summary>
        public string WBAppID { get; set; }
        /// <summary>
        /// 微博应用密钥
        /// </summary>
        public string WBAppSecret { get; set; }

        /// <summary>
        /// QQ应用ID
        /// </summary>
        public string QQAppID { get; set; }
        /// <summary>
        /// QQ应用密钥
        /// </summary>
        public string QQAppSecret { get; set; }


        /// <summary>
        /// 第三方短信接口用户名
        /// </summary>
        public string MsgUserName { get; set; }
        /// <summary>
        /// 第三方短信接口密码
        /// </summary>
        public string MsgPassword { get; set; }

        /// <summary>
        /// 第三方短信接口
        /// </summary>
        public string MsgAPI { get; set; }




        /// <summary>
        /// 账号锁定分钟
        /// </summary>
        public int LockedMinutes { get; set; }

        /// <summary>
        /// 密码错误次数
        /// </summary>
        public int FailedPassword { get; set; }

        /// <summary>
        /// 验证码等待秒数
        /// </summary>
        public int CodeSeconds { get; set; }

        /// <summary>
        /// 验证码有效时间
        /// </summary>

        public int CodeMinutes { get; set; }





        /// <summary>
        /// 邮件服务器主机
        /// </summary>
        public string EmailHost { get; set; }

        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        public int EmailPort { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public string EmailFrom { get; set; }
        /// <summary>
        /// 指定用户名
        /// </summary>
        public string EmailUser { get; set; }

        /// <summary>
        /// 指定密码
        /// </summary>
        public string EmailPassword { get; set; }

        /// <summary>
        /// 认证链接超时分钟
        /// </summary>
        public int ActiveMinutes { get; set; }

        /// <summary>
        /// 验证码邮件标题
        /// </summary>
        public string EmailCodeTitle { get; set; }

        /// <summary>
        /// 验证码邮件内容
        /// </summary>
        public string EmailCodeContent { get; set; }

        /// <summary>
        /// 激活邮箱邮件标题
        /// </summary>
        public string EmailLinkTitle { get; set; }

        /// <summary>
        /// 激活邮件邮件内容
        /// </summary>
        public string EmailLinkContent { get; set; }

        /// <summary>
        /// 重置密码邮件标题
        /// </summary>
        public string ResetLinkTitle { get; set; }

        /// <summary>
        /// 重置密码邮件内容
        /// </summary>
        public string ResetLinkContent { get; set; }
    }

}
