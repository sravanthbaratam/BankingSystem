create table Credentials  (
    AccountNumber  int not null primary key,
    Password nvarchar(10) not null,
    Role nvarchar(10)
  )

 create table Cheque  (
    RequestId int not null primary key,
    ChequeBookNumber int,
    AccountNumber int,
    ChequeBookStatus bit,
    ApprovedBy nvarchar(10)
)

create table Accounts (
    Name nvarchar(10),
    Pan nvarchar(10),
    AadhaarNumber int,
    AccountNumber int,
    AccountType int,
    Balance float,
    Email nvarchar(20),
    Mobile int,
    Address nvarchar(10),
    AccountStatus bit,
    ApprovedBy nvarchar(10),
    Transactions nvarchar(10)
)

