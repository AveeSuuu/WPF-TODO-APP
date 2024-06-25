using System.Collections.ObjectModel;
using System.Windows.Media.Animation;
using System.Xml;
using TODO_List.ViewModels;

namespace TODO_List.Utilities.XML;

public class XmlTasksWriter {
  readonly string _filePath;
  readonly XmlDocument _xmlDocument;
  
  public XmlTasksWriter() {
    var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    _filePath = System.IO.Path.Combine(documentsPath, "tasks.xml");
    _xmlDocument = initializeXmlDocument();
  }

  public void Write(ObservableCollection<TaskViewModel> tasks) {
    appendTasksToXml(tasks);
    _xmlDocument.Save(_filePath);
  }

  private XmlDocument initializeXmlDocument() {
    var xmlDocument = new XmlDocument();
    var xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
    xmlDocument.AppendChild(xmlDeclaration);
    var tasksElement = xmlDocument.CreateElement("tasks"); //root
    xmlDocument.AppendChild(tasksElement);
    return xmlDocument;
  }

  private void appendTasksToXml(ObservableCollection<TaskViewModel> tasks) {
    var root = _xmlDocument.DocumentElement;
    foreach (var task in tasks) {
      var taskElement = createTaskElement(task);
      root?.AppendChild(taskElement);
    }
  }
  
  private XmlElement createTaskElement(TaskViewModel task) {
    var taskElement = _xmlDocument.CreateElement("task");
    var title = createElementWithText("title", task.TaskTitle);
    var description = createElementWithText("description", task.TaskDescription);
    var status = createElementWithText("status", task.TaskStatus ? "1" : "0");
    taskElement.AppendChild(title);
    taskElement.AppendChild(description);
    taskElement.AppendChild(status);
    return taskElement;
  }
  
  private XmlElement createElementWithText(string name, string value) {
    var xmlElement = _xmlDocument.CreateElement(name);
    var elementValue = _xmlDocument.CreateTextNode(value);
    xmlElement.AppendChild(elementValue);
    return xmlElement;
  }
}
