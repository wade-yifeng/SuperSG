CREATE TABLE [dbo].[UserOrderShow]
(
	[Id]				INT				NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[UserUniqueId]		NVARCHAR(200)	NOT NULL /*CONSTRAINT [FK_UserOrderShow_User] FOREIGN KEY REFERENCES [User]([UserId])*/,
	[IsLegal]			BIT				NOT NULL,
	[ShowTime]			DATETIME		NOT NULL,
	--[LastUpdateUser]	INT				NOT NULL,
	[LastUpdateTime]	DATETIME		NOT NULL DEFAULT(GETUTCDATE()),
	[IsActive]			BIT				NOT NULL DEFAULT(1)
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-不合法 1-合法' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserOrderShow', @level2type=N'COLUMN',@level2name=N'IsLegal'
GO