CREATE TABLE [dbo].[Accounts] (
    [Name]          NVARCHAR (MAX) NOT NULL,
    [Pan]           NVARCHAR (10)  NOT NULL,
    [AadhaarNumber] BIGINT         NOT NULL,
    [AccountNumber] INT            NOT NULL,
    [AccountType]   INT            NOT NULL,
    [Balance]       FLOAT (53)     NOT NULL,
    [Email]         NVARCHAR (25)  NOT NULL,
    [Mobile]        BIGINT         NOT NULL,
    [Address]       NVARCHAR (10)  NOT NULL,
    [AccountStatus] BIT            NULL,
    [ApprovedBy]    NVARCHAR (10)  NULL,
    [Transactions]  NVARCHAR (10)  NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([AccountNumber] ASC)
);

