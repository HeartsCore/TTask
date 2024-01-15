using Behaviours.Helpers;
using UnityEngine;

namespace Extensions
{
    public static class ColliderExtensions
    {
        public static bool IsPlayer(this Collider2D col) => col.CompareTag(TagManager.Player);
    }
}