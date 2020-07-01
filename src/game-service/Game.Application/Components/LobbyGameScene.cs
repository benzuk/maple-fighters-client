using System.Collections.Generic;
using Common.Components;
using Common.MathematicsHelper;
using Game.Application.Objects;
using Physics.Box2D;

namespace Game.Application.Components
{
    public class LobbyGameScene : GameScene
    {
        private readonly IIdGenerator idGenerator;

        public LobbyGameScene(IIdGenerator idGenerator)
            : base(new Vector2(40, 5), new Vector2(10, 5))
        {
            this.idGenerator = idGenerator;

            Components.Add(new PlayerSpawnDataProvider(GetPlayerSpawnData()));
            Components.Add(new GameObjectCollection(GetGameObjects()));
            Components.Add(new GameSceneOrderExecutor());
            Components.Add(new GamePhysicsExecutor(GetWorld()));
        }

        IWorldWrapper GetWorld()
        {
            var lowerBound = new Vector2(-100, -100);
            var upperBound = new Vector2(100, 100);
            var gravity = new Vector2(0, -9.8f);
            var doSleep = false;
            var continuousPhysics = false;

            return new WorldWrapper(lowerBound, upperBound, gravity, doSleep, continuousPhysics);
        }

        PlayerSpawnData GetPlayerSpawnData()
        {
            return new PlayerSpawnData(new Vector2(18, -1.86f), new Vector2(10, 5));
        }

        IEnumerable<IGameObject> GetGameObjects()
        {
            yield return CreateGuardian();
            yield return CreatePortal();
        }

        IGameObject CreateGuardian()
        {
            var id = idGenerator.GenerateId();
            var position = new Vector2(-14.24f, -2.025f);
            var guardian = new GuardianGameObject(id, this);

            guardian.Transform.SetPosition(position);
            guardian.Transform.SetSize(Vector2.One);
            guardian.AddBubbleNotification("Hello", 1);

            return guardian;
        }

        IGameObject CreatePortal()
        {
            var id = idGenerator.GenerateId();
            var position = new Vector2(-17.125f, -1.5f);
            var portal = new PortalGameObject(id, this);

            portal.AddPortalData((byte)Map.TheDarkForest);

            return portal;
        }
    }
}