using System.Collections.ObjectModel;
using System.Runtime.Serialization.DataContracts;
using System.Windows;
using System.Windows.Input;
using TODO_List.Utilities;
using TODO_List.Utilities.XML;
using TODO_List.Views;

namespace TODO_List.ViewModels;

public class MainWindowViewModel : ViewModel {
 public ObservableCollection<TaskViewModel> Tasks { get; }
 public ICommand MinimizeAppCommand { get; }
 public ICommand CloseAppCommand { get; }
 public ICommand AddNewTaskCommand { get; }
 public ICommand SaveTasksCommand { get;  }
 
 public MainWindowViewModel() {
  var xmlReader = new XmlTasksReader();
  Tasks = xmlReader.Read();
  MinimizeAppCommand = new RelayCommand(minimizeApp);
  CloseAppCommand = new RelayCommand(closeApp);
  AddNewTaskCommand = new RelayCommand(addNewTask);
  SaveTasksCommand = new RelayCommand(saveTasksToXml);
 }

 private void minimizeApp(object sender) {
  Application.Current.MainWindow.WindowState = WindowState.Minimized;
 }

 private void closeApp(object sender) {
  saveTasksToXml();
  Application.Current.Shutdown();
 }
 
 private void addNewTask(object sender) {
  var insertViewModel = new InsertViewModel();
  var insertView = new InsertView(){ DataContext = insertViewModel};
  insertView.ShowDialog();

  var task = new TaskViewModel(Tasks) {
   TaskTitle = insertViewModel.TaskTitle,
   TaskDescription = insertViewModel.TaskDescription,
   TaskStatus = false
  };
  if (insertView.DialogResult == true) {
    Tasks.Add(task);
  }
 }

 private void saveTasksToXml(object sender = null) {
  var writer = new XmlTasksWriter();
  writer.Write(Tasks);
 }
}
