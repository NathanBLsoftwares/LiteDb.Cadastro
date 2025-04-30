using System.Runtime.CompilerServices;

namespace TesteBancoDeDadosLiteDB.Domain.Model.Wrapper
{
    public class ModelWrapper<T> : BindableBase
    {
        public T Model { get; }

        public ModelWrapper(T model)
        {
            Model = model;
        }

        protected virtual TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return default;
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model, null);
        }

        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName)) return;

                typeof(T).GetProperty(propertyName).SetValue(Model, value, null);
                RaisePropertyChanged(propertyName);
            }
            catch (Exception)
            {
            }
        }
    }
}
