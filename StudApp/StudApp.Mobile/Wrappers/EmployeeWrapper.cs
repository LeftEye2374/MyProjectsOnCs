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

        public string SecondName
        {
            get => model.PersonInfo.SecondName;
            set => SetProperty(Model.PersonInfo.SecondName, value, Model, (model, value) => Model.PersonInfo.SecondName = value);
        }

        public string LastName
        {
            get => model.PersonInfo.LastName;
            set => SetProperty(Model.PersonInfo.LastName, value, Model, (model, value) => Model.PersonInfo.LastName = value);
        }

        public string Phone
        {
            get => model.ContactInfo.Phone;
            set => SetProperty(Model.ContactInfo.Phone, value, Model, (model,value) => Model.ContactInfo.Phone = value);
        }

        public int Dormitry
        {
            get => model.ContactInfo.Dormitry;
            set => SetProperty(Model.ContactInfo.Dormitry, value, Model, (model, value) => Model.ContactInfo.Dormitry = value);
        }

        public int Flor
        {
            get => model.ContactInfo.Flor;
            set => SetProperty(Model.ContactInfo.Flor, value, Model, (model, value) => Model.ContactInfo.Flor = value);
        }

        public string Room
        {
            get => model.ContactInfo.Room;
            set => SetProperty(Model.ContactInfo.Room, value, Model, (model, value) => Model.ContactInfo.Room = value);
        }

        public string Faculty
        {
            get => model.ContactInfo.Faculty;
            set => SetProperty(Model.ContactInfo.Faculty, value, Model, (model, value) => Model.ContactInfo.Faculty = value);
        }

        public int Curs
        {
            get => model.ContactInfo.Curs;
            set => SetProperty(Model.ContactInfo.Curs, value, Model, (model, value) => Model.ContactInfo.Curs = value);
        }

        public string Role
        {
            get => model.Role;
            set => SetProperty(Model.Role, value, Model, (model, value) => Model.Role = value);
        }

        public int NumOfShift
        {
            get => model.NumOfShift;
            set => SetProperty(Model.NumOfShift, value, Model, (model, value) => Model.NumOfShift = value);
        }

        public ICollection<Report> Reports
        {
            get => model.Reports; 
        }
        public Guid ShiftId
        {
            get => (Guid)model.ShiftId;
            set => SetProperty(Model.ShiftId, value, Model, (model, value) => Model.ShiftId = value);
        }

        public Shift Shift
        {
            get => model.Shift;
            set => SetProperty(Model.Shift, value, Model, (model, value) => Model.Shift = value);
        }

        public string Password
        {
            get => Model.Password;
            set => SetProperty(Model.Password, value, Model, (model, value) => model.Password = value);
        }

        public string DormitryText
        {
            get => model.ContactInfo.Dormitry == 0 ? string.Empty : model.ContactInfo.Dormitry.ToString();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(Model.ContactInfo.Dormitry, 0, Model, (model, v) => Model.ContactInfo.Dormitry = 0);
                }
                else if (int.TryParse(value, out int result))
                {
                    SetProperty(Model.ContactInfo.Dormitry, result, Model, (model, v) => Model.ContactInfo.Dormitry = result);
                }
            }
        }

        public string FlorText
        {
            get => model.ContactInfo.Flor == 0 ? string.Empty : model.ContactInfo.Flor.ToString();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(Model.ContactInfo.Flor, 0, Model, (model, v) => Model.ContactInfo.Flor = 0);
                }
                else if (int.TryParse(value, out int result))
                {
                    SetProperty(Model.ContactInfo.Flor, result, Model, (model, v) => Model.ContactInfo.Flor = result);
                }
            }
        }

        public string CursText
        {
            get => model.ContactInfo.Curs == 0 ? string.Empty : model.ContactInfo.Curs.ToString();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(Model.ContactInfo.Curs, 0, Model, (model, v) => Model.ContactInfo.Curs = 0);
                }
                else if (int.TryParse(value, out int result))
                {
                    SetProperty(Model.ContactInfo.Curs, result, Model, (model, v) => Model.ContactInfo.Curs = result);
                }
            }
        }

        public string NumOfShiftText
        {
            get => model.NumOfShift == 0 ? string.Empty : model.NumOfShift.ToString();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(Model.NumOfShift, 0, Model, (model, v) => Model.NumOfShift = 0);
                }
                else if (int.TryParse(value, out int result))
                {
                    SetProperty(Model.NumOfShift, result, Model, (model, v) => Model.NumOfShift = result);
                }
            }
        }

    }
}
