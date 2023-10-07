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
        if (code == 0)
            return;

        switch (code)
        {
        case 0002:
        case 0007:
        case 0022:
        case 0023:
        case 0024:
        case 0025:
        case 0026:
        case 0051:
        case 0053:
        case 0065:
        case 0066:
        case 0088:
        case 9115:
        case 9414:
        case 7303:
        case 7305:
        case 7313:
        case 7314:
        case 7426:
        case 2057:
        case 8000:
        case 30121:
        case 30122:
        case 30123:
        case 30131:
            throw new G2BadInputException(code, message);
        case 0014:
        case 0019:
        case 0020:
        case 0021:
        case 0028:
        case 0030:
        case 0034:
        case 0035:
        case 0036:
        case 0040:
        case 0060:
        case 0061:
        case 0062:
        case 0064:
        case 0067:
        case 0089:
        case 0090:
        case 1019:
        case 7209:
        case 7211:
        case 7212:
        case 7216:
        case 7217:
        case 7218:
        case 7220:
        case 7221:
        case 7223:
        case 7224:
        case 7226:
        case 7227:
        case 7228:
        case 7230:
        case 7232:
        case 7233:
        case 7234:
        case 7235:
        case 7236:
        case 7237:
        case 7239:
        case 7240:
        case 7241:
        case 7242:
        case 7243:
        case 7244:
        case 7245:
        case 7246:
        case 7247:
        case 9107:
        case 9110:
        case 9111:
        case 9112:
        case 9113:
        case 9116:
        case 9117:
        case 9118:
        case 9119:
        case 9120:
        case 9210:
        case 9220:
        case 9222:
        case 9224:
        case 9228:
        case 9240:
        case 9241:
        case 9250:
        case 9251:
        case 9252:
        case 9253:
        case 9254:
        case 9255:
        case 9256:
        case 9257:
        case 9258:
        case 9259:
        case 9260:
        case 9261:
        case 9264:
        case 9265:
        case 9266:
        case 9269:
        case 9270:
        case 9284:
        case 9285:
        case 9286:
        case 9292:
        case 9293:
        case 9295:
        case 9296:
        case 9297:
        case 9298:
        case 9300:
        case 9301:
        case 9308:
        case 9309:
        case 9310:
        case 9408:
        case 9409:
        case 9413:
        case 7317:
        case 2001:
        case 2012:
        case 2015:
        case 2029:
        case 2034:
        case 2036:
        case 2037:
        case 2038:
        case 2131:
        case 2135:
        case 2136:
        case 2137:
        case 7344:
        case 2138:
        case 2139:
        case 2041:
        case 2045:
        case 2047:
        case 2048:
        case 2049:
        case 2050:
        case 2051:
        case 2061:
        case 2062:
        case 2065:
        case 2066:
        case 2067:
        case 2069:
        case 2070:
        case 2071:
        case 2075:
        case 2076:
        case 2077:
        case 2079:
        case 2080:
        case 2081:
        case 2082:
        case 2083:
        case 2084:
        case 2087:
        case 2088:
        case 2089:
        case 2090:
        case 2091:
        case 2092:
        case 2093:
        case 2094:
        case 2095:
        case 2099:
        case 2101:
        case 2102:
        case 2103:
        case 2104:
        case 2105:
        case 2106:
        case 2107:
        case 2108:
        case 2109:
        case 2110:
        case 2111:
        case 2112:
        case 2113:
        case 2114:
        case 2117:
        case 2118:
        case 2120:
        case 2121:
        case 2123:
        case 2205:
        case 2206:
        case 2207:
        case 2208:
        case 2209:
        case 2210:
        case 2211:
        case 2212:
        case 2213:
        case 2214:
        case 2215:
        case 2216:
        case 2217:
        case 2218:
        case 2219:
        case 2220:
        case 2221:
        case 2222:
        case 2223:
        case 2224:
        case 2225:
        case 2226:
        case 2227:
        case 2228:
        case 2230:
        case 2231:
        case 2232:
        case 2233:
        case 2234:
        case 2235:
        case 2236:
        case 2237:
        case 2238:
        case 2239:
        case 2240:
        case 2241:
        case 2242:
        case 2243:
        case 2244:
        case 2245:
        case 2246:
        case 2247:
        case 2248:
        case 2249:
        case 2250:
        case 2251:
        case 2252:
        case 2253:
        case 2254:
        case 2255:
        case 2256:
        case 2257:
        case 2258:
        case 2259:
        case 2260:
        case 2261:
        case 2262:
        case 2263:
        case 2264:
        case 2266:
        case 2267:
        case 2268:
        case 2269:
        case 2270:
        case 2271:
        case 2272:
        case 2273:
        case 2274:
        case 2275:
        case 2276:
        case 2277:
        case 2278:
        case 2279:
        case 2280:
        case 2281:
        case 2282:
        case 2283:
        case 2289:
        case 2290:
        case 2291:
        case 8501:
        case 8516:
        case 8517:
        case 8522:
        case 8525:
        case 8526:
        case 8527:
        case 8528:
        case 8529:
        case 8536:
        case 8538:
        case 8540:
        case 8543:
        case 8544:
        case 8556:
        case 8557:
        case 8599:
        case 8601:
        case 8602:
        case 8604:
        case 8605:
        case 8606:
        case 8607:
        case 8608:
        case 8701:
        case 8702:
        case 9500:
        case 9802:
        case 9803:
        case 8545:
            throw new G2ConfigurationException(code, message);

        case 1006:
        case 1007:
            throw new G2DatabaseConnectionLostException(code, message);

        case 0054:
        case 1000:
        case 1001:
        case 1002:
        case 1003:
        case 1004:
        case 1005:
        case 1008:
        case 1009:
        case 1010:
        case 1011:
        case 1012:
        case 1013:
        case 1014:
        case 1015:
        case 1016:
        case 1017:
        case 1018:
            throw new G2DatabaseException(code, message);

        case 999:
            throw new G2LicenseException(code, message);

        case 33:
        case 37:
            throw new G2NotFoundException(code, message);

        case 0048:
        case 0049:
        case 0050:
        case 0063:
            throw new G2NotInitializedException(code, message);

        case 10:
            throw new G2RetryTimeoutExceededException(code, message);

        case 87:
            throw new G2UnhandledException(code, message);

        case 27:
            throw new G2UnknownDatasourceException(code, message);

        default:
            throw new G2Exception(code, message);
        }
    }

    long _code;
}


public class G2BadInputException : G2Exception
{
    public G2BadInputException() { }
    public G2BadInputException(string message) : base(message) { }
    public G2BadInputException(long code, string message) : base(code,message) { }
    public G2BadInputException(string message, Exception inner) : base(message, inner) { }
}
public class G2NotFoundException : G2Exception
{
    public G2NotFoundException() { }
    public G2NotFoundException(string message) : base(message) { }
    public G2NotFoundException(long code, string message) : base(code,message) { }
    public G2NotFoundException(string message, Exception inner) : base(message, inner) { }
}
public class G2UnknownDatasourceException : G2Exception
{
    public G2UnknownDatasourceException() { }
    public G2UnknownDatasourceException(string message) : base(message) { }
    public G2UnknownDatasourceException(long code, string message) : base(code,message) { }
    public G2UnknownDatasourceException(string message, Exception inner) : base(message, inner) { }
}


public class G2ConfigurationException : G2Exception
{
    public G2ConfigurationException() { }
    public G2ConfigurationException(string message) : base(message) { }
    public G2ConfigurationException(long code, string message) : base(code,message) { }
    public G2ConfigurationException(string message, Exception inner) : base(message, inner) { }
}


public class G2RetryableException : G2Exception
{
    public G2RetryableException() { }
    public G2RetryableException(string message) : base(message) { }
    public G2RetryableException(long code, string message) : base(code,message) { }
    public G2RetryableException(string message, Exception inner) : base(message, inner) { }
}
public class G2DatabaseConnectionLostException : G2RetryableException
{
    public G2DatabaseConnectionLostException() { }
    public G2DatabaseConnectionLostException(string message) : base(message) { }
    public G2DatabaseConnectionLostException(long code, string message) : base(code,message) { }
    public G2DatabaseConnectionLostException(string message, Exception inner) : base(message, inner) { }
}
public class G2RetryTimeoutExceededException : G2RetryableException
{
    public G2RetryTimeoutExceededException() { }
    public G2RetryTimeoutExceededException(string message) : base(message) { }
    public G2RetryTimeoutExceededException(long code, string message) : base(code,message) { }
    public G2RetryTimeoutExceededException(string message, Exception inner) : base(message, inner) { }
}


public class G2UnrecoverableException : G2Exception
{
    public G2UnrecoverableException() { }
    public G2UnrecoverableException(string message) : base(message) { }
    public G2UnrecoverableException(long code, string message) : base(code,message) { }
    public G2UnrecoverableException(string message, Exception inner) : base(message, inner) { }
}
public class G2DatabaseException : G2UnrecoverableException
{
    public G2DatabaseException() { }
    public G2DatabaseException(string message) : base(message) { }
    public G2DatabaseException(long code, string message) : base(code,message) { }
    public G2DatabaseException(string message, Exception inner) : base(message, inner) { }
}
public class G2UnhandledException : G2UnrecoverableException
{
    public G2UnhandledException() { }
    public G2UnhandledException(string message) : base(message) { }
    public G2UnhandledException(long code, string message) : base(code,message) { }
    public G2UnhandledException(string message, Exception inner) : base(message, inner) { }
}
public class G2LicenseException : G2UnrecoverableException
{
    public G2LicenseException() { }
    public G2LicenseException(string message) : base(message) { }
    public G2LicenseException(long code, string message) : base(code,message) { }
    public G2LicenseException(string message, Exception inner) : base(message, inner) { }
}
public class G2NotInitializedException : G2UnrecoverableException
{
    public G2NotInitializedException() { }
    public G2NotInitializedException(string message) : base(message) { }
    public G2NotInitializedException(long code, string message) : base(code,message) { }
    public G2NotInitializedException(string message, Exception inner) : base(message, inner) { }
}
}
