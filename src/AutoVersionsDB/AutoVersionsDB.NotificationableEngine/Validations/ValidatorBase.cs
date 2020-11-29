using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.NotificationableEngine.Validations
{
    public abstract class ValidatorBase
    {
        public abstract string ValidatorName { get; }

        public abstract string ErrorInstructionsMessage { get; }

        public abstract string Validate();
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
