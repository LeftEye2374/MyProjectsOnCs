using CommunityToolkit.Mvvm.ComponentModel;
using StudApplication.Models;

namespace StudApplication.Mobile.Wrappers
{
    public abstract class BaseWrapper<T> : ObservableValidator where T : BaseModel
    {
        protected readonly T model;
        public BaseWrapper(T value)
        {
            this.model = value;
        }

        public T Model => model;
        public Guid Id => model.Id;
    }
}
