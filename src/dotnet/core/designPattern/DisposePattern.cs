/*
Definition: Disposes an object from memory efficiently
*/

using System;

namespace InterviewPreperationGuide.Core.DesignPattern {
    public class DisposePattern : IDisposable {
        private bool IsDisposed { get; set; }

        public DisposePattern () {
            IsDisposed = false;
        }

        // descructor to finalize the code only when the dispose method is not called
        ~DisposePattern () {
            Dispose (false);
        }

        public void Dispose () {
            Dispose (true);

            // since this object will be cleaned up by dispose method,
            // the same should be removed from the finalization queue
            // so that the finalization code is not executed the second time
            GC.SuppressFinalize (this);

            IsDisposed = true;
        }

        private void Dispose (bool disposing) {
            if (!IsDisposed) {
                if (disposing) {
                    // Dispose manage objects here.
                }

                // Dispose unmanage objects here.

                // Set large fields to null.
                IsDisposed = true;
            }
        }
    }
}