public class RateLimiter {
    private final ScheduledExecutorService scheduler;
    private final AtomicInteger counter = new AtomicInteger(0);
    private static final int MAX_REQUESTS_PER_SECOND = 5;