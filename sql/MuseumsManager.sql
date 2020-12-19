
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
