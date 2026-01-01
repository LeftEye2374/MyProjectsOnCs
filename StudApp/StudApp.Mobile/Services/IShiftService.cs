namespace StudApp.Mobile.Services
{
    public interface IShiftService
    {
        string GetCurrentShift();
        int GetShiftNumber(DateTime date);
    }
}
