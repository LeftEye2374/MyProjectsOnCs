namespace StudApp.Mobile.Services
{
    public class ShiftService : IShiftService
    {
        private readonly DateTime _startDate = new DateTime(2026, 1, 1);
        private const int TotalShifts = 4;
        private const int Offset = 2; 

        public string GetCurrentShift()
        {
            int daysPassed = (int)((DateTime.Now.Date - _startDate.Date).TotalDays + Offset);
            int currentShift = ((daysPassed % TotalShifts) + TotalShifts) % TotalShifts + 1; 
            return $"Смена {currentShift}";
        }
    }
}
