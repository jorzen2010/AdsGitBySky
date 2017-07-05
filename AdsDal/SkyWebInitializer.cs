using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AdsEntity;
using Common;

namespace AdsDal
{
    public class SkyWebInitializer : DropCreateDatabaseIfModelChanges<SkyWebContext>
    {
        protected override void Seed(SkyWebContext context)
        {
            base.Seed(context);

            #region 初始化配置

            var protocol = new System.Text.StringBuilder(942);
            protocol.AppendLine(@"<p> 当您申请用户时，表示您已经同意遵守本规章。");
            protocol.AppendLine(@"    欢迎您加入本站点参加交流和讨论，本站点为公共论坛，为维护网上公共秩序和社会稳定，请您自觉遵守以下条款：</p>");
            protocol.AppendLine(@"   <ul> ");
            protocol.AppendLine(@"       <li>一、不得利用本站危害国家安全、泄露国家秘密，不得侵犯国家社会集体的和公民的合法权益，不得利用本站制作、复制和传播下列信息：");
            protocol.AppendLine(@"           <ol>");
            protocol.AppendLine(@"               <li>（一）煽动抗拒、破坏宪法和法律、行政法规实施的；</li>");
            protocol.AppendLine(@"               <li>（二）煽动颠覆国家政权，推翻社会主义制度的；</li>");
            protocol.AppendLine(@"               <li>（三）煽动分裂国家、破坏国家统一的；</li>");
            protocol.AppendLine(@"               <li>（四）煽动民族仇恨、民族歧视，破坏民族团结的；</li>");
            protocol.AppendLine(@"               <li>（五）捏造或者歪曲事实，散布谣言，扰乱社会秩序的；</li>");
            protocol.AppendLine(@"               <li>（六）宣扬封建迷信、淫秽、色情、赌博、暴力、凶杀、恐怖、教唆犯罪的；</li>");
            protocol.AppendLine(@"               <li>（七）公然侮辱他人或者捏造事实诽谤他人的，或者进行其他恶意攻击的；</li>");
            protocol.AppendLine(@"               <li>（八）损害国家机关信誉的；</li>");
            protocol.AppendLine(@"               <li>（九）其他违反宪法和法律行政法规的；</li>");
            protocol.AppendLine(@"               <li>（十）进行商业广告行为的。</li>");
            protocol.AppendLine(@"           </ol>");
            protocol.AppendLine(@"       </li>");
            protocol.AppendLine(@"    <li>二、互相尊重，对自己的言论和行为负责。</li>");
            protocol.AppendLine(@"    <li>三、禁止在申请用户时使用相关本站的词汇，或是带有侮辱、毁谤、造谣类的或是有其含义的各种语言进行注册用户，否则我们会将其删除。</li>");
            protocol.AppendLine(@"    <li>四、禁止以任何方式对本站进行各种破坏行为。</li>");
            protocol.AppendLine(@"   <li> 五、如果您有违反国家相关法律法规的行为，本站概不负责，您的登录论坛信息均被记录无疑，必要时，我们会向相关的国家管理部门提供此类信息。</li>");
            protocol.AppendLine(@"   </ul>");

            var copyright = new System.Text.StringBuilder(143);
            copyright.AppendLine(@"Copyright©2008-2011 赵征 Co.,Ltd, All Rights Reserved  版权所有");
            copyright.AppendLine(@"增值电信业务经营许可证京-2-4-20090003 广播电视节目制作经营许可证 文网文 [2009]011号");

            context.Settings.Add(new Setting
            {
                SiteName = "自闭症家庭训练系统",
                DomainName = "http://www.zzd123.com",
                Logo = "Upload/Site/logo.jpg",
                Protocol = protocol.ToString(),
                Title = "自闭症家庭训练系统",
                Keywords = "自闭症家庭训练系统",
                Description = "自闭症家庭训练系统",
                Copyright = copyright.ToString(),
                Statistics = string.Empty,

                FileUploadUrl = "~/File/Upload/",
                ImgUploadUrl = "~/File/Upload/",
                EditorUploadUrl = "~/File/Upload/",
                AvatarUploadUrl = "~/File/Upload/",
                BaseUrl = "~",



                FailedPassword = 5,
                CodeSeconds = 60,
                CodeMinutes = 2,
                LockedMinutes = 5,


                EmailHost = "smtp.126.com",
                EmailPort = 25,
                EmailFrom = "ccvixo@126.com",
                EmailUser = "ccvixo",
                EmailPassword = "1qaz2wsx",
                ActiveMinutes = 30,
                EmailCodeTitle = "验证码邮件",
                EmailCodeContent = "<p>您的验证码是:</p><span style=\"font-size:14px;\">{0}</span>",
                EmailLinkTitle = "激活邮件",
                EmailLinkContent = "<p>点击链接激活:</p><a href=\"{0}\">链接：{1}</a>",
                ResetLinkTitle = "重置邮件",
                ResetLinkContent = "<p>点击链接重置:</p><a href=\"{0}\">链接：{1}</a>",


            });

            context.SaveChanges();

            #endregion 初始化配置

            #region 用户初始化
            var sysUsers = new List<SysUser>
            {
                new SysUser{SysUserName="Tom",SysPassword=CommonTools.ToMd5("111111"),SysEmail="Tom@163.com",SysStatus=true},
                new SysUser{SysUserName="Jerry",SysPassword=CommonTools.ToMd5("111111"),SysEmail="Tom@163.com",SysStatus=true},
                new SysUser{SysUserName="Jeem",SysPassword=CommonTools.ToMd5("1111111"),SysEmail="Tom@163.com",SysStatus=true}
            };
            sysUsers.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();

            #endregion 初始化配置

            #region 量表初始化

            var scales = new List<Scale>
            {
                new Scale{ScaleName="自闭症行为ABC量表",ScaleInfo="自闭症行为量表简介"},
            };
            scales.ForEach(s => context.Scales.Add(s));
            context.SaveChanges();

            #endregion 量表初始化

            #region 分类初始化


            var categorys = new List<Category>
            {
                new Category{CategoryName="顶级分类",CategoryIcon="glyphicon glyphicon-list",CategoryInfo="顶级分类",CategoryParentID=0,CategoryStatus=true,CategorySort=0},

                new Category{CategoryName="视频类型",CategoryIcon="glyphicon glyphicon-list",CategoryInfo="视频类型",CategoryParentID=1,CategoryStatus=true,CategorySort=0},

                new Category{CategoryName="训练项目",CategoryIcon="glyphicon glyphicon-list",CategoryInfo="训练项目",CategoryParentID=2,CategoryStatus=true,CategorySort=0},
                new Category{CategoryName="心理服务",CategoryIcon="glyphicon glyphicon-list",CategoryInfo="心理服务",CategoryParentID=2,CategoryStatus=true,CategorySort=0},



                new Category{CategoryName="感觉能力",CategoryIcon="glyphicon glyphicon-bell",CategoryInfo="感觉能力",CategoryParentID=3,CategoryStatus=true,CategorySort=0},
                new Category{CategoryName="交往能力",CategoryIcon="glyphicon glyphicon-bed",CategoryInfo="交往能力",CategoryParentID=3,CategoryStatus=true,CategorySort=0},
                new Category{CategoryName="运动能力",CategoryIcon="glyphicon glyphicon-fire",CategoryInfo="运动能力",CategoryParentID=3,CategoryStatus=true,CategorySort=0},
                new Category{CategoryName="语言能力",CategoryIcon="glyphicon glyphicon-headphones",CategoryInfo="语言能力",CategoryParentID=3,CategoryStatus=true,CategorySort=0},
                new Category{CategoryName="自理能力",CategoryIcon="glyphicon glyphicon-glass",CategoryInfo="自理能力",CategoryParentID=3,CategoryStatus=true,CategorySort=0},


                new Category{CategoryName="焦虑",CategoryIcon="glyphicon glyphicon-list",CategoryInfo="心理服务",CategoryParentID=4,CategoryStatus=true,CategorySort=0},
                new Category{CategoryName="夫妻关系",CategoryIcon="glyphicon glyphicon-list",CategoryInfo="心理服务",CategoryParentID=4,CategoryStatus=true,CategorySort=0},
                new Category{CategoryName="亲子关系",CategoryIcon="glyphicon glyphicon-list",CategoryInfo="心理服务",CategoryParentID=4,CategoryStatus=true,CategorySort=0},

            };
            categorys.ForEach(s => context.Categorys.Add(s));
            context.SaveChanges();

            var adsVideos = new List<AdsVideo>
            {
                new AdsVideo{ VideoNumber="ceshi123456789",VideoName="感觉能力视频",VideoPhoto="暂无",VideoUrl="61823886c6614369a10025d6fd4fff07",VideoInfo="感觉能力视频",VideoFor="感觉能力视频",VideoBeizhu="暂无",
                VideoCategory=5,VideoPrice=100,VideoVIPPrice=99,VideoZKPrice=99,VideoFree=true,VideoTry=true,VideoTime=DateTime.Now,VideoTeacher="贺苏",VideoViewTimes=100,VideoWeight=0.3f},
                new AdsVideo{ VideoNumber="ceshi123456789",VideoName="交往能力视频",VideoPhoto="暂无",VideoUrl="61823886c6614369a10025d6fd4fff07",VideoInfo="感觉能力视频",VideoFor="感觉能力视频",VideoBeizhu="暂无",
                VideoCategory=6,VideoPrice=100,VideoVIPPrice=99,VideoZKPrice=99,VideoFree=true,VideoTry=true,VideoTime=DateTime.Now,VideoTeacher="贺苏",VideoViewTimes=100,VideoWeight=0.3f},
                new AdsVideo{ VideoNumber="ceshi123456789",VideoName="运动能力视频",VideoPhoto="暂无",VideoUrl="61823886c6614369a10025d6fd4fff07",VideoInfo="感觉能力视频",VideoFor="感觉能力视频",VideoBeizhu="暂无",
                VideoCategory=7,VideoPrice=100,VideoVIPPrice=99,VideoZKPrice=99,VideoFree=true,VideoTry=true,VideoTime=DateTime.Now,VideoTeacher="贺苏",VideoViewTimes=100,VideoWeight=0.3f},
                new AdsVideo{ VideoNumber="ceshi123456789",VideoName="语言能力视频",VideoPhoto="暂无",VideoUrl="61823886c6614369a10025d6fd4fff07",VideoInfo="感觉能力视频",VideoFor="感觉能力视频",VideoBeizhu="暂无",
                VideoCategory=8,VideoPrice=100,VideoVIPPrice=99,VideoZKPrice=99,VideoFree=true,VideoTry=true,VideoTime=DateTime.Now,VideoTeacher="贺苏",VideoViewTimes=100,VideoWeight=0.3f},
                new AdsVideo{ VideoNumber="ceshi123456789",VideoName="自理能力视频",VideoPhoto="暂无",VideoUrl="61823886c6614369a10025d6fd4fff07",VideoInfo="感觉能力视频",VideoFor="感觉能力视频",VideoBeizhu="暂无",
                VideoCategory=9,VideoPrice=100,VideoVIPPrice=99,VideoZKPrice=99,VideoFree=true,VideoTry=true,VideoTime=DateTime.Now,VideoTeacher="贺苏",VideoViewTimes=100,VideoWeight=0.3f},
                new AdsVideo{ VideoNumber="ceshi123456789",VideoName="心理服务视频",VideoPhoto="暂无",VideoUrl="61823886c6614369a10025d6fd4fff07",VideoInfo="感觉能力视频",VideoFor="感觉能力视频",VideoBeizhu="暂无",
                VideoCategory=10,VideoPrice=100,VideoVIPPrice=99,VideoZKPrice=99,VideoFree=true,VideoTry=true,VideoTime=DateTime.Now,VideoTeacher="贺苏",VideoViewTimes=100,VideoWeight=0.3f},

         
            
            };
            adsVideos.ForEach(s => context.AdsVideos.Add(s));
            context.SaveChanges();

            #endregion



        }
    }
}
