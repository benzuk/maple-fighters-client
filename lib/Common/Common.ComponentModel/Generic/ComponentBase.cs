﻿using System;

namespace Common.ComponentModel.Generic
{
    public class ComponentBase<TOwner> : IComponent<TOwner>
        where TOwner : class
    {
        protected TOwner Owner { get; private set; }
        protected IComponentsContainer Components { get; private set; }

        void IComponent<TOwner>.Awake(TOwner owner, IComponentsContainer components)
        {
            Owner = owner;
            Components = components;

            OnAwake();
        }

        void IDisposable.Dispose()
        {
            OnRemoved();
        }

        protected virtual void OnAwake()
        {
            // Left blank intentionally
        }

        protected virtual void OnRemoved()
        {
            // Left blank intentionally
        }
    }
}