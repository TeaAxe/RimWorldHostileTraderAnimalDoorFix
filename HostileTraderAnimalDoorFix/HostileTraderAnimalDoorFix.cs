using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace HostileTraderAnimalDoorFix
{
    [StaticConstructorOnStartup]
    public static class HostileTraderAnimalDoorFix
    {
        private static readonly Type patchType = typeof(HostileTraderAnimalDoorFix);

        static HostileTraderAnimalDoorFix()
        {
            Harmony harmony = new Harmony(nameof(HostileTraderAnimalDoorFix));
            harmony.Patch(AccessTools.Method(typeof(LordJob_TradeWithColony), nameof(LordJob_TradeWithColony.CanOpenAnyDoor)),
                prefix: new HarmonyMethod(patchType, nameof(CanOpenAnyDoorPrefix)));
        }

        private static bool CanOpenAnyDoorPrefix(Pawn p, ref bool __result)
        {
            // This fixes the cases where the caravan's faction becomes hostile (such as if you attack them or you a psychic animal pulser).
            // It probably doesn't work for cases where the animal goes manhunter without angering the faction, but it is unclear whether such a scenario is possible.
            __result = p.RaceProps.FenceBlocked && !p.Faction.HostileTo(Faction.OfPlayer);

            return false;
        }
    }
}
