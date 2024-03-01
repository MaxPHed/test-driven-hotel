using FluentAssertions;

namespace TestDrivenHotel.BLL.Tests
{
    public class DateManagerTests
    {
        [Fact]
        public void ReturnListOfDateTime_ShouldReturnListOfAllDatesBetweenTwoTimes()
        {
            //Given
            DateTime startingDate = new DateTime(2024, 1, 1);
            DateTime endingDate = new DateTime(2024, 1, 5);

            //When
            List<DateTime> result = DateManager.ReturnListOfDateTime(startingDate, endingDate);

            //Then
            result.Count().Should().Be(5);
            result.Should().Contain(new DateTime(2024, 1, 3));
        }
    }


}
