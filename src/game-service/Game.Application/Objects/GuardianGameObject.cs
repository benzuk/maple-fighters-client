using Common.MathematicsHelper;
using Game.Application.Objects.Components;
using InterestManagement;

namespace Game.Application.Objects
{
    public class GuardianGameObject : GameObject
    {
        public GuardianGameObject(int id, Vector2 position)
            : base(id, name: "Guardian")
        {
            Transform.SetPosition(position);

            Components.Add(new GameObjectGetter(this));
        }

        public void AddProximityChecker(IMatrixRegion<IGameObject> matrixRegion)
        {
            var proximityChecker = Components.Add(new ProximityChecker());
            proximityChecker.SetMatrixRegion(matrixRegion);
        }

        public void AddBubbleNotification(string text, int time)
        {
            Components.Add(new BubbleNotificationSender(text, time));
        }
    }
}