using UnityEngine;

namespace Behaviours.Helpers
{
    public class TagManager : MonoBehaviour
    {
        public const string Player = "Player";

        private void OnValidate() => CompareTag(Player);
    }
}