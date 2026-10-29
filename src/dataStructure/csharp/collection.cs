/*

*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.DataStructure.CustomCollection {
    public class CCollection : CollectionBase {
        public void Add (Object item) {
            this.InnerList.Add (item);
        }

        public void Remove (Object item) {
            this.InnerList.Remove (item);
        }

        public new void Clear () {
            this.InnerList.Clear ();
        }

        public new int Count () {
            return this.InnerList.Count;
        }
    }
}