CREATE PROCEDURE [dbo].[DeleteSezione] (@idSezione INT)
AS
DECLARE @ids TABLE	(idSezione INT)
	DECLARE @tmp TABLE	(idSezione INT)
	DELETE Sezione WHERE idSezione = @idSezione;

	INSERT INTO @tmp(idSezione)
	SELECT idSezione 
	FROM Sezione 
	WHERE idSezionePadre = @idSezione;

	INSERT INTO @ids
	SELECT idSezione
	FROM @tmp;


	WHILE (SELECT COUNT(idSezione) FROM @tmp) > 0
	BEGIN
	DELETE @tmp;
	DELETE Sezione WHERE idSezione IN (SELECT idSezione FROM @ids);

	INSERT INTO @tmp(idSezione)
	SELECT idSezione 
	FROM Sezione 
	WHERE idSezionePadre IN (SELECT idSezione FROM @ids);

	DELETE @ids;

	INSERT INTO @ids
	SELECT idSezione
	FROM @tmp;
END