using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;

namespace AsgardLegacy
{
    class RuneTable
    {
        public static void AddRuneTablePiece()
        {
            var runeTable = new CustomPiece(AsgardLegacy.runeTableAssets, "AL_piece_runetable", false, new PieceConfig
            {
                PieceTable = "Hammer",
                Category = "Crafting",
                CraftingStation = "piece_workbench",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "Wood", Amount = 10, Recover = true },
                    new RequirementConfig { Item = "GreydwarfEye", Amount = 5, Recover = true },
                    new RequirementConfig { Item = "TrophyEikthyr", Amount = 1, Recover = true }
                }
            });

            var runeTableExt1 = new CustomPiece(AsgardLegacy.runeTableAssets, "AL_piece_runetable_ext1", false, new PieceConfig
            {
                PieceTable = "Hammer",
                Category = "Crafting",
                CraftingStation = "piece_workbench",
                ExtendStation = "AL_piece_runetable",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "AxeBronze", Amount = 1, Recover = true },
                    new RequirementConfig { Item = "MushroomYellow", Amount = 2, Recover = true },
                    new RequirementConfig { Item = "TrophyTheElder", Amount = 1, Recover = true }
                }
            });

            var runeTableExt2 = new CustomPiece(AsgardLegacy.runeTableAssets, "AL_piece_runetable_ext2", false, new PieceConfig
            {
                PieceTable = "Hammer",
                Category = "Crafting",
                CraftingStation = "piece_workbench",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "Iron", Amount = 10, Recover = true },
                    new RequirementConfig { Item = "Guck", Amount = 2, Recover = true },
                    new RequirementConfig { Item = "Ruby", Amount = 3, Recover = true },
                    new RequirementConfig { Item = "TrophyBonemass", Amount = 1, Recover = true }
                }
            });

            var runeTableExt3 = new CustomPiece(AsgardLegacy.runeTableAssets, "AL_piece_runetable_ext3", false, new PieceConfig
            {
                PieceTable = "Hammer",
                Category = "Crafting",
                CraftingStation = "piece_workbench",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "Silver", Amount = 10, Recover = true },
                    new RequirementConfig { Item = "TrophyDragonQueen", Amount = 1, Recover = true }
                }
            });

            var runeTableExt4 = new CustomPiece(AsgardLegacy.runeTableAssets, "AL_piece_runetable_ext4", false, new PieceConfig
            {
                PieceTable = "Hammer",
                Category = "Crafting",
                CraftingStation = "piece_workbench",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "BlackMetal", Amount = 10, Recover = true },
                    new RequirementConfig { Item = "TrophyGoblinKing", Amount = 1, Recover = true }
                }
            });

            if (runeTable != null)
                PieceManager.Instance.AddPiece(runeTable);
            if (runeTableExt1 != null)
                PieceManager.Instance.AddPiece(runeTableExt1);
            if (runeTableExt2 != null)
                PieceManager.Instance.AddPiece(runeTableExt2);
            if (runeTableExt3 != null)
                PieceManager.Instance.AddPiece(runeTableExt3);
            if (runeTableExt4 != null)
                PieceManager.Instance.AddPiece(runeTableExt4);
        }
    }
}
