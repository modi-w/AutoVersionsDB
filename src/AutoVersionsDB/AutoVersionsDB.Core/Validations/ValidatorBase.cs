using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Validations
{
    public abstract class ValidatorBase
    {
        internal abstract string ValidatorName { get; }

        internal abstract string ErrorInstructionsMessage { get; }

        internal abstract string Validate();
    }


    //internal abstract class ValidatorBase<TValidationArgs> : ValidatorBase
    //    where TValidationArgs : ValidationArgs
    //{
    //    internal override string Validate(ValidationArgs validationArgs)
    //    {
    //        return Validate(validationArgs as TValidationArgs);
    //    }

    //    internal abstract string Validate(TValidationArgs validationArgs);
    //}
}
