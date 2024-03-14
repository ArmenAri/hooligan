using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hooligan.Domain.OriginalsItems
{
    public class OriginalItems : IEnumerable<string>
    {
        public IEnumerator<string> GetEnumerator()
        {
            yield return "earth";
            yield return "wind";
            yield return "fire";
            yield return "water";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
