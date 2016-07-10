namespace Sleemon.Core
{
    using System.Collections.Generic;
    using Sleemon.Data;

    public interface IQuestionnaireService
    {
        IList<QuestionnaireListModel> GetQuestionnaireList(int pageIndex, int pageSize, string questionnaireTitle);

        QuestionnaireDetailModel GetQuestionnaireDetailById(int questionnaireId);

        ResultBase SaveQuestionnaireDetail(QuestionnaireDetailModel questionnaire);

        ResultBase DeleteQuestionnaireById(int questionnaireId);

        int GetQuestionnaireQuestionCount(int taskId);

        IList<QuestionnaireQuestionModel> GetQuestionnaireQuestions(int taskId);

        ResultBase CommitQuestionnaire(IList<QuestionnaireAnswerModel> context);

        IList<QuestionnaireQuestionPreviewModel> GetQuestionnaireStatistics(string userUniqueId, int taskId);
    }
}
