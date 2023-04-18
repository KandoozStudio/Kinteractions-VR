using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandooz.InteractionSystem.Core
{
    public interface IPose
    {
        float this[int indexer]{set;}
        string Name { get; set; }

    }
}
