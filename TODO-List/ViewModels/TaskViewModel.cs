using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TODO_List.Models;
using TODO_List.Utilities;
using TODO_List.Views;

namespace TODO_List.ViewModels;

public class TaskViewModel : ViewModel {
  readonly TaskModel _task;
  
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

  public bool TaskStatus {
    get => _task.TaskStatus;
    set {
      _task.TaskStatus = value;
      OnPropertyChanged();
    }
  }

  public ICommand FinishTaskCommand { get; }
  public ICommand DeleteTaskCommand { get; }

  readonly ObservableCollection<TaskViewModel> _tasks;

  public TaskViewModel(ObservableCollection<TaskViewModel> tasks) {
    _task = new TaskModel() {
      TaskStatus = false
    };
    _tasks = tasks;
    FinishTaskCommand = new RelayCommand(finishTask);
    DeleteTaskCommand = new RelayCommand(deleteTask);
  }

  private void finishTask(object sender) {
    TaskStatus = !TaskStatus;
  }

  private void deleteTask(object sender) {
    if (!TaskStatus) {
    
    }
    _tasks.Remove(this);
  }
}
