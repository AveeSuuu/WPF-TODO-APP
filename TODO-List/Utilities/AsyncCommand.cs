﻿namespace TODO_List.Utilities;

using System;
using System.Threading.Tasks;
using System.Windows.Input;

public class AsyncCommand : ICommand {
  private readonly Func<Task> _execute;
  private readonly Func<bool> _canExecute;
  private bool _isExecuting;

  public event EventHandler CanExecuteChanged;

  public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null) {
    _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    _canExecute = canExecute;
  }

  public bool CanExecute(object parameter) {
    return !_isExecuting && (_canExecute?.Invoke() ?? true);
  }

  public async void Execute(object parameter) {
    _isExecuting = true;
    RaiseCanExecuteChanged();

    try {
      await _execute();
    } finally {
      _isExecuting = false;
      RaiseCanExecuteChanged();
    }
  }

  public void RaiseCanExecuteChanged() {
    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
  }
}
