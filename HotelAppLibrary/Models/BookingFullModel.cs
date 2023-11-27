using System;
using System.Collections.Generic;
using System.Text;

namespace HotelAppLibrary.Models
{
    public class BookingFullModel
    {
        // Booking
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CheckedIn { get; set; }
        public decimal TotalCost { get; set; }

        // Guest
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Room
        public string RoomNumber { get; set; }

        // RoomType
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
