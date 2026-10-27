/*
Definition: Ensures a class hs only one instance and provides a global point to access it
Marked sealed to prevent derivation
*/

using System;

namespace InterviewPreperationGuide.Core.DesignPattern {
    public sealed class Singleton {
        // volatile ensures that the assignment to the instance is done before its accessed
        private static volatile Singleton instance;

        // to avoid deadlocks
        private static object syncRoot = new();

        // private constructor to ensure that the instance of the class cannot be created
        private Singleton () { }

        public static Singleton Instance {
            get {
                if (instance == null) {
                    // allows only single thread to enter only when the instance is not yet created
                    lock (syncRoot) {
                        // double check to solve thread concurrency problem
                        if (instance == null) {
                            instance = new Singleton ();
                        }
                    }
                }

                return instance;
            }
        }
    }
}