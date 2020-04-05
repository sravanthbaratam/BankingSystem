CREATE PROCEDURE [dbo].[SP_UpdateTransactions]
(
  @TransactionsJson nvarchar(max)
)
AS BEGIN
  Truncate TABLE Transactions
  INSERT INTO Transactions (
            [TransactionId]    ,
    [AccountNumber]   ,
    [TransactionType]  ,
    [Amount]             ,
    [RecieverAccount]  ,
    [TimeStamp]
    )
  SELECT   [TransactionId]    ,
    [AccountNumber]   ,
    [TransactionType]  ,
    [Amount]             ,
    [RecieverAccount]  ,
    [TimeStamp]
  FROM OPENJSON(@TransactionsJson)
       WITH (
         [TransactionId]   BIGINT ,
    [AccountNumber]   INT,
    [TransactionType] INT ,
    [Amount]          FLOAT   ,
    [RecieverAccount] INT ,
    [TimeStamp]        nvarchar(MAX)
    )
end