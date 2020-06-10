using System;
using System.ComponentModel.DataAnnotations;

namespace Submissions.API.Filters
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RangeUntilCurrentYearAttribute : RangeAttribute
    {
        public RangeUntilCurrentYearAttribute(int minimum) : base(minimum, DateTime.UtcNow.Year)
        {
        }
    }
}