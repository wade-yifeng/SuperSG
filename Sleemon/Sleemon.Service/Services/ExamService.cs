﻿namespace Sleemon.Service
{
    using System;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    using Sleemon.Data;
    using Sleemon.Core;
    using Sleemon.Common;

    using Microsoft.Practices.Unity;

    public class ExamService : IExamService
    {
        private readonly ISleemonEntities _invoicingEntities;

        public ExamService([Dependency] ISleemonEntities invoicingEntities)
        {
            this._invoicingEntities = invoicingEntities;
        }

        public IList<ExamListModel> GetExamList(int pageIndex, int pageSize, string examTitle)
        {
            return
                this._invoicingEntities.Database.SqlQuery<ExamListModel>(@"
WITH [ExamWithRowNumber] AS
(
    SELECT [Exam].[Id]
          ,[Exam].[Title]
          ,[Exam].[Description]
          ,[Exam].[TotalScore]
          ,[Exam].[PassingScore]
          ,[User].[Name]                                                                AS [LastUpdateUserName]
          ,[Exam].[LastUpdateUser]
          ,[Exam].[LastUpdateTime]
          ,[Exam].[Status]
          ,ROW_NUMBER() OVER(ORDER BY [Exam].[Status], [Exam].[LastUpdateTime] DESC)    AS [Row]
          ,COUNT([Exam].[Id]) OVER()                                                    AS [TotalCount]
    FROM [dbo].[Exam]
    LEFT JOIN [dbo].[User]
        ON [User].[UserUniqueId] = [Exam].[LastUpdateUser] AND [User].[IsActive] = 1
    WHERE [Exam].[Title] LIKE CONCAT('%', ISNULL(@examTitle, N''), '%')
          AND [Exam].[IsActive] = 1
)
SELECT [ExamWithRowNumber].[Id]
      ,[ExamWithRowNumber].[Title]
      ,[ExamWithRowNumber].[Description]
      ,[ExamWithRowNumber].[TotalScore]
      ,[ExamWithRowNumber].[PassingScore]
      ,[ExamWithRowNumber].[LastUpdateUserName]
      ,[ExamWithRowNumber].[LastUpdateUser]
      ,[ExamWithRowNumber].[LastUpdateTime]
      ,[ExamWithRowNumber].[Status]
      ,[ExamWithRowNumber].[TotalCount]
FROM [ExamWithRowNumber]
WHERE [ExamWithRowNumber].[Row] BETWEEN (@pageIndex -1) * @pageSize + 1 AND @pageSize * @pageIndex", new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize),
                    new SqlParameter("@examTitle", examTitle ?? string.Empty)).ToList();
        }

        public ExamDetailModel GetExamDetailById(int id)
        {
            var examEntity = this._invoicingEntities.Exam.FirstOrDefault(p => p.IsActive && p.Id == id);

            if (examEntity == null) return null;

            var lastUpdateUser =
                this._invoicingEntities.User.FirstOrDefault(
                    p => p.IsActive && p.UserUniqueId == examEntity.LastUpdateUser);

            var examDetails = new ExamDetailModel()
            {
                Id = examEntity.Id,
                Title = examEntity.Title,
                Description = examEntity.Description,
                TotalScore = examEntity.TotalScore,
                PassingScore = examEntity.PassingScore,
                LastUpdateUser = lastUpdateUser != null ? lastUpdateUser.UserUniqueId : string.Empty,
                LastUpdateUserName = lastUpdateUser != null ? lastUpdateUser.Name : string.Empty,
                LastUpdateTime = examEntity.LastUpdateTime,
                State = examEntity.Status
            };

            examDetails.Questions =
                this._invoicingEntities.ExamQuestion.Where(p => p.IsActive && p.ExamId == examEntity.Id)
                    .Select(p => new ExamQuestionModel()
                    {
                        No = p.No,
                        Question = p.Question,
                        Image = p.Image,
                        Category = p.Category,
                        CorrectAnswer = p.CorrectAnswer,
                        Score = p.Score,
                        Choices = p.ExamChoice.Select(o => new ExamChoiceModel()
                        {
                            Choice = o.Choice,
                            Description = o.Description,
                            Image = o.Image,
                            IsAnswer = o.IsAnswer
                        }).ToList()
                    })
                    .ToList();

            return examDetails;
        }

        public ResultBase SaveExamDetail(ExamDetailModel model)
        {
            var uniqueIdentifierId = model.State == (byte)ActionCategory.Publish ? Guid.NewGuid() : Guid.Empty;

            var examEntity = this._invoicingEntities.Exam.FirstOrDefault(p => p.IsActive && p.Id == model.Id);
            if (examEntity != null)
            {
                if (examEntity.GroupKey.HasValue)
                {
                    uniqueIdentifierId = examEntity.GroupKey.Value;
                }
                this._invoicingEntities.spDeleteExamById(examEntity.Id);
            }

            var newExamEntity = this._invoicingEntities.Exam.Create();

            newExamEntity.Title = model.Title;
            newExamEntity.Description = model.Description;
            newExamEntity.TotalScore = model.TotalScore;
            newExamEntity.PassingScore = model.PassingScore;
            newExamEntity.LastUpdateTime = DateTime.UtcNow;
            newExamEntity.LastUpdateUser = model.LastUpdateUser;
            newExamEntity.Status = model.State;
            if (uniqueIdentifierId != Guid.Empty)
            {
                newExamEntity.GroupKey = uniqueIdentifierId;
            }
            newExamEntity.IsActive = true;

            this._invoicingEntities.Exam.Add(newExamEntity);
            this._invoicingEntities.SaveChanges();

            foreach (var examQuestionModel in model.Questions)
            {
                var examQuestionEntity = this._invoicingEntities.ExamQuestion.Create();

                examQuestionEntity.No = examQuestionModel.No;
                examQuestionEntity.ExamId = newExamEntity.Id;
                examQuestionEntity.Question = examQuestionModel.Question;
                examQuestionEntity.Image = examQuestionModel.Image;
                examQuestionEntity.Category = examQuestionModel.Category;
                examQuestionEntity.CorrectAnswer = examQuestionModel.CorrectAnswer;
                examQuestionEntity.Score = examQuestionModel.Score;

                examQuestionEntity.LastUpdateTime = DateTime.UtcNow;
                examQuestionEntity.LastUpdateUser = model.LastUpdateUser;
                examQuestionEntity.IsActive = true;

                this._invoicingEntities.ExamQuestion.Add(examQuestionEntity);
                this._invoicingEntities.SaveChanges();

                foreach (var examChoiceModel in examQuestionModel.Choices)
                {
                    var examChoiceEntity = this._invoicingEntities.ExamChoice.Create();

                    examChoiceEntity.ExamQuestionId = examQuestionEntity.Id;
                    examChoiceEntity.Choice = examChoiceModel.Choice;
                    examChoiceEntity.Description = examChoiceModel.Description;
                    examChoiceEntity.Image = examChoiceModel.Image;
                    examChoiceEntity.IsAnswer = examChoiceModel.IsAnswer;

                    examChoiceEntity.IsActive = true;
                    examChoiceEntity.LastUpdateTime = DateTime.UtcNow;
                    examChoiceEntity.LastUpdateUser = model.LastUpdateUser;

                    this._invoicingEntities.ExamChoice.Add(examChoiceEntity);
                    this._invoicingEntities.SaveChanges();
                }
            }

            return new ResultBase()
            {
                IsSuccess = true,
                StatusCode = (int)StatusCode.Success
            };
        }

        public ResultBase DeleteExamById(int id)
        {
            this._invoicingEntities.spDeleteExamById(id);

            return new ResultBase()
            {
                IsSuccess = true,
                StatusCode = (int)StatusCode.Success
            };
        }
    }
}