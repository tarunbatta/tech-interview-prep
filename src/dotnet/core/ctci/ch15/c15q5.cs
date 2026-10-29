/*
Call In Order: Suppose we have the following code:
public class Foo
{
    public Foo() { ... }
    public void first() { ... }
    public void second() { ... }
    public void third() { ... }
}
The same instance of Foo will be passed to three different threads.ThreadA will call first,
threadB will call second, and threadC will call third.Design a mechanism to ensure that
first is called before second and second is called before third.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c15q5 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }
    }
}