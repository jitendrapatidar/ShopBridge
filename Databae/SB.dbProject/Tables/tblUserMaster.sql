
CREATE TABLE [dbo].[tblUserMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](18) NULL,
	[HasPassword] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[OnDate] [datetime2](7) NULL,
 CONSTRAINT [PK_tblUserMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


