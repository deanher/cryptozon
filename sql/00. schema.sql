--// database

drop database if exists Cryptozon
go

create database Cryptozon
go

--// Users

drop table if exists Cryptozon.dbo.Users
go

create table Cryptozon.dbo.Users
     ( UserId        uniqueidentifier default newsequentialid(),
       Username      nvarchar(100)    not null,
       PasswordSalt  nvarchar(100)    not null,
       PasswordHash  nvarchar(100)    not null,
       LastEditedOn  datetime         not null,
       constraint PK_Users_UserId primary key nonclustered (UserId),
       constraint AK_Users_Username unique (Username) )
go

--// Purchases
        
drop table if exists Cryptozon.dbo.Purchases
go

create table Cryptozon.dbo.Purchases
     ( Reference   uniqueidentifier not null,
       UserId      uniqueidentifier not null,
       CoinId      int              not null,
       Quantity    decimal(18, 9)   not null, 
       UnitPrice   decimal(18, 9)   not null,
       PurchasedOn datetime         not null
       constraint PK_Purchases_Reference_CoinId primary key nonclustered (Reference, CoinId),
       constraint FK_Purchases_Users_UserId foreign key (UserId) references Cryptozon.dbo.Users(UserId) )
go
