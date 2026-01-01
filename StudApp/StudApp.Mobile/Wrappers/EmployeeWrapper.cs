using StudApp.Models;

namespace StudApp.Mobile.Wrappers
{
    public class EmployeeWrapper : WrapperBase<Employee>
    {
        public EmployeeWrapper(Employee model) : base(model) { }

        public string FirstName
        {
            get => model.PersonInfo.FirstName;
            set => SetProperty(Model.PersonInfo.FirstName, value, Model, (model, value) => Model.PersonInfo.FirstName = value);
        }

        public string Password
        {
            get => Model.Password;
            set => SetProperty(Model.Password, value, Model, (model, value) => model.Password = value);
        }
    }
}
