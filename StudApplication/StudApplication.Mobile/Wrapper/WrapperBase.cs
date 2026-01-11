using CommunityToolkit.Mvvm.ComponentModel;
using StudApplication.Models;

namespace StudApplication.Mobile.Wrapper
{
    public abstract class WrapperBase<T> : ObservableValidator where T : BaseModel
    {
        protected readonly T model;
        public WrapperBase(T value)
        {
            this.model = value;
        }

        public T Model => model;
        public Guid Id => model.Id;
    }
}
