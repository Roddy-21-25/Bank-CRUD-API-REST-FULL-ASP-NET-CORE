USE BANK
CREATE TABLE Client(
	ClientId int PRIMARY KEY IDENTITY (1,1),
	ClientFullName varchar(500),
	ClientAge int,
	ClientEmail varchar(500),
	ClientPassword varchar(500),
	ClientNotification varchar(2000)
)
GO
CREATE TABLE ClientAccount(
	AccountId int PRIMARY KEY IDENTITY (1,1),
	AccountName varchar(500),
	AccountAmount int,
	ClientIdAccount int,
	AccountTransaccion varchar(2000),
	AccountPaymentHistory varchar(2000),
	AccountNotification varchar(2000),
	AccountCardType varchar(500),
	AccountBadge varchar(500)
)
GO
CREATE TABLE BankAccount(
	BankId int PRIMARY KEY IDENTITY (1,1),
	BankUserAdmin varchar(500),
	BankPasswordAdmin varchar(500),
	ClientId int,
	AccountId int,
	BankAmount int
)
GO
