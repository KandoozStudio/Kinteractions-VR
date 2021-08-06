using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandooz.KVR
{
    interface IPose
    {
        float this[int indexer]{set;}
        float Weight { get; set; }
    }
}
