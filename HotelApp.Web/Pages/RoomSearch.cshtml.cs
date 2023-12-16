using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Web.Pages
{
    public class RoomSearchModel : PageModel
    {
        private readonly IDatabaseData _db;

        // This is where we tell the application to use the Date data type for the StartDate property
        [DataType(DataType.Date)]   
        // This is where we tell the application to bind the value of the StartDate property to the StartDate input field in the Razor page.
        // The SupportsGet property is used to tell the application to bind the value of the property to the input field when the page is loaded
        [BindProperty(SupportsGet = true)]      
        public DateTime StartDate { get; set; } = DateTime.Today;
        
        // This is where we tell the application to use the Date data type for the EndDate property
        [DataType(DataType.Date)]
        // This is where we tell the application to bind the value of the EndDate property to the EndDate input field in the Razor page.
        // The SupportsGet property is used to tell the application to bind the value of the property to the input field when the page is loaded
        [BindProperty(SupportsGet = true)]      
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

        [BindProperty(SupportsGet = true)] 
        public bool SearchEnabled { get; set; } = false;

        // This is where we tell the application to use the RoomTypeModel data type for the AvailableRoomsTypes property
        public List<RoomTypeModel> AvailableRoomsTypes { get; set; }

        public RoomSearchModel(IDatabaseData db)
        {
            _db = db;
        }


        public void OnGet()
        {
            if (SearchEnabled ==  true)
            {
                AvailableRoomsTypes = _db.GetAvailableRoomTypes(StartDate, EndDate);     
            }
        }

        public IActionResult OnPost()
        {
            return RedirectToPage(new 
                                { 
                                    SearchEnabled = true, 
                                    StartDate = StartDate.ToString("yyyy-MM-dd"), 
                                    EndDate = EndDate.ToString("yyyy-MM-dd")
                                });
        }
    }
}
