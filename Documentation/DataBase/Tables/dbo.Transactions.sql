CREATE TABLE [dbo].[Transactions] (
    [TransactionId]   BIGINT         NOT NULL,
    [AccountNumber]   INT         NOT NULL,
    [TransactionType] INT            NULL,
    [Amount]          FLOAT (53)     NOT NULL,
    [RecieverAccount] INT         NOT NULL,
    [TimeStamp]       NVARCHAR (MAX) NULL
);

