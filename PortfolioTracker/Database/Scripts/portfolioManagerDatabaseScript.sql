CREATE DATABASE PortfolioManager
GO

USE [PortfolioManager]
GO

CREATE TABLE [dbo].PortfolioManager(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50),
	[ConnectionString] [varchar](200),
	[NumberOfOrders] [int], 
	[Invested] DECIMAL(18, 4),
	[CurrentValue] DECIMAL(18, 4),
	[Profit] DECIMAL(18, 4)
	CONSTRAINT [PK_Portfolio] PRIMARY KEY CLUSTERED ([Id] ASC)
)	 