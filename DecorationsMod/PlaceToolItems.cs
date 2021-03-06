﻿using DecorationsMod.Controllers;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod
{
    public static class PlaceToolItems
    {
        private static void MakeItemPlaceable(TechType techType, GameObject item, Collider collider = null)
        {
            // We can pick this item
            PrefabsHelper.SetDefaultPickupable(item);

            // We can place this item
            PrefabsHelper.SetDefaultPlaceTool(item, collider);

            // Add TechType to the hand-equipments
            SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(techType, EquipmentType.Hand);

            // Set as selectable item.
            SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(techType, QuickSlotType.Selectable);
        }

        public static void MakeSnacksPlaceable()
        {
            GameObject snack1 = Resources.Load<GameObject>("WorldEntities/Food/Snack1");
            if (snack1 != null)
            {
                BoxCollider snack1Collider = snack1.AddComponent<BoxCollider>();
                snack1Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                MakeItemPlaceable(TechType.Snack1, snack1, snack1Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/Snack1");
#endif
            GameObject snack2 = Resources.Load<GameObject>("WorldEntities/Food/Snack2");
            if (snack2 != null)
            {
                BoxCollider snack2Collider = snack2.AddComponent<BoxCollider>();
                snack2Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                MakeItemPlaceable(TechType.Snack2, snack2, snack2Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/Snack2");
#endif
            GameObject snack3 = Resources.Load<GameObject>("WorldEntities/Food/Snack3");
            if (snack3 != null)
            {
                BoxCollider snack3Collider = snack3.AddComponent<BoxCollider>();
                snack3Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                MakeItemPlaceable(TechType.Snack3, snack3, snack3Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/Snack3");
#endif

            // Swap Snack2 and Snack3 techtypes (as tooltips do not match models)
            if (snack2 != null && snack3 != null)
            {
                var snack2PrefabId = snack2.GetComponent<PrefabIdentifier>();
                var snack3PrefabId = snack3.GetComponent<PrefabIdentifier>();
                var snack2TechTag = snack2.GetComponent<TechTag>();
                var snack3TechTag = snack3.GetComponent<TechTag>();
                string tmpclassid = snack2PrefabId.ClassId;
                snack2PrefabId.ClassId = snack3PrefabId.ClassId;
                snack3PrefabId.ClassId = tmpclassid;
                string tmpname = snack2PrefabId.name;
                snack2PrefabId.name = snack3PrefabId.name;
                snack3PrefabId.name = tmpname;
                TechType tmpTechType = snack2TechTag.type;
                snack2TechTag.type = snack3TechTag.type;
                snack3TechTag.type = tmpTechType;
            }
        }

        private static bool _batteriesMadePlaceable = false;
        public static void MakeBatteriesPlaceable()
        {
            if (!_batteriesMadePlaceable)
            {
                GameObject powercell = Resources.Load<GameObject>("WorldEntities/Tools/PowerCell");
                if (powercell != null)
                {
                    powercell.AddComponent<CustomPlaceToolController>();
                    powercell.AddComponent<PowerCell_PT>();
                    MakeItemPlaceable(TechType.PowerCell, powercell);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PowerCell");
#endif
                GameObject battery = Resources.Load<GameObject>("WorldEntities/Tools/Battery");
                if (battery != null)
                {
                    battery.AddComponent<CustomPlaceToolController>();
                    battery.AddComponent<Battery_PT>();
                    MakeItemPlaceable(TechType.Battery, battery);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PowerCell");
#endif
                GameObject ionpowercell = Resources.Load<GameObject>("WorldEntities/Tools/PrecursorIonPowerCell");
                if (ionpowercell != null)
                {
                    ionpowercell.AddComponent<CustomPlaceToolController>();
                    ionpowercell.AddComponent<IonPowerCell_PT>();
                    MakeItemPlaceable(TechType.PrecursorIonPowerCell, ionpowercell);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PrecursorIonPowerCell");
#endif
                GameObject ionbattery = Resources.Load<GameObject>("WorldEntities/Tools/PrecursorIonBattery");
                if (ionbattery != null)
                {
                    ionbattery.AddComponent<CustomPlaceToolController>();
                    ionbattery.AddComponent<IonBattery_PT>();
                    MakeItemPlaceable(TechType.PrecursorIonBattery, ionbattery);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PrecursorIonBattery");
#endif

                _batteriesMadePlaceable = true;
            }
        }

        private static bool _madeItemsPlaceable = false;
        public static void MakeItemsPlaceable()
        {
            if (!_madeItemsPlaceable)
            {
                Logger.Log("Making some existing items positionable/pickupable...");

                // Chimicals
                GameObject bleach = Resources.Load<GameObject>("WorldEntities/Natural/bleach");
                if (bleach != null)
                {
                    bleach.AddComponent<CustomPlaceToolController>();
                    bleach.AddComponent<Bleach_PT>();
                    MakeItemPlaceable(TechType.Bleach, bleach);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/bleach");
#endif
                GameObject lubricant = Resources.Load<GameObject>("WorldEntities/Natural/lubricant");
                if (lubricant != null)
                {
                    lubricant.AddComponent<CustomPlaceToolController>();
                    lubricant.AddComponent<Lubricant_PT>();
                    MakeItemPlaceable(TechType.Lubricant, lubricant);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/lubricant");
#endif
                GameObject polyaniline = Resources.Load<GameObject>("WorldEntities/Natural/polyaniline");
                if (polyaniline != null)
                    MakeItemPlaceable(TechType.Polyaniline, polyaniline);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/polyaniline");
#endif
                GameObject benzene = Resources.Load<GameObject>("WorldEntities/Natural/benzene");
                if (benzene != null)
                    MakeItemPlaceable(TechType.Benzene, benzene);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/benzene");
#endif
                GameObject hydrochloricacid = Resources.Load<GameObject>("WorldEntities/Natural/hydrochloricacid");
                if (hydrochloricacid != null)
                    MakeItemPlaceable(TechType.HydrochloricAcid, hydrochloricacid);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/hydrochloricacid");
#endif
                GameObject hatchingenzymes = Resources.Load<GameObject>("WorldEntities/Natural/HatchingEnzymes");
                if (hatchingenzymes != null)
                    MakeItemPlaceable(TechType.HatchingEnzymes, hatchingenzymes);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/HatchingEnzymes");
#endif

                // Food & water
                GameObject coffee = Resources.Load<GameObject>("WorldEntities/Food/Coffee");
                if (coffee != null)
                    MakeItemPlaceable(TechType.Coffee, coffee);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/Coffee");
#endif
                GameObject bigfilteredwater = Resources.Load<GameObject>("WorldEntities/Food/BigFilteredWater");
                if (bigfilteredwater != null)
                    MakeItemPlaceable(TechType.BigFilteredWater, bigfilteredwater);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/BigFilteredWater");
#endif
                GameObject disinfectedwater = Resources.Load<GameObject>("WorldEntities/Food/DisinfectedWater");
                if (disinfectedwater != null)
                {
                    disinfectedwater.AddComponent<CustomPlaceToolController>();
                    disinfectedwater.AddComponent<DisinfectedWater_PT>();
                    MakeItemPlaceable(TechType.DisinfectedWater, disinfectedwater);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/DisinfectedWater");
#endif
                GameObject filteredwater = Resources.Load<GameObject>("WorldEntities/Food/FilteredWater");
                if (filteredwater != null)
                {
                    filteredwater.AddComponent<CustomPlaceToolController>();
                    filteredwater.AddComponent<FilteredWater_PT>();
                    MakeItemPlaceable(TechType.FilteredWater, filteredwater);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/FilteredWater");
#endif

                // Snacks
                MakeSnacksPlaceable();

                // Electronics
                GameObject wiringkit = Resources.Load<GameObject>("WorldEntities/Natural/wiringkit");
                if (wiringkit != null)
                {
                    wiringkit.AddComponent<CustomPlaceToolController>();
                    wiringkit.AddComponent<WiringKit_PT>();
                    MakeItemPlaceable(TechType.WiringKit, wiringkit);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/wiringkit");
#endif
                GameObject advancedwiringkit = Resources.Load<GameObject>("WorldEntities/Natural/advancedwiringkit");
                if (advancedwiringkit != null)
                {
                    advancedwiringkit.AddComponent<CustomPlaceToolController>();
                    advancedwiringkit.AddComponent<AdvancedWiringKit_PT>();
                    MakeItemPlaceable(TechType.AdvancedWiringKit, advancedwiringkit);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/advancedwiringkit");
#endif
                GameObject computerchip = Resources.Load<GameObject>("WorldEntities/Natural/computerchip");
                if (computerchip != null)
                {
                    computerchip.AddComponent<CustomPlaceToolController>();
                    computerchip.AddComponent<ComputerChip_PT>();
                    MakeItemPlaceable(TechType.ComputerChip, computerchip);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/computerchip");
#endif

                if (ConfigSwitcher.EnablePlaceBatteries)
                    MakeBatteriesPlaceable();

                // Precursor
                GameObject ionCrystal = Resources.Load<GameObject>("WorldEntities/Natural/PrecursorIonCrystal");
                if (ionCrystal != null)
                    MakeItemPlaceable(TechType.PrecursorIonCrystal, ionCrystal);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/PrecursorIonCrystal");
#endif

                // Others
                GameObject stalkertooth = Resources.Load<GameObject>("WorldEntities/Natural/stalkertooth");
                if (stalkertooth != null)
                {
                    stalkertooth.AddComponent<CustomPlaceToolController>();
                    stalkertooth.AddComponent<StalkerTooth_PT>();
                    MakeItemPlaceable(TechType.StalkerTooth, stalkertooth);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/stalkertooth");
#endif
                GameObject firstaidkit = Resources.Load<GameObject>("WorldEntities/Natural/firstaidkit");
                if (firstaidkit != null)
                    MakeItemPlaceable(TechType.FirstAidKit, firstaidkit);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/firstaidkit");
#endif

                _madeItemsPlaceable = true;
            }
        }
    }
}
