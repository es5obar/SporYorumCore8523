USE master
GO
if exists (select name from sys.databases where name = 'SporYorum')
begin
	alter database SporYorum set single_user with rollback immediate -- veritabanı bağlantısını koparmak için
	drop database SporYorum
end
go
create database SporYorum
go
USE [SporYorum]
GO
/****** Object:  Table [dbo].[Spor]    Script Date: 19.03.2022 13:35:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TakimAdi] [varchar](50) NOT NULL,
	[TakimUlke] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Spor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Yorum]    Script Date: 19.03.2022 13:35:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Yorum](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Icerik] [varchar](200) NOT NULL,
	[Yorumcu] [varchar](100) NOT NULL,
	[SporId] [int] NOT NULL,
 CONSTRAINT [PK_Yorum] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Spor] ON 

INSERT [dbo].[Spor] ([Id], [TakimAdi], [TakimUlke]) VALUES (1, N'Galatasaray', N'Türkiye')
INSERT [dbo].[Spor] ([Id], [TakimAdi], [TakimUlke]) VALUES (2, N'Barcelona', N'İspanya')
INSERT [dbo].[Spor] ([Id], [TakimAdi], [TakimUlke]) VALUES (3, N'Fenerbahçe', N'Türkiye')
INSERT [dbo].[Spor] ([Id], [TakimAdi], [TakimUlke]) VALUES (4, N'Real Madrid', N'İspanya')
INSERT [dbo].[Spor] ([Id], [TakimAdi], [TakimUlke]) VALUES (9, N'Beşiktaş', N'Türkiye')
INSERT [dbo].[Spor] ([Id], [TakimAdi], [TakimUlke]) VALUES (10, N'Manchester City', N'İngiltere')
SET IDENTITY_INSERT [dbo].[Spor] OFF
GO
SET IDENTITY_INSERT [dbo].[Yorum] ON 

INSERT [dbo].[Yorum] ([Id], [Icerik], [Yorumcu], [SporId]) VALUES (2, N'Galatasaray son dönemde çıkış yakaladı', N'Es5obar', 1)
INSERT [dbo].[Yorum] ([Id], [Icerik], [Yorumcu], [SporId]) VALUES (3, N'Barcelona xavi ile beraber gençleşme operasyonuna gitti', N'Zidane', 2)
INSERT [dbo].[Yorum] ([Id], [Icerik], [Yorumcu], [SporId]) VALUES (4, N'Fenerbahçe avrupada büyük hayal kırıklığı yarattı', N'Kör Osman', 3)
INSERT [dbo].[Yorum] ([Id], [Icerik], [Yorumcu], [SporId]) VALUES (5, N'Real Madrid ispanyada şampiyonluğa çok yakın', N'Deadpool', 4)
INSERT [dbo].[Yorum] ([Id], [Icerik], [Yorumcu], [SporId]) VALUES (7, N'Fenerbahçe teknik direktör arıyor', N'Fatih Terim', 3)
INSERT [dbo].[Yorum] ([Id], [Icerik], [Yorumcu], [SporId]) VALUES (9, N'Okan Manchester City''e transfer oldu.', N'Yahya Önder', 10)
SET IDENTITY_INSERT [dbo].[Yorum] OFF
GO
ALTER TABLE [dbo].[Yorum]  WITH CHECK ADD  CONSTRAINT [FK_Yorum_Spor] FOREIGN KEY([SporId])
REFERENCES [dbo].[Spor] ([Id])
GO
ALTER TABLE [dbo].[Yorum] CHECK CONSTRAINT [FK_Yorum_Spor]
GO
