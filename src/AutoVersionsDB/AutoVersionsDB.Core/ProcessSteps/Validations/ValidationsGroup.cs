using AutoVersionsDB.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
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

        internal void Add(ValidatorBase validator)
        {
            _validators.Add(validator);
        }

        internal IEnumerable<ValidatorBase> GetValidators()
        {
            return _validators.ToList();
        }
    }
}
