CREATE TABLE [dbo].[Transactions]
(
	[RowId] INT NOT NULL PRIMARY KEY, 
    [externalId] UNIQUEIDENTIFIER NOT NULL, 
    [fromAccount] NVARCHAR(50) NOT NULL, 
    [toAccount] NVARCHAR(50) NOT NULL, 
    [description] NCHAR(10) NOT NULL, 
    [amount] NUMERIC(18, 4) NOT NULL, 
    [date] DATETIME2 NOT NULL, 
    [owner] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_Transactions_ToCustomers] FOREIGN KEY ([owner]) REFERENCES [Customers]([Id])
)
