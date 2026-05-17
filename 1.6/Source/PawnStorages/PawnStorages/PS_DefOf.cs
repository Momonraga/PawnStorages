using RimWorld;
using Verse;

namespace PawnStorages;

[DefOf]
public static class PS_DefOf
{
    // Core Jobs & Functionality (Needed by CompPawnStorage, Warden, and Utility)
    public static JobDef PS_Enter;
    public static JobDef PS_Release;
    public static JobDef PS_CaptureInPawnStorage;
    public static JobDef PS_CaptureEntityInPawnStorage;
    public static JobDef PS_CaptureAnimalInPawnStorage;
    
    // Farm Jobs
    public static JobDef PS_CaptureAnimalToFarm;
    public static JobDef PS_RopeToFarm;
    
    // Time Assignments
    public static TimeAssignmentDef PS_Home;

    // Things
    public static ThingDef PS_BatteryFarm;
    public static ThingDef PS_FarmHopper;
}