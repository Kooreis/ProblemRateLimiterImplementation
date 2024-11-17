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