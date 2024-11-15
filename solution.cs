Here is a simple implementation of a rate limiter using C#:

```C#
using System;
using System.Collections.Generic;
using System.Threading;

public class RateLimiter
{
    private int _requestsPerSecond;
    private Queue<DateTime> _requests;

    public RateLimiter(int requestsPerSecond)
    {
        _requestsPerSecond = requestsPerSecond;
        _requests = new Queue<DateTime>(requestsPerSecond);
    }

    public bool ShouldAllowRequest()
    {
        while (_requests.Count > 0 && (DateTime.Now - _requests.Peek()).TotalSeconds > 1)
        {
            _requests.Dequeue();
        }

        if (_requests.Count < _requestsPerSecond)
        {
            _requests.Enqueue(DateTime.Now);
            return true;
        }
        else
        {
            return false;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        RateLimiter rateLimiter = new RateLimiter(5);

        for (int i = 0; i < 10; i++)
        {
            if (rateLimiter.ShouldAllowRequest())
            {
                Console.WriteLine("Request " + i + " processed");
            }
            else
            {
                Console.WriteLine("Request " + i + " denied");
            }

            Thread.Sleep(200);
        }

        Console.ReadLine();
    }
}
```

This program creates a rate limiter that allows up to 5 requests per second. It then tries to make 10 requests, waiting 200 milliseconds between each request. The rate limiter uses a queue to keep track of the timestamps of the requests. When a new request comes in, it removes all timestamps that are more than a second old from the queue. If the queue has room for the new request, it is added to the queue and the request is allowed. If the queue is full, the request is denied.