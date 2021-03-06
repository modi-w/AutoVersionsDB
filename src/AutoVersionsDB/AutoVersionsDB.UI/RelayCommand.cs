﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoVersionsDB.UI
{
    //https://gist.github.com/colinkiama/32ff261222a5f7d95e5ca56b95bbf960

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());

            if (parameter != null)
            {
                return _canExecute == null || _canExecute();
            }
            else
            {
                return false;
            }
        }

        public void Execute()
        {
            ExecuteWrapped();
        }
        public void Execute(object parameter)
        {
            ExecuteWrapped();
        }

        public Task ExecuteWrapped()
        {
            var results = Task.Run(() =>
                {
                    try
                    {
                        _execute();
                    }
                    catch (Exception ex)
                    {
                        UIGeneralEvents.FireOnException(this, ex);
                    }
                });

            return results;
        }

        public RelayCommand(Action execute) : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

    }


    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());

            if (parameter != null)
            {
                return _canExecute == null || _canExecute((T)parameter);
            }
            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            ExecuteWrapped(parameter);
        }

        public Task ExecuteWrapped(object parameter)
        {
            var results = Task.Run(() =>
            {
                try
                {
                    _execute((T)parameter);
                }
                catch (Exception ex)
                {
                    UIGeneralEvents.FireOnException(this, ex);
                }
            });

            return results;
        }


        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }



        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

    }
}
