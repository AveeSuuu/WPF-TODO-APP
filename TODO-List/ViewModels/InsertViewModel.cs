using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TODO_List.Models;
using TODO_List.Utilities;

namespace TODO_List.ViewModels;

public class InsertViewModel : ViewModel {
  TaskModel _task;
  
  public string? TaskTitle {
    get => _task.TaskTitle;
    set {
      _task.TaskTitle = value;
      OnPropertyChanged();
    }
  }

  public string? TaskDescription {
    get => _task.TaskDescription;
    set {
      _task.TaskDescription = value;
      OnPropertyChanged();
    }
  }
  
  public ICommand CancelCommand { get; }
  public ICommand AddCommand { get;  }
  
  public InsertViewModel() {
    _task = new TaskModel();
    CancelCommand = new RelayCommand(closeWindow);
    AddCommand = new RelayCommand(addTask);
  }

  void closeWindow(object sender) {
    Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
    if (window != null)
      window.DialogResult = false;
  }
  

  void addTask(object sender) {
    if (canSave()) 
      save();
  }
  
  private void save() {
    Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
    if (window != null)
      window.DialogResult = true;
  }

  private bool canSave() {
    return !string.IsNullOrWhiteSpace(TaskTitle) && 
           !string.IsNullOrWhiteSpace(TaskDescription) &&
           TaskTitle.Length < 35;
  } 
}
