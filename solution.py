Here is a simple Python console application that implements a rate limiter using the token bucket algorithm. This application restricts the number of requests that can be processed per second.

```python
import time
import threading

class RateLimiter:
    def __init__(self, max_rate):
        self.tokens = 0
        self.last = time.time()
        self.lock = threading.Lock()
        self.max_rate = max_rate

    def acquire(self):
        with self.lock:
            now = time.time()
            elapsed = now - self.last
            self.last = now
            self.tokens += elapsed * self.max_rate
            if self.tokens > self.max_rate:
                self.tokens = self.max_rate
            if self.tokens < 1:
                time.sleep(1 - self.tokens)
                self.tokens -= 1
            else:
                self.tokens -= 1

def process_request(rate_limiter):
    rate_limiter.acquire()
    print("Request processed at", time.time())

if __name__ == "__main__":
    rate_limiter = RateLimiter(5)
    while True:
        process_request(rate_limiter)
```

This application creates a `RateLimiter` object that allows up to 5 requests per second. The `acquire` method is called before processing each request. If the rate limit has been reached, the `acquire` method will pause the thread until it is allowed to proceed.