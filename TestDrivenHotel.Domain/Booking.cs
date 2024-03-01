namespace TestDrivenHotel.Domain
{
    public class Booking
    {
        public DateTime BookingDate { get; set; }
        public string BookedBy { get; set; }

        public Booking(DateTime bookingDate, string bookedBy)
        {
            BookingDate = bookingDate;
            BookedBy = bookedBy;
        }
    }
}