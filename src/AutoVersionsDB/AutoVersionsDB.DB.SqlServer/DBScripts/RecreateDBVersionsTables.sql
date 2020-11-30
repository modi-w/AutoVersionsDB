
IF (EXISTS (SELECT * 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'AutoVersionsDB' 
            AND  TABLE_NAME = 'DBScriptsExecutionHistory'))
BEGIN
	EXEC sys.sp_executesql N'DROP TABLE [AutoVersionsDB].[DBScriptsExecutionHistory]'
END

IF (EXISTS (SELECT * 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'AutoVersionsDB' 
            AND  TABLE_NAME = 'DBScriptsExecutionHistoryFiles'))
BEGIN
	EXEC sys.sp_executesql N'DROP TABLE [AutoVersionsDB].[DBScriptsExecutionHistoryFiles]'
END

GO



IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'AutoVersionsDB')
EXEC sys.sp_executesql N'CREATE SCHEMA [AutoVersionsDB]'
GO


CREATE TABLE [AutoVersionsDB].[DBScriptsExecutionHistory](
	[DBScriptsExecutionHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[ExecutionTypeName] [nvarchar](250) NOT NULL,
	[StartProcessDateTime] [datetime] NOT NULL,
	[EndProcessDateTime] [datetime] NOT NULL,
	[ProcessDurationInMs] [float] NOT NULL,
	[IsVirtualExecution] [bit] NOT NULL,
	[NumOfScriptFiles] [int] NOT NULL,
	[DBBackupFileFullPath] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DBScriptsExecutionHistory] PRIMARY KEY CLUSTERED 
(
	[DBScriptsExecutionHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [AutoVersionsDB].[DBScriptsExecutionHistoryFiles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DBScriptsExecutionHistoryID] [int] NOT NULL,
	[ExecutedDateTime] [datetime] NOT NULL,
	[IsVirtualExecution] [bit] NOT NULL,
	[Filename] [nvarchar](500) NOT NULL,
	[FileFullPath] [nvarchar](max) NOT NULL,
	[ScriptFileType] [nvarchar](50) NOT NULL,
	[ComputedFileHash] [nvarchar](250) NOT NULL,
	[ComputedFileHashDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_DBScriptsExecutionHistoryFiles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


