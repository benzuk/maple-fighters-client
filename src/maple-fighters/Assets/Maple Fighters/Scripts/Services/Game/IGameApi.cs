using Game.Messages;

namespace Scripts.Services
{
    interface IGameApi
    {
        void EnterScene(EnterSceneMessage message);

        void ChangeScene(ChangeSceneMessage message);

        void ChangePosition(ChangePositionMessage message);

        void ChangeAnimationState(ChangeAnimationStateMessage message);
    }
}