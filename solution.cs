using System;
using System.Collections.Generic;

public class RateLimiter
{
    private int _requestsPerSecond;
    private Queue<DateTime> _requests;

    public RateLimiter(int requestsPerSecond)
    {
        _requestsPerSecond = requestsPerSecond;
        _requests = new Queue<DateTime>(requestsPerSecond);
    }
}