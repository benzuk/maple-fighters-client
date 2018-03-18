﻿using Chat.Common;
using CommonCommunicationInterfaces;
using CommonTools.Log;
using ComponentModel.Common;
using PeerLogic.Common.Components;
using ServerApplication.Common.ApplicationBase;
using ServerApplication.Common.Components;

namespace Chat.Application.PeerLogic.Components
{
    internal class ChatMessageEventSender : Component, IChatMessageEventSender
    {
        private IPeerContainer peerContainer;
        private IMinimalPeerGetter peerGetter;

        protected override void OnAwake()
        {
            base.OnAwake();

            peerContainer = Server.Components.GetComponent<IPeerContainer>().AssertNotNull();
            peerGetter = Components.GetComponent<IMinimalPeerGetter>().AssertNotNull();
        }

        public void SendChatMessage(string message)
        {
            foreach (var peerWrapper in peerContainer.GetAllPeerWrappers())
            {
                if (peerWrapper.PeerId == peerGetter.PeerId)
                {
                    continue;
                }

                var eventSenderWrapper = peerWrapper.PeerLogic.Components.GetComponent<IEventSenderWrapper>().AssertNotNull();
                var parameters = new ChatMessageEventParameters(message);
                eventSenderWrapper.Send((byte)ChatEvents.ChatMessage, parameters, MessageSendOptions.DefaultReliable());
            }
        }
    }
}