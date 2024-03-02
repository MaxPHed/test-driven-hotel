using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotel.BLL;
using TestDrivenHotel.Domain;

namespace TestDrivenHotel.UI.Pages
{
    public class IndexModel : PageModel
    {
        public List<Room> Rooms;
        private RoomManager manager = new();
        [BindProperty]
        public DateTime StartingDate { get; set; }
        [BindProperty]
        public DateTime EndingDate { get; set; }
        public List<DateTime>? Dates;

        public void OnGet()
        {
            if (Rooms == null)
            {
                Rooms = manager.returnAllRooms();
            }

        }

        public void OnPost()
        {
            Dates = DateManager.ReturnListOfDateTime(StartingDate, EndingDate);
            Rooms = manager.ReturnAllAvailableRooms(Dates, "");
        }
    }
}
