using System.Collections.Generic;
using Sleemon.Core;

namespace Sleemon.Portal.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.ObjectBuilder2;
    using Sleemon.Common;
    using Sleemon.Data;
    using Sleemon.Portal.Common;
    using Sleemon.Portal.Core;

    public class ExamController : BaseController
    {
        /// <summary>
        /// Exam list
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="examTitle"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int pageIndex, int pageSize, string examTitle)
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.CurrentUserId = UserUniqueId;

            var examList = ServiceClient.Request<IExamService, IList<ExamListModel>>(
                service => service.GetExamList(pageIndex, pageSize, examTitle));

            return PartialView("ExamList", examList);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var exam =
                ServiceClient.Request<IExamService, ExamDetailModel>(
                    service => service.GetExamDetailById(id));

            return PartialView("ExamDetail", exam);
        }

        [HttpGet]
        public ActionResult ExamCreate(int id = 0)
        {
            var exam = id == 0
                ? new ExamDetailModel()
                : ServiceClient.Request<IExamService, ExamDetailModel>(service => service.GetExamDetailById(id));

            return PartialView("ExamCreate", exam);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCreate(ExamDetailModel exam)
        {
            var msg = "Save successful.";
            var validation = ValidateModelForExam(exam);
            if (string.IsNullOrEmpty(validation))
            {
                EnrichExamDetailModel(exam);
                var result =
                    ServiceClient.Request<IExamService, ResultBase>(service => service.SaveExamDetail(exam));
                if (!result.IsSuccess)
                {
                    msg = result.Message;
                }
            }
            else
            {
                msg = validation;
            }

            return new JsonResult() { Data = msg };
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteExam(int id)
        {
            var msg = "Delete successful.";
            var exam = ServiceClient.Request<IExamService, ExamDetailModel>(service => service.GetExamDetailById(id));
            if (exam != null && exam.LastUpdateUser == UserUniqueId)
            {
                var result = ServiceClient.Request<IExamService, ResultBase>(service => service.DeleteExamById(id));
                if (!result.IsSuccess)
                {
                    msg = result.Message;
                }
            }
            else
            {
                msg = @"Exam info is not found.";
            }

            return new JsonResult() {Data = msg};
        }

        private string ValidateModelForExam(ExamDetailModel exam)
        {
            return string.Empty;
        }
        
        private void EnrichExamDetailModel(ExamDetailModel exam)
        {
            exam.LastUpdateUser = UserUniqueId;

            for (int i = 0; i < exam.Questions.Count; i++)
            {
                var question = exam.Questions[i];
                question.No = (short) (i + 1);
                
                for (int j = 0; j < question.Choices.Count; j++)
                {
                    var choice = question.Choices[j];
                    choice.Choice = (byte)(j + 1);
                }

                var answers = question.Choices.Where(u => u.IsAnswer).ToList();
                question.CorrectAnswer = answers.Select(u => u.Choice).JoinStrings(",");
                question.Category = answers.Count == 1
                    ? (byte)ExamQuestionCategory.SingleOption
                    : (byte)ExamQuestionCategory.MultiOptions;
            }
        }
    }
}