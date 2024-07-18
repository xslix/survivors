using System.Collections.Generic;
using Survivors.Unit;
using UnityEngine;

namespace Survivors
{
    [CreateAssetMenu(fileName="GeneralSettings", menuName="Settings/GeneralSettings")]
    public class GeneralSettings : ScriptableObject
    {
        public int enemiesCount;
        public List<UnitData> enemies;
    }
}