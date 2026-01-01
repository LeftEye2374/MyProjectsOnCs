
namespace StudApp.Mobile.Services
{
    public class ShiftService : IShiftService
    {
        private readonly DateTime _startDate = new DateTime(2026, 1, 1);
        private const int TotalShifts = 4;

        public string GetCurrentShift()
        {
            int shiftNumber = GetShiftNumber(DateTime.Now) + 2;
            return $"Смена {shiftNumber}";
        }

        public int GetShiftNumber(DateTime date)
        {
            int daysPassed = (int)(date.Date - _startDate.Date).TotalDays;

            int shiftIndex = daysPassed % TotalShifts;

            return shiftIndex + 1;
        }
    }
}
