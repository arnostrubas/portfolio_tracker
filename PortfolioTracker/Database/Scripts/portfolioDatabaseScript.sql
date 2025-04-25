CREATE DATABASE Portfolio
GO

USE [Portfolio]
GO

CREATE TABLE [dbo].Portfolio(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderType] [varchar](20),
	[Ticker] [varchar](5),
	[Amount] [int],
	[Date] [DATE], 
	[Price] [float],
	[CurrentPrice] [float]
	CONSTRAINT [PK_Portfolio] PRIMARY KEY CLUSTERED ([Id] ASC)
)	 