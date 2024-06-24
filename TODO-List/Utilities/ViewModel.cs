using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TODO_List.Utilities;

public abstract class ViewModel : INotifyPropertyChanged {
  public event PropertyChangedEventHandler? PropertyChanged;

  public void OnPropertyChanged([CallerMemberName] string propertyName = null) {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
