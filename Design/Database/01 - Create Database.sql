USE [master]
GO

/****** Object:  Database [SmartSession]    Script Date: 2018/08/13 10:42:17 ******/
CREATE DATABASE [SmartSession]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SmartSession', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.ROBLT\MSSQL\DATA\SmartSession.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SmartSession_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.ROBLT\MSSQL\DATA\SmartSession_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [SmartSession] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SmartSession].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [SmartSession] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [SmartSession] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [SmartSession] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [SmartSession] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [SmartSession] SET ARITHABORT OFF 
GO

ALTER DATABASE [SmartSession] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [SmartSession] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [SmartSession] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [SmartSession] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [SmartSession] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [SmartSession] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [SmartSession] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [SmartSession] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [SmartSession] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [SmartSession] SET  DISABLE_BROKER 
GO

ALTER DATABASE [SmartSession] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [SmartSession] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [SmartSession] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [SmartSession] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [SmartSession] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [SmartSession] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [SmartSession] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [SmartSession] SET RECOVERY FULL 
GO

ALTER DATABASE [SmartSession] SET  MULTI_USER 
GO

ALTER DATABASE [SmartSession] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [SmartSession] SET DB_CHAINING OFF 
GO

ALTER DATABASE [SmartSession] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [SmartSession] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [SmartSession] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [SmartSession] SET QUERY_STORE = OFF
GO

USE [SmartSession]
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [SmartSession] SET  READ_WRITE 
GO

