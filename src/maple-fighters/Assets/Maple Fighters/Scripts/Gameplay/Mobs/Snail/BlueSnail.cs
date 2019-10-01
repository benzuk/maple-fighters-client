﻿using Game.Common;
using Scripts.Gameplay.GameEntity;
using Scripts.Gameplay.Player;
using Scripts.Network.Services;
using UnityEngine;

namespace Scripts.Gameplay.Actors
{
    public class BlueSnail : MonoBehaviour
    {
        [Header("Attack"), SerializeField]
        private Vector2 hitAmount;

        private void Start()
        {
            var gameSceneApi = ServiceProvider.GameService.GetGameSceneApi();
            gameSceneApi?.PlayerAttacked.AddListener(OnPlayerAttacked);
        }

        private void OnDestroy()
        {
            var gameSceneApi = ServiceProvider.GameService.GetGameSceneApi();
            gameSceneApi?.PlayerAttacked.RemoveListener(OnPlayerAttacked);
        }

        private void OnPlayerAttacked(PlayerAttackedEventParameters parameters)
        {
            var entity = EntityContainer.GetInstance().GetLocalEntity();
            if (entity != null)
            {
                var spawnedCharacter =
                    entity.GameObject.GetComponent<ISpawnedCharacter>();
                var character = spawnedCharacter.GetCharacterGameObject();
                if (character != null)
                {
                    var point = 
                        new Vector3(parameters.ContactPointX, parameters.ContactPointY);
                    var direction = new Vector2(
                        x: ((character.transform.position - point).normalized.x
                            > 0
                                ? 1
                                : -1) * hitAmount.x,
                        y: hitAmount.y);

                    var attackPlayer = character.GetComponent<IAttackPlayer>();
                    attackPlayer.OnPlayerAttacked(direction);
                }
            }
        }
    }
}