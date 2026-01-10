using StudApplication.Models;

namespace StudApplication.Mobile.Wrappers
{
    public class EmployeeWrapper : BaseWrapper<Employee>
    {
        public EmployeeWrapper(Employee model) : base(model) { }
        

        public string FirstName
        {
            get => Model.PersonInformation.FirstName;
            set => SetProperty(Model.PersonInformation.FirstName, value, Model, (model,value) => model.PersonInformation.FirstName = value); 
        }

        public string MiddleName
        {
            get => Model.PersonInformation.MiddleName;
            set => SetProperty(Model.PersonInformation.MiddleName, value, Model, (model,value) => model.PersonInformation.MiddleName = value);
        }

        public string LastName
        {
            get => Model.PersonInformation.LastName;
            set => SetProperty(Model.PersonInformation.LastName, value, Model, (model,value) => model.PersonInformation.LastName = value);
        }

        public string Faculty
        {
            get => Model.PersonInformation.Faculty;
            set => SetProperty(Model.PersonInformation.Faculty, value, Model, (model, value) => model.PersonInformation.Faculty = value);
        }

        public string Curs
        {
            get => model.PersonInformation.Curs == 0 ? string.Empty : model.PersonInformation.Curs.ToString();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(Model.PersonInformation.Curs, 0, Model, (model, v) => Model.PersonInformation.Curs = 0);
                }
                else if (int.TryParse(value, out int result))
                {
                    SetProperty(Model.PersonInformation.Curs, result, Model, (model, v) => Model.PersonInformation.Curs = result);
                }
            }
        }

        public string Login
        {
            get => model.Autorization.Login;
            set => SetProperty(Model.Autorization.Login, value, Model, (model, value) => model.Autorization.Login = value);
        }

        public string Password
        {
            get => model.Autorization.Password;
            set => SetProperty(Model.Autorization.Password, value, Model, (model, value) => model.Autorization.Password = value);
        }

        public string Role
        {
            get => model.Autorization.Role;
            set => SetProperty(Model.Autorization.Role, value, Model, (model, value) => model.Autorization.Role = value);
        }

        public string Shift
        {
            get => model.Shift == 0 ? string.Empty : model.Shift.ToString();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(Model.Shift, 0, Model, (model, v) => Model.Shift = 0);
                }
                else if (int.TryParse(value, out int result))
                {
                    SetProperty(Model.Shift, result, Model, (model, v) => Model.Shift = result);
                }
            }
        }

        public string NumberOfReports
        {
            get => model.NumberOfReports == 0 ? string.Empty : model.NumberOfReports.ToString();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(Model.NumberOfReports, 0, Model, (model, v) => Model.NumberOfReports = 0);
                }
                else if (int.TryParse(value, out int result))
                {
                    SetProperty(Model.NumberOfReports, result, Model, (model, v) => Model.NumberOfReports = result);
                }
            }
        }

        public ICollection<Report> Reports
        {
            get => model.Reports;
        }
    }
}
