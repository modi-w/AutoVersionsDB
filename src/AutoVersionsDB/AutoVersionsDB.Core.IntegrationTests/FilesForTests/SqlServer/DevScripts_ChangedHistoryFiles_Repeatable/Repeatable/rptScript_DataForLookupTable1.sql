--Comment: Change File
TRUNCATE TABLE Schema2.[LookupTable1]
GO

INSERT Schema2.[LookupTable1] ([Lookup1Key], [Lookup1Value]) VALUES (1, N'Value1')
GO
INSERT Schema2.[LookupTable1] ([Lookup1Key], [Lookup1Value]) VALUES (2, N'Value2')
GO
