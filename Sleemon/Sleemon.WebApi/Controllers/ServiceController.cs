using Sleemon.Core;
using Sleemon.WebApi.Core;

namespace Sleemon.WebApi.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using Sleemon.Common;
    using Sleemon.Data;

    using Get = System.Web.Http.HttpGetAttribute;
    using Post = System.Web.Http.HttpPostAttribute;

    public class ServiceController : ApiController
    {
        private readonly ImplementServiceClient _serviceClient;

        public ServiceController([Dependency] ImplementServiceClient serviceClient)
        {
            this._serviceClient = serviceClient;
        }

        #region Message ...

        [Get]
        public IList<MessageViewModel> GetBroadcastMessage(int maxCount)
        {
            return _serviceClient.Request<IMessageService, IList<MessageViewModel>>(
                service => service.GetBroadcastMessage(maxCount));
        }

        #endregion

        #region User ...

        /// <summary>
        /// 同步用户
        /// </summary>
        [Post]
        public SyncResult SyncUser(IEnumerable<UserProfile> userlist)
        {
            return _serviceClient.Request<IUserService, SyncResult>(
                service => service.SyncUser(userlist ?? new List<UserProfile>()));
        }

        [Post]
        public UserDailySignInModel UserDailySignIn(JObject param)
        {
            return _serviceClient.Request<IUserService, UserDailySignInModel>(
                service => service.UserDailySignIn(JObjectHelper.TryGetValue<string>(param, "userId")));
        }

        [Get]
        public UserViewModel GetUserInfoById(string userId)
        {
            return _serviceClient.Request<IUserService, UserViewModel>(
                service => service.GetUserInfoById(userId));
        }

        [Post]
        public ResultBase UpdateUserProfile(UserProfile model)
        {
            return _serviceClient.Request<IUserService, ResultBase>(
                service => service.UpdateUserProfile(model));
        }

        #endregion

        #region Department ...

        /// <summary>
        /// 同步部门
        /// </summary>
        [Post]
        public SyncResult SyncDepartment(IEnumerable<DepartmentSyncModel> department)
        {
            return  _serviceClient.Request<IDepartmentService, SyncResult>(
                service => service.SyncDepartment(department ?? new List<DepartmentSyncModel>()));
        }

        #endregion

        #region Notice ..

        /// <summary>
        /// [资讯] - 获取资讯详情
        /// </summary>
        [Get]
        public EnterpriseNoticeDetailModel GetNoticeById(int noticeId)
        {
            return _serviceClient.Request<IEnterpriseNoticeService, EnterpriseNoticeDetailModel>(
                service => service.GetEnterpriseNoticeById(noticeId));
        }

        /// <summary>
        /// [资讯] - 获取资讯列表
        /// </summary>
        [Get]
        public IList<EnterpriseNoticePreviewModel> GetNoticeList(int pageIndex, int pageSize)
        {
            return _serviceClient.Request<IEnterpriseNoticeService, IList<EnterpriseNoticePreviewModel>>(
                service => service.GetEnterpriseSummeryNotices(pageIndex, pageSize));
        }

        /// <summary>
        /// [资讯] - 获取滚动资讯
        /// </summary>
        [Get]
        public IList<EnterpriseNoticePreviewModel> GetSlideNoticeList(int topCount = 5)
        {
            return _serviceClient.Request<IEnterpriseNoticeService, IList<EnterpriseNoticePreviewModel>>(
                service => service.GetSlideEnterpriseSummeryNotices(topCount));
        }

        /// <summary>
        /// [资讯] - 获取资讯热门评论
        /// </summary>
        [Get]
        public IList<UserComment> GetTopCommentsByNoticeId(int noticeId, string userId, int topCount = 3)
        {
            return _serviceClient.Request<IEnterpriseNoticeService, IList<UserComment>>(
                service => service.GetTopCommentsByNoticeId(noticeId, userId, topCount));
        }

        /// <summary>
        /// [资讯] - 获取资讯最新评论
        /// </summary>
        [Get]
        public PagedData<UserComment> GetPagedCommentsByNoticeId(int noticeId, string userId, int pageIndex, int pageSize)
        {
            var totalCount = 0;

            return new PagedData<UserComment>()
            {
                Data = _serviceClient.Request<IEnterpriseNoticeService, IList<UserComment>>(
                    service => service.GetPagedCommentsByNoticeId(noticeId, userId, pageIndex, pageSize, out totalCount)),
                TotalCount = totalCount
            };
        }

        /// <summary>
        /// [资讯] - 顶资讯
        /// </summary>
        [Post]
        public ProConNoticeResult ProNoticeById(JObject param)
        {
            return _serviceClient.Request<IEnterpriseNoticeService, ProConNoticeResult>(
                service => service.ProNoticeById(
                    JObjectHelper.TryGetValue<int>(param, "noticeId"),
                    JObjectHelper.TryGetValue<string>(param, "userId")));
        }

        /// <summary>
        /// [资讯] - 踩资讯
        /// </summary>
        [Post]
        public ProConNoticeResult ConNoticeById(JObject param)
        {
            return _serviceClient.Request<IEnterpriseNoticeService, ProConNoticeResult>(
                service => service.ConNoticeById(
                    JObjectHelper.TryGetValue<int>(param, "noticeId"),
                    JObjectHelper.TryGetValue<string>(param, "userId")));
        }

        /// <summary>
        /// [资讯] - 赞评论
        /// </summary>
        [Post]
        public ProCommentResult ProCommentById(JObject param)
        {
            return _serviceClient.Request<IEnterpriseNoticeService, ProCommentResult>(
                service => service.ProCommentById(
                    JObjectHelper.TryGetValue<int>(param, "commentId"),
                    JObjectHelper.TryGetValue<string>(param, "userId")));
        }

        /// <summary>
        /// [资讯] - 获取最新资讯
        /// </summary>
        [Get]
        public IList<EnterpriseNoticePreviewModel> GetLatestNotices(int previousLatestNoticeId)
        {
            return _serviceClient.Request<IEnterpriseNoticeService, IList<EnterpriseNoticePreviewModel>>(
                service => service.GetLatestNotices(previousLatestNoticeId));
        }

        /// <summary>
        /// [资讯] - 创建评论
        /// </summary>
        [Post]
        public NewCommentViewModel AddNoticeComment(JObject param)
        {
            return _serviceClient.Request<IEnterpriseNoticeService, NewCommentViewModel>(
                service => service.AddNoticeComment(
                    JObjectHelper.TryGetValue<int>(param, "noticeId"),
                    JObjectHelper.TryGetValue<string>(param, "userId"),
                    JObjectHelper.TryGetValue<string>(param, "comment"),
                    JObjectHelper.TryGetValue<byte>(param, "category")));
        }

        #endregion

        #region Task ...

        /// <summary>
        /// [任务] - 检索用户任务列表
        /// </summary>
        [Get]
        public IList<UserTaskModel> SearchUserTaskList(string userId, byte listType, string input)
        {
            return _serviceClient.Request<ITaskService, IList<UserTaskModel>>(
                service => service.SearchUserTaskList(userId, listType, input));
        }

        /// <summary>
        /// [任务] - 获取用户任务列表
        /// </summary>
        [Get]
        public IList<UserTaskInfo> GetUserTaskList(string userId, byte listType, int pageIndex, int pageSize)
        {
            return _serviceClient.Request<ITaskService, IList<UserTaskInfo>>(
                service => service.GetUserTaskList(userId, listType, pageIndex, pageSize).ToList());
        }

        /// <summary>
        /// [任务] - 获取用户任务详情
        /// </summary>
        [Get]
        public UserTaskDetailsModel GetUserTaskDetail(int userTaskId)
        {
            return _serviceClient.Request<ITaskService, UserTaskDetailsModel>(
                service => service.GetUserTaskDetail(userTaskId));
        }

        /// <summary>
        /// [任务] - 获取用户任务状态
        /// </summary>
        /// <param name="userTaskId"></param>
        /// <returns></returns>
        [Get]
        public byte GetUserTaskStatus(int userTaskId)
        {
            var userTask = _serviceClient.Request<ITaskService, UserTaskDetailsModel>(
                service => service.GetUserTaskDetail(userTaskId));

            return userTask == null ? (byte)0 : userTask.Status;
        }

        /// <summary>
        /// [任务] - 更新用户任务状态
        /// </summary>
        [Post]
        public ResultBase UpdateUserTaskStatus(JObject param)
        {
            return _serviceClient.Request<ITaskService, ResultBase>(
                service => service.UpdateUserTaskStatus(JObjectHelper.TryGetValue<int>(param, "userTaskId"),
                JObjectHelper.TryGetValue<byte>(param, "userTaskStatus")));
        }

        /// <summary>
        /// [任务] - [考试] - 获取考试任务其它信息
        /// </summary>
        [Get]
        public ExamPreviewModel GetExamTaskOtherInfo(int taskId)
        {
            return _serviceClient.Request<ITaskService, ExamPreviewModel>(
                service => service.GetExamTaskOtherInfo(taskId));
        }

        /// <summary>
        /// [任务] - [考试] - 获取考试题目
        /// </summary>
        [Get]
        public IList<ExamQuestionModel> GetExamQuestions(int taskId)
        {
            return _serviceClient.Request<ITaskService, IList<ExamQuestionModel>>(
                service => service.GetExamQuestions(taskId));
        }

        /// <summary>
        /// [任务] - [考试] - 获取用户考试答案
        /// </summary>
        [Get]
        public IList<ExamAnswerModel> GetUserExamAnswers(int taskId, string userUniqueId)
        {
            return _serviceClient.Request<ITaskService, IList<ExamAnswerModel>>(
                service => service.GetUserExamAnswers(taskId, userUniqueId));
        }

        /// <summary>
        /// [任务] - [考试] - 答题
        /// </summary>
        [Post]
        public ResultBase CommitSingleExamQuestion(JObject param)
        {
            return _serviceClient.Request<ITaskService, ResultBase>(
                service => service.CommitSingleExamQuestion(
                    JObjectHelper.TryGetValue<int>(param, "taskId"),
                    JObjectHelper.TryGetValue<string>(param, "userUniqueId"),
                    JObjectHelper.TryGetValue<int>(param, "examQuestionId"),
                    JObjectHelper.TryGetValue<string>(param, "myAnswer")));
        }

        /// <summary>
        /// [任务] - [考试] - 交卷
        /// </summary>
        [Post]
        public ExamResultModel CommitEntireExam(JObject param)
        {
            return
                _serviceClient.Request<ITaskService, ExamResultModel>(
                service => service.CommitEntireExam(
                    JObjectHelper.TryGetValue<string>(param, "userUniqueId"),
                    JObjectHelper.TryGetValue<int>(param, "userTaskId")));
        }

        /// <summary>
        /// GetQuestionnaireQuestionCount
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [Get]
        public int GetQuestionnaireQuestionCount(int taskId)
        {
            var questions = _serviceClient.Request<IQuestionnaireService, IList<QuestionnaireQuestionModel>>(
                service => service.GetQuestionnaireQuestions(taskId));

            return questions.Count;
        }

        [Get]
        public PagedData<TaskListModel> TaskBasicInfoList(TaskSearchContext searchContext)
        {
            var tasks = _serviceClient.Request<ITaskService, IList<TaskListModel>>(
                service => service.GetTaskList(searchContext));

            return new PagedData<TaskListModel>()
            {
                Data = tasks,
                TotalCount = tasks == null || tasks.Count == 0 ? 0 : tasks[0].TotalCount
            };
        }

        [Get]
        public TaskDetailsModel GetTaskBasicInfoDetailById(int taskId)
        {
            return _serviceClient.Request<ITaskService, TaskDetailsModel>(
                service => service.GetTaskDetailById(taskId));
        }

        #endregion

        #region Exam ...

        [Get]
        public IList<ExamListModel> GetExamList(int pageIndex, int pageSize, string examTitle)
        {
            return _serviceClient.Request<IExamService, IList<ExamListModel>>(
                    service => service.GetExamList(pageIndex, pageSize, examTitle));
        }

        [Get]
        public ExamDetailModel GetExamDetailById(int id)
        {
            return _serviceClient.Request<IExamService, ExamDetailModel>(
                    service => service.GetExamDetailById(id));
        }

        #endregion

        #region StorePatrol ...

        [Get]
        public IList<int> GetTaskPatrolCategories(int taskId)
        {
            return _serviceClient.Request<IStorePatrolService, IList<int>>(
                    service => service.GetTaskPatrolCategories(taskId));
        }

        [Get]
        public UserStorePatrolDetailModel GetUserStorePatrolDetails(int userTaskId)
        {
            return _serviceClient.Request<IStorePatrolService, UserStorePatrolDetailModel>(
                    service => service.GetUserStorePatrolDetails(userTaskId));
        }

        [Get]
        public PagedData<UserStorePatrolPreviewModel> GetStorePatrolList(int pageIndex, int pageSize, string userName, DateTime? startFrom, DateTime? endTo)
        {
            var count = 0;
            var result = _serviceClient.Request<IStorePatrolService, IList<UserStorePatrolPreviewModel>>(
                    service => service.GetStorePatrolList(pageIndex, pageSize, userName, startFrom, endTo, out count));

            return new PagedData<UserStorePatrolPreviewModel>()
            {
                Data = result.ToList(),
                TotalCount = count
            };
        }

        [Post]
        public ResultBase UploadStorePatrol(UploadStorePatrolContext context)
        {
            return _serviceClient.Request<IStorePatrolService, ResultBase>(
                    service => service.UpLoadStorePatrol(context.UserUniqueId, context.UserStorePatrols));
        }

        [Post]
        public ResultBase PointStorePatrol(PointStorePatrolContext context)
        {
            return _serviceClient.Request<IStorePatrolService, ResultBase>(
                    service => service.PointStorePatrol(context.IsPass, context.UserStorePatrols));
        }

        //TODO: Confirm this api can be remove?
        //[Get]
        //public UserStorePatrolDetailsModel GetStorePatrolDetail(int taskId, string userUniqueId)
        //{
        //    return this.storePatrolModelClient.GetStorePatrolDetail(taskId, userUniqueId);
        //}

        [Get]
        public IList<StorePatrolSceneModel> GetStorePatrolScenes()
        {
            return _serviceClient.Request<IStorePatrolService, IList<StorePatrolSceneModel>>(
                    service => service.GetStorePatrolScenes());
        }

        #endregion

        #region Training ...

        /// <summary>
        /// [培训] - 获取用户培训列表
        /// </summary>
        [Get]
        public IList<UserTrainingPreviewModel> GetUserTrainingList(bool isAll, string userId, int pageIndex, int pageSize)
        {
            return _serviceClient.Request<ITrainingService, IList<UserTrainingPreviewModel>>(
                service => service.GetUserTrainingList(isAll, userId, pageIndex, pageSize));
        }

        /// <summary>
        /// [培训] - 获取用户培训详情
        /// </summary>
        [Get]
        public UserTrainingDetailModel GetUserTrainingDetail(string userId, int trainingId)
        {
            return _serviceClient.Request<ITrainingService, UserTrainingDetailModel>(
                service => service.GetUserTrainingDetail(userId, trainingId));
        }

        /// <summary>
        /// [培训] - 获取培训报名用户
        /// </summary>
        [Get]
        public IList<UserViewModel> GetTrainingParticipants(int trainingId)
        {
            return _serviceClient.Request<ITrainingService, IList<UserViewModel>>(
                service => service.GetTrainingParticipants(trainingId));
        }

        /// <summary>
        /// [培训] - 培训报名
        /// </summary>
        [Post]
        public ResultBase JoinTraining(JObject param)
        {
            return _serviceClient.Request<ITrainingService, ResultBase>(
                service =>
                    service.JoinTraining(JObjectHelper.TryGetValue<string>(param, "userId"),
                        JObjectHelper.TryGetValue<int>(param, "trainingId")));
        }

        #endregion

        [Get]
        public IList<QuestionnaireListModel> GetQuestionnaireList(int pageIndex, int pageSize, string questionnaireTitle)
        {
            return _serviceClient.Request<IQuestionnaireService, IList<QuestionnaireListModel>>(
                        service => service.GetQuestionnaireList(pageIndex, pageSize, questionnaireTitle));
        }

        [Get]
        public QuestionnaireDetailModel GetQuestionnaireDetailById(int questionnaireId)
        {
            return _serviceClient.Request<IQuestionnaireService, QuestionnaireDetailModel>(
                        service => service.GetQuestionnaireDetailById(questionnaireId));
        }


        #region Learning File ...

        /// <summary>
        /// [课程] - 获取大课程列表
        /// </summary>
        [Get]
        public IList<CoursePreviewModel> GetCoursesList()
        {
            return _serviceClient.Request<ILearningFileService, IList<CoursePreviewModel>>(
                service => service.GetCoursesList());
        }

        /// <summary>
        /// [课程] - 获取/检索大课程详情
        /// </summary>
        [Get]
        public CourseDetailModel GetCourseDetail(int courseId, string keyword)
        {
            return _serviceClient.Request<ILearningFileService, CourseDetailModel>(
                service => service.GetCourseDetail(courseId, keyword));
        }

        /// <summary>
        /// [课程] - 获取原子小节详情
        /// </summary>
        [Get]
        public CourseLearningFileModel GetCourseLearningFile(int learningFileId, int taskId)
        {
            return _serviceClient.Request<ILearningFileService, CourseLearningFileModel>(
                service => service.GetCourseLearningFile(learningFileId, taskId));
        }

        /// <summary>
        /// [课程] - 完成课程任务
        /// </summary>
        [Post]
        public ResultBase FinishCourseLearningTask(JObject param)
        {
            return _serviceClient.Request<ILearningFileService, ResultBase>(
                service =>
                    service.FinishCourseLearningTask(JObjectHelper.TryGetValue<string>(param, "userUniqueId"),
                        JObjectHelper.TryGetValue<int>(param, "taskId")));
        }

        #endregion
    }
}
