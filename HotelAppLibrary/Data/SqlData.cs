using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelAppLibrary.Data
{
    /// <summary>
    /// This class is used to get data from the database
    /// </summary>
    /// <param name="startDate">The start date of the booking</param>
    /// <param name="endDate">The end date of the booking</param>
    /// <returns>A list of available room types</returns></returns>
    public class SqlData
    {
        private readonly ISqlDataAccess _db;
        private const string connectionStringName = "SqlDb";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        /// <summary>
        /// is designed to retrieve a list of available room types for a specified date range in a hotel 
        /// management system. Here's a summary of the method:
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            return _db.LoadData<RoomTypeModel, dynamic>("dbo.spRoomType_GetAvailableTypes",
                                                  new { startDate, endDate }, // Parameters
                                                  connectionStringName,  // Connection string name to sql server
                                                  true);  // Is stored procedure
        }

        /// <summary>
        /// The BookGuest method encapsulates the process of booking a hotel room for a guest, starting
        /// from guest registration to room booking. It uses a series of database operations to handle
        /// different aspects of the booking process, ensuring that all necessary information is captured
        /// and processed correctly. The method demonstrates a clear sequence of steps, maintaining
        /// a separation of concerns and leveraging the database for critical operations.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="roomTypeId"></param>
        public void BookGuest(string firstName,
                              string lastName,
                              DateTime startDate,
                              DateTime endDate,
                              int roomTypeId)
        {
            
            GuestModel guest = _db.LoadData<GuestModel, dynamic>("dbo.spGuests_Insert",
                                                                 new {firstName, lastName },
                                                                 connectionStringName,
                                                                 true).First(); 

            RoomTypeModel roomType = _db.LoadData<RoomTypeModel, dynamic>("dbo.spRoomType_GetById where Id = @Id",
                                                                          new { Id = roomTypeId },
                                                                          connectionStringName,
                                                                           false).First();

            TimeSpan timeStaying = endDate.Date.Subtract(startDate.Date);
            
            List<RoomModel> availableRooms = _db.LoadData<RoomModel, dynamic>("dbo.spRooms_GetAvailableRooms",
                                                                              new { startDate, endDate, roomTypeId },
                                                                              connectionStringName,
                                                                              true);

            _db.SaveData("dbo.spBookings_Insert",
                         new 
                         { 
                             roomId = availableRooms.First().Id,
                             guestId = guest.Id,
                             startDate = startDate, 
                             endDate = endDate,
                             totalCost = timeStaying.Days * roomType.Price
                         },
                         connectionStringName,
                         true);
        }                               

        /// <summary>
        /// The SearchBookings method is designed to retrieve a list of bookings for a specified guest
        /// with current date as the start date.
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public List<BookingFullModel> SearchBookings(string lastName)
        {
            return _db.LoadData<BookingFullModel, dynamic>("dbo.spBookings_Search",
                                                    new { lastName, startDate =DateTime.Now.Date },
                                                    connectionStringName,
                                                    true);
        }
    }
}
