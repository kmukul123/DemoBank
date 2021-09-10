CREATE TABLE [dbo].[Transactions]
(
	[RowId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [externalId] UNIQUEIDENTIFIER NOT NULL UNIQUE, 
    [fromAccount] NVARCHAR(50) NOT NULL, 
    [toAccount] NVARCHAR(50) NOT NULL, 
    [description] NVARCHAR(100) NOT NULL, 
    [amount] NUMERIC(18, 4) NOT NULL, 
    [date] DATETIME2 NOT NULL, 
    [ownerId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_Transactions_ToCustomers] FOREIGN KEY ([ownerId]) REFERENCES [Customers]([Id])
)

GO

CREATE INDEX [IX_Transactions_ExternalId] ON [dbo].[Transactions] ([externalId])
