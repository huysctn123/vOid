﻿using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Void.Weapons.Components
{
    /*
  * This weapon component is responsible for showing an optional sprite during an attack. The OptionalSprite GameObject has a sprite renderer where
  * this optional sprite is displayed. The OptionalSprite GameObject is put into the appropriate position by the base animations.
  * The SetOptionalSpriteEnabled() and SetOptionalSpriteDisabled() animation events are used to enable and disable this optional sprite at the appropriate
  * times. This sprite, as the title states, is optional and not needed for every attack if the component is added.
  */
    public class OptionalSprite : WeaponComponent<OptionalSpriteData, AttackOptionalSprite>
    {
        private SpriteRenderer spriteRenderer;

        private float fpsCouter;
        private int animationStep = 0;

        private void HandleSetOptionalSpriteActive(bool value)
        {
            spriteRenderer.enabled = value;
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();

            if (!currentAttackData.UseOptionalSprite)
                return;

            spriteRenderer.sprite = currentAttackData.Sprite[0];
        }
        protected virtual void HandlOnUpdate()
        {
            if (currentAttackData.Sprite.Length == 1) return;
            if (currentAttackData.UseOptionalSprite)
            {
                fpsCouter += Time.deltaTime;
                if (fpsCouter >= 1f / currentAttackData.Fps)
                {
                    animationStep++;
                    if (animationStep == currentAttackData.Sprite.Length) animationStep = 0;

                    spriteRenderer.sprite = currentAttackData.Sprite[animationStep];

                    fpsCouter = 0f;
                }
            }             
        }
        private void Update()
        {
            HandlOnUpdate();
        }
        #region Plumbing

        protected override void Awake()
        {
            base.Awake();

            // Find the correct sprite renderer we care about using the OptionalSpriteMarker MonoBehaviour
            spriteRenderer = GetComponentInChildren<OptionalSpriteMarker>().SpriteRenderer;
            spriteRenderer.enabled = false;
        }

        protected override void Start()
        {
            base.Start();

            AnimationEventHandler.OnSetOptionalSpriteActive += HandleSetOptionalSpriteActive;
            HandleEnter();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            AnimationEventHandler.OnSetOptionalSpriteActive -= HandleSetOptionalSpriteActive;
        }

        #endregion
    }
}