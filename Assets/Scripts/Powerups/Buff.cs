using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Buff : MonoBehaviour
    {
        public int timer = 1;
        
        public abstract bool ApplyBuff(Collider2D player);
        public abstract bool Debuff(Collider2D player);
    }
}