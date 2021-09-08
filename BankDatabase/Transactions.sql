CREATE TABLE [dbo].[Transactions]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [externalId] UNIQUEIDENTIFIER NOT NULL, 
    [fromAccount] NVARCHAR(50) NOT NULL, 
    [toAccount] NVARCHAR(50) NULL, 
    [description] NCHAR(10) NULL, 
    [amount] NUMERIC(18, 4) NULL, 
    [date] DATETIME2 NULL, 
    [owner] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_Transactions_ToCustomers] FOREIGN KEY ([owner]) REFERENCES [Customers]([Id])
)
