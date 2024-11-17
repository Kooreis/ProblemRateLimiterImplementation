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