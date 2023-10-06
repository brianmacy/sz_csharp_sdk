using System;

namespace Senzing
{
public class G2Exception : Exception
{
    public G2Exception() {
        _code=-1;
    }
    public G2Exception(string message) : base(message) {
        _code=-1;
    }
    public G2Exception(long code, string message) : base(message) {
        _code=code;
    }
    public G2Exception(string message, Exception inner) : base(message, inner) {
        _code=-1;
    }

    public long code() {
        return _code;
    }


    public static void HandleError(long code, string message)
    {
        if (code != 0)
            throw new G2Exception(code, message);
    }

    long _code;
}


public class G2BadInputException : G2Exception
{
    public G2BadInputException() { }
    public G2BadInputException(string message) : base(message) { }
    public G2BadInputException(string message, Exception inner) : base(message, inner) { }
}
public class G2NotFoundException : G2Exception
{
    public G2NotFoundException() { }
    public G2NotFoundException(string message) : base(message) { }
    public G2NotFoundException(string message, Exception inner) : base(message, inner) { }
}
public class G2UnknownDatasourceException : G2Exception
{
    public G2UnknownDatasourceException() { }
    public G2UnknownDatasourceException(string message) : base(message) { }
    public G2UnknownDatasourceException(string message, Exception inner) : base(message, inner) { }
}


public class G2ConfigurationException : G2Exception
{
    public G2ConfigurationException() { }
    public G2ConfigurationException(string message) : base(message) { }
    public G2ConfigurationException(string message, Exception inner) : base(message, inner) { }
}


public class G2RetryableException : G2Exception
{
    public G2RetryableException() { }
    public G2RetryableException(string message) : base(message) { }
    public G2RetryableException(string message, Exception inner) : base(message, inner) { }
}
public class G2DatabaseConnectionLostException : G2RetryableException
{
    public G2DatabaseConnectionLostException() { }
    public G2DatabaseConnectionLostException(string message) : base(message) { }
    public G2DatabaseConnectionLostException(string message, Exception inner) : base(message, inner) { }
}
public class G2RetryTimeoutExceededException : G2RetryableException
{
    public G2RetryTimeoutExceededException() { }
    public G2RetryTimeoutExceededException(string message) : base(message) { }
    public G2RetryTimeoutExceededException(string message, Exception inner) : base(message, inner) { }
}


public class G2UnrecoverableException : G2Exception
{
    public G2UnrecoverableException() { }
    public G2UnrecoverableException(string message) : base(message) { }
    public G2UnrecoverableException(string message, Exception inner) : base(message, inner) { }
}
public class G2DatabaseException : G2UnrecoverableException
{
    public G2DatabaseException() { }
    public G2DatabaseException(string message) : base(message) { }
    public G2DatabaseException(string message, Exception inner) : base(message, inner) { }
}
public class G2UnhandledException : G2UnrecoverableException
{
    public G2UnhandledException() { }
    public G2UnhandledException(string message) : base(message) { }
    public G2UnhandledException(string message, Exception inner) : base(message, inner) { }
}
public class G2LicenseException : G2UnrecoverableException
{
    public G2LicenseException() { }
    public G2LicenseException(string message) : base(message) { }
    public G2LicenseException(string message, Exception inner) : base(message, inner) { }
}
public class G2NotInitializedException : G2UnrecoverableException
{
    public G2NotInitializedException() { }
    public G2NotInitializedException(string message) : base(message) { }
    public G2NotInitializedException(string message, Exception inner) : base(message, inner) { }
}
}
