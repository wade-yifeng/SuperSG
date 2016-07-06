namespace Sleemon.Core
{
    using System.Collections.Generic;

    using Sleemon.Data;

    public interface ITaskService
    {
        IList<UserTaskModel> SearchUserTaskList(string userUnqiueId, byte taskCategory, string input);

        IList<UserTaskModel> GetUserTaskList(string userId, byte taskCategory, int pageIndex, int pageSize);

        IList<TaskDetailsModel> GetTaskBasicInfoList(TaskBasicInfoSearchContext searchContext, out int totalCount);

        TaskDetailsModel GetTaskBasicInfoDetailById(int taskId);

        UserTaskDetailsModel GetUserTaskDetail(int userTaskId);

        ExamPreviewModel GetExamTaskOtherInfo(int taskId);

        IList<ExamQuestionModel> GetExamQuestions(int taskId);

        IList<ExamAnswerModel> GetUserExamAnswers(int taskId, string userUniqueId);

        ResultBase CommitSingleExamQuestion(int taskId, string userUniqueId, int examQuestionId, string myAnswer);

        ExamResultModel CommitEntireExam(string userUniqueId, int userTaskId);

        ResultBase SaveTaskBasicInfo(TaskDetailsModel taskModel);

        ResultBase DeleteTaskBasicInfo(int taskId);

        int GetQuestionnaireQuestionCount(int taskId);

        ResultBase UpdateUserTaskStatus(int userTaskId, byte userTaskCategory);

        byte GetUserTaskStatus(int userTaskId);
    }
}
