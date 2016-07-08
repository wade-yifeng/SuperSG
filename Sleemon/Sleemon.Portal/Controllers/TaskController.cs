using System.Collections.Generic;
using Sleemon.Common;
using Sleemon.Core;

namespace Sleemon.Portal.Controllers
{
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Sleemon.Data;

    public class TaskController : BaseController
    {
        #region Task shared

        [HttpGet]
        public ActionResult Index(TaskSearchContext condition)
        {
            condition.BelongTo = (int)TaskBelongTo.SingleTask;
            ViewBag.PageIndex = condition.PageIndex;
            ViewBag.CurrentUserId = UserUniqueId;

            var tasks = ServiceClient.Request<ITaskService, IList<TaskListModel>>(
                service => service.GetTaskList(condition));

            return PartialView("TaskList", tasks);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreateTask(TaskDetailsModel task)
        {
            var msg = "Save successful.";
            var validation = ValidateModelForTask(task);
            if (string.IsNullOrEmpty(validation))
            {
                EnrichTaskDetailModel(task);
                var result = ServiceClient.Request<ITaskService, ResultBase>(
                    service => service.SaveTaskDetail(task));
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
        public ActionResult DeleteTask(int id)
        {
            var msg = "Delete successful.";
            var task = ServiceClient.Request<ITaskService, TaskDetailsModel>(
                    service => service.GetTaskDetailById(id));
            if (task != null && task.Status == (byte)ActionCategory.Save &&
                task.LastUpdateUser == UserUniqueId)
            {
                var result = ServiceClient.Request<ITaskService, ResultBase>(
                    service => service.DeleteTaskById(id));
                if (!result.IsSuccess)
                {
                    msg = result.Message;
                }
            }
            else
            {
                msg = @"Task info is not found.";
            }

            return new JsonResult() { Data = msg };
        }

        #endregion

        #region Exam tasks

        [HttpGet]
        public ActionResult ExamTaskDetail(int id)
        {
            var task = ServiceClient.Request<ITaskService, TaskDetailsModel>(
                    service => service.GetTaskDetailById(id));

            return PartialView("ExamTaskDetail", task);
        }

        [HttpGet]
        public ActionResult ExamTaskCreate(int id = 0)
        {
            var task = id == 0
                ? new TaskDetailsModel()
                : ServiceClient.Request<ITaskService, TaskDetailsModel>(
                    service => service.GetTaskDetailById(id));

            return PartialView("ExamTaskCreate", task);
        }

        #endregion

        #region Learning tasks
        
        [HttpGet]
        public ActionResult LearningTaskDetail(int id)
        {
            var task = ServiceClient.Request<ITaskService, TaskDetailsModel>(
                service => service.GetTaskDetailById(id));

            return PartialView("LearningTaskDetail", task);
        }

        [HttpGet]
        public ActionResult LearningTaskCreate(int id = 0)
        {
            var task = id == 0
                ? new TaskDetailsModel()
                : ServiceClient.Request<ITaskService, TaskDetailsModel>(
                    service => service.GetTaskDetailById(id));
            return PartialView("LearningTaskCreate", task);
        }

        #endregion

        #region Questionnaire tasks

        [HttpGet]
        public ActionResult QuestionnaireTaskDetail(int id)
        {
            var task = ServiceClient.Request<ITaskService, TaskDetailsModel>(
                service => service.GetTaskDetailById(id));

            return PartialView("QuestionnaireTaskDetail", task);
        }

        [HttpGet]
        public ActionResult QuestionnaireTaskCreate(int id = 0)
        {
            var task = id == 0
                ? new TaskDetailsModel()
                : ServiceClient.Request<ITaskService, TaskDetailsModel>(
                    service => service.GetTaskDetailById(id));

            return PartialView("QuestionnaireTaskCreate", task);
        }
        
        #endregion
        
        private void EnrichTaskDetailModel(TaskDetailsModel task)
        {
            task.LastUpdateUser = UserUniqueId;
            task.BelongTo = (int)TaskBelongTo.SingleTask;

            if (task.Status == (byte)ActionCategory.Publish)
            {
                task.DispatchSubject = task.Title;
                task.DispatchType = (byte)MsgDispatchType.Immediate;
                task.DispatchPriority = 1;

                if (task.Exams!=null)
                {
                    foreach (var exam in task.Exams)
                    {
                        exam.LastUpdateUser = UserUniqueId;
                    }
                }

                if (task.LearningFiles != null)
                {
                    foreach (var learningFile in task.LearningFiles)
                    {
                        learningFile.LastUpdateUser = UserUniqueId;
                    }
                }

                if (task.Questionnaires != null)
                {
                    foreach (var questionnaire in task.Questionnaires)
                    {
                        questionnaire.LastUpdateUser = UserUniqueId;
                    }
                }
            }
        }

        private string ValidateModelForTask(TaskDetailsModel task)
        {
            return string.Empty;
        }
    }
}