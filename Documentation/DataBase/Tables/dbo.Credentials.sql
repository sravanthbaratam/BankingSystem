CREATE TABLE [dbo].[Credentials] (
    [AccountNumber] BIGINT        NOT NULL,
    [Password]      NVARCHAR (10) NOT NULL,
    [Role]          NVARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([AccountNumber] ASC)
);

