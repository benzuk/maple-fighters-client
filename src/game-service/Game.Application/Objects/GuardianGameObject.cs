using Game.Application.Components;
using Game.Application.Objects.Components;

namespace Game.Application.Objects
{
    public class GuardianGameObject : GameObject
    {
        public GuardianGameObject(int id, IGameScene gameScene)
            : base(id, "Guardian")
        {
            Components.Add(new PresenceSceneProvider(gameScene));
            Components.Add(new ProximityChecker());
        }

        public void AddBubbleNotification(string text, int time)
        {
            Components.Add(new BubbleNotificationSender(text, time));
        }
    }
}