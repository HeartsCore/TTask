using Controllers.Square;
using Extensions;
using UnityEngine;

namespace Behaviours.Square
{
    public class SquareCollisionBehaviour : MonoBehaviour
    {
        private ISquareSpawnPool _squareSpawnPool;

        public void SetPoolManager(ISquareSpawnPool squareSpawnPool) => _squareSpawnPool = squareSpawnPool;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.IsPlayer()) return;
            
            _squareSpawnPool.ReturnSquareToPool(gameObject);
        }
    }
}