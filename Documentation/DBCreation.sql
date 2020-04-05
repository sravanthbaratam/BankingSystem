--CREATE PROCEDURE [dbo].[SP_UpdateCheque]
--AS
--begin
--	Declare @JSON varchar(max)
--	DROP TABLE IF EXISTS Cheque
--	SELECT @JSON=BulkColumn
--	FROM OPENROWSET (BULK 'C:\Users\bs\Desktop\jsons\Cheque.json', SINGLE_CLOB) import
--	SELECT * INTO  Cheque
--	FROM OPENJSON (@JSON) with
--	(
--	[AccountNumbber] int,
--	[Password] nvarchar(10),
--	[Role] nvarchar(10)
--	)
--end

ALTER PROCEDURE [dbo].[SP_UpdateCredentials]
AS
begin
	Declare @JSON varchar(max)
	DROP TABLE IF EXISTS Credentials
	SELECT @JSON=BulkColumn
	FROM OPENROWSET (BULK 'C:\Users\bs\Desktop\jsons\Credentials.json', SINGLE_CLOB) import
	SELECT * INTO  Credentials
	FROM OPENJSON (@JSON) with
	(
	[AccountNumbber] int primary key,
	[Password] nvarchar(10) not null,
	[Role] nvarchar(10) not null
	)
end