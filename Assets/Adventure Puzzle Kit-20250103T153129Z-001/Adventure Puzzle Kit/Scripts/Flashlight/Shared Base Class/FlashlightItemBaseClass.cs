/// <summary>
/// This script is the base class that shares fields for both the FlashlightItem and Flashlight trigger scripts. This also has a custom editor called "FlashlightItemBaseClassEditor"
/// </summary>

using UnityEngine;

namespace AdventurePuzzleKit.FlashlightSystem
{
    public class FlashlightItemBaseClass : MonoBehaviour
    {
        [Space(10)] [SerializeField] protected ObjectType _objectType = ObjectType.None;
        public enum ObjectType { None, Battery, Flashlight }

        [SerializeField] protected int batteryNumber = 1;
    }
}