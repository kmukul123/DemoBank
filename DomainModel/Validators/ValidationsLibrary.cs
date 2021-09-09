using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Validators
{

    /// <summary>
    /// TODO add tests for these
    /// </summary>
    static class ValidationsLibrary
    {
        public static void HasMinLength(string input, int min, string v2)
        {
            if (input.Length < min)
                throw new ApplicationException($"minlength of {v2} is {input.Length} should be at least {min}");
        }
        public static void HasMaxLength(string input, int min, string v2)
        {
            if (input.Length > min)
                throw new ApplicationException($"maxlength of {v2} is {input.Length} should be at least {min}");
        }

        internal static void HasLessThenFourPrecision(decimal input, string v)
        {
            if (input * 10000 != (int)(input * 10000))
                throw new ApplicationException($"precision of {v} is more then 4");
        }
        internal static void HasNotNull(object input, string v)
        {
            if (input == null)
                throw new ApplicationException($"{v} is null");
        }
    }
}

