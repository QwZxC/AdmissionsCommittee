using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdmissionsCommittee.Infrastructure
{
    public abstract class NotifyPropertyChangedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propetyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propetyName));

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propetyName = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propetyName);
            return true;
        }
    }

}
