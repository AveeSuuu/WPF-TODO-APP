using System.Collections.ObjectModel;
using System.Windows.Input;
using TODO_List.Utilities;
namespace TODO_List.ViewModels;

public class TaskViewModel : ViewModel {
  string _taskTitle;
  string _taskDescription;
  bool _taskStatus;

  public string TaskTitle {
    get => _taskTitle;
    set {
      _taskTitle = value;
      OnPropertyChanged();
    }
  }

  public string TaskDescription {
    get => _taskDescription;
    set {
      _taskDescription = value;
      OnPropertyChanged();
    }
  }

  bool TaskStatus {
    get => _taskStatus;
    set {
      _taskStatus = value;
      OnPropertyChanged();
    }
  }

  public ICommand FinishTaskCommand { get; }
  public ICommand DeleteTaskCommand { get; }

  readonly ObservableCollection<TaskViewModel> _tasks;
  
  public TaskViewModel(ObservableCollection<TaskViewModel> tasks) {
    _taskStatus = false;
    _tasks = tasks;
    FinishTaskCommand = new RelayCommand(finishTask);
    DeleteTaskCommand = new RelayCommand(deleteTask);
  }

  private void finishTask(object sender) {
    _taskStatus = true;
  }

  private void deleteTask(object sender) {
    _tasks.Remove(this);
  }
}
