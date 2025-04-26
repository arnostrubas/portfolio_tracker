CREATE DATABASE Portfolio
GO

USE [Portfolio]
GO

CREATE TABLE [dbo].Portfolio(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderType] [int],
	[Ticker] [varchar](5),
	[Amount] [float],
	[Date] [DATE], 
	[Price] [float],
	[CurrentPrice] [float]
	CONSTRAINT [PK_Portfolio] PRIMARY KEY CLUSTERED ([Id] ASC)
)	 