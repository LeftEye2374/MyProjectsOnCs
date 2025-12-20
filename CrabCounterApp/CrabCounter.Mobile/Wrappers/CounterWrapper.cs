using CrabCounter.Models;

namespace CrabCounter.Mobile.Wrappers
{
    public class CounterWrapper : WrapperBase<Counter>
    {
        public CounterWrapper(Counter model) : base(model) { }

        public int Number
        {
            get => Model.Number;
            set => SetProperty(Model.Number, value, Model, (model, value) => model.Number = value);
        }
    }
}
