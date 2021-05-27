--Comment: Script Error  [nvarcharaaaa]

CREATE TABLE Schema3.[TransTable1](
	[TransID] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [float] (50) NULL,
	[Price] [nvarcharaaaa] NULL,
	[TotalPrice] [float] NULL
) ON [PRIMARY]
GO