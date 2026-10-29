using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Multiply two numbers represented by linked lists
    ///
    /// Input:
    /// First List: >6->3   // represents number 63
    /// Second List: 4->2   // represents number 42
    /// Output : 2646
    /// </summary>
    public class Question31 {
        public static void Init (string[] args) {
            Question31Node first_a = new Question31Node (6);
            first_a.next = new Question31Node (3);

            Question31Node second_a = new Question31Node (4);
            second_a.next = new Question31Node (2);

            Console.WriteLine (MultiplywoLists (first_a, second_a));
        }

        public static int MultiplywoLists (Question31Node first, Question31Node second) {
            int num1 = 0;
            int num2 = 0;

            while (first != null || second != null) {
                if (first != null) {
                    num1 = num1 * 10 + first.data;
                    first = first.next;
                }

                if (second != null) {
                    num2 = num2 * 10 + second.data;
                    second = second.next;
                }
            }

            return num1 * num2;
        }
    }

    public class Question31Node {
        public int data { get; set; }
        public Question31Node next { get; set; }

        public Question31Node (int num) {
            this.data = num;
            this.next = null;
        }
    }
}