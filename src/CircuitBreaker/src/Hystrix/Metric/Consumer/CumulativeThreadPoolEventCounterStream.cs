// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Concurrent;
using Steeltoe.Common;

namespace Steeltoe.CircuitBreaker.Hystrix.Metric.Consumer;

public class CumulativeThreadPoolEventCounterStream : BucketedCumulativeCounterStream<HystrixCommandCompletion, long[], long[]>
{
    private static readonly ConcurrentDictionary<string, CumulativeThreadPoolEventCounterStream> Streams = new();

    private static readonly int AllEventTypesSize = ThreadPoolEventTypeHelper.Values.Count;

    public override long[] EmptyBucketSummary => new long[AllEventTypesSize];

    public override long[] EmptyOutputValue => new long[AllEventTypesSize];

    private CumulativeThreadPoolEventCounterStream(IHystrixThreadPoolKey threadPoolKey, int numCounterBuckets, int counterBucketSizeInMs,
        Func<long[], HystrixCommandCompletion, long[]> reduceCommandCompletion, Func<long[], long[], long[]> reduceBucket)
        : base(HystrixThreadPoolCompletionStream.GetInstance(threadPoolKey), numCounterBuckets, counterBucketSizeInMs, reduceCommandCompletion, reduceBucket)
    {
    }

    public static CumulativeThreadPoolEventCounterStream GetInstance(IHystrixThreadPoolKey threadPoolKey, IHystrixThreadPoolOptions properties)
    {
        int counterMetricWindow = properties.MetricsRollingStatisticalWindowInMilliseconds;
        int numCounterBuckets = properties.MetricsRollingStatisticalWindowBuckets;
        int counterBucketSizeInMs = counterMetricWindow / numCounterBuckets;

        return GetInstance(threadPoolKey, numCounterBuckets, counterBucketSizeInMs);
    }

    public static CumulativeThreadPoolEventCounterStream GetInstance(IHystrixThreadPoolKey threadPoolKey, int numBuckets, int bucketSizeInMs)
    {
        return Streams.GetOrAddEx(threadPoolKey.Name, _ =>
        {
            var stream = new CumulativeThreadPoolEventCounterStream(threadPoolKey, numBuckets, bucketSizeInMs, HystrixThreadPoolMetrics.AppendEventToBucket,
                HystrixThreadPoolMetrics.CounterAggregator);

            stream.StartCachingStreamValuesIfUnstarted();
            return stream;
        });
    }

    public static void Reset()
    {
        foreach (CumulativeThreadPoolEventCounterStream stream in Streams.Values)
        {
            stream.Unsubscribe();
        }

        HystrixThreadPoolCompletionStream.Reset();

        Streams.Clear();
    }

    public long GetLatestCount(ThreadPoolEventType eventType)
    {
        return Latest[(int)eventType];
    }
}