using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine.Validations
{
    public abstract class ValidationsFactory
    {
        public abstract string ValidationName { get; }

        public abstract ValidationsGroup Create(ProcessContext processContext);
    }
}
