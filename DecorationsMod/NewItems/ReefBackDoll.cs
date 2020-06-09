﻿using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class ReefBackDoll : DecorationItem
    {
        public ReefBackDoll() // Feeds abstract class
        {
            this.ClassID = "ReefBackDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("reefbackleviathan");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("ReefBackDollName"),
                                                        LanguageHelper.GetFriendlyWord("ReefBackDollDescription"),
                                                        true);

            CrafterLogicFixer.ReefBackDoll = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[3]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.FiberMesh, 1),
                        new Ingredient(TechType.Silicone, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[3]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.FiberMesh, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Silicone, 1)
                    }),
            };
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Scale model
                GameObject reefbackModel = this.GameObject.FindChild("Pivot");
                reefbackModel.transform.localScale *= 0.5f;

                // Set tech tag
                var techTag = this.GameObject.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                this.GameObject.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;
                
                // Add collider
                var collider = this.GameObject.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.8f, 0.5f, 0.5f);

                // Add large world entity
                this.GameObject.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal1 = AssetsHelper.Assets.LoadAsset<Texture>("coral_reef_grass_04_normal");
                Texture normal2 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_red_seaweed_03_normal");
                Texture normal3 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_coral_flat_normal");
                Texture normal4 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_reef_tile_normal");
                Texture normal5 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_01_01_normal");
                Texture spec5 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_01_01");
                Texture illum5 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_01_01_illum");
                Texture normal6 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_01_02_normal");
                Texture spec6 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_01_02_spec");
                Texture illum6 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_01_02_illum");
                var renderers = this.GameObject.GetComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.materials.Length > 0)
                        {
                            foreach (Material tmpMat in rend.materials)
                            {
                                tmpMat.shader = marmosetUber;
                                if (string.Compare(tmpMat.name, "coral_reef_grass_04 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal1);
                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                                else if (string.Compare(tmpMat.name, "Coral_reef_red_seaweed_03 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal2);
                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                                else if (string.Compare(tmpMat.name, "Reefback_coral_flat (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal3);
                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                                else if (string.Compare(tmpMat.name, "Reefback_reef_tile (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal4);
                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                                else if (string.Compare(tmpMat.name, "Reefback_01_01 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal5);
                                    tmpMat.SetTexture("_SpecTex", spec5);
                                    tmpMat.SetTexture("_Illum", illum5);
                                    tmpMat.SetFloat("_EmissionLM", 0.8f); // Set always visible

                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                    tmpMat.EnableKeyword("MARMO_SPECMAP");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                                else if (string.Compare(tmpMat.name, "Reefback_01_02 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal6);
                                    tmpMat.SetTexture("_SpecTex", spec6);
                                    tmpMat.SetTexture("_Illum", illum6);
                                    tmpMat.SetFloat("_EmissionLM", 0.8f); // Set always visible

                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                    tmpMat.EnableKeyword("MARMO_SPECMAP");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                            }
                        }
                    }
                }

                // Add sky applier
                var applier = this.GameObject.AddComponent<SkyApplier>();
                applier.renderers = renderers;
                applier.anchorSky = Skies.Auto;
                
                // We can pick this item
                var pickupable = this.GameObject.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                this.GameObject.AddComponent<CustomPlaceToolController>();
                var placeTool = this.GameObject.AddComponent<GenericPlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = false;
                placeTool.allowedOnCeiling = false;
                placeTool.allowedOnConstructable = true;
                placeTool.allowedOnGround = true;
                placeTool.allowedOnRigidBody = true;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = true;
                placeTool.rotationEnabled = true;
                placeTool.enabled = true;
                placeTool.hasAnimations = false;
                placeTool.hasBashAnimation = false;
                placeTool.hasFirstUseAnimation = false;
                placeTool.mainCollider = collider;
                placeTool.pickupable = pickupable;
                placeTool.drawTime = 0.5f;
                placeTool.dropTime = 1;
                placeTool.holsterTime = 0.35f;

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                SMLHelper.V2.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to Hand Equipment type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("reefbackicon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.6f;
            fabricatingA.posOffset = new Vector3(0.08f, 0f, 0.04f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 1f;

            return prefab;
        }
    }
}
