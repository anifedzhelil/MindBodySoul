using System.ComponentModel.DataAnnotations;

namespace MindBodySoul.Validation
{
    public class NotEmptyGuidAttribute: ValidationAttribute
    {
        public NotEmptyGuidAttribute()
        : base ("The {0} field must be a valid not empty GUID."){ }

        public override bool IsValid(object? value)
        {  
            return value is Guid guid && guid != Guid.Empty;
        }

    }
}
