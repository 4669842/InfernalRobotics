﻿using System;
using System.Collections.Generic;
using System.Linq;
using InfernalRobotics.Module;
using UnityEngine;

namespace InfernalRobotics.Gui
{
    //using UnityEngine;

    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class IRAddonEditorFilter : MonoBehaviour
    {
        private static List<AvailablePart> avPartItems = new List<AvailablePart>();
        internal string category = "Filter by Function";
        internal string subCategoryTitle = "IR Items";
        internal string defaultTitle = "IR";
        internal string iconName = "R&D_node_icon_evatech";
        internal bool filter = true;

        void Awake()
        {
            GameEvents.onGUIEditorToolbarReady.Add(SubCategories);

            avPartItems.Clear();
            foreach (AvailablePart avPart in PartLoader.LoadedPartsList)
            {
                if (avPart.name == "kerbalEVA" || avPart.name == "kerbalEVA_RD" || !avPart.partPrefab) continue;
                MuMechToggle moduleItem = avPart.partPrefab.GetComponent<MuMechToggle>();
                if (moduleItem)
                {
                    avPartItems.Add(avPart);
                }
            }

        }

        private bool EditorItemsFilter(AvailablePart avPart)
        {
            if (avPartItems.Contains(avPart))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SubCategories()
        {

            PartCategorizer.Icon icon = PartCategorizer.Instance.GetIcon(iconName);
            PartCategorizer.Category Filter = PartCategorizer.Instance.filters.Find(f => f.button.categoryName == category);
            PartCategorizer.AddCustomSubcategoryFilter(Filter, subCategoryTitle, icon, p => EditorItemsFilter(p));

            RUIToggleButtonTyped button = Filter.button.activeButton;
            button.SetFalse(button, RUIToggleButtonTyped.ClickType.FORCED);
            button.SetTrue(button, RUIToggleButtonTyped.ClickType.FORCED);
        }
    }
}
