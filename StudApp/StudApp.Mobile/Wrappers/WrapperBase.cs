using CommunityToolkit.Mvvm.ComponentModel;
using StudApp.Models;

namespace StudApp.Mobile.Wrappers
{
    public abstract class WrapperBase<T> : ObservableValidator where T : BaseEntity
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
