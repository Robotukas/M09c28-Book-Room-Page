using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            return _db.LoadData<RoomTypeModel, dynamic> ("dbo.spRoomType_GetAvailableTypes",
                                                  new { startDate, endDate }, // Parameters
                                                  connectionStringName,  // Connection string name to sql server
                                                  true);  // Is stored procedure
        }
    }
}
