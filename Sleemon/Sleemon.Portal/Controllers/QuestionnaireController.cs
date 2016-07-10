using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ObjectBuilder2;
using Sleemon.Common;
using Sleemon.Core;
using Sleemon.Data;

namespace Sleemon.Portal.Controllers
{
    public class QuestionnaireController : BaseController
    {
        /// <summary>
        /// Exam list
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="questionnaireTitle"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int pageIndex, int pageSize, string questionnaireTitle)
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.CurrentUserId = UserUniqueId;

            var questionnaireList = ServiceClient.Request<IQuestionnaireService, IList<QuestionnaireListModel>>(
                        service => service.GetQuestionnaireList(pageIndex, pageSize, questionnaireTitle));
            return PartialView("QuestionnaireList", questionnaireList);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var questionnaire = ServiceClient.Request<IQuestionnaireService, QuestionnaireDetailModel>(
                        service => service.GetQuestionnaireDetailById(id));
            return PartialView("QuestionnaireDetail", questionnaire);
        }

        [HttpGet]
        public ActionResult QuestionnaireCreate(int id = 0)
        {
            var questionnaire = id == 0
                ? new QuestionnaireDetailModel()
                : ServiceClient.Request<IQuestionnaireService, QuestionnaireDetailModel>(
                    service => service.GetQuestionnaireDetailById(id));

            return PartialView("QuestionnaireCreate", questionnaire);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionnaireCreate(QuestionnaireDetailModel questionnaire)
        {
            var msg = "Save successful.";
            var validation = ValidateModelForQuestionnaire(questionnaire);
            if (string.IsNullOrEmpty(validation))
            {
                EnrichQuestionnaireDetailModel(questionnaire);
                var result = ServiceClient.Request<IQuestionnaireService, ResultBase>(
                    service => service.SaveQuestionnaireDetail(questionnaire));

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
            var questionnaire = ServiceClient.Request<IQuestionnaireService, QuestionnaireDetailModel>(
                    service => service.GetQuestionnaireDetailById(id));
            if (questionnaire != null && questionnaire.LastUpdateUser == UserUniqueId)
            {
                var result = ServiceClient.Request<IQuestionnaireService, ResultBase>(
                    service => service.DeleteQuestionnaireById(id));
                if (!result.IsSuccess)
                {
                    msg = result.Message;
                }
            }
            else
            {
                msg = @"Questionnaire info is not found.";
            }

            return new JsonResult() {Data = msg};
        }

        private string ValidateModelForQuestionnaire(QuestionnaireDetailModel questionnaire)
        {
            return string.Empty;
        }

        private void EnrichQuestionnaireDetailModel(QuestionnaireDetailModel questionnaire)
        {
            questionnaire.LastUpdateUser = UserUniqueId;

            for (int i = 0; i < questionnaire.Questions.Count; i++)
            {
                var question = questionnaire.Questions[i];
                question.No = (short)(i + 1);
            }
        }
    }
}