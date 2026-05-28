using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
namespace PawnStorages.Farm.Comps
{
    public class CompFarmGatherable : ThingComp
    {
        public void GatherableTick(CompHasGatherableBodyResource gatherable, int tickInterval = 1)
        {
            if (!gatherable.Active)
                return;
            float gatherableReadyIncrement = (float)(1f / ((double)gatherable.GatherResourcesIntervalDays * 60000f));
            gatherableReadyIncrement *= PawnUtility.BodyResourceGrowthSpeed(gatherable.parent as Pawn);
            // we're not doing this every tick so bump the progress
            gatherableReadyIncrement *= tickInterval;
            gatherableReadyIncrement *= PawnStoragesMod.settings.ProductionScale;
            gatherable.fullness += gatherableReadyIncrement;
            gatherable.fullness = Mathf.Clamp(gatherable.fullness, 0f, 1f);
            if (!gatherable.ActiveAndFull)
                return;
            int amountToGenerate = GenMath.RoundRandom(gatherable.ResourceAmount * gatherable.fullness);
            while (amountToGenerate > 0f)
            {
                int generateThisLoop = Mathf.Clamp(amountToGenerate, 1, gatherable.ResourceDef.stackLimit);
                amountToGenerate -= generateThisLoop;
                Thing thing = ThingMaker.MakeThing(gatherable.ResourceDef);
                thing.stackCount = generateThisLoop;
                parent.TryGetComp<CompPawnStorageProducer>().DaysProduce.Add(thing);
            }
            gatherable.fullness = 0f;
        }
    }
}

