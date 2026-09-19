namespace Application.Common;
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }

    // Ensures that success has no error and failure always has an error message
    protected Result(bool isSuccess, string? error)
    {
        if (isSuccess && error != null)
            throw new InvalidOperationException("A successful result cannot have an error.");

        if (!isSuccess && error == null)
            throw new InvalidOperationException("A failed result must have an error.");

        IsSuccess = isSuccess;
        Error = error;
    }

    // Factory methods to create results cleanly
    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
}

// Represents the outcome of an operation returning data of type T
public class Result<T> : Result
{
    private readonly T? _value;

    // Returns the value on success; prevents accessing invalid data on failure
    public T Value => IsSuccess 
        ? _value! 
        : throw new InvalidOperationException("Cannot access Value of a failure result.");

    protected internal Result(T? value, bool isSuccess, string? error) 
        : base(isSuccess, error)
    {
        _value = value;
    }

    public static Result<T> Success(T value) => new(value, true, null);
    public new static Result<T> Failure(string error) => new(default, false, error);
}