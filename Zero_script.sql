USE [master]
GO
/****** Object:  Database [ZeroCarbonDb]    Script Date: 24.06.2024 23:29:16 ******/
CREATE DATABASE [ZeroCarbonDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ZeroCarbon', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ZeroCarbon.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ZeroCarbon_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ZeroCarbon_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ZeroCarbonDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ZeroCarbonDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ZeroCarbonDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ZeroCarbonDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ZeroCarbonDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ZeroCarbonDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ZeroCarbonDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ZeroCarbonDb] SET  MULTI_USER 
GO
ALTER DATABASE [ZeroCarbonDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ZeroCarbonDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ZeroCarbonDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ZeroCarbonDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ZeroCarbonDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ZeroCarbonDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ZeroCarbonDb] SET QUERY_STORE = OFF
GO
USE [ZeroCarbonDb]
GO
/****** Object:  User [ZeroCarbonUser]    Script Date: 24.06.2024 23:29:17 ******/
CREATE USER [ZeroCarbonUser] FOR LOGIN [ZeroCarbonUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ZeroCarbonUser]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Status] [bit] NOT NULL,
	[RecordUsername] [nvarchar](50) NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[UpdateUsername] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[Ip] [nvarchar](70) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[PhoneNumber] [varchar](255) NOT NULL,
	[CountyId] [int] NOT NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyDatas]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyDatas](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[EmissionSourceId] [bigint] NOT NULL,
	[Value] [decimal](18, 4) NOT NULL,
	[KisiSayisi] [int] NULL,
	[UcakKapasitesi] [int] NULL,
	[RecordUsername] [nvarchar](50) NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[UpdateUsername] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[Ip] [nvarchar](70) NOT NULL,
	[OdaSayisi] [int] NULL,
	[YukAgirligi] [int] NULL,
 CONSTRAINT [PK_CompanyData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyEmissionSources]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyEmissionSources](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[EmissionSourceId] [bigint] NOT NULL,
	[RecordUsername] [nvarchar](50) NOT NULL,
	[RecordDate] [datetime2](7) NOT NULL,
	[UpdateUsername] [nvarchar](50) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[Ip] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_CompanyEmissionSources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_CompanyEmissionSources] UNIQUE NONCLUSTERED 
(
	[CompanyId] ASC,
	[EmissionSourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Counties]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Counties](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CityId] [bigint] NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK_Counties] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmissionSources]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmissionSources](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EmissionSourceScopeId] [bigint] NOT NULL,
	[GroupCode] [nvarchar](10) NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Unit] [nvarchar](250) NULL,
	[CalorificBasic] [decimal](26, 8) NULL,
	[CalorificBasicUnit] [nvarchar](50) NULL,
	[CO2] [decimal](26, 8) NULL,
	[CO2Unit] [nvarchar](50) NULL,
	[CH4] [decimal](26, 8) NULL,
	[CH4Unit] [nvarchar](50) NULL,
	[N2O] [decimal](26, 8) NULL,
	[N2OUnit] [nvarchar](50) NULL,
	[Density] [decimal](26, 8) NULL,
	[DensityUnit] [nvarchar](50) NULL,
	[KIP] [decimal](26, 8) NULL,
	[WeightOfCargo] [int] NULL,
	[NumberOfRoom] [int] NULL,
	[NumberOfPeopleTraveling] [int] NULL,
	[Status] [bit] NOT NULL,
	[RecordUsername] [nvarchar](50) NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[UpdateUsername] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[Ip] [nvarchar](70) NOT NULL,
 CONSTRAINT [PK_EmissionSources2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmissionSourceScopes]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmissionSourceScopes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
	[RecordUsername] [nvarchar](50) NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[UpdateUsername] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[Ip] [nvarchar](70) NOT NULL,
 CONSTRAINT [PK_EmissionScopes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_EmissionSourceScopes] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupClaims]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupClaims](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupId] [bigint] NOT NULL,
	[OperationClaimId] [bigint] NOT NULL,
	[RecordUsername] [nvarchar](max) NULL,
	[RecordDate] [datetime2](7) NOT NULL,
	[UpdateUsername] [nvarchar](max) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[Ip] [nvarchar](max) NULL,
 CONSTRAINT [PK_GroupClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](max) NULL,
	[RecordUsername] [nvarchar](max) NULL,
	[RecordDate] [datetime2](7) NOT NULL,
	[UpdateUsername] [nvarchar](max) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[Ip] [nvarchar](max) NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NULL,
	[Exception] [nvarchar](max) NULL,
	[UserId] [bigint] NULL,
	[Method] [nvarchar](max) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperationClaims]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationClaims](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Alias] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[RecordUsername] [nvarchar](max) NULL,
	[RecordDate] [datetime2](7) NOT NULL,
	[UpdateUsername] [nvarchar](max) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[Ip] [nvarchar](max) NULL,
 CONSTRAINT [PK_OperationClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroups]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroups](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RecordUsername] [nvarchar](max) NULL,
	[RecordDate] [datetime2](7) NOT NULL,
	[UpdateUsername] [nvarchar](max) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[Ip] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 24.06.2024 23:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](max) NULL,
	[Lastname] [nvarchar](max) NULL,
	[Username] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Avatar] [varbinary](max) NULL,
	[PasswordSalt] [varbinary](max) NULL,
	[PasswordHash] [varbinary](max) NULL,
	[RefreshToken] [nvarchar](max) NULL,
	[Status] [bit] NOT NULL,
	[LastLogin] [datetime2](7) NULL,
	[RecordUsername] [nvarchar](max) NULL,
	[RecordDate] [datetime2](7) NOT NULL,
	[UpdateUsername] [nvarchar](max) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[Ip] [nvarchar](max) NULL,
	[LoginType] [int] NOT NULL,
	[IsEmailActive] [bit] NOT NULL,
	[ActivationCode] [nvarchar](100) NOT NULL,
	[ActivationCodeTime] [datetime2](7) NOT NULL,
	[ActivationTime] [datetime2](7) NULL,
	[ForgotPasswordCode] [nvarchar](100) NULL,
	[ForgotPasswordCodeTime] [datetime2](7) NULL,
	[ForgotPasswordTime] [datetime2](7) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanyDatas]  WITH CHECK ADD  CONSTRAINT [Company Id] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[CompanyDatas] CHECK CONSTRAINT [Company Id]
GO
ALTER TABLE [dbo].[CompanyDatas]  WITH CHECK ADD  CONSTRAINT [Emission Source Id] FOREIGN KEY([EmissionSourceId])
REFERENCES [dbo].[EmissionSources] ([Id])
GO
ALTER TABLE [dbo].[CompanyDatas] CHECK CONSTRAINT [Emission Source Id]
GO
ALTER TABLE [dbo].[CompanyEmissionSources]  WITH CHECK ADD  CONSTRAINT [CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[CompanyEmissionSources] CHECK CONSTRAINT [CompanyId]
GO
ALTER TABLE [dbo].[Counties]  WITH CHECK ADD  CONSTRAINT [FK_Counties_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
GO
ALTER TABLE [dbo].[Counties] CHECK CONSTRAINT [FK_Counties_Cities]
GO
ALTER TABLE [dbo].[EmissionSources]  WITH CHECK ADD  CONSTRAINT [Emission Source Scope] FOREIGN KEY([EmissionSourceScopeId])
REFERENCES [dbo].[EmissionSourceScopes] ([Id])
GO
ALTER TABLE [dbo].[EmissionSources] CHECK CONSTRAINT [Emission Source Scope]
GO
USE [master]
GO
ALTER DATABASE [ZeroCarbonDb] SET  READ_WRITE 
GO
