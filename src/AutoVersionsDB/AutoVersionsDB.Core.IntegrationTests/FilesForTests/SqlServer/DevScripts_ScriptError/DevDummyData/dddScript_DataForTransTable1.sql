
TRUNCATE TABLE  Schema3.[TransTable1]
GO

SET IDENTITY_INSERT Schema3.[TransTable1] ON 
GO
INSERT Schema3.[TransTable1] ([TransID], [Quantity], [Price], [TotalPrice]) VALUES (1, 2, 100, 200)
GO
INSERT Schema3.[TransTable1] ([TransID], [Quantity], [Price], [TotalPrice]) VALUES (2, 5, 200, 1000)
GO
SET IDENTITY_INSERT Schema3.[TransTable1] OFF
GO
