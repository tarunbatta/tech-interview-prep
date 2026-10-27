/*
Bounded Blocking Queue (Producer / Consumer)
https://leetcode.com/problems/design-bounded-blocking-queue/ (LeetCode 1188)

- Enqueue blocks while the queue is full.
- Dequeue blocks while the queue is empty.

In production .NET code, prefer System.Collections.Concurrent.BlockingCollection<T>
or System.Threading.Channels — this is the "implement it yourself" interview version.
*/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewPreperationGuide.Core.Concurrency {
    public class BlockingQueue<T> {
        private readonly Queue<T> queue = new Queue<T> ();
        private readonly object sync = new object ();
        private readonly int capacity;

        public BlockingQueue (int capacity) {
            if (capacity <= 0) {
                throw new ArgumentOutOfRangeException (nameof (capacity));
            }
            this.capacity = capacity;
        }

        public static void Init () {
            BlockingQueue<int> q = new BlockingQueue<int> (2);
            const int itemsPerProducer = 5;
            int consumedSum = 0;

            Task[] producers = new Task[2];
            for (int p = 0; p < producers.Length; p++) {
                int id = p;
                producers[p] = Task.Run (() => {
                    for (int i = 1; i <= itemsPerProducer; i++) {
                        q.Enqueue (i);
                        Console.WriteLine ($"Producer {id} enqueued {i} (size {q.Size ()})");
                    }
                });
            }

            Task consumer = Task.Run (() => {
                for (int i = 0; i < producers.Length * itemsPerProducer; i++) {
                    int item = q.Dequeue ();
                    Interlocked.Add (ref consumedSum, item);
                    Thread.Sleep (5);
                }
            });

            Task.WaitAll (producers);
            consumer.Wait ();
            Console.WriteLine ($"Consumed sum: {consumedSum}"); // 30
        }

        // Time: O (1) amortized, excluding wait time
        public void Enqueue (T item) {
            lock (sync) {
                while (queue.Count == capacity) {
                    Monitor.Wait (sync);
                }

                queue.Enqueue (item);
                Monitor.PulseAll (sync);
            }
        }

        // Time: O (1), excluding wait time
        public T Dequeue () {
            lock (sync) {
                while (queue.Count == 0) {
                    Monitor.Wait (sync);
                }

                T item = queue.Dequeue ();
                Monitor.PulseAll (sync);
                return item;
            }
        }

        public int Size () {
            lock (sync) {
                return queue.Count;
            }
        }
    }
}
