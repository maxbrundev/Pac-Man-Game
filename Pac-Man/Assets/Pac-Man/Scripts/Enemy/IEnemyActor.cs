using System.Collections;
using System.Collections.Generic;
using System.Numerics;

using Vector2 = UnityEngine.Vector2;

namespace PacMan
{
    public interface IEnemyActor
    {
        void Move(Vector2 p_direction);

        void TakeDamage(uint p_amount);
        void ApplyDamage(uint p_amount);

        void Spawn();
    }
}