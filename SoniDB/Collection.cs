using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoniDB
{
    public class Collection<T> : List<T>
    {
        public string Path { get; set; }

        public int Save()
        {
            new Serializer().Serialize(Path, this);
            return 1;
        }
    }
}
