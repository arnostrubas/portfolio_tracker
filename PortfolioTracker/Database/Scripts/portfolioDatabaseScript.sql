CREATE DATABASE Portfolio
GO

USE [Portfolio]
GO

CREATE TABLE [dbo].Portfolio(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderType] [int],
	[Ticker] [varchar](5),
	[Amount] DECIMAL(18, 4),
	[Date] [DATE], 
	[Price] DECIMAL(18, 4),
	[CurrentPrice] DECIMAL(18, 4)
	CONSTRAINT [PK_Portfolio] PRIMARY KEY CLUSTERED ([Id] ASC)
)	 