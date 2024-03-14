namespace TestDrivenHotel.BLL
{
    public static class DateManager
    {
        public static List<DateTime> ReturnListOfDateTime(DateTime startingDate, DateTime endingDate)
        {
            //Sätter ihop två DateTIme till en lista med Date Times för alla datum där emllan
            List<DateTime> allDates = new();
            for (DateTime currentDate = startingDate; currentDate <= endingDate; currentDate = currentDate.AddDays(1))
            {
                allDates.Add(currentDate);
            }
            return allDates;
        }

    }
}
