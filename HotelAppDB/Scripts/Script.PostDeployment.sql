/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


/* The provided code snippet is a SQL script designed to insert data into a 
	table named RoomTypes in a database, but only if the table currently contains no records*/
if not exists (select 1 from dbo.RoomTypes)
begin
	insert into dbo.RoomTypes (Title, Description, Price)
    values ('King Size Bed', 'A room with a king-size bed and window.', 100),
		   ('Two Queen Size Beds', 'A room with two queen-size beds and window.', 150),
		   ('Executive Suite', 'Two room, each with a king-size bed and window.', 285),
		   ('Double Bed', 'A room with two double beds and window.', 80),
		   ('Single Bed', 'A room with a single bed and window.', 50)
end

/* The provided code snippet is a SQL script designed to insert data into a 
	table named Rooms in a database, but only if the table currently contains no records*/
if not exists (select 1 from dbo.Rooms)
begin
	declare @roomId1 int;
	declare @roomId2 int;
	declare @roomId3 int;
	declare @roomId4 int;
	declare @roomId5 int;

	select @roomId1 = Id from dbo.RoomTypes where Title = 'King Size Bed';
	select @roomId2 = Id from dbo.RoomTypes where Title = 'Two Queen Size Beds';
	select @roomId3 = Id from dbo.RoomTypes where Title = 'Executive Suite';
	select @roomId4 = Id from dbo.RoomTypes where Title = 'Double Bed';
	select @roomId5 = Id from dbo.RoomTypes where Title = 'Single Bed';

	insert into dbo.Rooms (RoomNumber, RoomTypeId)
	values ('101', @roomId1),
			('102', @roomId1),
			('103', @roomId2),
			('104', @roomId2),
			('105', @roomId2),
			('201', @roomId3),
			('202', @roomId3),
			('203', @roomId3),
			('301', @roomId4),
			('302', @roomId4),
			('401', @roomId2)
end