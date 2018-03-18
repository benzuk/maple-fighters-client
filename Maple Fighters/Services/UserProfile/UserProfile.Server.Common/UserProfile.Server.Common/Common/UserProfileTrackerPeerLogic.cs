﻿using Authorization.Server.Common;
using CommonCommunicationInterfaces;
using CommonTools.Log;
using CommunicationHelper;
using PeerLogic.Common;
using ServerCommunicationInterfaces;

namespace UserProfile.Server.Common
{
    /*using Server = ServerApplication.Common.ApplicationBase.Server;

    public class UserProfileTrackerPeerLogic : PeerLogicBase<EmptyOperationCode, EmptyEventCode>
    {
        private bool isManuallyDisconnected;

        private readonly int userId;
        private readonly ServerType serverType;

        public UserProfileTrackerPeerLogic(int userId, ServerType serverType)
        {
            this.userId = userId;
            this.serverType = serverType;
        }

        public override void Initialize(IClientPeerWrapper<IClientPeer> peer)
        {
            base.Initialize(peer);

            var userProfileServiceAPI = Server.Components.GetComponent<IUserProfileServiceAPI>().AssertNotNull();
            var parameters = new ChangeUserProfilePropertiesRequestParameters(userId, peer.PeerId, serverType, ConnectionStatus.Connected);
            userProfileServiceAPI.ChangeUserProfileProperties(parameters);

            AddUserIdToConverter();

            SubscribeToDisconnectionNotifier();
            SubscribeToUserProfilePropertiesChanged();
        }

        public override void Dispose()
        {
            base.Dispose();

            RemoveUserIdFromConverter();

            UnsubscribeFromDisconnectionNotifier();
            UnsubscribeFromUserProfilePropertiesChanged();
        }

        private void AddUserIdToConverter()
        {
            var userToPeerIdConverter = Server.Components.GetComponent<IUserIdToPeerIdConverter>().AssertNotNull();
            userToPeerIdConverter.Add(userId, PeerWrapper.PeerId);
        }

        private void RemoveUserIdFromConverter()
        {
            var userToPeerIdConverter = Server.Components.GetComponent<IUserIdToPeerIdConverter>().AssertNotNull();
            userToPeerIdConverter.Remove(userId);
        }

        private void SubscribeToUserProfilePropertiesChanged()
        {
            var userProfilePropertiesChangesEventInvoker = Components.AddComponent(new UserProfilePropertiesChangesEventInvoker());
            userProfilePropertiesChangesEventInvoker.UserProfilePropertiesChanged += OnUserProfilePropertiesChanged;
        }

        private void UnsubscribeFromUserProfilePropertiesChanged()
        {
            var userProfilePropertiesChangesEventInvoker = Components.GetComponent<IUserProfilePropertiesChangesEventInvoker>();
            userProfilePropertiesChangesEventInvoker.UserProfilePropertiesChanged -= OnUserProfilePropertiesChanged;
        }

        private void OnUserProfilePropertiesChanged(UserProfilePropertiesChangedEventParameters parameters)
        {
            if (parameters.ConnectionStatus != ConnectionStatus.Connected || parameters.ServerType == serverType)
            {
                return;
            }

            isManuallyDisconnected = true;
            PeerWrapper.Peer.Disconnect();
        }

        private void SubscribeToDisconnectionNotifier()
        {
            PeerWrapper.Peer.PeerDisconnectionNotifier.Disconnected += OnDisconnected;
        }

        private void UnsubscribeFromDisconnectionNotifier()
        {
            PeerWrapper.Peer.PeerDisconnectionNotifier.Disconnected -= OnDisconnected;
        }

        private void OnDisconnected(DisconnectReason disconnectReason, string details)
        {
            UnsubscribeFromDisconnectionNotifier();

            if (!isManuallyDisconnected)
            {
                OnClientDisconnected();
            }

            RemoveUserIdFromConverter();
        }

        private void OnClientDisconnected()
        {
            var userProfileServiceAPI = Server.Components.GetComponent<IUserProfileServiceAPI>().AssertNotNull();
            var parameters = new ChangeUserProfilePropertiesRequestParameters(userId, ConnectionStatus.Disconnected);
            userProfileServiceAPI.ChangeUserProfileProperties(parameters);

            var authorizationServiceAPI = Server.Components.GetComponent<IAuthorizationServiceAPI>().AssertNotNull();
            authorizationServiceAPI.RemoveAuthorization(new RemoveAuthorizationRequestParameters(userId));
        }
    }*/
}