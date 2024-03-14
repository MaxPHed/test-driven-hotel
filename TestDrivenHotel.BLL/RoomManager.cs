using System.Data;
using TestDrivenHotel.Domain;

namespace TestDrivenHotel.BLL
{
    //En instans av klassen Dependency injectas vid varje uppstart av appen.
    public class RoomManager
    {
        //"Databasen" som mockas av en lista med Room. Blir en ny för varje runtime av appen
        public MockRoomDb db = new();
        public Room? ReturnFirstRoomByType(List<Room> rooms, string roomType)
        {
            return rooms.Where(r => r.Type == roomType).FirstOrDefault();
        }

        //Metoden som hanterar förfrågningar från UI-lager.
        public string BookRoom(List<DateTime> dates, string roomType, string name)
        {
            try
            {
                List<Room> Rooms = ReturnAllAvailableRooms(dates, roomType);
                Room room = ReturnFirstRoomByType(Rooms, roomType);
                if (room == null) { return "Rummet finns inte kvar"; }
                return BookRoomById(room.Id, dates, name);
            }
            catch (Exception ex)
            { return ex.Message; };
        }

        public string BookRoomById(int roomNumber, List<DateTime> dates, string name)
        {
            try
            {
                Room room = db.Rooms.Where(r => r.Id == roomNumber).First();
                bool allDatesAvailable = true;
                foreach (var date in dates)
                {
                    if (!RoomIsAvailable(room, date))
                    {
                        allDatesAvailable = false;
                    }
                }
                if (allDatesAvailable)
                {
                    foreach (var date in dates)
                    {
                        room.Bookings.Add(new Booking(room, date, name, db.BookingReferenceCount));
                    }
                    string returnMessage = $"Booking successfull! Booking reference is {db.BookingReferenceCount}";
                    //Uppdaterar referensnummer med en för varje bokning.
                    db.BookingReferenceCount = db.BookingReferenceCount + 1;
                    return returnMessage;
                }
                else { return "Date is already booked"; }
            }
            catch { throw new ArgumentException(); };
        }

        public bool RoomIsAvailable(Room room, DateTime date)
        {
            bool isAvailable = true;

            foreach (Booking b in room.Bookings)
            {
                if (b.BookingDate == date) { isAvailable = false; }
            }
            return isAvailable;
        }

        public List<Room> ReturnAllRooms()
        {
            return db.Rooms;
        }

        public List<Room> ReturnAllAvailableRooms(List<DateTime> dates, string? roomType)
        {
            List<Room> availableRooms = new();
            foreach (var room in db.Rooms)
            {
                if (AreAllDatesAvailable(room, dates))
                {
                    if (string.IsNullOrEmpty(roomType) || room.Type.Equals(roomType))
                    {
                        availableRooms.Add(room);
                    }
                }
            }
            return availableRooms;
        }

        private bool AreAllDatesAvailable(Room room, List<DateTime> dates)
        {
            foreach (var date in dates)
            {
                if (!RoomIsAvailable(room, date)) return false;
            }
            return true;
        }

        public (List<Booking>? Bookings, string Message) ReturnBookingsByBookingNameAndReference(int referenceNumber, string name)
        {

            //Room? booking = db.Rooms.FirstOrDefault(room => room.Bookings.Any(booking => booking.BookingReference == referenceNumber && booking.BookedBy == name));
            List<Booking>? bookings = db.Rooms
                .SelectMany(room => room.Bookings)
                .Where(booking => booking.BookingReference == referenceNumber && booking.BookedBy == name)
                .ToList();

            if (bookings.Any()) return (bookings, "Room found");
            else return (null, "Room not found");
        }
    }
}
