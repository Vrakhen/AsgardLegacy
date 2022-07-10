using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using System.Collections;
using UnityEngine;

namespace AsgardLegacy
{
    class RunesController
    {
        public static void AddRuneTableRecipes()
        {
            var rune_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone");
            var icerune1_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_ice_1");
            var icerune2_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_ice_2");
            var icerune3_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_ice_3");
            var icerune4_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_ice_4");
            var icerune5_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_ice_5");
            var firerune1_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_fire_1");
            var firerune2_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_fire_2");
            var firerune3_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_fire_3");
            var firerune4_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_fire_4");
            var firerune5_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_fire_5");
            var poisonrune1_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_poison_1");
            var poisonrune2_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_poison_2");
            var poisonrune3_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_poison_3");
            var poisonrune4_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_poison_4");
            var poisonrune5_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_poison_5");
            var lightningrune1_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_lightning_1");
            var lightningrune2_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_lightning_2");
            var lightningrune3_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_lightning_3");
            var lightningrune4_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_lightning_4");
            var lightningrune5_prefab = AsgardLegacy.runeTableAssets.LoadAsset<GameObject>("AL_rune_stone_lightning_5");

            var rune = new CustomItem(rune_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[0] * 100 + "% physical damage dealt",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "Stone", Amount = 1 },
                    new RequirementConfig { Item = "Flint", Amount = 1 },
                    new RequirementConfig { Item = "GreydwarfEye", Amount = 3 },
                }
            });

            var icerune1 = new CustomItem(icerune1_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[0] * 100 + "% damage dealt as frost damage" +
                "\n-" + Utility.RuneResistanceBonus[0] * 100 + "% frost damage taken" +
                //"\n-" + Utility.RuneDamageMalus[0] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[0] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone", Amount = 1 },
                    new RequirementConfig { Item = "FreezeGland", Amount = 1 },
                }
            });
            var icerune2 = new CustomItem(icerune2_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 2,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[1] * 100 + "% damage dealt as frost damage" +
                "\n-" + Utility.RuneResistanceBonus[1] * 100 + "% frost damage taken" +
                //"\n-" + Utility.RuneDamageMalus[1] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[1] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_ice_1", Amount = 1 },
                    new RequirementConfig { Item = "FreezeGland", Amount = 1 },
                }
            });
            var icerune3 = new CustomItem(icerune3_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 3,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[2] * 100 + "% damage dealt as frost damage" +
                "\n-" + Utility.RuneResistanceBonus[2] * 100 + "% frost damage taken" +
                //"\n-" + Utility.RuneDamageMalus[2] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[2] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_ice_2", Amount = 1 },
                    new RequirementConfig { Item = "FreezeGland", Amount = 1 },
                }
            });
            var icerune4 = new CustomItem(icerune4_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 4,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[3] * 100 + "% damage dealt as frost damage" +
                "\n-" + Utility.RuneResistanceBonus[3] * 100 + "% frost damage taken" +
                //"\n-" + Utility.RuneDamageMalus[3] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[3] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_ice_3", Amount = 1 },
                    new RequirementConfig { Item = "FreezeGland", Amount = 1 },
                }
            });
            var icerune5 = new CustomItem(icerune5_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 5,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[4] * 100 + "% damage dealt as frost damage" +
                "\n-" + Utility.RuneResistanceBonus[4] * 100 + "% frost damage taken" +
                //"\n-" + Utility.RuneDamageMalus[4] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[4] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_ice_4", Amount = 1 },
                    new RequirementConfig { Item = "FreezeGland", Amount = 1 },
                    new RequirementConfig { Item = "YmirRemains", Amount = 1 },
                }
            });

            var firerune1 = new CustomItem(firerune1_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[0] * 100 + "% damage dealt as fire damage" +
                "\n-" + Utility.RuneResistanceBonus[0] * 100 + "% fire damage taken" +
                //"\n-" + Utility.RuneDamageMalus[0] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[0] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone", Amount = 1 },
                    new RequirementConfig { Item = "Resin", Amount = 5 },
                }
            });
            var firerune2 = new CustomItem(firerune2_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 2,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[1] * 100 + "% damage dealt as fire damage" +
                "\n-" + Utility.RuneResistanceBonus[1] * 100 + "% fire damage taken" +
                //"\n-" + Utility.RuneDamageMalus[1] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[1] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_fire_1", Amount = 1 },
                    new RequirementConfig { Item = "Resin", Amount = 5 },
                }
            });
            var firerune3 = new CustomItem(firerune3_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 3,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[2] * 100 + "% damage dealt as fire damage" +
                "\n-" + Utility.RuneResistanceBonus[2] * 100 + "% fire damage taken" +
                //"\n-" + Utility.RuneDamageMalus[2] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[2] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_fire_2", Amount = 1 },
                    new RequirementConfig { Item = "Resin", Amount = 5 },
                }
            });
            var firerune4 = new CustomItem(firerune4_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 4,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[3] * 100 + "% damage dealt as fire damage" +
                "\n-" + Utility.RuneResistanceBonus[3] * 100 + "% fire damage taken" +
                //"\n-" + Utility.RuneDamageMalus[3] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[3] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_fire_3", Amount = 1 },
                    new RequirementConfig { Item = "Resin", Amount = 5 },
                }
            });
            var firerune5 = new CustomItem(firerune5_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 5,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[4] * 100 + "% damage dealt as fire damage" +
                "\n-" + Utility.RuneResistanceBonus[4] * 100 + "% fire damage taken" +
                //"\n-" + Utility.RuneDamageMalus[4] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[4] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_fire_4", Amount = 1 },
                    new RequirementConfig { Item = "Resin", Amount = 5 },
                    new RequirementConfig { Item = "SurtlingCore", Amount = 2 },
                }
            });

            var lightningrune1 = new CustomItem(lightningrune1_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[0] * 100 + "% damage dealt as lightning damage" +
                "\n-" + Utility.RuneResistanceBonus[0] * 100 + "% lightning damage taken" +
                //"\n-" + Utility.RuneDamageMalus[0] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[0] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone", Amount = 1 },
                    new RequirementConfig { Item = "Crystal", Amount = 1 },
                }
            });
            var lightningrune2 = new CustomItem(lightningrune2_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 2,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[1] * 100 + "% damage dealt as lightning damage" +
                "\n-" + Utility.RuneResistanceBonus[1] * 100 + "% lightning damage taken" +
                //"\n-" + Utility.RuneDamageMalus[1] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[1] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_lightning_1", Amount = 1 },
                    new RequirementConfig { Item = "Crystal", Amount = 1 },
                }
            });
            var lightningrune3 = new CustomItem(lightningrune3_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 3,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[2] * 100 + "% damage dealt as lightning damage" +
                "\n-" + Utility.RuneResistanceBonus[2] * 100 + "% lightning damage taken" +
                //"\n-" + Utility.RuneDamageMalus[2] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[2] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_lightning_2", Amount = 1 },
                    new RequirementConfig { Item = "Crystal", Amount = 1 },
                }
            });
            var lightningrune4 = new CustomItem(lightningrune4_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 4,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[3] * 100 + "% damage dealt as lightning damage" +
                "\n-" + Utility.RuneResistanceBonus[3] * 100 + "% lightning damage taken" +
                //"\n-" + Utility.RuneDamageMalus[3] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[3] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_lightning_3", Amount = 1 },
                    new RequirementConfig { Item = "Crystal", Amount = 1 },
                }
            });
            var lightningrune5 = new CustomItem(lightningrune5_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 5,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[4] * 100 + "% damage dealt as lightning damage" +
                "\n-" + Utility.RuneResistanceBonus[4] * 100 + "% lightning damage taken" +
                //"\n-" + Utility.RuneDamageMalus[4] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[4] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_lightning_4", Amount = 1 },
                    new RequirementConfig { Item = "Crystal", Amount = 1 },
                    new RequirementConfig { Item = "Thunderstone", Amount = 1 },
                }
            });

            var poisonrune1 = new CustomItem(poisonrune1_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[0] * 100 + "% damage dealt as poison damage" +
                "\n-" + Utility.RuneResistanceBonus[0] * 100 + "% poison damage taken" +
                //"\n-" + Utility.RuneDamageMalus[0] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[0] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone", Amount = 1 },
                    new RequirementConfig { Item = "Ooze", Amount = 1 },
                }
            });
            var poisonrune2 = new CustomItem(poisonrune2_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 2,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[1] * 100 + "% damage dealt as poison damage" +
                "\n-" + Utility.RuneResistanceBonus[1] * 100 + "% poison damage taken" +
                //"\n-" + Utility.RuneDamageMalus[1] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[1] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_poison_1", Amount = 1 },
                    new RequirementConfig { Item = "Ooze", Amount = 1 },
                }
            });
            var poisonrune3 = new CustomItem(poisonrune3_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 3,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[2] * 100 + "% damage dealt as poison damage" +
                "\n-" + Utility.RuneResistanceBonus[2] * 100 + "% poison damage taken" +
                //"\n-" + Utility.RuneDamageMalus[2] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[2] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_poison_2", Amount = 1 },
                    new RequirementConfig { Item = "Ooze", Amount = 1 },
                }
            });
            var poisonrune4 = new CustomItem(poisonrune4_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 4,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[3] * 100 + "% damage dealt as poison damage" +
                "\n-" + Utility.RuneResistanceBonus[3] * 100 + "% poison damage taken" +
                //"\n-" + Utility.RuneDamageMalus[3] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[3] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_poison_3", Amount = 1 },
                    new RequirementConfig { Item = "Ooze", Amount = 1 },
                }
            });
            var poisonrune5 = new CustomItem(poisonrune5_prefab, fixReference: false, new ItemConfig
            {
                Amount = 1,
                MinStationLevel = 5,
                CraftingStation = "AL_piece_runetable",
                Description = "+" + Utility.RuneDamageBonus[4] * 100 + "% damage dealt as poison damage" +
                "\n-" + Utility.RuneResistanceBonus[4] * 100 + "% poison damage taken" +
                //"\n-" + Utility.RuneDamageMalus[4] * 100 + "% other elemental damage dealt" +
                "\n+" + Utility.RuneResistanceMalus[4] * 100 + "% other elemental damage taken",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AL_rune_stone_poison_4", Amount = 1 },
                    new RequirementConfig { Item = "Ooze", Amount = 1 },
                    new RequirementConfig { Item = "Guck", Amount = 2 },
                }
            });

            ItemManager.Instance.AddItem(rune);
            ItemManager.Instance.AddItem(icerune1);
            ItemManager.Instance.AddItem(icerune2);
            ItemManager.Instance.AddItem(icerune3);
            ItemManager.Instance.AddItem(icerune4);
            ItemManager.Instance.AddItem(icerune5);
            ItemManager.Instance.AddItem(firerune1);
            ItemManager.Instance.AddItem(firerune2);
            ItemManager.Instance.AddItem(firerune3);
            ItemManager.Instance.AddItem(firerune4);
            ItemManager.Instance.AddItem(firerune5);
            ItemManager.Instance.AddItem(lightningrune1);
            ItemManager.Instance.AddItem(lightningrune2);
            ItemManager.Instance.AddItem(lightningrune3);
            ItemManager.Instance.AddItem(lightningrune4);
            ItemManager.Instance.AddItem(lightningrune5);
            ItemManager.Instance.AddItem(poisonrune1);
            ItemManager.Instance.AddItem(poisonrune2);
            ItemManager.Instance.AddItem(poisonrune3);
            ItemManager.Instance.AddItem(poisonrune4);
            ItemManager.Instance.AddItem(poisonrune5);
        }

        public static void ApplyRuneEffect(Player player, string runeName)
        {
            var seMan = player.GetSEMan();

            var statusToRemove = "";
            foreach(var status in seMan.GetStatusEffects())
            {
                if (!status.name.ToLower().Contains("alrune"))
                    continue;

                statusToRemove = status.name;
            }
            seMan.RemoveStatusEffect(statusToRemove, true);

            var st = runeName.Split(' ');

            var level = st.Length < 3 ? -1 : int.Parse(st[2]) - 1;
            var runeType = st[0];

            var seRune = (SE_ALRune) ScriptableObject.CreateInstance(typeof(SE_ALRune));
            seRune.RuneDamageBonus = level == -1 ? Utility.RuneDamageBonus[0] : Utility.RuneDamageBonus[level];
            seRune.RuneResistanceBonus = level == -1 ? 0f : Utility.RuneResistanceBonus[level];
            seRune.RuneDamageMalus = level == -1 ? 0f : Utility.RuneDamageMalus[level];
            seRune.RuneResistanceMalus = level == -1 ? 0f : Utility.RuneResistanceMalus[level];

            switch (runeType)
            {
                case "simple":
                    seRune.m_name = "Simple Rune";
                    seRune.RuneType = HitData.DamageType.Physical;
                    seRune.m_icon = AsgardLegacy.runeTableAssets.LoadAsset<Sprite>("AL_rune_stone_icon");

                    break;
                case "fire":
                    seRune.m_name = "Fire Rune";
                    seRune.RuneType = HitData.DamageType.Fire;
                    seRune.m_icon = AsgardLegacy.runeTableAssets.LoadAsset<Sprite>("AL_rune_stone_fire_icon");

                    break;
                case "ice":
                    seRune.m_name = "Ice Rune";
                    seRune.RuneType = HitData.DamageType.Frost;
                    seRune.m_icon = AsgardLegacy.runeTableAssets.LoadAsset<Sprite>("AL_rune_stone_ice_icon");

                    break;
                case "lightning":
                    seRune.m_name = "Lightning Rune";
                    seRune.RuneType = HitData.DamageType.Lightning;
                    seRune.m_icon = AsgardLegacy.runeTableAssets.LoadAsset<Sprite>("AL_rune_stone_lightning_icon");

                    break;
                case "poison":
                    seRune.m_name = "Poison Rune";
                    seRune.RuneType = HitData.DamageType.Poison;
                    seRune.m_icon = AsgardLegacy.runeTableAssets.LoadAsset<Sprite>("AL_rune_stone_poison_icon");

                    break;
            }

            seMan.AddStatusEffect(seRune);
        }
    }
}
