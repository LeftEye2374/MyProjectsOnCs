using CrabCounter.Models;

namespace CrabCounter.Mobile.Wrappers
{
    public partial class UserWrapper : WrapperBase<User>
    {
        public UserWrapper(User model) : base(model) { }

        public string Username
        {
            get => Model.Username;
            set => SetProperty(Model.Username, value, Model, (model, value) => model.Username = value);
        }
        public string Password
        {
            get => Model.Password;
            set => SetProperty(Model.Password, value, Model, (model, value) => model.Password = value);
        }
 
    }
}
