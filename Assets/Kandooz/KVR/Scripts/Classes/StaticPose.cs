using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandooz.KVR
{
    class StaticPose : IPose
    {
        public float this[FingerName index] { set => throw new NotImplementedException(); }
        public float this[int index] { set => throw new NotImplementedException(); }
        public float Weight { set => throw new NotImplementedException(); }
    }
}
