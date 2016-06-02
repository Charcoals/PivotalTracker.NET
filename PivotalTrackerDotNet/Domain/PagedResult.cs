using System.Collections;
using System.Collections.Generic;

namespace PivotalTrackerDotNet.Domain
{
    public class PagedResult<T> : IEnumerable<T>
    {
        public Pagination Pagination { get; set; }
        public List<T> Data { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
