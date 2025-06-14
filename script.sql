USE [KelimeEzberlemeKG]
GO
/****** Object:  Table [dbo].[DogruBilinenler]    Script Date: 2.06.2025 23:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DogruBilinenler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[WordId] [int] NULL,
	[TestDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GunlukTestTakip]    Script Date: 2.06.2025 23:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GunlukTestTakip](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[TestDate] [date] NULL,
	[WordID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KelimeResimleri]    Script Date: 2.06.2025 23:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KelimeResimleri](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kelime] [nvarchar](100) NOT NULL,
	[ResimYolu] [nvarchar](300) NOT NULL,
	[KayitTarihi] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2.06.2025 23:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[UserName] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WordProgresses]    Script Date: 2.06.2025 23:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WordProgresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[WordId] [int] NOT NULL,
	[StepIndex] [int] NULL,
	[CorrectStreak] [int] NULL,
	[LastCorrectDate] [datetime] NULL,
	[IsMastered] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Words]    Script Date: 2.06.2025 23:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Words](
	[WordID] [int] IDENTITY(1,1) NOT NULL,
	[EngWordName] [nvarchar](255) NULL,
	[TurWordName] [nvarchar](255) NULL,
	[Picture] [nvarchar](max) NULL,
	[KullaniciAdi] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[WordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WordSamples]    Script Date: 2.06.2025 23:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WordSamples](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WordId] [int] NOT NULL,
	[Samples] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YanlisBilinenler]    Script Date: 2.06.2025 23:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YanlisBilinenler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[WordId] [int] NULL,
	[TestDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DogruBilinenler] ON 

INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (137, 32, 147, CAST(N'2025-05-30' AS Date))
INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (138, 32, 148, CAST(N'2025-05-30' AS Date))
INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (141, 34, 139, CAST(N'2025-05-31' AS Date))
INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (142, 34, 141, CAST(N'2025-05-31' AS Date))
INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (143, 32, 143, CAST(N'2025-06-01' AS Date))
INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (144, 32, 144, CAST(N'2025-06-01' AS Date))
INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (145, 32, 145, CAST(N'2025-06-01' AS Date))
INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (146, 32, 149, CAST(N'2025-06-01' AS Date))
INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (147, 32, 150, CAST(N'2025-06-01' AS Date))
INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (148, 32, 151, CAST(N'2025-06-01' AS Date))
INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (149, 34, 139, CAST(N'2025-06-01' AS Date))
INSERT [dbo].[DogruBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (150, 34, 140, CAST(N'2025-06-01' AS Date))
SET IDENTITY_INSERT [dbo].[DogruBilinenler] OFF
GO
SET IDENTITY_INSERT [dbo].[GunlukTestTakip] ON 

INSERT [dbo].[GunlukTestTakip] ([Id], [UserId], [TestDate], [WordID]) VALUES (73, 34, CAST(N'2025-05-31' AS Date), 139)
INSERT [dbo].[GunlukTestTakip] ([Id], [UserId], [TestDate], [WordID]) VALUES (74, 34, CAST(N'2025-05-31' AS Date), 140)
INSERT [dbo].[GunlukTestTakip] ([Id], [UserId], [TestDate], [WordID]) VALUES (75, 34, CAST(N'2025-05-31' AS Date), 141)
INSERT [dbo].[GunlukTestTakip] ([Id], [UserId], [TestDate], [WordID]) VALUES (76, 32, CAST(N'2025-06-01' AS Date), 143)
INSERT [dbo].[GunlukTestTakip] ([Id], [UserId], [TestDate], [WordID]) VALUES (77, 32, CAST(N'2025-06-01' AS Date), 144)
INSERT [dbo].[GunlukTestTakip] ([Id], [UserId], [TestDate], [WordID]) VALUES (78, 32, CAST(N'2025-06-01' AS Date), 145)
INSERT [dbo].[GunlukTestTakip] ([Id], [UserId], [TestDate], [WordID]) VALUES (79, 34, CAST(N'2025-06-01' AS Date), 139)
SET IDENTITY_INSERT [dbo].[GunlukTestTakip] OFF
GO
SET IDENTITY_INSERT [dbo].[KelimeResimleri] ON 

INSERT [dbo].[KelimeResimleri] ([ID], [Kelime], [ResimYolu], [KayitTarihi]) VALUES (20, N'apple', N'C:\Users\defne\OneDrive\Masaüstü\KelimeEzberleme\WindowsFormsApp2\WindowsFormsApp2\bin\Debug\images5\apple_11fe5.png', CAST(N'2025-05-30T19:52:46.377' AS DateTime))
INSERT [dbo].[KelimeResimleri] ([ID], [Kelime], [ResimYolu], [KayitTarihi]) VALUES (21, N'apple', N'C:\Users\defne\OneDrive\Masaüstü\KelimeEzberleme\WindowsFormsApp2\WindowsFormsApp2\bin\Debug\images5\apple_7d171.png', CAST(N'2025-05-31T14:38:04.557' AS DateTime))
SET IDENTITY_INSERT [dbo].[KelimeResimleri] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Email], [UserName], [Password]) VALUES (32, N'serenayargc157@gmail.com', N'Serenay', N'2222')
INSERT [dbo].[Users] ([UserID], [Email], [UserName], [Password]) VALUES (34, N'defneoktay35@gmail.com', N'Defne', N'1234')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[WordProgresses] ON 

INSERT [dbo].[WordProgresses] ([Id], [UserId], [WordId], [StepIndex], [CorrectStreak], [LastCorrectDate], [IsMastered]) VALUES (144, 32, 147, 3, 3, CAST(N'2025-05-30T21:29:33.530' AS DateTime), 0)
INSERT [dbo].[WordProgresses] ([Id], [UserId], [WordId], [StepIndex], [CorrectStreak], [LastCorrectDate], [IsMastered]) VALUES (145, 32, 148, 3, 3, CAST(N'2025-05-30T21:29:36.770' AS DateTime), 0)
INSERT [dbo].[WordProgresses] ([Id], [UserId], [WordId], [StepIndex], [CorrectStreak], [LastCorrectDate], [IsMastered]) VALUES (146, 32, 149, 1, 1, CAST(N'2025-06-01T00:05:57.253' AS DateTime), 0)
INSERT [dbo].[WordProgresses] ([Id], [UserId], [WordId], [StepIndex], [CorrectStreak], [LastCorrectDate], [IsMastered]) VALUES (147, 32, 150, 1, 1, CAST(N'2025-06-01T00:06:01.600' AS DateTime), 0)
INSERT [dbo].[WordProgresses] ([Id], [UserId], [WordId], [StepIndex], [CorrectStreak], [LastCorrectDate], [IsMastered]) VALUES (148, 32, 151, 1, 1, CAST(N'2025-06-01T00:06:04.970' AS DateTime), 0)
INSERT [dbo].[WordProgresses] ([Id], [UserId], [WordId], [StepIndex], [CorrectStreak], [LastCorrectDate], [IsMastered]) VALUES (153, 34, 139, 2, 2, CAST(N'2025-06-01T18:45:06.450' AS DateTime), 0)
INSERT [dbo].[WordProgresses] ([Id], [UserId], [WordId], [StepIndex], [CorrectStreak], [LastCorrectDate], [IsMastered]) VALUES (154, 34, 140, 1, 1, CAST(N'2025-06-01T18:45:46.820' AS DateTime), 0)
INSERT [dbo].[WordProgresses] ([Id], [UserId], [WordId], [StepIndex], [CorrectStreak], [LastCorrectDate], [IsMastered]) VALUES (155, 34, 141, 1, 1, CAST(N'2025-05-31T14:39:12.367' AS DateTime), 0)
INSERT [dbo].[WordProgresses] ([Id], [UserId], [WordId], [StepIndex], [CorrectStreak], [LastCorrectDate], [IsMastered]) VALUES (156, 32, 143, 1, 1, CAST(N'2025-06-01T00:05:19.630' AS DateTime), 0)
INSERT [dbo].[WordProgresses] ([Id], [UserId], [WordId], [StepIndex], [CorrectStreak], [LastCorrectDate], [IsMastered]) VALUES (157, 32, 144, 1, 1, CAST(N'2025-06-01T00:05:24.503' AS DateTime), 0)
INSERT [dbo].[WordProgresses] ([Id], [UserId], [WordId], [StepIndex], [CorrectStreak], [LastCorrectDate], [IsMastered]) VALUES (158, 32, 145, 1, 1, CAST(N'2025-06-01T00:05:30.210' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[WordProgresses] OFF
GO
SET IDENTITY_INSERT [dbo].[Words] ON 

INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (139, N'apple', N'elma', N'', N'Defne')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (140, N'run', N'koşmak', N'', N'Defne')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (141, N'angry', N'sinirli', N'', N'Defne')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (142, N'happy', N'mutlu', N'', N'Defne')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (143, N'cat', N'kedi', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (144, N'dog', N'köpek', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (145, N'book', N'kitap', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (146, N'pen', N'kalem', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (147, N'sun', N'güneş', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (148, N'moon', N'ay', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (149, N'car', N'araba', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (150, N'tree', N'ağaç', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (151, N'ball', N'top', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (152, N'milk', N'süt', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (153, N'egg', N'yumurta', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (154, N'fish', N'balık', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (155, N'bird', N'kuş', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (156, N'shoe', N'ayakkabı', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (157, N'house', N'ev', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (173, N'chair', N'sandalye', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (174, N'door', N'kapı', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (175, N'window', N'pencere', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (176, N'phone', N'telefon', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (177, N'bag', N'çanta', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (178, N'clock', N'saat', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (179, N'cake', N'pasta', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (180, N'juice', N'meyve suyu', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (181, N'milkshake', N'milkshake', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (182, N'computer', N'bilgisayar', NULL, N'Serenay')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (193, N'aa', N'aa', N'', N'Selda')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (194, N'b', N'aa', N'', N'Selda')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (195, N'c', N'aa', N'', N'Selda')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (196, N'd', N'aa', N'', N'Selda')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (197, N'apple', N'elma', N'', N'Defne')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (198, N'angry', N'sinirli', N'', N'Defne')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (199, N'run', N'koşmak', N'', N'Defne')
INSERT [dbo].[Words] ([WordID], [EngWordName], [TurWordName], [Picture], [KullaniciAdi]) VALUES (200, N'happy', N'mutlu', N'', N'Defne')
SET IDENTITY_INSERT [dbo].[Words] OFF
GO
SET IDENTITY_INSERT [dbo].[WordSamples] ON 

INSERT [dbo].[WordSamples] ([Id], [WordId], [Samples]) VALUES (104, 197, N'I ate an apple.')
INSERT [dbo].[WordSamples] ([Id], [WordId], [Samples]) VALUES (105, 198, N'He was angry.')
INSERT [dbo].[WordSamples] ([Id], [WordId], [Samples]) VALUES (106, 199, N'I run.')
INSERT [dbo].[WordSamples] ([Id], [WordId], [Samples]) VALUES (107, 200, N'I am very happy')
SET IDENTITY_INSERT [dbo].[WordSamples] OFF
GO
SET IDENTITY_INSERT [dbo].[YanlisBilinenler] ON 

INSERT [dbo].[YanlisBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (21, 32, 149, CAST(N'2025-05-30' AS Date))
INSERT [dbo].[YanlisBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (22, 32, 150, CAST(N'2025-05-30' AS Date))
INSERT [dbo].[YanlisBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (23, 32, 151, CAST(N'2025-05-30' AS Date))
INSERT [dbo].[YanlisBilinenler] ([Id], [UserId], [WordId], [TestDate]) VALUES (25, 34, 140, CAST(N'2025-05-31' AS Date))
SET IDENTITY_INSERT [dbo].[YanlisBilinenler] OFF
GO
/****** Object:  Index [UQ_WordProgresses_User_Word]    Script Date: 2.06.2025 23:31:56 ******/
ALTER TABLE [dbo].[WordProgresses] ADD  CONSTRAINT [UQ_WordProgresses_User_Word] UNIQUE NONCLUSTERED 
(
	[UserId] ASC,
	[WordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[KelimeResimleri] ADD  DEFAULT (getdate()) FOR [KayitTarihi]
GO
ALTER TABLE [dbo].[DogruBilinenler]  WITH CHECK ADD  CONSTRAINT [FK_DogruBilinenler_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DogruBilinenler] CHECK CONSTRAINT [FK_DogruBilinenler_Users]
GO
ALTER TABLE [dbo].[DogruBilinenler]  WITH CHECK ADD  CONSTRAINT [FK_DogruBilinenler_WordProgresses] FOREIGN KEY([UserId], [WordId])
REFERENCES [dbo].[WordProgresses] ([UserId], [WordId])
GO
ALTER TABLE [dbo].[DogruBilinenler] CHECK CONSTRAINT [FK_DogruBilinenler_WordProgresses]
GO
ALTER TABLE [dbo].[DogruBilinenler]  WITH CHECK ADD  CONSTRAINT [FK_DogruBilinenler_Words] FOREIGN KEY([WordId])
REFERENCES [dbo].[Words] ([WordID])
GO
ALTER TABLE [dbo].[DogruBilinenler] CHECK CONSTRAINT [FK_DogruBilinenler_Words]
GO
ALTER TABLE [dbo].[WordProgresses]  WITH CHECK ADD  CONSTRAINT [FK_WordProgresses_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[WordProgresses] CHECK CONSTRAINT [FK_WordProgresses_User]
GO
ALTER TABLE [dbo].[WordProgresses]  WITH CHECK ADD  CONSTRAINT [FK_WordProgresses_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[WordProgresses] CHECK CONSTRAINT [FK_WordProgresses_Users]
GO
ALTER TABLE [dbo].[WordProgresses]  WITH CHECK ADD  CONSTRAINT [FK_WordProgresses_Word] FOREIGN KEY([WordId])
REFERENCES [dbo].[Words] ([WordID])
GO
ALTER TABLE [dbo].[WordProgresses] CHECK CONSTRAINT [FK_WordProgresses_Word]
GO
ALTER TABLE [dbo].[WordProgresses]  WITH CHECK ADD  CONSTRAINT [FK_WordProgresses_Words] FOREIGN KEY([WordId])
REFERENCES [dbo].[Words] ([WordID])
GO
ALTER TABLE [dbo].[WordProgresses] CHECK CONSTRAINT [FK_WordProgresses_Words]
GO
ALTER TABLE [dbo].[WordSamples]  WITH CHECK ADD FOREIGN KEY([WordId])
REFERENCES [dbo].[Words] ([WordID])
GO
ALTER TABLE [dbo].[YanlisBilinenler]  WITH CHECK ADD  CONSTRAINT [FK_YanlisBilinenler_Words] FOREIGN KEY([WordId])
REFERENCES [dbo].[Words] ([WordID])
GO
ALTER TABLE [dbo].[YanlisBilinenler] CHECK CONSTRAINT [FK_YanlisBilinenler_Words]
GO
