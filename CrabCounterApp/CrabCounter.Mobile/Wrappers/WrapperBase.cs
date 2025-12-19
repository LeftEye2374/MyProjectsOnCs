using CommunityToolkit.Mvvm.ComponentModel;
using CrabCounter.Models;

namespace CrabCounter.Mobile.Wrappers
{
    public abstract class WrapperBase<T> : ObservableValidator where T : ModelBase
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
