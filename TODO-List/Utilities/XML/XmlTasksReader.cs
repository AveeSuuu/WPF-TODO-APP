using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Animation;
using System.Xml;
using TODO_List.ViewModels;

namespace TODO_List.Utilities.XML;

public class XmlTasksReader {
  readonly string _filePath;

  public XmlTasksReader() {
    var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    _filePath = System.IO.Path.Combine(documentsPath, "tasks.xml");  
  }
  
  public ObservableCollection<TaskViewModel> Read() {
    var tasks = new ObservableCollection<TaskViewModel>();
    if (!File.Exists(_filePath)) return tasks;
    var taskNodes = getTaskNodesFromXml();
    loadXmlToCollection(taskNodes, tasks);
    return tasks;
  }

  private XmlNodeList? getTaskNodesFromXml() {
    var xmlDocument = new XmlDocument(); 
    xmlDocument.Load(_filePath);
    var taskNodes = xmlDocument.SelectNodes("/tasks/task");
    return taskNodes;
  }
  
  private void loadXmlToCollection(XmlNodeList? taskNodes, ObservableCollection<TaskViewModel> tasks) {
    if (taskNodes == null) return;
    foreach (XmlNode taskNode in taskNodes) {
      var task = new TaskViewModel(tasks) {
        TaskTitle = taskNode["title"]?.InnerText,
        TaskDescription = taskNode["description"]?.InnerText,
        TaskStatus = taskNode["status"]?.InnerText == "1"
      };
      tasks.Add(task);
    }
  }
}
