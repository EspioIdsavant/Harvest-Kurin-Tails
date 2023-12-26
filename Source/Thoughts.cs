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
            try
            {
                if (p.def.defName == "Kurin_Race") { isKurin = true; } // Kurin HAR
                if (p.def.defName == "Kurin") { isKurin = true; } // Kurin Deluxe
            } catch (Exception e) { }
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
            try
            {
                if (p.def.defName == "Kurin_Race") { iAmKurin = true; } // Kurin HAR
                if (otherPawn.def.defName == "Kurin_Race") { theyAreKurin = true; } // Kurin HAR
                if (p.def.defName == "Kurin") { iAmKurin = true; } // Kurin Deluxe
                if (otherPawn.def.defName == "Kurin") { theyAreKurin = true; } // Kurin Deluxe
            } catch (Exception e) { }


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

    public class ThoughtWorker_Precept_ReviaWoolApparel : ThoughtWorker
    {
        public Type workerClass;
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            List<Apparel> worn = p.apparel.WornApparel;
            bool isRevia = false;
            try
            {
                if (p.def.defName == "ReviaRaceAlien") { isRevia = true; } // Revia HAR
            } catch (Exception e) { }
            if (!isRevia) { return ThoughtState.Inactive; }
            int count = 0;
            string text = null;
            foreach (Apparel w in worn)
            {
                try
                {
                    if (w.Stuff.defName == "WoolRevia")
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

    public class ThoughtWorker_ReviaWoolWorn : ThoughtWorker
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
            bool iAmRevia = false;
            bool theyAreRevia = false;
            try
            {
                if (p.def.defName == "ReviaRaceAlien") { iAmRevia = true; } // Revia HAR
                if (otherPawn.def.defName == "ReviaRaceAlien") { theyAreRevia = true; } // Revia HAR
            } catch (Exception) { }


            if (!iAmRevia) { return false; }
            List<Apparel> otherworn = otherPawn.apparel.WornApparel;
            int othercount = 0;
            foreach (Apparel w in otherworn)
            {
                try
                {
                    if (w.Stuff.defName == "WoolRevia") othercount++;
                }
                catch { }
            }
            List<Apparel> myworn = p.apparel.WornApparel;
            int mycount = 0;
            foreach (Apparel w in myworn)
            {
                try
                {
                    if (w.Stuff.defName == "WoolRevia") mycount++;
                }
                catch { }
            }

            if (othercount == 0 && !theyAreRevia) { return false; }
            if (othercount < 3 && !theyAreRevia) { return ThoughtState.ActiveAtStage(0); }
            if (othercount > 2 && !theyAreRevia) { return ThoughtState.ActiveAtStage(1); }
            if ((mycount - othercount) == 0 && theyAreRevia) { return false; }
            if ((mycount - othercount)>0 && theyAreRevia) { return ThoughtState.ActiveAtStage(2); }
            if ((mycount - othercount) < 0 && theyAreRevia) { return ThoughtState.ActiveAtStage(3); }

            return false;

        }
    }

}