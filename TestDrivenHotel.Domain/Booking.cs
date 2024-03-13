namespace TestDrivenHotel.Domain
{
    public class Booking
    {
        public DateTime BookingDate { get; set; }
        public string BookedBy { get; set; }
        public int BookingReference { get; set; }

        public Room BookedRoom { get; }

        public Booking(Room bookedRoom, DateTime bookingDate, string bookedBy, int bookingReference)
        {
            BookedRoom = bookedRoom;
            BookingDate = bookingDate;
            BookedBy = bookedBy;
            BookingReference = bookingReference;
        }
    }
}