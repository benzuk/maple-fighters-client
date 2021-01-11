﻿using System;
using TMPro;
using UI.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI.CharacterSelection
{
    [RequireComponent(typeof(Animator), typeof(UIFadeAnimation))]
    public class ClickableCharacterImage : UIElement,
                                           IPointerClickHandler,
                                           IClickableCharacterView
    {
        public event Action<UICharacterIndex, bool> CharacterClicked;

        public string CharacterName
        {
            set
            {
                if (characterNameText != null)
                {
                    characterNameText.text = value;
                }
            }
        }

        public UICharacterIndex CharacterIndex
        {
            set => characterIndex = value;
        }

        public UICharacterClass CharacterClass
        {
            set => characterClass = value;
        }

        public GameObject GameObject => gameObject;

        [Header("Text"), SerializeField]
        private TextMeshProUGUI characterNameText;

        private UICharacterClass characterClass;
        private UICharacterIndex characterIndex;
        private bool hasCharacter;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            Hidden += OnHidden;
        }

        private void OnDestroy()
        {
            Hidden -= OnHidden;
        }

        private void OnHidden()
        {
            Destroy(gameObject);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var hasCharacter = characterClass != UICharacterClass.Sample;

            CharacterClicked?.Invoke(characterIndex, hasCharacter);
        }

        public void PlayAnimation(UICharacterAnimation characterAnimation)
        {
            switch (characterAnimation)
            {
                case UICharacterAnimation.Idle:
                    {
                        animator.SetBool("Walk", false);
                        break;
                    }

                case UICharacterAnimation.Walk:
                    {
                        animator.SetBool("Walk", true);
                        break;
                    }
            }
        }
    }
}