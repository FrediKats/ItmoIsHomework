namespace AppliedMath.LoadBalancer.Models
{
    public class HandlerStateInfo
    {
        public HandlerStateInfo(int workerId, int queueSize, long waitingTimeMilliseconds, long executingTimeMilliseconds)
        {
            WorkerId = workerId;
            QueueSize = queueSize;
            WaitingTimeMilliseconds = waitingTimeMilliseconds;
            ExecutingTimeMilliseconds = executingTimeMilliseconds;
        }

        public int WorkerId { get; }
        public int QueueSize { get; }

        public long WaitingTimeMilliseconds { get; }
        public long ExecutingTimeMilliseconds { get; }

        public double LoadingPercent =>
            (double)ExecutingTimeMilliseconds / (ExecutingTimeMilliseconds + WaitingTimeMilliseconds);
    }
}