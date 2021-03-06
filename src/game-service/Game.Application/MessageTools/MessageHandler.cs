using System;

namespace Game.MessageTools
{
    public class MessageHandler<T> : IMessageHandler<T>
        where T : struct
    {
        private readonly Action<T> handler;

        public MessageHandler(Action<T> handler)
        {
            this.handler = handler;
        }

        public void Handle(T message)
        {
            handler?.Invoke(message);
        }
    }
}