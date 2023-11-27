CREATE PROCEDURE [dbo].[spGuests_Insert]
	@firstName nvarchar(50),
	@lastName nvarchar(50)
AS
begin
	set nocount on;	/* prevents the "1 row affected" message */

	if not exists (select * from dbo.Guests where FirstName = @firstName and LastName = @lastName)
	begin
		insert into dbo.Guests (FirstName, LastName)
		values (@firstName, @lastName);
	end

	select top 1 [Id], [FirstName], [LastName]
	from dbo.Guests 
	where FirstName = @firstName and LastName = @lastName;
end
