using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using WeChatTool.Dal;
using WeChatTool.Models;

namespace WeChatTool.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 上传首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 插入模板值和机构配置
        /// </summary>
        /// <returns></returns>
        public ActionResult SetConfig()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 上传微信授权文件
        /// </summary>
        /// <returns></returns>

        public  ActionResult  WechatUpload()
        {
            JsonResult jsResult = new JsonResult() { JsonRequestBehavior= JsonRequestBehavior.AllowGet };
            Stream uploadStream = null;
            FileStream fs = null;
            try
            {
                //文件上传，一次上传1M的数据，防止出现大文件无法上传  
                HttpPostedFileBase postFileBase = Request.Files["authFile"];//授权文件
                uploadStream = postFileBase.InputStream;
                int bufferLen = 1024;
                byte[] buffer = new byte[bufferLen];
                int contentLen = 0;

                string fileName = Path.GetFileName(postFileBase.FileName);
                if (postFileBase.FileName==""|| uploadStream.Length==0)
                {
                    jsResult.Data = "请选择文件之后再上传";
                    return jsResult;
                }
                string baseUrl = Server.MapPath("/");
                string uploadPath = baseUrl + @"Upload\";
                fs = new FileStream(uploadPath + fileName, FileMode.Create, FileAccess.ReadWrite);

                while ((contentLen = uploadStream.Read(buffer, 0, bufferLen)) != 0)
                {
                    fs.Write(buffer, 0, bufferLen);
                    fs.Flush();
                }


                //保存页面数据，上传的文件只保存路径  
                string productImage = "/Product/" + fileName;//保存路径
               

              
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            finally
            {
                if (null != fs)
                {
                    fs.Close();
                }
                if (null != uploadStream)
                {
                    uploadStream.Close();
                }
            }
           
            jsResult.Data = 1;
            jsResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return  View("SetConfig");


        }




        /// <summary>
        /// 进行微信设置
        /// </summary>
        /// <returns></returns>
        public JsonResult SetOrgWeChat(OrgWeChat orgWeChat)
        {
            JsonResult js=new JsonResult() {JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            string data = Request.Form.Get("data") ??"[]";
            //OrgStuClass para = JsonConvert.DeserializeObject<OrgStuClass>(data);
            string sqlOrg =
                "insert into `mfg_wechat_org` (`OrgID`, `ToUserID`, `AppID`, `AppSecret`, `IsSys`, `OrgUrl`, `IsEnable`, `QRCode`, `OrgMessage`, `CreateTime`) values(@OrgId,@ToUserID,@AppID,@AppSecret,'','www.mofangge.net','','0',@OrgMessage,now());insert into `mfg_wechat_template` (`OrgID`, `TemplateId`, `TemplateType`, `Messages`, `Remark`, `CreateTime`) values(@OrgId,@AddCourseId,'1','报课','报课',NOW());insert into `mfg_wechat_template` (`OrgID`, `TemplateId`, `TemplateType`, `Messages`, `Remark`, `CreateTime`) values(@OrgId, @CutCourseId, '2', '下课', '下课', NOW());insert into `mfg_wechat_template` (`OrgID`, `TemplateId`, `TemplateType`, `Messages`, `Remark`, `CreateTime`) values(@OrgId, @LogMsgId, '3', '监督提醒', '监督提醒',NOW());insert into `mfg_wechat_template` (`OrgID`, `TemplateId`, `TemplateType`, `Messages`, `Remark`, `CreateTime`) values(@OrgId, @BackCourseId, '4', '退课', '退课', NOW());";

            List<MySqlParameter>listPar=new List<MySqlParameter>();
            listPar.Add(new MySqlParameter("@OrgId", orgWeChat.OrgId));
            listPar.Add(new MySqlParameter("@ToUserID", orgWeChat.BegId));
            listPar.Add(new MySqlParameter("@AppID", orgWeChat.AppId));
            listPar.Add(new MySqlParameter("@AppSecret", orgWeChat.AppSecret));
            listPar.Add(new MySqlParameter("@OrgMessage", orgWeChat.OrgName));
            //模板
            listPar.Add(new MySqlParameter("@AddCourseId", orgWeChat.AddCourseId));
            listPar.Add(new MySqlParameter("@CutCourseId", orgWeChat.CutCourseId));
            listPar.Add(new MySqlParameter("@LogMsgId", orgWeChat.LogMsgId));
            listPar.Add(new MySqlParameter("@BackCourseId", orgWeChat.BackCourseId));



          int result=DBHelper.ExecSql(sqlOrg,listPar.ToArray());//执行添加
            js.Data = result;

            return js;

        }



    }
}