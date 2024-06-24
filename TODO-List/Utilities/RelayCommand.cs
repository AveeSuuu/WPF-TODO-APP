﻿using System.Windows.Input;

namespace TODO_List.Utilities;

public class RelayCommand : ICommand {
  protected Action<object> _execute;
  protected Func<object, bool> _canExecute;

  public event EventHandler? CanExecuteChanged;

  public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null) {
    _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    _canExecute = canExecute;
  }

  public bool CanExecute(object parameter) {
    return _canExecute == null || _canExecute(parameter);
  }

  public void Execute(object parameter) {
    _execute(parameter);
  }

  public void RaiseCanExecuteChanged() {
    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
  }
}
