namespace Sleemon.Service
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using Sleemon.Core;
    using Sleemon.Data;
    using Sleemon.Common;

    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly ISleemonEntities _invoicingEntities;

        public QuestionnaireService()
        {
            this._invoicingEntities = new SleemonEntities();
        }

        public IList<QuestionnaireListModel> GetQuestionnaireList(int pageIndex, int pageSize, string questionnaireTitle)
        {
            return
                this._invoicingEntities.Database.SqlQuery<QuestionnaireListModel>(@"
WITH [QuestionnaireWithRowNumber] AS
(
    SELECT [Questionnaire].[Id]
          ,[Questionnaire].[Title]
          ,[Questionnaire].[Description]
          ,[User].[Name]                                                                                                        AS [LastUpdateUserName]
          ,[Questionnaire].[LastUpdateUser]
          ,[Questionnaire].[LastUpdateTime]
          ,[Questionnaire].[Status]
          ,ROW_NUMBER() OVER(ORDER BY [Questionnaire].[Status], [Questionnaire].[LastUpdateTime] DESC, [Questionnaire].[Id])    AS [Row]
          ,COUNT([Questionnaire].[Id]) OVER()                                                    AS [TotalCount]
    FROM [dbo].[Questionnaire]
    LEFT JOIN [dbo].[User]
        ON [User].[UserUniqueId] = [Questionnaire].[LastUpdateUser] AND [User].[IsActive] = 1
    WHERE [Questionnaire].[Title] LIKE CONCAT('%', ISNULL(@questionnaireTitle, N''), '%')
          AND [Questionnaire].[IsActive] = 1
)
SELECT [QuestionnaireWithRowNumber].[Id]
      ,[QuestionnaireWithRowNumber].[Title]
      ,[QuestionnaireWithRowNumber].[Description]
      ,[QuestionnaireWithRowNumber].[LastUpdateUserName]
      ,[QuestionnaireWithRowNumber].[LastUpdateUser]
      ,[QuestionnaireWithRowNumber].[LastUpdateTime]
      ,[QuestionnaireWithRowNumber].[Status]
      ,[QuestionnaireWithRowNumber].[TotalCount]
FROM [QuestionnaireWithRowNumber]
WHERE [QuestionnaireWithRowNumber].[Row] BETWEEN (@pageIndex -1) * @pageSize + 1 AND @pageSize * @pageIndex",
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize),
                    new SqlParameter("@questionnaireTitle", questionnaireTitle ?? string.Empty)).ToList();
        }

        public QuestionnaireDetailModel GetQuestionnaireDetailById(int questionnaireId)
        {
            var result =
                this._invoicingEntities.Questionnaire.Include("QuestionnaireItem.QuestionnaireChoice")
                    .FirstOrDefault(p => p.IsActive && p.Id == questionnaireId);

            if (result == null) return null;

            var lastUpdateUser = this._invoicingEntities.User.FirstOrDefault(p => p.IsActive && p.UserUniqueId == result.LastUpdateUser);

            return new QuestionnaireDetailModel()
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                LastUpdateTime = result.LastUpdateTime,
                LastUpdateUser = result.LastUpdateUser,
                LastUpdateUserName = lastUpdateUser == null ? string.Empty : lastUpdateUser.Name,
                Status = result.Status,
                Questions = result.QuestionnaireItem.Where(p => p.IsActive).Select(p => new QuestionnaireItemModel()
                {
                    No = p.No,
                    Question = p.Question,
                    Image = p.Image,
                    Category = p.Category,
                    Rate = p.Rate,
                    Choices = p.QuestionnaireChoice.Where(o => o.IsActive).Select(o => new QuestionnaireChoiceModel()
                    {
                        Choice = o.Choice,
                        Description = o.Description,
                        Image = o.Image
                    }).ToList()
                }).ToList()
            };
        }

        public ResultBase SaveQuestionnaireDetail(QuestionnaireDetailModel questionnaire)
        {
            //TODO: Refactor this Function

            var uniqueIdentifierId = questionnaire.Status == (byte)ActionCategory.Publish ? Guid.NewGuid() : Guid.Empty;

            var questionnaireEntity = this._invoicingEntities.Questionnaire.FirstOrDefault(p => p.IsActive && p.Id == questionnaire.Id);
            if (questionnaireEntity != null)
            {
                if (questionnaireEntity.GroupKey.HasValue)
                {
                    uniqueIdentifierId = questionnaireEntity.GroupKey.Value;
                }
                this._invoicingEntities.spDeleteQuestionnaireById(questionnaireEntity.Id);
            }

            var newquestionnaireEntity = this._invoicingEntities.Questionnaire.Create();

            newquestionnaireEntity.Title = questionnaire.Title;
            newquestionnaireEntity.Description = questionnaire.Description;
            newquestionnaireEntity.LastUpdateTime = DateTime.UtcNow;
            newquestionnaireEntity.LastUpdateUser = questionnaire.LastUpdateUser;
            newquestionnaireEntity.Status = questionnaire.Status;
            if (uniqueIdentifierId != Guid.Empty)
            {
                newquestionnaireEntity.GroupKey = uniqueIdentifierId;
            }
            newquestionnaireEntity.IsActive = true;

            foreach (var questionnaireItemModel in questionnaire.Questions)
            {
                var questionnaireItemEntity = this._invoicingEntities.QuestionnaireItem.Create();

                questionnaireItemEntity.IsActive = true;
                questionnaireItemEntity.LastUpdateUser = questionnaire.LastUpdateUser;
                questionnaireItemEntity.LastUpdateTime = DateTime.UtcNow;

                questionnaireItemEntity.No = questionnaireItemModel.No;
                questionnaireItemEntity.Question = questionnaireItemModel.Question;
                questionnaireItemEntity.Image = questionnaireItemModel.Image;
                questionnaireItemEntity.Category = questionnaireItemModel.Category;
                questionnaireItemEntity.Rate = questionnaireItemModel.Rate;

                foreach (var questionnaireChoiceModel in questionnaireItemModel.Choices)
                {
                    var questionnaireChoiceEntity = this._invoicingEntities.QuestionnaireChoice.Create();

                    questionnaireChoiceEntity.IsActive = true;
                    questionnaireChoiceEntity.LastUpdateUser = questionnaire.LastUpdateUser;
                    questionnaireChoiceEntity.LastUpdateTime = DateTime.UtcNow;

                    questionnaireChoiceEntity.Choice = questionnaireChoiceModel.Choice;
                    questionnaireChoiceEntity.Description = questionnaireChoiceModel.Description;
                    questionnaireChoiceEntity.Image = questionnaireChoiceModel.Image;

                    questionnaireItemEntity.QuestionnaireChoice.Add(questionnaireChoiceEntity);
                }

                newquestionnaireEntity.QuestionnaireItem.Add(questionnaireItemEntity);
            }

            this._invoicingEntities.Questionnaire.Add(newquestionnaireEntity);
            this._invoicingEntities.SaveChanges();

            return new ResultBase()
            {
                IsSuccess = true,
                StatusCode = (int)StatusCode.Success
            };
        }

        public ResultBase DeleteQuestionnaireById(int questionnaireId)
        {
            this._invoicingEntities.spDeleteQuestionnaireById(questionnaireId);

            return new ResultBase()
            {
                IsSuccess = true,
                StatusCode = (int)StatusCode.Success
            };
        }
    }
}
