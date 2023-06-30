
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/29/2023 19:50:28
-- Generated from EDMX file: C:\Users\Mark Well Mapanao\source\repos\CSharpRDL\CSharpRDL\Models\MVCTestModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MVCTest];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Employee201file]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee201file];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Employee201file'
CREATE TABLE [dbo].[Employee201file] (
    [EmployeeId] int IDENTITY(1,1) NOT NULL,
    [Firstname] varchar(100)  NOT NULL,
    [Lastname] varchar(100)  NOT NULL,
    [Middlename] varchar(100)  NULL,
    [Suffix] varchar(100)  NULL,
    [ContactNo] varchar(100)  NOT NULL,
    [Address] varchar(100)  NOT NULL,
    [Birthdate] varchar(100)  NOT NULL,
    [Age] int  NULL,
    [Department] varchar(100)  NULL,
    [ProfileImg] varbinary(max)  NULL,
    [ImagePath] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Username] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Email] varchar(100)  NULL,
    [IsActive] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [EmployeeId] in table 'Employee201file'
ALTER TABLE [dbo].[Employee201file]
ADD CONSTRAINT [PK_Employee201file]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------