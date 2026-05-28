using System;
using System.Collections.Generic;
using PawnStorages.Farm.Comps;
using RimWorld;
using UnityEngine;
using Verse;

//Mirror of Production Tab shows male and female animals allows ejecting each animal

namespace PawnStorages.Farm
{
    public class ITab_Breeding_Animals : ITab
    {
        public CompFarmStorage compFarmStorage => SelThing.TryGetComp<CompFarmStorage>();

        public ITab_Breeding_Animals()
        {
            size = WinSize;
            labelKey = "PS_BreedingAnimalsTab";
        }

        public float PawnFullness(Pawn pawn)
        {
            if (pawn.TryGetComp(out CompHasGatherableBodyResource compGatherable))
                return compGatherable.Fullness;
            return 0;
        }

        public bool DrawLine(float position, float width, Pawn pawn)
        {
            Rect rect = new Rect(0f, position, width, LineHeight);
            if (alternate)
                Widgets.DrawRectFast(rect, new Color(1f, 1f, 1f, ITab_Pawn_Log_Utility.AlternateAlpha));
            alternate = !alternate;

            Widgets.DefIcon(new Rect(5f, position + 7.5f, 45f, 45f), pawn.def, drawPlaceholder: true, color: Listing_TreeThingFilter.NoMatchColor);

            Pawn_NeedsTracker needs = pawn.needs;
            bool starving = needs?.food?.Starving ?? false;

            Widgets.Label(new Rect(55f, position, width - 90f, 20f),
                (starving ? "PS_FarmTab_NameStarving" : "PS_FarmTab_Name").Translate(pawn.LabelShort));

            Widgets.Label(new Rect(55f, position + 20f, width - 90f, 20f),
                "PS_FarmTab_Nutrition".Translate((needs?.food?.CurLevelPercentage ?? 0f).ToStringPercent()));

            Widgets.Label(new Rect(55f, position + 40f, width - 90f, 20f),
                "PS_FarmTab_Fullness".Translate(PawnFullness(pawn).ToStringPercent(), pawn.gender.GetLabel(animal: true)));

            return Widgets.ButtonImage(new Rect(new Vector2(width - 50f, position + 15f), new Vector2(30f, 30f)), TexButton.Drop, Color.white, GenUI.MouseoverColor);
        }

        protected override void FillTab()
        {
            if (compFarmStorage == null)
                return;
            if (compFarmStorage.GetDirectlyHeldThings().Count <= 0)
                return;

            Widgets.Label(new Rect(5f, 0f, WinSize.x, 30f), "PS_BreedingAnimalsTab_TopLabel".Translate());

            Rect tabRect = new Rect(0f, 30f, WinSize.x, WinSize.y - 30f).ContractedBy(10f);
            Rect scrollViewRect = new Rect(tabRect);
            float totalHeight = compFarmStorage.GetDirectlyHeldThings().Count * LineHeight;
            Rect viewRect = new Rect(0f, 0f, scrollViewRect.width, totalHeight);

            Widgets.AdjustRectsForScrollView(tabRect, ref scrollViewRect, ref viewRect);
            Widgets.BeginScrollView(scrollViewRect, ref ThingFilterState.scrollPosition, viewRect);

            alternate = false;
            float num = 0f;
            List<Pawn> removed = [];
            foreach (Thing thing in compFarmStorage.GetDirectlyHeldThings())
            {
                Pawn pawn = (Pawn)thing;
                if (DrawLine(num, scrollViewRect.width, pawn))
                    removed.Add(pawn);
                num += LineHeight;
            }

            FarmJob_MapComponent comp = SelThing.Map.GetComponent<FarmJob_MapComponent>();
            foreach (Pawn pawn in removed)
            {
                compFarmStorage.ReleaseSingle(compFarmStorage.parent.Map, pawn);
                if (comp != null && comp.farmAssignments.ContainsKey(pawn))
                    comp.farmAssignments.Remove(pawn);
            }

            Widgets.EndScrollView();
        }

        private static readonly Vector2 WinSize = new Vector2(300f, 480f);
        public readonly ThingFilterUI.UIState ThingFilterState = new ThingFilterUI.UIState();
        public const float LineHeight = 60f;
        private bool alternate;
    }
}
