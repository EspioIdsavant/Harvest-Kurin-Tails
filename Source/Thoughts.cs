using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RimWorld;
using Verse;

namespace harvest_kurin_tails
{
    public class ThoughtWorker_Precept_KurinWoolApparel : ThoughtWorker
    {
        public Type workerClass;
          protected override ThoughtState CurrentStateInternal(Pawn p)
          {
            List<Apparel> worn = p.apparel.WornApparel;
            bool isKurin = false;
            if (p.RaceProps.body.defName == "Kurin_Body") { isKurin = true;  } // Kurin HAR
            if (p.genes.Xenotype.defName == "Kurin") { isKurin = true;  } // Kurin Deluxe
            if (!isKurin) { return ThoughtState.Inactive;  }
            int count = 0;
            string text = null;
            foreach (Apparel w in worn)
            {
                try
                {
                    if (w.Stuff.defName == "WoolKurin")
                    {
                        count++;
                        if (text == null)
                        { text = w.def.label; }
                        else { text = text + "," + w.def.label; }
                    }
                }
                catch (Exception) { }

            }
            if (count == 0)
            {
                return ThoughtState.Inactive;
            }
            if (count >= 5)
            {
                return ThoughtState.ActiveAtStage(4, text);
            }
            return ThoughtState.ActiveAtStage(count - 1, text);
        }

    }

    public class ThoughtWorker_KurinWoolWorn_Disapproved : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        {
            if (!p.RaceProps.Humanlike)
            {
                return false;
            }
            if (!RelationsUtility.PawnsKnowEachOther(p, otherPawn))
            {
                return false;
            }
            bool iAmKurin = false;
            bool theyAreKurin = false;
            if (p.RaceProps.body.defName == "Kurin_Body") { iAmKurin = true; } // Kurin HAR
            if (otherPawn.RaceProps.body.defName == "Kurin_Body") { theyAreKurin = true; } // Kurin HAR
            if (p.genes.Xenotype.defName == "Kurin") { iAmKurin = true; } // Kurin Deluxe
            if (otherPawn.genes.Xenotype.defName == "Kurin") { theyAreKurin = true; } // Kurin Deluxe


            if (!iAmKurin) { return false; }
            List<Apparel> worn = otherPawn.apparel.WornApparel;
            int count = 0;
            string text = null;
            foreach (Apparel w in worn)
            {
                try
                {

                    if (w.Stuff.defName == "WoolKurin")
                    {
                        count++;
                        if (text == null)
                        { text = w.def.label; }
                        else { text = text + "," + w.def.label; }
                    }
                } catch { }
            }
            if (count == 0) { return false; }
            if (count < 3 && !theyAreKurin) { return ThoughtState.ActiveAtStage(0); }
            if (count > 2 && !theyAreKurin) { return ThoughtState.ActiveAtStage(1); }
            if (count < 3 && theyAreKurin) { return ThoughtState.ActiveAtStage(2); }
            if (count > 2 && theyAreKurin) { return ThoughtState.ActiveAtStage(3); }

            return false;

           
        }
    }
}