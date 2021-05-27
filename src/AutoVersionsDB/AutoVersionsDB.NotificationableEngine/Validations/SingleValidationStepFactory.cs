namespace AutoVersionsDB.NotificationableEngine.Validations
{
    public class SingleValidationStepFactory
    {
        public SingleValidationStepFactory()
        {

        }

        internal virtual SingleValidationStep Create(ValidatorBase validator)
        {
            return new SingleValidationStep(validator);
        }
    }
}
