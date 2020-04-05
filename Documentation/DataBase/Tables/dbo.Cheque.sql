CREATE TABLE [dbo].[Cheque] (
    [RequestId]        BIGINT        NOT NULL,
    [ChequeBookNumber] BIGINT        NOT NULL,
    [AccountNumber]    INT           NOT NULL,
    [ChequeBookStatus] BIT           NOT NULL,
    [ApprovedBy]       NVARCHAR (10) NULL,
    CONSTRAINT [PK_Cheque] PRIMARY KEY CLUSTERED ([AccountNumber] ASC)
);

