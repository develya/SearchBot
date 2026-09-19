using FluentValidation;

namespace Application.Validators;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    public const int MinIdValue = 0;
    
    protected static bool BeAValidUri(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return false;
        }
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}