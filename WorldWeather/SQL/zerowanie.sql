USE [WorldWeather.WeatherDbContext]
Select * From dbo.Weathers;
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
go
exec sp_MSforeachtable 'TRUNCATE TABLE ?'
GO
EXEC sp_MSforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
GO
DBCC CHECKIDENT('dbo.Weathers', RESEED, 1);