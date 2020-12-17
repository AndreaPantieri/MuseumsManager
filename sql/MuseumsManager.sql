USE [MuseumsManagerDB]
GO
/****** Object:  Table [dbo].[Biglietto]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Biglietto](
	[idBiglietto] [int] IDENTITY(1,1) NOT NULL,
	[DataValidita] [date] NOT NULL,
	[PrezzoAcquisto] [float] NOT NULL,
	[idMuseo] [int] NOT NULL,
	[idTipoBiglietto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idBiglietto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalendarioApertureSpeciali]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalendarioApertureSpeciali](
	[idCalendarioApertureSpeciali] [int] IDENTITY(1,1) NOT NULL,
	[Data] [date] NOT NULL,
	[OrarioApertura] [time](7) NOT NULL,
	[OrarioChiusura] [time](7) NOT NULL,
	[NumBigliettiMax] [int] NULL,
	[idMuseo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCalendarioApertureSpeciali] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalendarioChiusure]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalendarioChiusure](
	[idCalendarioChiusure] [int] IDENTITY(1,1) NOT NULL,
	[Data] [date] NOT NULL,
	[idMuseo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCalendarioChiusure] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contenuto]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contenuto](
	[idContenuto] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
	[Descrizione] [nvarchar](512) NOT NULL,
	[DataRitrovamento] [date] NOT NULL,
	[DataArrivoMuseo] [date] NOT NULL,
	[idContenutoPadre] [int] NULL,
	[idProvenienza] [int] NOT NULL,
	[idPeriodoStorico] [int] NOT NULL,
	[idSezione] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idContenuto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contenuto_Tipologia]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contenuto_Tipologia](
	[idContenuto] [int] NOT NULL,
	[idTipoContenuto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idContenuto] ASC,
	[idTipoContenuto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Creato]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Creato](
	[idContenuto] [int] NOT NULL,
	[idCreatore] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idContenuto] ASC,
	[idCreatore] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Creatore]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Creatore](
	[idCreatore] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
	[Cognome] [nvarchar](255) NOT NULL,
	[Descrizione] [nvarchar](512) NOT NULL,
	[AnnoNascita] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCreatore] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FamigliaMusei]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamigliaMusei](
	[idFamiglia] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idFamiglia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Museo]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Museo](
	[idMuseo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
	[Luogo] [nvarchar](255) NOT NULL,
	[OrarioAperturaGenerale] [time](7) NOT NULL,
	[OrarioChiusuraGenerale] [time](7) NOT NULL,
	[NumBigliettiMaxGenerale] [int] NULL,
	[idFamiglia] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idMuseo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Museo_Creatore]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Museo_Creatore](
	[idCreatore] [int] NOT NULL,
	[idMuseo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCreatore] ASC,
	[idMuseo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Museo_PeriodoStorico]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Museo_PeriodoStorico](
	[idMuseo] [int] NOT NULL,
	[idPeriodoStorico] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPeriodoStorico] ASC,
	[idMuseo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Museo_Provenienza]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Museo_Provenienza](
	[idMuseo] [int] NOT NULL,
	[idProvenienza] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idProvenienza] ASC,
	[idMuseo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Museo_Tipologia]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Museo_Tipologia](
	[idMuseo] [int] NOT NULL,
	[idTipoMuseo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipoMuseo] ASC,
	[idMuseo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PeriodoStorico]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PeriodoStorico](
	[idPeriodoStorico] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
	[Descrizione] [nvarchar](512) NOT NULL,
	[AnnoInizio] [int] NOT NULL,
	[AnnoFine] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPeriodoStorico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personale]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personale](
	[idPersonale] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
	[Cognome] [nvarchar](255) NOT NULL,
	[Cellulare] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[StipendioOra] [float] NOT NULL,
	[idMuseo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPersonale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personale_Tipologia]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personale_Tipologia](
	[idPersonale] [int] NOT NULL,
	[idTipoPersonale] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPersonale] ASC,
	[idTipoPersonale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provenienza]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provenienza](
	[idProvenienza] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
	[Descrizione] [nvarchar](512) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idProvenienza] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistroManutenzioni]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroManutenzioni](
	[idManutenzione] [int] IDENTITY(1,1) NOT NULL,
	[idMuseo] [int] NOT NULL,
	[Data] [date] NOT NULL,
	[Descrizione] [nvarchar](512) NOT NULL,
	[Prezzo] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idManutenzione] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistroPresenze]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroPresenze](
	[idRegistro] [int] IDENTITY(1,1) NOT NULL,
	[idPersonale] [int] NOT NULL,
	[DataEntrata] [datetime2](7) NOT NULL,
	[DataUscita] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idRegistro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sezione]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sezione](
	[idSezione] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
	[Descrizione] [nvarchar](512) NOT NULL,
	[idSezionePadre] [int] NULL,
	[idMuseo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idSezione] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sezione_Tipologia]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sezione_Tipologia](
	[idSezione] [int] NOT NULL,
	[idTipoSezione] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipoSezione] ASC,
	[idSezione] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statistiche]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statistiche](
	[idStatistiche] [int] IDENTITY(1,1) NOT NULL,
	[MeseAnno] [date] NOT NULL,
	[SpeseTotali] [float] NOT NULL,
	[Fatturato] [float] NOT NULL,
	[NumBigliettiVenduti] [int] NOT NULL,
	[NumPresenzeTotali] [int] NOT NULL,
	[NumManutenzioni] [int] NOT NULL,
	[NumContenutiNuovi] [int] NOT NULL,
	[NumChiusure] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idStatistiche] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatisticheFamigliaMusei]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatisticheFamigliaMusei](
	[idStatistiche] [int] NOT NULL,
	[idFamiglia] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idStatistiche] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatisticheMuseo]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatisticheMuseo](
	[idStatistiche] [int] NOT NULL,
	[idMuseo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idStatistiche] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoBiglietto]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoBiglietto](
	[idTipoBiglietto] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
	[Prezzo] [float] NOT NULL,
	[Descrizione] [nvarchar](512) NOT NULL,
	[idMuseo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipoBiglietto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoContenuto]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoContenuto](
	[idTipoContenuto] [int] IDENTITY(1,1) NOT NULL,
	[Descrizione] [nvarchar](512) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipoContenuto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoMuseo]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoMuseo](
	[idTipoMuseo] [int] IDENTITY(1,1) NOT NULL,
	[Descrizione] [nvarchar](512) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipoMuseo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoPersonale]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoPersonale](
	[idTipoPersonale] [int] IDENTITY(1,1) NOT NULL,
	[Descrizione] [nvarchar](512) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipoPersonale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoSezione]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoSezione](
	[idTipoSezione] [int] IDENTITY(1,1) NOT NULL,
	[Descrizione] [nvarchar](512) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipoSezione] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Creatore] ON 

INSERT [dbo].[Creatore] ([idCreatore], [Nome], [Cognome], [Descrizione], [AnnoNascita]) VALUES (1, N'Jan ', N'Vermeer', N'Pittore olandese.', 1632)
SET IDENTITY_INSERT [dbo].[Creatore] OFF
GO
SET IDENTITY_INSERT [dbo].[FamigliaMusei] ON 

INSERT [dbo].[FamigliaMusei] ([idFamiglia], [Nome]) VALUES (1, N'Famiglia di musei Cesenati')
SET IDENTITY_INSERT [dbo].[FamigliaMusei] OFF
GO
SET IDENTITY_INSERT [dbo].[Museo] ON 

INSERT [dbo].[Museo] ([idMuseo], [Nome], [Luogo], [OrarioAperturaGenerale], [OrarioChiusuraGenerale], [NumBigliettiMaxGenerale], [idFamiglia]) VALUES (1, N'Museo di storia romana', N'Cesena', CAST(N'08:00:00' AS Time), CAST(N'20:00:00' AS Time), 100, 1)
INSERT [dbo].[Museo] ([idMuseo], [Nome], [Luogo], [OrarioAperturaGenerale], [OrarioChiusuraGenerale], [NumBigliettiMaxGenerale], [idFamiglia]) VALUES (2, N'Museo di Arte', N'Cesena', CAST(N'08:00:00' AS Time), CAST(N'17:00:00' AS Time), 50, 1)
SET IDENTITY_INSERT [dbo].[Museo] OFF
GO
INSERT [dbo].[Museo_Tipologia] ([idMuseo], [idTipoMuseo]) VALUES (1, 1)
INSERT [dbo].[Museo_Tipologia] ([idMuseo], [idTipoMuseo]) VALUES (1, 2)
INSERT [dbo].[Museo_Tipologia] ([idMuseo], [idTipoMuseo]) VALUES (1, 3)
INSERT [dbo].[Museo_Tipologia] ([idMuseo], [idTipoMuseo]) VALUES (2, 4)
INSERT [dbo].[Museo_Tipologia] ([idMuseo], [idTipoMuseo]) VALUES (2, 5)
INSERT [dbo].[Museo_Tipologia] ([idMuseo], [idTipoMuseo]) VALUES (2, 6)
INSERT [dbo].[Museo_Tipologia] ([idMuseo], [idTipoMuseo]) VALUES (2, 7)
GO
SET IDENTITY_INSERT [dbo].[PeriodoStorico] ON 

INSERT [dbo].[PeriodoStorico] ([idPeriodoStorico], [Nome], [Descrizione], [AnnoInizio], [AnnoFine]) VALUES (1, N'Medioevo Romano', N'Periodo storico rappresentativo del medioevo romano.', 476, 1492)
INSERT [dbo].[PeriodoStorico] ([idPeriodoStorico], [Nome], [Descrizione], [AnnoInizio], [AnnoFine]) VALUES (2, N'XVII Secolo', N'Periodo storico rappresentante gli anni dal 1600 al 1700.', 1600, 1700)
SET IDENTITY_INSERT [dbo].[PeriodoStorico] OFF
GO
SET IDENTITY_INSERT [dbo].[Personale] ON 

INSERT [dbo].[Personale] ([idPersonale], [Nome], [Cognome], [Cellulare], [Email], [StipendioOra], [idMuseo]) VALUES (1, N'Paolo', N'Rossi', N'1234567890', N'paolo.rossi@live.it', 10, 1)
INSERT [dbo].[Personale] ([idPersonale], [Nome], [Cognome], [Cellulare], [Email], [StipendioOra], [idMuseo]) VALUES (2, N'Giorgio', N'Verdi', N'987654321', N'giorgio.verdi@live.it', 15, 1)
INSERT [dbo].[Personale] ([idPersonale], [Nome], [Cognome], [Cellulare], [Email], [StipendioOra], [idMuseo]) VALUES (3, N'Marco', N'Bianchi', N'1324576890', N'marco.bianchi@live.it', 15, 1)
SET IDENTITY_INSERT [dbo].[Personale] OFF
GO
INSERT [dbo].[Personale_Tipologia] ([idPersonale], [idTipoPersonale]) VALUES (1, 1)
INSERT [dbo].[Personale_Tipologia] ([idPersonale], [idTipoPersonale]) VALUES (2, 3)
INSERT [dbo].[Personale_Tipologia] ([idPersonale], [idTipoPersonale]) VALUES (2, 4)
GO
SET IDENTITY_INSERT [dbo].[Provenienza] ON 

INSERT [dbo].[Provenienza] ([idProvenienza], [Nome], [Descrizione]) VALUES (1, N'Italia, Cesena', N'Oggetto proveniente dalla città di Cesena, situata in Emilia-Romagna, Italia.')
SET IDENTITY_INSERT [dbo].[Provenienza] OFF
GO
SET IDENTITY_INSERT [dbo].[Sezione] ON 

INSERT [dbo].[Sezione] ([idSezione], [Nome], [Descrizione], [idSezionePadre], [idMuseo]) VALUES (1, N'Sezione mosaici', N'Sezione che racchiude tutti i mosaici romani rinvenuti a Cesena', NULL, 1)
INSERT [dbo].[Sezione] ([idSezione], [Nome], [Descrizione], [idSezionePadre], [idMuseo]) VALUES (2, N'Sezione cimeli romani', N'Sezione che racchiude tutti i cimeli romani rinvenuti a Cesena', NULL, 1)
INSERT [dbo].[Sezione] ([idSezione], [Nome], [Descrizione], [idSezionePadre], [idMuseo]) VALUES (3, N'Sezione armi romane', N'Sezione che racchiude tutte le armi romane rinvenute in Romagna', NULL, 1)
INSERT [dbo].[Sezione] ([idSezione], [Nome], [Descrizione], [idSezionePadre], [idMuseo]) VALUES (5, N'Armi da terra', N'Sezione che racchiude tutte le armi romane da terra rinvenute in Romagna', 3, 1)
INSERT [dbo].[Sezione] ([idSezione], [Nome], [Descrizione], [idSezionePadre], [idMuseo]) VALUES (6, N'Armi da mare', N'Sezione che racchiude tutte le armi romane da mare rinvenute in Romagna', 3, 1)
INSERT [dbo].[Sezione] ([idSezione], [Nome], [Descrizione], [idSezionePadre], [idMuseo]) VALUES (7, N'Sezione dipinti', N'Sezione dedicata ai dipinti.', NULL, 2)
INSERT [dbo].[Sezione] ([idSezione], [Nome], [Descrizione], [idSezionePadre], [idMuseo]) VALUES (8, N'Sezione dipinti olandesi', N'Sezione dedicata ai quadri di pittori olandesi.', 7, 2)
SET IDENTITY_INSERT [dbo].[Sezione] OFF
GO
INSERT [dbo].[Sezione_Tipologia] ([idSezione], [idTipoSezione]) VALUES (1, 1)
INSERT [dbo].[Sezione_Tipologia] ([idSezione], [idTipoSezione]) VALUES (2, 2)
INSERT [dbo].[Sezione_Tipologia] ([idSezione], [idTipoSezione]) VALUES (3, 3)
INSERT [dbo].[Sezione_Tipologia] ([idSezione], [idTipoSezione]) VALUES (5, 3)
INSERT [dbo].[Sezione_Tipologia] ([idSezione], [idTipoSezione]) VALUES (6, 3)
INSERT [dbo].[Sezione_Tipologia] ([idSezione], [idTipoSezione]) VALUES (7, 4)
INSERT [dbo].[Sezione_Tipologia] ([idSezione], [idTipoSezione]) VALUES (8, 4)
GO
SET IDENTITY_INSERT [dbo].[TipoBiglietto] ON 

INSERT [dbo].[TipoBiglietto] ([idTipoBiglietto], [Nome], [Prezzo], [Descrizione], [idMuseo]) VALUES (1, N'Biglietto Standard', 10, N'Biglietto standard del museo di storia romana di Cesena.', 1)
INSERT [dbo].[TipoBiglietto] ([idTipoBiglietto], [Nome], [Prezzo], [Descrizione], [idMuseo]) VALUES (2, N'Biglietto Studente', 5, N'Biglietto studente del museo di storia romana di Cesena. Sconto del 50%.', 1)
INSERT [dbo].[TipoBiglietto] ([idTipoBiglietto], [Nome], [Prezzo], [Descrizione], [idMuseo]) VALUES (3, N'Biglietto Over 60', 8, N'Biglietto del museo di storia romana di Cesena. Valido per persone oltre i 60 anni di età. Sconto del 20%.', 1)
INSERT [dbo].[TipoBiglietto] ([idTipoBiglietto], [Nome], [Prezzo], [Descrizione], [idMuseo]) VALUES (4, N'Biglietto Bambini', 3, N'Biglietto del museo di storia romana di Cesena. Valido per bambini sotto i 13 anni di età. Sconto del 70%.', 1)
INSERT [dbo].[TipoBiglietto] ([idTipoBiglietto], [Nome], [Prezzo], [Descrizione], [idMuseo]) VALUES (5, N'Biglietto Abitanti', 0, N'Biglietto del museo di storia romana di Cesena. Entrata gratuita per ogni abitante nato a Cesena.', 1)
SET IDENTITY_INSERT [dbo].[TipoBiglietto] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoContenuto] ON 

INSERT [dbo].[TipoContenuto] ([idTipoContenuto], [Descrizione]) VALUES (3, N'Arma romana')
INSERT [dbo].[TipoContenuto] ([idTipoContenuto], [Descrizione]) VALUES (1, N'Cimelio romano')
INSERT [dbo].[TipoContenuto] ([idTipoContenuto], [Descrizione]) VALUES (2, N'Mosaico romano')
SET IDENTITY_INSERT [dbo].[TipoContenuto] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoMuseo] ON 

INSERT [dbo].[TipoMuseo] ([idTipoMuseo], [Descrizione]) VALUES (4, N'Arte')
INSERT [dbo].[TipoMuseo] ([idTipoMuseo], [Descrizione]) VALUES (3, N'Medioevo')
INSERT [dbo].[TipoMuseo] ([idTipoMuseo], [Descrizione]) VALUES (7, N'Pittura')
INSERT [dbo].[TipoMuseo] ([idTipoMuseo], [Descrizione]) VALUES (5, N'Quadri')
INSERT [dbo].[TipoMuseo] ([idTipoMuseo], [Descrizione]) VALUES (6, N'Scultura')
INSERT [dbo].[TipoMuseo] ([idTipoMuseo], [Descrizione]) VALUES (1, N'Storia')
INSERT [dbo].[TipoMuseo] ([idTipoMuseo], [Descrizione]) VALUES (2, N'Storia romana')
SET IDENTITY_INSERT [dbo].[TipoMuseo] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoPersonale] ON 

INSERT [dbo].[TipoPersonale] ([idTipoPersonale], [Descrizione]) VALUES (4, N'Cassiere')
INSERT [dbo].[TipoPersonale] ([idTipoPersonale], [Descrizione]) VALUES (3, N'Cicerone')
INSERT [dbo].[TipoPersonale] ([idTipoPersonale], [Descrizione]) VALUES (1, N'Manager')
INSERT [dbo].[TipoPersonale] ([idTipoPersonale], [Descrizione]) VALUES (2, N'Spazzino')
SET IDENTITY_INSERT [dbo].[TipoPersonale] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoSezione] ON 

INSERT [dbo].[TipoSezione] ([idTipoSezione], [Descrizione]) VALUES (3, N'Armi')
INSERT [dbo].[TipoSezione] ([idTipoSezione], [Descrizione]) VALUES (2, N'Cimeli')
INSERT [dbo].[TipoSezione] ([idTipoSezione], [Descrizione]) VALUES (4, N'Dipinti')
INSERT [dbo].[TipoSezione] ([idTipoSezione], [Descrizione]) VALUES (1, N'Mosaici')
SET IDENTITY_INSERT [dbo].[TipoSezione] OFF
GO
/****** Object:  Index [UQ__Calendar__37A668215F5C078C]    Script Date: 16/12/2020 23:19:02 ******/
ALTER TABLE [dbo].[CalendarioApertureSpeciali] ADD UNIQUE NONCLUSTERED 
(
	[Data] ASC,
	[idMuseo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Calendar__37A6682143B28A60]    Script Date: 16/12/2020 23:19:02 ******/
ALTER TABLE [dbo].[CalendarioChiusure] ADD UNIQUE NONCLUSTERED 
(
	[Data] ASC,
	[idMuseo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Registro__09E152A4691B1C6C]    Script Date: 16/12/2020 23:19:02 ******/
ALTER TABLE [dbo].[RegistroManutenzioni] ADD UNIQUE NONCLUSTERED 
(
	[idMuseo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Registro__9C399B98A2E2C9E5]    Script Date: 16/12/2020 23:19:02 ******/
ALTER TABLE [dbo].[RegistroPresenze] ADD UNIQUE NONCLUSTERED 
(
	[idPersonale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TipoCont__4964852B395980BD]    Script Date: 16/12/2020 23:19:02 ******/
ALTER TABLE [dbo].[TipoContenuto] ADD UNIQUE NONCLUSTERED 
(
	[Descrizione] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TipoMuse__4964852B5B4C094B]    Script Date: 16/12/2020 23:19:02 ******/
ALTER TABLE [dbo].[TipoMuseo] ADD UNIQUE NONCLUSTERED 
(
	[Descrizione] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TipoPers__4964852BA9705B07]    Script Date: 16/12/2020 23:19:02 ******/
ALTER TABLE [dbo].[TipoPersonale] ADD UNIQUE NONCLUSTERED 
(
	[Descrizione] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TipoSezi__4964852BB45A5AF6]    Script Date: 16/12/2020 23:19:02 ******/
ALTER TABLE [dbo].[TipoSezione] ADD UNIQUE NONCLUSTERED 
(
	[Descrizione] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Biglietto]  WITH CHECK ADD  CONSTRAINT [REF_Bigli_Museo_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
GO
ALTER TABLE [dbo].[Biglietto] CHECK CONSTRAINT [REF_Bigli_Museo_FK]
GO
ALTER TABLE [dbo].[Biglietto]  WITH CHECK ADD  CONSTRAINT [REF_Bigli_TipoB_FK] FOREIGN KEY([idTipoBiglietto])
REFERENCES [dbo].[TipoBiglietto] ([idTipoBiglietto])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Biglietto] CHECK CONSTRAINT [REF_Bigli_TipoB_FK]
GO
ALTER TABLE [dbo].[CalendarioApertureSpeciali]  WITH CHECK ADD  CONSTRAINT [REF_Calen_Museo_1_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CalendarioApertureSpeciali] CHECK CONSTRAINT [REF_Calen_Museo_1_FK]
GO
ALTER TABLE [dbo].[CalendarioChiusure]  WITH CHECK ADD  CONSTRAINT [REF_Calen_Museo_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CalendarioChiusure] CHECK CONSTRAINT [REF_Calen_Museo_FK]
GO
ALTER TABLE [dbo].[Contenuto]  WITH CHECK ADD  CONSTRAINT [EQU_Conte_Prove_FK] FOREIGN KEY([idProvenienza])
REFERENCES [dbo].[Provenienza] ([idProvenienza])
GO
ALTER TABLE [dbo].[Contenuto] CHECK CONSTRAINT [EQU_Conte_Prove_FK]
GO
ALTER TABLE [dbo].[Contenuto]  WITH CHECK ADD  CONSTRAINT [REF_Conte_Perio_FK] FOREIGN KEY([idPeriodoStorico])
REFERENCES [dbo].[PeriodoStorico] ([idPeriodoStorico])
GO
ALTER TABLE [dbo].[Contenuto] CHECK CONSTRAINT [REF_Conte_Perio_FK]
GO
ALTER TABLE [dbo].[Contenuto]  WITH CHECK ADD  CONSTRAINT [REF_Conte_Sezio_FK] FOREIGN KEY([idSezione])
REFERENCES [dbo].[Sezione] ([idSezione])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contenuto] CHECK CONSTRAINT [REF_Conte_Sezio_FK]
GO
ALTER TABLE [dbo].[Contenuto_Tipologia]  WITH CHECK ADD  CONSTRAINT [EQU_Conte_Conte] FOREIGN KEY([idContenuto])
REFERENCES [dbo].[Contenuto] ([idContenuto])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contenuto_Tipologia] CHECK CONSTRAINT [EQU_Conte_Conte]
GO
ALTER TABLE [dbo].[Contenuto_Tipologia]  WITH CHECK ADD  CONSTRAINT [REF_Conte_TipoC_FK] FOREIGN KEY([idTipoContenuto])
REFERENCES [dbo].[TipoContenuto] ([idTipoContenuto])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contenuto_Tipologia] CHECK CONSTRAINT [REF_Conte_TipoC_FK]
GO
ALTER TABLE [dbo].[Creato]  WITH CHECK ADD  CONSTRAINT [EQU_Creat_Creat_FK] FOREIGN KEY([idCreatore])
REFERENCES [dbo].[Creatore] ([idCreatore])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Creato] CHECK CONSTRAINT [EQU_Creat_Creat_FK]
GO
ALTER TABLE [dbo].[Creato]  WITH CHECK ADD  CONSTRAINT [REF_Creat_Conte] FOREIGN KEY([idContenuto])
REFERENCES [dbo].[Contenuto] ([idContenuto])
GO
ALTER TABLE [dbo].[Creato] CHECK CONSTRAINT [REF_Creat_Conte]
GO
ALTER TABLE [dbo].[Museo]  WITH CHECK ADD  CONSTRAINT [EQU_Museo_Famig_FK] FOREIGN KEY([idFamiglia])
REFERENCES [dbo].[FamigliaMusei] ([idFamiglia])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Museo] CHECK CONSTRAINT [EQU_Museo_Famig_FK]
GO
ALTER TABLE [dbo].[Museo_Creatore]  WITH CHECK ADD  CONSTRAINT [EQU_Museo_Creat] FOREIGN KEY([idCreatore])
REFERENCES [dbo].[Creatore] ([idCreatore])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Museo_Creatore] CHECK CONSTRAINT [EQU_Museo_Creat]
GO
ALTER TABLE [dbo].[Museo_Creatore]  WITH CHECK ADD  CONSTRAINT [REF_Museo_Museo_2_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Museo_Creatore] CHECK CONSTRAINT [REF_Museo_Museo_2_FK]
GO
ALTER TABLE [dbo].[Museo_PeriodoStorico]  WITH CHECK ADD  CONSTRAINT [EQU_Museo_Perio] FOREIGN KEY([idPeriodoStorico])
REFERENCES [dbo].[PeriodoStorico] ([idPeriodoStorico])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Museo_PeriodoStorico] CHECK CONSTRAINT [EQU_Museo_Perio]
GO
ALTER TABLE [dbo].[Museo_PeriodoStorico]  WITH CHECK ADD  CONSTRAINT [REF_Museo_Museo_1_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Museo_PeriodoStorico] CHECK CONSTRAINT [REF_Museo_Museo_1_FK]
GO
ALTER TABLE [dbo].[Museo_Provenienza]  WITH CHECK ADD  CONSTRAINT [EQU_Museo_Prove] FOREIGN KEY([idProvenienza])
REFERENCES [dbo].[Provenienza] ([idProvenienza])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Museo_Provenienza] CHECK CONSTRAINT [EQU_Museo_Prove]
GO
ALTER TABLE [dbo].[Museo_Provenienza]  WITH CHECK ADD  CONSTRAINT [REF_Museo_Museo_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Museo_Provenienza] CHECK CONSTRAINT [REF_Museo_Museo_FK]
GO
ALTER TABLE [dbo].[Museo_Tipologia]  WITH CHECK ADD  CONSTRAINT [EQU_Museo_Museo_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Museo_Tipologia] CHECK CONSTRAINT [EQU_Museo_Museo_FK]
GO
ALTER TABLE [dbo].[Museo_Tipologia]  WITH CHECK ADD  CONSTRAINT [REF_Museo_TipoM] FOREIGN KEY([idTipoMuseo])
REFERENCES [dbo].[TipoMuseo] ([idTipoMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Museo_Tipologia] CHECK CONSTRAINT [REF_Museo_TipoM]
GO
ALTER TABLE [dbo].[Personale]  WITH CHECK ADD  CONSTRAINT [EQU_Perso_Museo_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Personale] CHECK CONSTRAINT [EQU_Perso_Museo_FK]
GO
ALTER TABLE [dbo].[Personale_Tipologia]  WITH CHECK ADD  CONSTRAINT [EQU_Perso_Perso] FOREIGN KEY([idPersonale])
REFERENCES [dbo].[Personale] ([idPersonale])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Personale_Tipologia] CHECK CONSTRAINT [EQU_Perso_Perso]
GO
ALTER TABLE [dbo].[Personale_Tipologia]  WITH CHECK ADD  CONSTRAINT [REF_Perso_TipoP_FK] FOREIGN KEY([idTipoPersonale])
REFERENCES [dbo].[TipoPersonale] ([idTipoPersonale])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Personale_Tipologia] CHECK CONSTRAINT [REF_Perso_TipoP_FK]
GO
ALTER TABLE [dbo].[RegistroManutenzioni]  WITH CHECK ADD  CONSTRAINT [SID_Regis_Museo_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RegistroManutenzioni] CHECK CONSTRAINT [SID_Regis_Museo_FK]
GO
ALTER TABLE [dbo].[RegistroPresenze]  WITH CHECK ADD  CONSTRAINT [SID_Regis_Perso_FK] FOREIGN KEY([idPersonale])
REFERENCES [dbo].[Personale] ([idPersonale])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RegistroPresenze] CHECK CONSTRAINT [SID_Regis_Perso_FK]
GO
ALTER TABLE [dbo].[Sezione]  WITH CHECK ADD  CONSTRAINT [EQU_Sezio_Museo_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sezione] CHECK CONSTRAINT [EQU_Sezio_Museo_FK]
GO
ALTER TABLE [dbo].[Sezione]  WITH CHECK ADD  CONSTRAINT [REF_Sezio_Sezio_FK] FOREIGN KEY([idSezionePadre])
REFERENCES [dbo].[Sezione] ([idSezione])
GO
ALTER TABLE [dbo].[Sezione] CHECK CONSTRAINT [REF_Sezio_Sezio_FK]
GO
ALTER TABLE [dbo].[Sezione_Tipologia]  WITH CHECK ADD  CONSTRAINT [EQU_Sezio_Sezio_FK] FOREIGN KEY([idSezione])
REFERENCES [dbo].[Sezione] ([idSezione])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sezione_Tipologia] CHECK CONSTRAINT [EQU_Sezio_Sezio_FK]
GO
ALTER TABLE [dbo].[Sezione_Tipologia]  WITH CHECK ADD  CONSTRAINT [REF_Sezio_TipoS] FOREIGN KEY([idTipoSezione])
REFERENCES [dbo].[TipoSezione] ([idTipoSezione])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sezione_Tipologia] CHECK CONSTRAINT [REF_Sezio_TipoS]
GO
ALTER TABLE [dbo].[StatisticheFamigliaMusei]  WITH CHECK ADD  CONSTRAINT [ID_Stati_Stati_1_FK] FOREIGN KEY([idStatistiche])
REFERENCES [dbo].[Statistiche] ([idStatistiche])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StatisticheFamigliaMusei] CHECK CONSTRAINT [ID_Stati_Stati_1_FK]
GO
ALTER TABLE [dbo].[StatisticheFamigliaMusei]  WITH CHECK ADD  CONSTRAINT [REF_Stati_Famig_FK] FOREIGN KEY([idFamiglia])
REFERENCES [dbo].[FamigliaMusei] ([idFamiglia])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StatisticheFamigliaMusei] CHECK CONSTRAINT [REF_Stati_Famig_FK]
GO
ALTER TABLE [dbo].[StatisticheMuseo]  WITH CHECK ADD  CONSTRAINT [ID_Stati_Stati_FK] FOREIGN KEY([idStatistiche])
REFERENCES [dbo].[Statistiche] ([idStatistiche])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StatisticheMuseo] CHECK CONSTRAINT [ID_Stati_Stati_FK]
GO
ALTER TABLE [dbo].[StatisticheMuseo]  WITH CHECK ADD  CONSTRAINT [REF_Stati_Museo_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StatisticheMuseo] CHECK CONSTRAINT [REF_Stati_Museo_FK]
GO
ALTER TABLE [dbo].[TipoBiglietto]  WITH CHECK ADD  CONSTRAINT [EQU_TipoB_Museo_FK] FOREIGN KEY([idMuseo])
REFERENCES [dbo].[Museo] ([idMuseo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TipoBiglietto] CHECK CONSTRAINT [EQU_TipoB_Museo_FK]
GO
/****** Object:  StoredProcedure [dbo].[DeleteContenuto]    Script Date: 16/12/2020 23:19:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteContenuto] (@idContenuto INT)
AS
BEGIN
	DECLARE @ids TABLE	(idContenuto INT)
	DECLARE @tmp TABLE	(idContenuto INT)
	DELETE Contenuto WHERE idContenuto = @idContenuto;

	INSERT INTO @tmp(idContenuto)
	SELECT idContenuto 
	FROM Contenuto 
	WHERE idContenutoPadre = @idContenuto;

	INSERT INTO @ids
	SELECT idContenuto
	FROM @tmp;


	WHILE (SELECT COUNT(idContenuto) FROM @tmp) > 0
	BEGIN
	DELETE @tmp;
	DELETE Contenuto WHERE idContenuto IN (SELECT idContenuto FROM @ids);

	INSERT INTO @tmp(idContenuto)
	SELECT idContenuto 
	FROM Contenuto 
	WHERE idContenutoPadre IN (SELECT idContenuto FROM @ids);

	DELETE @ids;

	INSERT INTO @ids
	SELECT idContenuto
	FROM @tmp;
	END
END
GO
