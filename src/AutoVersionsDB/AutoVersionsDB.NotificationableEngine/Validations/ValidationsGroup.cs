using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine.Validations
{
    public class ValidationsGroup
    {
        private readonly List<ValidatorBase> _validators;

        public int Count
        {
            get
            {
                return _validators.Count;
            }
        }
        public bool ShouldContinueWhenFindError { get; }


        public ValidationsGroup(bool shouldContinueWhenFindError)
        {
            ShouldContinueWhenFindError = shouldContinueWhenFindError;

            _validators = new List<ValidatorBase>();
        }

        public void Add(ValidatorBase validator)
        {
            _validators.Add(validator);
        }

        public IEnumerable<ValidatorBase> GetValidators()
        {
            return _validators.ToList();
        }
    }
}
