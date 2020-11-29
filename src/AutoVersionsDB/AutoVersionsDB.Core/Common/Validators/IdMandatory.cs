using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class IdMandatory : ValidatorBase
    {
        private readonly string _id;

        public override string ValidatorName => "IdMandatory";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public IdMandatory(string id)
        {
            _id = id;
        }

        public override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_id))
            {
                string errorMsg = "Id is mandatory";
                return errorMsg;
            }

            return "";
        }
    }
}
