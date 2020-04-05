CREATE PROCEDURE [dbo].[SP_UpdateCheque]
(
  @ChequeJson nvarchar(max)
)
AS BEGIN
  Truncate TABLE Cheque
  INSERT INTO Cheque 
  (
   RequestId ,
    ChequeBookNumber ,
    AccountNumber ,
    ChequeBookStatus ,
    ApprovedBy
  )
  SELECT RequestId ,
    ChequeBookNumber ,
    AccountNumber ,
    ChequeBookStatus ,
    ApprovedBy
  FROM OPENJSON(@ChequeJson)
       WITH (
        RequestId bigint,
    ChequeBookNumber bigint,
    AccountNumber int,
    ChequeBookStatus bit,
    ApprovedBy nvarchar(10)
       )
end