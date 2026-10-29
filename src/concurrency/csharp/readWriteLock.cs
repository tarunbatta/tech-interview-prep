/*
Read-Write Lock
Allows many concurrent readers OR a single writer, never both.

This implementation is writer-preferring: once a writer is waiting, new readers block,
which prevents writer starvation under a steady stream of readers.

In production .NET code, prefer System.Threading.ReaderWriterLockSlim — this is the
"implement it yourself" version commonly asked in interviews.
*/

using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewPreperationGuide.Core.Concurrency {
    public class ReadWriteLock {
        private readonly object sync = new object ();
        private int activeReaders = 0;
        private int waitingWriters = 0;
        private bool writerActive = false;

        public static void Init () {
            ReadWriteLock rwLock = new ReadWriteLock ();
            int sharedValue = 0;
            Task[] tasks = new Task[8];

            for (int i = 0; i < tasks.Length; i++) {
                int id = i;
                tasks[i] = Task.Run (() => {
                    if (id % 4 == 0) {
                        rwLock.AcquireWrite ();
                        try {
                            sharedValue++;
                            Console.WriteLine ($"Writer {id} wrote {sharedValue}");
                            Thread.Sleep (20);
                        } finally {
                            rwLock.ReleaseWrite ();
                        }
                    } else {
                        rwLock.AcquireRead ();
                        try {
                            Console.WriteLine ($"Reader {id} read {sharedValue}");
                            Thread.Sleep (10);
                        } finally {
                            rwLock.ReleaseRead ();
                        }
                    }
                });
            }

            Task.WaitAll (tasks);
            Console.WriteLine ($"Final value: {sharedValue}"); // 2
        }

        public void AcquireRead () {
            lock (sync) {
                while (writerActive || waitingWriters > 0) {
                    Monitor.Wait (sync);
                }
                activeReaders++;
            }
        }

        public void ReleaseRead () {
            lock (sync) {
                activeReaders--;
                if (activeReaders == 0) {
                    Monitor.PulseAll (sync);
                }
            }
        }

        public void AcquireWrite () {
            lock (sync) {
                waitingWriters++;
                try {
                    while (writerActive || activeReaders > 0) {
                        Monitor.Wait (sync);
                    }
                } finally {
                    waitingWriters--;
                }
                writerActive = true;
            }
        }

        public void ReleaseWrite () {
            lock (sync) {
                writerActive = false;
                Monitor.PulseAll (sync);
            }
        }
    }
}
