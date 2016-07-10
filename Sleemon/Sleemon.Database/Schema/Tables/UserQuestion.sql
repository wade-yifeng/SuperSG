CREATE TABLE [dbo].[UserQuestion]
(
	[Id]				INT				NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[UserUniqueId]		NVARCHAR(200)	NOT NULL,
	[Title]				NVARCHAR(200)	NOT NULL,
	[Question]			NVARCHAR(500)	NULL,
	[AskTime]			DATETIME		NOT NULL,
	[Replier]			NVARCHAR(200)	NOT NULL,
	[Answer]			NVARCHAR(4000)	NOT NULL,
	[AnswerTime]		DATETIME		NOT NULL,
	[Status]			TINYINT			NOT NULL,
	[LastUpdateTime]	DATETIME		NOT NULL,
	[IsActive]			BIT				NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题状态： 0-未解决 1-已解决' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserQuestion', @level2type=N'COLUMN',@level2name=N'Status'
GO