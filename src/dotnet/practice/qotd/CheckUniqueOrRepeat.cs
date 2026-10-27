namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Given an array of ints, return true if all unique and false if any repeat. Without extra memory. No sort
    /// </summary>
    internal class CheckUniqueOrRepeat {
        //public static void Main(string[] args)
        //{
        //    Console.WriteLine(IsUniqueOrRepeat(new int[] {1, 2, 3, 4}));
        //    Console.WriteLine(IsUniqueOrRepeat(new int[] {1, 2, 3, 1}));
        //}

        public static bool IsUniqueOrRepeat (int[] arr) {
            bool result = true;

            if (arr != null && arr.Length > 0) {
                for (int i = 0; i < arr.Length - 1; i++) {
                    for (int j = i + 1; j < arr.Length; j++) {
                        if (arr[i] == arr[j]) {
                            return false;
                        }
                    }
                }
            }

            return result;
        }
    }
}