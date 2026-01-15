using Content.Shared.Clothing.Components;
using Content.Shared.Examine;
using Content.Shared.Inventory;
using Content.Shared.Inventory.Events;
using Robust.Shared.Utility;

namespace Content.Shared.Clothing.EntitySystems;

/// <summary>
/// System that prevents ALL clothing from being removed when a ClothingLock item is worn.
/// This is intended for use with collar modules to create a full clothing lock.
/// </summary>
public sealed class ClothingLockSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ClothingLockComponent, ExaminedEvent>(OnExamined);
        // Listen on the ClothingLock item itself - the inventory relay system will forward unequip attempts
        SubscribeLocalEvent<ClothingLockComponent, InventoryRelayedEvent<IsUnequippingTargetAttemptEvent>>(OnUnequipAttempt);
    }

    private void OnExamined(Entity<ClothingLockComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup(Loc.GetString("clothing-lock-examine"));
    }

    private void OnUnequipAttempt(Entity<ClothingLockComponent> ent, ref InventoryRelayedEvent<IsUnequippingTargetAttemptEvent> args)
    {
        // Allow the collar itself to be removed, but prevent all other clothing removal
        if (args.Args.Equipment == ent.Owner)
            return;

        // Cancel all other unequip attempts when the clothing lock is worn
        args.Args.Reason = "clothing-lock-prevent-removal";
        args.Args.Cancel();
    }
}
