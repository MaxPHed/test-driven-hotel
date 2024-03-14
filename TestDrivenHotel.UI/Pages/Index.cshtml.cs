using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotel.BLL;
using TestDrivenHotel.Domain;

namespace TestDrivenHotel.UI.Pages
{
    public class IndexModel : PageModel
    {
        public List<Room> Rooms { get; set; }
        private readonly RoomManager manager;
        [BindProperty]
        public DateTime StartingDate { get; set; }

        [BindProperty]
        public DateTime EndingDate { get; set; }
        [BindProperty]
        public bool SingleRoom { get; set; }
        [BindProperty]
        public bool DoubleRoom { get; set; }
        public bool CheckAvailabilityPressed { get; set; }
        public bool FindBookingPressed { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public int ReferenceNumber { get; set; }

        public List<Booking>? FoundBookings { get; set; }
        public string? FoundBookingMessage { get; set; }

        public List<DateTime>? Dates
        {
            get { return HttpContext.Session.GetObject<List<DateTime>>("Dates"); }
            set { HttpContext.Session.SetObject("Dates", value); }
        }
        public IndexModel(RoomManager roomManager)
        {
            manager = roomManager;
        }
        public void OnGet()
        {
            CheckAvailabilityPressed = false;
            FindBookingPressed = false;
            if (Rooms == null)
            {
                if (Dates == null)
                {
                    Rooms = manager.ReturnAllRooms();
                }
                else
                {
                    Rooms = manager.ReturnAllAvailableRooms(Dates, "");
                }
            }
        }

        public IActionResult OnPost()
        {
            //Switchmetoden tar en string från objekten som initierar den och kör metod utifrån det
            //Att hela OnPost är en IActionResult är för att kunna länga genom en button. Hade nog varit snyggare att ha en a-tagg som ser ut som en knapp istället då alla knappar inte går till en länk.
            string action = Request.Form["action"];
            IActionResult myAction = null;
            switch (action)
            {
                case "CheckAvailability":
                    pressCheckAvailability();
                    break;
                case "BookRoom Single":
                    myAction = BookRoom("Single");
                    break;
                case "BookRoom Double":
                    myAction = BookRoom("Double");
                    break;
                case "FindBooking":
                    FindBooking();
                    break;
            }
            return myAction;
        }

        private IActionResult? FindBooking()
        {
            FindBookingPressed = true;
            var result = manager.ReturnBookingsByBookingNameAndReference(ReferenceNumber, Name);
            FoundBookings = result.Bookings;
            FoundBookingMessage = result.Message;
            return null;
        }

        public void pressCheckAvailability()
        {
            Dates = DateManager.ReturnListOfDateTime(StartingDate, EndingDate);

            if (SingleRoom && !DoubleRoom)
            {
                Rooms = manager.ReturnAllAvailableRooms(Dates, "Single");
            }
            if (DoubleRoom && !SingleRoom)
            {
                Rooms = manager.ReturnAllAvailableRooms(Dates, "Double");
            }
            if (SingleRoom && DoubleRoom)
            {
                Rooms = manager.ReturnAllAvailableRooms(Dates, "");
            }
            CheckAvailabilityPressed = true;
        }

        public IActionResult BookRoom(string roomType)
        {
            return RedirectToPage("/Book", new { roomType });
        }
    }
}
