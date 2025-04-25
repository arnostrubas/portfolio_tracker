CREATE DATABASE PortfolioManager
GO

USE [PortfolioManager]
GO

CREATE TABLE [dbo].PortfolioManager(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50),
	[ConnectionString] [varchar](200),
	[NumberOfCompanies] [int], 
	[Invested] [float],
	[CurrentValue] [float],
	[Profit] [float]
	CONSTRAINT [PK_Portfolio] PRIMARY KEY CLUSTERED ([Id] ASC)
)	 