using Content.Shared.Clothing.EntitySystems;
using Robust.Shared.GameStates;

namespace Content.Shared.Clothing.Components;

/// <summary>
/// When applied to a collar (or other clothing item), prevents ALL clothing from being removed
/// while this item is worn. This includes both self-removal and removal by others.
/// </summary>
[RegisterComponent]
[NetworkedComponent]
[Access(typeof(ClothingLockSystem))]
public sealed partial class ClothingLockComponent : Component
{
}
