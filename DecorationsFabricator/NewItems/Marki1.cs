﻿using SMLHelper;
using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsFabricator.NewItems
{
    public class Marki1 : DecorationItem
    {
        public Marki1()
        {
            this.ClassID = "MarkiDoll1"; // cb89366d-eac0-4011-8665-fafde75b215c
            this.ResourcePath = "Submarine/Build/Marki_01";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);
            
            if (ItemTypeSwitcher.MarkiDoll1_asBuildable)
                this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                            LanguageHelper.GetFriendlyWord("MarkiDollName"),
                                                            LanguageHelper.GetFriendlyWord("MarkiDollDescription"),
                                                            true);
            else
                this.TechType = TechType.Marki1;

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[2]
                    {
                        new IngredientHelper(TechType.FiberMesh, 1),
                        new IngredientHelper(TechType.Glass, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (ItemTypeSwitcher.MarkiDoll1_asBuildable)
                {
                    // Add new TechType to the buildables
                    CraftDataPatcher.customBuildables.Add(this.TechType);
                    CraftDataPatcher.AddToCustomGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);
                }
                else
                {
                    // Add the new TechType to the hand-equipments
                    CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);
                }
                
                // Set the buildable prefab
                if (ItemTypeSwitcher.MarkiDoll1_asBuildable)
                    CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));
                else
                    CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, this.ResourcePath, this.TechType, this.GetPrefab));

                // Set the custom icon
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, SpriteManager.Get(TechType.Marki1)));
                
                if (ItemTypeSwitcher.MarkiDoll1_asBuildable)
                {
                    // Associate recipe to the new TechType
                    CraftDataPatcher.customTechData[this.TechType] = this.Recipe;
                }

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;
            
            if (!ItemTypeSwitcher.MarkiDoll1_asBuildable)
            {
                // Retrieve collider
                GameObject model = prefab.FindChild("Marki_01");
                Collider collider = model.GetComponentInChildren<Collider>();
                
                // We can pick this item
                var pickupable = prefab.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                var placeTool = prefab.AddComponent<PlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = true;
                placeTool.allowedOnCeiling = false;
                placeTool.allowedOnConstructable = true;
                placeTool.allowedOnGround = true;
                placeTool.allowedOnRigidBody = true;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = false;
                placeTool.rotationEnabled = true;
                placeTool.enabled = true;
                placeTool.hasAnimations = false;
                placeTool.hasBashAnimation = false;
                placeTool.hasFirstUseAnimation = false;
                placeTool.mainCollider = collider;
                placeTool.pickupable = pickupable;

                // Add fabricating animation
                var fabricating = model.FindChild("Marki_01").AddComponent<VFXFabricating>();
                fabricating.localMinY = -0.1f;
                fabricating.localMaxY = 0.6f;
                fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
                fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
                fabricating.scaleFactor = 1f;
            }

            return prefab;
        }
    }
}