namespace TestDrivenHotel.Domain
{
    public class MockRoomDb
    {
        public List<Room> Rooms;

        public int BookingReferenceCount = 1000;
        public MockRoomDb()
        {
            if (Rooms == null)
            {
                Rooms = new List<Room>(){
                new Room(1, "Single"),
                new Room(2, "Double"),
                new Room(3, "Single"),
                new Room(4, "Double"),
                new Room(5, "Single"),
                new Room(6, "Double"),
                new Room(7, "Single"),
                new Room(8, "Double"),
                new Room(9, "Single"),
                new Room(10, "Double"),
                new Room(11, "Single"),
                new Room(12, "Double"),
                new Room(13, "Single"),
                new Room(14, "Double"),
                new Room(15, "Single"),
                new Room(16, "Double"),
                new Room(17, "Single"),
                new Room(18, "Double"),
                new Room(19, "Single"),
                new Room(20, "Double")
            };

            }
        }


    }
}
