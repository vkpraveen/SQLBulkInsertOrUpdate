/************************************
-- Created By: Praveen Kumar
-- Created On: 31st May 2016
-- Description: To work with bulk insert or update operation.
************************************/
USE PKV
GO

--Create Table
CREATE TABLE Tbl_MyData
(
Id INT NOT NULL PRIMARY KEY,
RandomString VARCHAR(50)
)
GO

SELECT * FROM Tbl_MyData
GO
--Create Custom Type
CREATE TYPE [dbo].[MyCustomType] AS TABLE
(
Id INT NOT NULL PRIMARY KEY,
RandomString VARCHAR(50)
)
GO

--Stored Procedure for bulk insert or update operation
CREATE PROCEDURE USP_BulkInsertOrUpdate
(
@P_MyCustomType MyCustomType READONLY
)
AS
BEGIN
	--Only Insert goes here
	--INSERT INTO Tbl_MyData
	--SELECT Id,RandomString FROM @P_MyCustomType;
	--Only Update goes here
	--UPDATE D
	--SET D.RandomString=S.RandomString
	--FROM Tbl_MyData D WITH(NOLOCK)
	--JOIN @P_MyCustomType S
	--ON D.Id=S.Id
	--Merge goes here
	MERGE Tbl_MyData D
	USING @P_MyCustomType S
	ON D.Id=S.Id
	WHEN NOT MATCHED THEN
	INSERT VALUES
	(S.Id,S.RandomString)
	WHEN MATCHED THEN
	UPDATE SET
	D.Id=S.Id,D.RandomString=S.RandomString;
END
GO