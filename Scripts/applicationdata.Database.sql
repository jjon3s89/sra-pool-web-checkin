USE [master]
GO
/****** Object:  Database [applicationdata]    Script Date: 5/30/2013 7:02:15 PM ******/
CREATE DATABASE [applicationdata]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ApplicationDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\pooldata.mdf' , SIZE = 861824KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ApplicationDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\pooldata_log.ldf' , SIZE = 5184KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
GO
ALTER DATABASE [applicationdata] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [applicationdata].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [applicationdata] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [applicationdata] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [applicationdata] SET ANSI_PADDING ON 
GO
ALTER DATABASE [applicationdata] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [applicationdata] SET ARITHABORT OFF 
GO
ALTER DATABASE [applicationdata] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [applicationdata] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [applicationdata] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [applicationdata] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [applicationdata] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [applicationdata] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [applicationdata] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [applicationdata] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [applicationdata] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [applicationdata] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [applicationdata] SET  DISABLE_BROKER 
GO
ALTER DATABASE [applicationdata] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [applicationdata] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [applicationdata] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [applicationdata] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [applicationdata] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [applicationdata] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [applicationdata] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [applicationdata] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [applicationdata] SET  MULTI_USER 
GO
ALTER DATABASE [applicationdata] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [applicationdata] SET DB_CHAINING OFF 
GO
ALTER DATABASE [applicationdata] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [applicationdata] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [applicationdata] SET  READ_WRITE 
GO
