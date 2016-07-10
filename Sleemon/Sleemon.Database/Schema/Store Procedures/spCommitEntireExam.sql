CREATE PROCEDURE [dbo].[spCommitEntireExam]
	@userTaskId		INT,
	@userId		NVARCHAR(200)
AS
BEGIN TRY
	BEGIN TRANSACTION
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

		DECLARE @taskId INT = (SELECT TOP(1) [UserTask].[TaskId] FROM [dbo].[UserTask] WHERE [UserTask].[Id] = @userTaskId AND [UserTask].[IsActive] = 1)
		DECLARE @examId INT = (SELECT TOP(1) [TaskExam].[ExamId] FROM [dbo].[TaskExam] WHERE [TaskExam].[IsActive] = 1 AND [TaskExam].[TaskId] = @taskId);

		DECLARE @totalScore FLOAT = (SELECT ISNULL((SELECT SUM([ExamQuestion].[Score]) AS [TotalScore]
									 FROM [dbo].[ExamQuestion]
									 JOIN [dbo].[UserExamAnswer]
									     ON [UserExamAnswer].[ExamQuestionId] = [ExamQuestion].[Id]
											AND [ExamQuestion].[CorrectAnswer] = [UserExamAnswer].[MyAnswer]
											AND [UserExamAnswer].[TaskId] = @taskId
									 WHERE [ExamQuestion].[ExamId] = @examId
											AND [UserExamAnswer].[UserUniqueId] = @userId
									 GROUP BY [ExamQuestion].[ExamId]), 0))

		DECLARE @passingScore FLOAT;
		DECLARE @point INT;
		DECLARE @overduPoint INT;
		DECLARE @endTo	DATE;

		SELECT @passingScore = [Exam].[PassingScore]
		FROM [dbo].[Exam]
		WHERE [Exam].[Id] = @examId

		SELECT @point = [Task].[Point]
			  ,@overduPoint = [Task].[OverduePoint]
			  ,@endTo = [Task].[EndTo]
		FROM [dbo].[Task]
		WHERE [Task].[Id] = @taskId
			AND [Task].[IsActive] = 1

		UPDATE [UserTask]
		SET [UserTask].[CompleteTime] = GETUTCDATE()
		   ,[UserTask].[Score] = @totalScore
		   ,[UserTask].[Point] = (CASE WHEN @totalScore - @passingScore < 0 THEN 0
									   WHEN @endTo IS NULL THEN @point
									   WHEN @endTo >= GETUTCDATE() THEN @point
									   ELSE @overduPoint END)
		   ,[UserTask].[Status] = (CASE WHEN @totalScore - @passingScore < 0 THEN 5
										ELSE 4 END)
		FROM [dbo].[UserTask]
		WHERE [UserTask].[UserUniqueId] =  @userId
			AND [UserTask].[TaskId] = @taskId
			AND [UserTask].[IsActive] = 1

		--TODO: INSERT INTO [dbo].[UserMoments]
		--TODO: INSERT INTO [dbo].[UserPointRecord]

		SELECT @totalScore AS [UserScore]
			  ,(CASE WHEN @totalScore - @passingScore < 0 THEN 0
									   WHEN @endTo IS NULL THEN @point
									   WHEN @endTo > GETUTCDATE() THEN @point
									   ELSE @overduPoint END) AS [Point]
			  ,CASE WHEN @totalScore - @passingScore < 0 THEN 0
					ELSE [Task].[ProductAbility] END AS [ProductAbility]
			  ,CASE WHEN @totalScore - @passingScore < 0 THEN 0
					ELSE [Task].[SalesAbility] END AS [SalesAbility]
			  ,CASE WHEN @totalScore - @passingScore < 0 THEN 0
					ELSE [Task].[ExhibitAbility] END AS [ExhibitAbility]
		FROM [dbo].[Task]
		WHERE [Task].[Id] = @taskId
			AND [Task].[IsActive] = 1

		IF @totalScore - @passingScore < 0
		BEGIN
			DELETE 
			FROM [dbo].[UserExamAnswer]
			WHERE [UserExamAnswer].[TaskId] = @taskId
				AND [UserExamAnswer].[UserUniqueId] = @userId
				AND [UserExamAnswer].[ExamQuestionId] IN (SELECT [ExamQuestion].[Id] FROM [dbo].[ExamQuestion] WHERE [ExamQuestion].[ExamId] = @examId AND [ExamQuestion].[IsActive] = 1)
		END

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION

		DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT
			SELECT @ErrMsg = ERROR_MESSAGE(),
					@ErrSeverity = ERROR_SEVERITY()

		RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH