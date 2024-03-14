using FluentAssertions;
using TestDrivenHotel.Domain;

namespace TestDrivenHotel.BLL.Tests
{
    public class RoomManagerTests
    {
        [Fact]
        public void ReturnAllRooms_shouldReturnAllRooms()
        {
            //Given
            RoomManager roomManager = new RoomManager();
            List<Room> actualRooms = roomManager.db.Rooms;

            //When
            List<Room> result = roomManager.returnAllRooms();

            //Then
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterThan(0);
            result.Should().BeEquivalentTo(actualRooms);
        }

        [Fact]
        public void BookRoomById_shouldBookRoomOnSpecifiedDate()
        {
            //Given
            RoomManager roomManager = new();
            Room? actualRoom = roomManager.db.Rooms.First();
            actualRoom.Bookings.Add(new Booking(actualRoom, new DateTime(2024, 3, 1), "John Doe", roomManager.db.BookingReferenceCount));

            //When
            Room room = roomManager.db.Rooms.Where(r => r.Id == 1).FirstOrDefault();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1) };
            roomManager.BookRoomById(1, dates, "John Doe");
            //Then

            room.Should().BeEquivalentTo(actualRoom);
        }

        [Fact]
        public void BookRoomById_DateIsAvailable_ShouldBookIfRoomIsAvailableAndReturnSuccessMessage()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1) };

            //When
            string expectedResult = "Booking successfull";
            string result = roomManager.BookRoomById(1, dates, "John Doe");

            //Then
            result.Should().StartWith(expectedResult);
        }

        [Fact]
        public void BookRoomById_DateIsNotAvailable_ShouldBookIfRoomIsAvailableAndReturnErrorMessage()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1) };
            roomManager.BookRoomById(1, dates, "John Doe");

            //When
            string expectedResult = "Date is already booked";
            string result = roomManager.BookRoomById(1, dates, "John Doe");

            //Then
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void BookRoomById_RoomIsNotFound_ShouldThrowException()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1) };
            roomManager.BookRoomById(1, dates, "John Doe");

            //When

            Action test = () => roomManager.BookRoomById(100, dates, "John Doe");

            //Then
            test.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void BookRoomById_MultipleAvailableDates_ShouldBookRooms()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1), new DateTime(2024, 3, 2), new DateTime(2024, 3, 3) };

            //When
            string expectedResult = "Booking successfull";
            string result = roomManager.BookRoomById(1, dates, "John Doe");

            //Then
            result.Should().StartWith(expectedResult);
        }

        [Fact]
        public void BookRoomId_OneOfThreeDatesAreUnavailable_ShoulReturnErrorMessage()
        {
            //Given
            RoomManager roomManager = new();
            Room bookedRoom = roomManager.db.Rooms.Where(r => r.Id == 1).FirstOrDefault();
            bookedRoom.Bookings.Add(new Booking(bookedRoom, new DateTime(2024, 3, 2), "John Doe", roomManager.db.BookingReferenceCount));

            List<DateTime> dates = new() { new DateTime(2024, 3, 1), new DateTime(2024, 3, 2), new DateTime(2024, 3, 3) };


            //When
            string expectedResult = "Date is already booked";
            string result = roomManager.BookRoomById(1, dates, "John Doe");

            //Then
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void ReturnAllAvailableRooms__returnsAllAvailableRoomsBetweenTheDates()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1), new DateTime(2024, 3, 2), new DateTime(2024, 3, 3) };

            //When
            List<Room> expectedRooms = roomManager.ReturnAllAvailableRooms(dates, null);
            List<Room> availableRooms = roomManager.returnAllRooms();

            expectedRooms.Should().BeEquivalentTo(availableRooms);
        }

        [Fact]
        public void ReturnAllAvailableRooms_SomeRoomsAreBooked_returnsAllAvailableRoomsBetweenTheDates()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1), new DateTime(2024, 3, 2), new DateTime(2024, 3, 3) };
            roomManager.BookRoomById(1, dates, "Arne Anka");
            roomManager.BookRoomById(5, dates, "Arne Anka");
            roomManager.BookRoomById(8, dates, "Arne Anka");
            roomManager.BookRoomById(16, dates, "Arne Anka");

            //When
            List<Room> actualResult = roomManager.ReturnAllAvailableRooms(dates, null);

            //Then
            actualResult.Count().Should().Be(16);
        }

        [Fact]
        public void ReturnsAllAvailableRooms_AllRoomsAreBooked_ReturnsEmptyList()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1), new DateTime(2024, 3, 2), new DateTime(2024, 3, 3) };
            roomManager.BookRoomById(1, dates, "Arne Anka");
            roomManager.BookRoomById(2, dates, "Arne Anka");
            roomManager.BookRoomById(3, dates, "Arne Anka");
            roomManager.BookRoomById(4, dates, "Arne Anka");
            roomManager.BookRoomById(5, dates, "Arne Anka");
            roomManager.BookRoomById(6, dates, "Arne Anka");
            roomManager.BookRoomById(7, dates, "Arne Anka");
            roomManager.BookRoomById(8, dates, "Arne Anka");
            roomManager.BookRoomById(9, dates, "Arne Anka");
            roomManager.BookRoomById(10, dates, "Arne Anka");
            roomManager.BookRoomById(11, dates, "Arne Anka");
            roomManager.BookRoomById(12, dates, "Arne Anka");
            roomManager.BookRoomById(13, dates, "Arne Anka");
            roomManager.BookRoomById(14, dates, "Arne Anka");
            roomManager.BookRoomById(15, dates, "Arne Anka");
            roomManager.BookRoomById(16, dates, "Arne Anka");
            roomManager.BookRoomById(17, dates, "Arne Anka");
            roomManager.BookRoomById(18, dates, "Arne Anka");
            roomManager.BookRoomById(19, dates, "Arne Anka");
            roomManager.BookRoomById(20, dates, "Arne Anka");

            //When
            List<Room> actualResult = roomManager.ReturnAllAvailableRooms(dates, null);

            //Then
            actualResult.Count().Should().Be(0);
        }

        [Fact]
        public void ReturnsAllEmptyRooms_RoomTypeSpecified_ShouldReturnEmptyRoomsMathcingType()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1), new DateTime(2024, 3, 2), new DateTime(2024, 3, 3) };
            roomManager.BookRoomById(1, dates, "Arne Anka");
            roomManager.BookRoomById(5, dates, "Arne Anka");
            roomManager.BookRoomById(8, dates, "Arne Anka");
            roomManager.BookRoomById(16, dates, "Arne Anka");

            //When
            List<Room> actualResult = roomManager.ReturnAllAvailableRooms(dates, "Single");

            //THen
            actualResult.Count().Should().Be(8);
            actualResult.Should().OnlyContain(room => room.Type == "Single");
        }

        [Fact]
        public void ReturnBookingsByBookingNameAndReference_CorrectInputs_ShouldReturnListOfBookingAndMessage()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1), new DateTime(2024, 3, 2), new DateTime(2024, 3, 3) };
            roomManager.BookRoomById(1, dates, "Fantomen");
            int referenceNumber = 1000; //Default för varje roomManager

            //When
            var result = roomManager.ReturnBookingsByBookingNameAndReference(referenceNumber, "Fantomen");

            //THen
            result.Bookings.Should().NotBeNull();
            result.Bookings.FirstOrDefault().BookingReference.Should().Be(1000);
            result.Message.Should().Be("Room found");
        }

        [Fact]
        public void ReturnBookingsByBookingNameAndReference_CorrectInputsNoBooking_ShouldReturnNullAndMessage()
        {
            //Given
            RoomManager roomManager = new();

            //When
            var result = roomManager.ReturnBookingsByBookingNameAndReference(1, "Max");
            //Then
            result.Bookings.Should().BeNullOrEmpty();
            result.Message.Should().Be("Room not found");
        }

        //TODO SKriv test för bokningsreferenssökning.
    }
}
