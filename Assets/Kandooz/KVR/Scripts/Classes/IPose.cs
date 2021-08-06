namespace Kandooz.KVR
{
    interface IPose
    {
        float this[FingerName index]
        {
            set;
        }
        float this[int index]
        {
            set;
        }
        float Weight { set;  }
    }
}
