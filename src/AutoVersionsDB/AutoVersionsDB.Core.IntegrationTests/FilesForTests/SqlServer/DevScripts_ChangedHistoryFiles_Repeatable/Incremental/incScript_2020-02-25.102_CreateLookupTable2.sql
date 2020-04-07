
CREATE TABLE Schema2.[LookupTable2](
	[Lookup2Key] [int] NOT NULL,
	[Lookup2Value] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LookupTable2] PRIMARY KEY CLUSTERED 
(
	[Lookup2Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO