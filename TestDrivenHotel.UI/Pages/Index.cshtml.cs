using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotel.BLL;
using TestDrivenHotel.Domain;

namespace TestDrivenHotel.UI.Pages
{
    public class IndexModel : PageModel
    {
        public List<Room> Rooms;
        private RoomManager manager = new();

        public void OnGet()
        {
            if (Rooms == null)
            {
                Rooms = manager.returnAllRooms();
            }

        }
    }
}
