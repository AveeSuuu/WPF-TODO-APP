using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TODO_List.Utilities;

namespace TODO_List.ViewModels;

public class MainWindowViewModel : ViewModel {
 public ObservableCollection<TaskViewModel> Tasks { get; }
 public ICommand MinimizeAppCommand { get; }
 public ICommand CloseAppCommand { get; }
 public ICommand AddNewTaskCommand { get; }
 static int indexer = 0; 
 
 public MainWindowViewModel() {
  Tasks = new();
  //logika do wczytywania z xml
  MinimizeAppCommand = new RelayCommand(minimizeApp);
  CloseAppCommand = new RelayCommand(closeApp);
  AddNewTaskCommand = new RelayCommand(addNewTask);
 }

 private void minimizeApp(object sender) {
  Application.Current.MainWindow.WindowState = WindowState.Minimized;
 }

 private void closeApp(object sender) {
  //zapisanie rzeczy do xml
  Application.Current.Shutdown();
 }
 
 private void addNewTask(object sender) {
  var task = new TaskViewModel(Tasks) {
   TaskTitle = $"Task nr.{indexer++}",
   TaskDescription = $"Task description..."
  };

  Tasks.Add(task);
 }
}
