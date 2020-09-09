using AutoVersionsDB.Core.ProcessDefinitions;

namespace AutoVersionsDB.Core.Validations
{
    public abstract class ValidatorBase
    {
        public abstract string ValidatorName { get; }

        public abstract string ErrorInstructionsMessage { get; }

        public abstract string Validate(AutoVersionsDbProcessParams executionParam);
    }


    //public abstract class ValidatorBase<TValidationArgs> : ValidatorBase
    //    where TValidationArgs : ValidationArgs
    //{
    //    public override string Validate(ValidationArgs validationArgs)
    //    {
    //        return Validate(validationArgs as TValidationArgs);
    //    }

    //    public abstract string Validate(TValidationArgs validationArgs);
    //}
}
