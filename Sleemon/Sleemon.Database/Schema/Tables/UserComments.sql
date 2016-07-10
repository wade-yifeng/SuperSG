CREATE TABLE [dbo].[UserComments]
(
	[Id]					INT				NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[ParentId]				INT				NULL	CONSTRAINT [FK_ParentId_UserComments] FOREIGN KEY REFERENCES [UserComments]([Id]),
	[UserUniqueId]			NVARCHAR(200)	NOT NULL /*CONSTRAINT[FK_UserTask_User] FOREIGN KEY REFERENCES  [User]([UserId])*/,
	[Comment]				NVARCHAR(250)	NOT NULL,
	[Category]				TINYINT			NOT NULL,
	[LinkedId]				INT				NULL,
	[IsLegal]				BIT				NOT NULL DEFAULT(0),
	[CommentTime]			DATETIME		NOT NULL DEFAULT(GETUTCDATE()),
	[IsActive]				BIT				NOT NULL DEFAULT(1)
)
