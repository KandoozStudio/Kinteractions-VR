using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandooz.KVR
{
    public interface IPose
    {
        float this[int indexer]{set;}
        string Name { get; set; }

    }
}
