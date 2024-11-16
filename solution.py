import time
import threading

class RateLimiter:
    def __init__(self, max_rate):
        self.tokens = 0
        self.last = time.time()
        self.lock = threading.Lock()
        self.max_rate = max_rate