using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouVideo.Models.Validators
{
    internal abstract class ValidatorBase<T>
    {
        public void Validate(T instance)
        {
            ValidateCore(instance);
        }

        protected abstract void ValidateCore(T instance);
    }
}
