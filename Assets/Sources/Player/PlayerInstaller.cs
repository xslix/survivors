using System.Collections.Generic;
using Cinemachine;
using Survivors.Spell;
using Survivors.Unit;
using UnityEngine;
using Zenject;

namespace Survivors.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Unit.Unit playerPrefab;
        [SerializeField] private UnitData playerUnitData;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private List<SpellBase> spells;
        public override void InstallBindings()
        {
            var player = GameObject.Instantiate(playerPrefab);
            player.Init(playerUnitData);
            player.gameObject.tag = Tags.Player;
            player.gameObject.layer = LayerMask.NameToLayer(Tags.Player);
            
            virtualCamera.Follow = player.transform;
            virtualCamera.LookAt = player.transform;
            
            Container.BindInterfacesTo<PlayerManager>().AsSingle().WithArguments(player);
            Container.BindInterfacesTo<BulletManager>().AsSingle().WithArguments(bulletPrefab);
            Container.BindInterfacesTo<SpellManager>().AsSingle().WithArguments(spells);
            foreach (var spell in spells)
            {
                Container.QueueForInject(spell);
            }

        }
    }
}