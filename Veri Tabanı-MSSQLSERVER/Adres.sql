USE [dbAdresDefteri]
GO

/****** Object:  Table [dbo].[Adres]    Script Date: 9.01.2018 21:28:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Adres](
	[AdresID] [int] IDENTITY(1,1) NOT NULL,
	[Ad] [nvarchar](20) NULL,
	[Soyad] [nvarchar](20) NULL,
	[TelefonNo] [nvarchar](20) NULL,
	[EMail] [nvarchar](20) NULL,
 CONSTRAINT [PK_Adres] PRIMARY KEY CLUSTERED 
(
	[AdresID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


