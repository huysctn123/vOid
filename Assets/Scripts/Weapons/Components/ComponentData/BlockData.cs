﻿
namespace Void.Weapons.Components
{
    public class BlockData : ComponentData<AttackBlock>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Block);
        }
    }
}