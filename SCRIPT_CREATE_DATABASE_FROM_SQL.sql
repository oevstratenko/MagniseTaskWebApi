/*
	YOU CAN CREATE DB USING COMMAND "UPDATE-DATABASE" FROM PACKAGE MANAGEMENT CONSOLE
	OR
	RUN SCRIPT BELOW
*/

USE [master]
GO

CREATE DATABASE [MagniseTaskDb] 
GO

USE [MagniseTaskDb]
GO

CREATE TABLE [dbo].[Crypto](
	[Id] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Price_usd] [numeric](18, 5) NULL,
	[Updated] [datetime] NULL,
 CONSTRAINT [PK_Crypto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))
GO

CREATE TABLE [dbo].[CryptoHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CryptoId] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Price_usd] [numeric](18, 5) NULL,
	[Updated] [datetime] NULL,
 CONSTRAINT [PK_CryptoHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
GO

ALTER TABLE [dbo].[CryptoHistory]  WITH CHECK ADD  CONSTRAINT [FK_CryptoHistory_Crypto_CryptoId] FOREIGN KEY([CryptoId])
REFERENCES [dbo].[Crypto] ([Id])
GO
