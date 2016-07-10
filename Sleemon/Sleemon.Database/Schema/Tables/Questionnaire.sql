﻿CREATE TABLE [dbo].[Questionnaire]
(
	[Id]				INT					NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Title]				NVARCHAR(200)		NOT NULL,
	[Description]		NVARCHAR(500)		NULL,
	[GroupKey]			UNIQUEIDENTIFIER	NULL,
	[Status]			INT					NOT NULL DEFAULT(0),
	[LastUpdateUser]	NVARCHAR(200)		NOT NULL,
	[LastUpdateTime]	DATETIME			NOT NULL DEFAULT(GETUTCDATE()),
	[IsActive]			BIT					NOT NULL DEFAULT(1)
)
GO