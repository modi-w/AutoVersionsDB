
TRUNCATE TABLE  Schema3.[InvoiceTable1]
GO

SET IDENTITY_INSERT Schema3.[InvoiceTable1] ON 
GO
INSERT Schema3.[InvoiceTable1] ([InvoiceID], [TotalPrice], [Comments]) VALUES (1, 400, N'Comment 1')
GO
INSERT Schema3.[InvoiceTable1] ([InvoiceID], [TotalPrice], [Comments]) VALUES (2, 800, N'Comment 2')
GO
SET IDENTITY_INSERT Schema3.[InvoiceTable1] OFF
GO
