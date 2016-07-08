namespace Sleemon.Portal.Controllers
{
    using Microsoft.Practices.Unity;
    /**
    *add by wolfgump 20160521
*/

    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using Sleemon.Data;
    using System.Web;
    using System.Security.Cryptography;
    using System.Web.Security;
    using System.Text;
    using System.IO;
    using Sleemon.Data.Models;
    using Sleemon.Common;
    using Sleemon.Portal.Core;
    using Sleemon.Portal.Common;

    
    public class EnterpriseNoticeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.pageIndex = 1;
            return PartialView("NoticeList");
        }

        [HttpGet]
        public ActionResult GetNoticeList(int pageIndex, int pageSize, string noticeTitle)
        {
            //ViewBag.pageIndex = pageIndex;
            //int totalCount = 0;
            NoticeListModel list = new NoticeListModel();
            //var noticeList = enterpriseNoticeModelClient.GetEnterpriseNotices(pageIndex, pageSize, noticeTitle, out totalCount);
            //list.NoticeList = noticeList;
            //list.PageIndex = pageIndex;
            //list.PageSize = pageSize;
            //list.TotalCount = totalCount;

            //ViewBag.pageIndex = pageIndex;
            //ViewBag.noticeTitle = noticeTitle;

            return PartialView("NoticeList", list);
        }

        // GET: EnterpriseNotice
        public ActionResult Create()
        {
            var enModel = new EnterpriseNoticeSubmitModel();
            var result = new ResultBase();
            result.StatusCode = -2;
            result.Message = "初次加载";
            enModel.Result = result;
            // ViewData["NoticeType"] = new SelectList(GetNoticeType(), "Value", "Text", "");
            return PartialView(enModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SubmitEnterpriseNotice(EnterpriseNoticeSubmitModel enModel)
        {
            //var result = new ResultBase();
            //if (string.IsNullOrEmpty(enModel.Subject) || enModel.Subject.Length > 25)
            //{
            //    result.StatusCode = -1;
            //    result.Message = "主题不能为空且不能超过25个字";
            //    enModel.Result = result;
            //    return PartialView("Create", enModel);
            //}

            //if (string.IsNullOrEmpty(enModel.Context))
            //{
            //    result.StatusCode = -1;
            //    result.Message = "内容不能为空";
            //    enModel.Result = result;
            //    return PartialView("Create", enModel);
            //}

            ////存储之前将Html做Encode
            //enModel.Context = HttpUtility.HtmlEncode(enModel.Context);

            //if (enModel.NoticeType == 0)
            //{
            //    result.StatusCode = -1;
            //    result.Message = "请选择咨讯类型";
            //    enModel.Result = result;
            //    return PartialView("Create", enModel);
            //}
            //if (enModel.Category == 0)
            //{
            //    result.StatusCode = -1;
            //    result.Message = "请选择咨讯显示类型";
            //    enModel.Result = result;
            //    return PartialView("Create", enModel);
            //}
            //if (string.IsNullOrEmpty(enModel.Summary))
            //{
            //    int subLength = enModel.Context.Length >= 50 ? 50 : enModel.Context.Length;
            //    enModel.Summary = enModel.Context.Substring(0, subLength);
            //}
            //enModel.LastUpdateTime = DateTime.Now;
            //enModel.IsActive = true;
            ////enModel.Category = enModel.IsTopText == "on";//Comment by Jesse: Istop changed to Category. Category values: 1- Normal; 2- Top; 3- Slide. TODO: this filed need update
            //bool resultSubmit = enterpriseNoticeModelClient.SubmitEnterpriseNotice(enModel);
            //if (!resultSubmit)
            //{
            //    result.StatusCode = -1;
            //    result.Message = "提交失败";
            //    enModel.Result = result;
            //    return PartialView("Create", enModel);
            //}
            //result.StatusCode = 0;
            //result.Message = "提交成功";
            //enModel.Result = result;
            //// ViewData["NoticeType"] = new SelectList(GetNoticeType(), "Value", "Text", enModel.NoticeType);
            return PartialView("Create", enModel);
        }

        public JsonResult UpLoadImageFile()
        {
            var model = new UpLoadFileModel();
            var result = new ResultBase();

            if (Request.Files.Count <= 0)
            {
                result.StatusCode = -1;
                result.Message = "请选择要上传的图片";
                model.Result = result;
            }
            else
            {
                string message;
                var file = Request.Files[0];

                if (Utilities.UpLoadImageFile(file, Server, out message))
                {
                    result.StatusCode = 0;
                    result.Message = "上传成功";
                    model.Result = result;
                    model.filePath = message;
                }
                else
                {
                    result.StatusCode = -1;
                    result.Message = message;
                    model.Result = result;
                }
            }
            return new JsonResult() { Data = model };
        }
        

        /// <summary>
        /// 验证是否指定的图片格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsImage(string str)
        {
            var isimage = false;
            var thestr = str.ToLower();
            //限定只能上传jpg和gif图片
            string[] allowExtension = { ".jpg", ".gif", ".bmp", ".png" };
            //对上传的文件的类型进行一个个匹对
            for (var i = 0; i < allowExtension.Length; i++)
            {
                if (thestr == allowExtension[i])
                {
                    isimage = true;
                    break;
                }
            }
            return isimage;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string Md5Hash(string input)
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        //public List<SelectListItem> GetNoticeType()
        //{
        //    string[] textArr = { "--请选择资讯类型--", "资讯", "活动", "推广" };
        //    string[] valueArr = { "", "1", "2", "3" };
        //    List<SelectListItem> selNoticeType = new List<SelectListItem>();
        //    for (int i = 0; i < valueArr.Length; i++)
        //    {
        //        selNoticeType.Add(new SelectListItem
        //        {
        //            Text = textArr[i].ToString(),
        //            Value = valueArr[i].ToString()
        //        });
        //    }
        //    return selNoticeType;
        //}
    }
}