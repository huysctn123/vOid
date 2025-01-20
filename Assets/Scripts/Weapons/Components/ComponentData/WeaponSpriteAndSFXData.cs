﻿using System.Collections;
using UnityEngine;

namespace Void.Weapons.Components
{
    public class WeaponSpriteAndSFXData : ComponentData<AttackSprites>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(WeaponSpriteAndSFX);
        }
    }
}