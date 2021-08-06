using System;

namespace Kandooz.KVR
{
    public class TweenablePose : IPose
    {
        public float this[int indexer] { set => throw new NotImplementedException(); }

        public float Weight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

}
