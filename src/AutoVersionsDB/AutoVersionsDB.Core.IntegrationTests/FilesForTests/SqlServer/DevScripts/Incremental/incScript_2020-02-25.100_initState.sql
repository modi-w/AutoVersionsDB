CREATE SCHEMA Schema1
go

CREATE SCHEMA Schema2
go

CREATE SCHEMA Schema3
go


CREATE TABLE Schema1.[Table1](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Col1] [nvarchar](50) NULL,
	[Col2] [int] NULL
) ON [PRIMARY]
GO

SET IDENTITY_INSERT Schema1.[Table1] ON 
GO
INSERT Schema1.[Table1] ([ID], [Col1], [Col2]) VALUES (1, N'aa', 11)
GO
INSERT Schema1.[Table1] ([ID], [Col1], [Col2]) VALUES (2, N'bb', 22)
GO
SET IDENTITY_INSERT Schema1.[Table1] OFF
GO


CREATE PROCEDURE Schema1.[SpOnTable1]

AS
BEGIN
	
	SELECT * FROM Schema1.[Table1]

END
GO