using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatTool.Models
{
    public class OrgWeChat
    {


        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        ///  机构名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 开发id
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 开发密码
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 原始id
        /// </summary>
        public string BegId { get; set; }
        /// <summary>
        /// 报课
        /// </summary>
        public string AddCourseId { get; set; }
        /// <summary>
        /// 下课
        /// </summary>
        public string CutCourseId { get; set; }
        /// <summary>
        /// 登录
        /// </summary>
        public string LogMsgId { get; set; }
        /// <summary>
        /// 退课
        /// </summary>
        public string BackCourseId { get; set; }

     



    }
}