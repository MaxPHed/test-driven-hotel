namespace TestDrivenHotel.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<Booking>? Bookings { get; set; }

        public Room(int id, string type)
        {
            Id = id;
            Type = type;
            Bookings = new List<Booking>();
        }
    }
}
