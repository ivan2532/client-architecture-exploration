using UnityEngine;

namespace Core.Bootstrapping
{
    public class ScriptableObjectRepository : MonoBehaviour
    {
        [field: SerializeField]
        public Features.Game.Configuration.GameConfiguration GameConfiguration { get; private set; }
    }
}