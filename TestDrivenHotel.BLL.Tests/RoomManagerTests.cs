﻿using FluentAssertions;
using TestDrivenHotel.Domain;

namespace TestDrivenHotel.BLL.Tests
{
    public class RoomManagerTests
    {
        [Fact]
        public void returnAllRooms_shouldReturnAllRooms()
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
        public void bookRoom_shouldBookRoomOnSpecifiedDate()
        {
            //Given
            RoomManager roomManager = new();
            Room? actualRoom = new Room(1, "Single");
            actualRoom.Bookings.Add(new Booking(new DateTime(2024, 3, 1), "John Doe"));

            //When
            Room room = roomManager.db.Rooms.Where(r => r.Id == 1).FirstOrDefault();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1) };
            roomManager.bookRoom(1, dates, "John Doe");
            //Then

            room.Should().BeEquivalentTo(actualRoom);
        }

        [Fact]
        public void bookRoom_DateIsAvailable_ShouldBookIfRoomIsAvailableAndReturnSuccessMessage()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1) };

            //When
            string expectedResult = "Booking successfull";
            string result = roomManager.bookRoom(1, dates, "John Doe");

            //Then
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void bookRoom_DateIsNotAvailable_ShouldBookIfRoomIsAvailableAndReturnErrorMessage()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1) };
            roomManager.bookRoom(1, dates, "John Doe");

            //When
            string expectedResult = "Date is already booked";
            string result = roomManager.bookRoom(1, dates, "John Doe");

            //Then
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void bookRoom_RoomIsNotFound_ShouldThrowException()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1) };
            roomManager.bookRoom(1, dates, "John Doe");

            //When

            Action test = () => roomManager.bookRoom(100, dates, "John Doe");

            //Then
            test.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void bookRoom_MultipleAvailableDates_ShouldBookRooms()
        {
            //Given
            RoomManager roomManager = new();
            List<DateTime> dates = new() { new DateTime(2024, 3, 1), new DateTime(2024, 3, 2), new DateTime(2024, 3, 3) };

            //When
            string expectedResult = "Booking successfull";
            string result = roomManager.bookRoom(1, dates, "John Doe");

            //Then
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void bookRoom_OneOfThreeDatesAreUnavailable_ShoulReturnErrorMessage()
        {
            //Given
            RoomManager roomManager = new();
            Room bookedRoom = roomManager.db.Rooms.Where(r => r.Id == 1).FirstOrDefault();
            bookedRoom.Bookings.Add(new Booking(new DateTime(2024, 3, 2), "John Doe"));

            List<DateTime> dates = new() { new DateTime(2024, 3, 1), new DateTime(2024, 3, 2), new DateTime(2024, 3, 3) };


            //When
            string expectedResult = "Date is already booked";
            string result = roomManager.bookRoom(1, dates, "John Doe");

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
            roomManager.bookRoom(1, dates, "Arne Anka");
            roomManager.bookRoom(5, dates, "Arne Anka");
            roomManager.bookRoom(8, dates, "Arne Anka");
            roomManager.bookRoom(16, dates, "Arne Anka");

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
            roomManager.bookRoom(1, dates, "Arne Anka");
            roomManager.bookRoom(2, dates, "Arne Anka");
            roomManager.bookRoom(3, dates, "Arne Anka");
            roomManager.bookRoom(4, dates, "Arne Anka");
            roomManager.bookRoom(5, dates, "Arne Anka");
            roomManager.bookRoom(6, dates, "Arne Anka");
            roomManager.bookRoom(7, dates, "Arne Anka");
            roomManager.bookRoom(8, dates, "Arne Anka");
            roomManager.bookRoom(9, dates, "Arne Anka");
            roomManager.bookRoom(10, dates, "Arne Anka");
            roomManager.bookRoom(11, dates, "Arne Anka");
            roomManager.bookRoom(12, dates, "Arne Anka");
            roomManager.bookRoom(13, dates, "Arne Anka");
            roomManager.bookRoom(14, dates, "Arne Anka");
            roomManager.bookRoom(15, dates, "Arne Anka");
            roomManager.bookRoom(16, dates, "Arne Anka");
            roomManager.bookRoom(17, dates, "Arne Anka");
            roomManager.bookRoom(18, dates, "Arne Anka");
            roomManager.bookRoom(19, dates, "Arne Anka");
            roomManager.bookRoom(20, dates, "Arne Anka");

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
            roomManager.bookRoom(1, dates, "Arne Anka");
            roomManager.bookRoom(5, dates, "Arne Anka");
            roomManager.bookRoom(8, dates, "Arne Anka");
            roomManager.bookRoom(16, dates, "Arne Anka");

            //When
            List<Room> actualResult = roomManager.ReturnAllAvailableRooms(dates, "Single");

            //THen
            actualResult.Count().Should().Be(8);
            actualResult.Should().OnlyContain(room => room.Type == "Single");
        }
    }
}
