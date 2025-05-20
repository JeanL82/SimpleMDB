

namespace SimpleMDB;


public class Result<T>
{
    public bool IsValid{get;}
    public T? Value{get;}
    public Exception? Error{get;}

    public Result(T value) 
    {
        IsValid = true;
        Value = value;
    }

    public Result(Exception error)
    {
        IsValid = false;
        Error = error;
    }

    internal static Result<User> Success(object newUser)
    {
        throw new NotImplementedException();
    }

    internal static Result<User> Fail(Exception exception)
    {
        throw new NotImplementedException();
    }

    public static implicit operator Result<T>(Result<Actor> v)
    {
        throw new NotImplementedException();
    }
}