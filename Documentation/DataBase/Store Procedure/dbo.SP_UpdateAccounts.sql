CREATE PROCEDURE [dbo].[SP_UpdateAccounts]
(
  @AccountsJson nvarchar(max)
)
AS BEGIN
  Truncate TABLE Accounts
  INSERT INTO Accounts (
    [Name],
    Pan,
    AadhaarNumber,
    AccountNumber,
    AccountType,
    Balance,
    Email,
    Mobile,
    [Address],
    AccountStatus,
    ApprovedBy,
    Transactions
    )
  SELECT  [Name],
    Pan,
    AadhaarNumber,
    AccountNumber,
    AccountType,
    Balance,
    Email,
    Mobile,
    [Address],
    AccountStatus,
    ApprovedBy,
    Transactions
  FROM OPENJSON(@AccountsJson)
       WITH (
        [Name] nvarchar(10),
        [Pan] nvarchar(10),
        AadhaarNumber bigint,
        AccountNumber int,
        AccountType int,
        Balance float,
        Email nvarchar(20),
        Mobile bigint,
        [Address] nvarchar(10),
        AccountStatus bit,
        ApprovedBy nvarchar(10),
        Transactions nvarchar(10)
    )
end