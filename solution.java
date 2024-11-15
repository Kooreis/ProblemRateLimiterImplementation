Here is a simple implementation of a rate limiter using Java's ScheduledExecutorService. This implementation allows a maximum of 5 requests per second.

```java
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicInteger;

public class RateLimiter {
    private final ScheduledExecutorService scheduler;
    private final AtomicInteger counter = new AtomicInteger(0);
    private static final int MAX_REQUESTS_PER_SECOND = 5;

    public RateLimiter() {
        scheduler = Executors.newScheduledThreadPool(1);
        scheduler.scheduleAtFixedRate(() -> counter.set(0), 1, 1, TimeUnit.SECONDS);
    }

    public boolean allowRequest() {
        return counter.incrementAndGet() <= MAX_REQUESTS_PER_SECOND;
    }

    public static void main(String[] args) throws InterruptedException {
        RateLimiter rateLimiter = new RateLimiter();

        for (int i = 0; i < 20; i++) {
            new Thread(() -> {
                if (rateLimiter.allowRequest()) {
                    System.out.println("Request processed by " + Thread.currentThread().getName());
                } else {
                    System.out.println("Request denied for " + Thread.currentThread().getName());
                }
            }).start();
            Thread.sleep(200);
        }

        rateLimiter.scheduler.shutdown();
    }
}
```

This program creates a rate limiter that allows up to 5 requests per second. It uses a ScheduledExecutorService to reset a counter every second. Each request increments the counter, and the request is only allowed if the counter is less than or equal to the maximum number of requests per second. The main method tests this by creating 20 threads that each make a request, with a delay of 200 milliseconds between each request.