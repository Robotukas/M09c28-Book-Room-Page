CREATE PROCEDURE [dbo].[spBookings_Insert]
	@roomID int,
	@guestID int,
	@startDate date,
	@endDate date,
	@totalCost money

AS
begin
	set nocount on;
	insert into dbo.Bookings (RoomId, GuestId, StartDate, EndDate, TotalCost)
	values (@roomID, @guestID, @startDate, @endDate, @totalCost);
end
