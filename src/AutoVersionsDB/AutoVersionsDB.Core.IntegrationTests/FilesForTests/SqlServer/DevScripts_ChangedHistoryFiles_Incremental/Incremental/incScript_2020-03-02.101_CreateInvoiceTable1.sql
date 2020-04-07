
CREATE TABLE Schema3.[InvoiceTable1](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[TotalPrice] [float] NULL,
	[Comments] [nvarchar] (500) NULL
) ON [PRIMARY]
GO