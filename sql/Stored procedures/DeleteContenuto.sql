SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE DeleteContenuto (@idContenuto INT)
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
