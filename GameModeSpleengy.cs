using BingoAdvancedCustomGeneration.AdvancedGameModes;
using BingoSync.CustomGoals;
using BingoSync.Interfaces;
using MonoMod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BingoGoalPackSpleengy
{
    internal class GameModeSpleengy : AdvancedGameMode
    {
        public override string DisplayName => "spleengy";
        public override bool CanBeRenamed => false;

        public override string SetName(string _)
        {
            return DisplayName;
        }

        private readonly Dictionary<string, AdvancedGoal> _goals = [];

        public GameModeSpleengy() : base()
        {
            AddExistingGoals();
            SetupExclusions();
        }

        private void AddExistingGoals()
        {
            _goals.AddRange(GoalGroups["spleengy"]);
            Dictionary<string, BingoGoal> vanillaGoals = Goals.GetVanillaGoals();
            Dictionary<string, BingoGoal> extendedGoals = Goals.GetGoalsByGroupName("Extended");
            Dictionary<string, BingoGoal> extendedPlusGoals = Goals.GetGoalsByGroupName("Extended+");
            Dictionary<string, BingoGoal> relicGoals = Goals.GetGoalsByGroupName("Relics");
            string[] addGoals = [
                "Charged Lumafly Journal Entry",
                "Defeat 3 dream warriors",
                "Dream Nail Willoh's meal",
                "Kill a Lightseed",
                "Kill two different Alubas",
                "Nothing? (junk pit chest)",
                "Open the Dirtmouth / Crystal Peak elevator"
            ];
            _goals.AddRange(GoalGroups["Vanilla"]);
            _goals.Remove("Kill Myla");
            _goals.Remove("Have 4 Rancid Eggs");
            _goals.Remove("Unlock Queen's Stag + King's Stag Stations");
            _goals.AddRange(GoalGroups["Extended"]);
            _goals.Remove("Slash Millibelle in Pleasure House");
            _goals.Remove("Open 6 geo chests (not in junk pit)");
            _goals.Remove("Decipher Hunter's Notes: Maskfly + Shrumeling");
            _goals.Remove("Collect 4 Simple Keys");
            foreach (string goal in addGoals)
            {
                _goals.Add(goal, ToAdvancedGoal(extendedPlusGoals[goal]));
            }
            _goals.Add("Seal: Deepnest near Mantis Lords", ToAdvancedGoal(relicGoals["Seal: Deepnest near Mantis Lords"]));
            _goals.Add("Seal: Queen's Station", ToAdvancedGoal(relicGoals["Seal: Queen's Station"]));
            _goals.Add("Idol: Deepnest Zote", ToAdvancedGoal(relicGoals["Idol: Deepnest Zote"]));
            _goals.Add("Idol: Dung Defender", ToAdvancedGoal(relicGoals["Idol: Dung Defender"]));
        }

        private static AdvancedGoal ToAdvancedGoal(BingoGoal goal)
        {
            return new AdvancedGoal()
            {
                Name = goal.Name,
                FullExclusions = [.. goal.Exclusions.Select(s => string.Copy(s))],
            };
        }

        private void SetupExclusions()
        {
            const bool lineExclusion = true;
            const bool fullExclusion = false;

            Exclude(_goals, "Shade Soul", "Kill 2 Soul Warriors", fullExclusion);
            Exclude(_goals, "Read Bretta's diary", "Sprintmaster + Dashmaster", lineExclusion);
            Exclude(_goals, "Crystal Guardian 1", "Crystal Heart", lineExclusion);

            Exclude(_goals, "Lumafly Lantern", "Crystal Heart", lineExclusion);
            Exclude(_goals, "Lumafly Lantern", "Crystal Guardian 1", lineExclusion);

            Exclude(_goals, "Descending Dark", "Desolate Dive", lineExclusion);
            Exclude(_goals, "Descending Dark", "Soul Master", lineExclusion);

            Exclude(_goals, "Slash Zote's corpse in Greenpath", "Defeat Colosseum Zote", fullExclusion);
            Exclude(_goals, "Slash Zote's corpse in Greenpath", "Rescue Zote in Deepnest", fullExclusion);
            Exclude(_goals, "Slash Zote's corpse in Greenpath", "Vengefly King + Massive Moss Charger", fullExclusion);

            Exclude(_goals, "Idol: Deepnest Zote", "Rescue Zote in Deepnest", fullExclusion);

            Exclude(_goals, "Dream Nail Willoh's meal", "Seal: Queen's Station", fullExclusion);

            Unexclude(_goals, "Save the 2 grubs in Hive", "Mask Shard  in the Hive");
            Exclude(_goals, "Save the 2 grubs in Hive", "Mask Shard  in the Hive", lineExclusion);
            Exclude(_goals, "Save the 2 grubs in Hive", "Hive Knight", lineExclusion);
            Exclude(_goals, "Save the 2 grubs in Hive", "Hiveblood", lineExclusion);
            Exclude(_goals, "Save the 2 grubs in Hive", "Complete the Hive Root", lineExclusion);

            Exclude(_goals, "Complete the Hive Root", "Mask Shard  in the Hive", lineExclusion);
            Exclude(_goals, "Complete the Hive Root", "Hive Knight", lineExclusion);
            Exclude(_goals, "Complete the Hive Root", "Hiveblood", lineExclusion);

            Unexclude(_goals, "Tram Pass + Visit all 5 Tram Stations", "Hive Knight");
            Unexclude(_goals, "Tram Pass + Visit all 5 Tram Stations", "Hiveblood");
            Unexclude(_goals, "Tram Pass + Visit all 5 Tram Stations", "Mask Shard  in the Hive");

            Exclude(_goals, "Unlock Deepnest Stag", "Talk to Midwife", fullExclusion);
            Exclude(_goals, "Unlock Deepnest Stag", "Talk to Mask Maker", lineExclusion);
            Exclude(_goals, "Unlock Deepnest Stag", "Herrah", lineExclusion);

            Exclude(_goals, "Charged Lumafly Journal Entry", "Uumuu", fullExclusion);

            Exclude(_goals, "Open the Dirtmouth / Crystal Peak elevator", "Kill 4 Mimics", fullExclusion);

            Exclude(_goals, "2 nail arts", "Cyclone Slash", fullExclusion);
            Exclude(_goals, "2 nail arts", "Dash Slash", fullExclusion);
            Exclude(_goals, "2 nail arts", "Great Slash", fullExclusion);

            Exclude(_goals, "Read both lore tablets in Soul Sanctum", "Soul Master", fullExclusion);
            Exclude(_goals, "Read both lore tablets in Soul Sanctum", "Desolate Dive", fullExclusion);

            Exclude(_goals, "King's Brand", "Hornet 2", fullExclusion);

            Exclude(_goals, "Defeat 3 dream warriors", "Xero", lineExclusion);
            Exclude(_goals, "Defeat 3 dream warriors", "Galien", lineExclusion);
            Exclude(_goals, "Defeat 3 dream warriors", "No Eyes", lineExclusion);
            Exclude(_goals, "Defeat 3 dream warriors", "Elder Hu", lineExclusion);
            Exclude(_goals, "Defeat 3 dream warriors", "Gorb", lineExclusion);
            Exclude(_goals, "Defeat 3 dream warriors", "Marmu", lineExclusion);

            Exclude(_goals, "Break the 420 geo rock in Kingdom's Edge", "Read the Kingdom's Edge lore tablet", fullExclusion);
            Exclude(_goals, "Break the 420 geo rock in Kingdom's Edge", "Save the 2 grubs in Kingdom's Edge", fullExclusion);

            Exclude(_goals, "Broken Vessel", "Unlock Hidden Stag Station", lineExclusion);
            Exclude(_goals, "Kill a Lightseed", "Broken Vessel", fullExclusion);
            Exclude(_goals, "Kill a Lightseed", "Monarch Wings", fullExclusion);

        }

        public override List<string> GenerateBoard(int seed)
        {
            Random rng = new(seed);

            List<AdvancedGoal> board = [];
            for (int i = 0; i < 25; ++i)
            {
                board.Add(null);
            }

            for (int goalNr = 0; goalNr < 25; ++goalNr)
            {
                int slot = PickRandomSlot(rng, board);
                AdvancedGoal goal = PickRandomGoal(rng, board, slot);
                board[slot] = goal;
            }

            return [.. board.Select(advancedGoal => advancedGoal.Name)];
        }

        private int PickRandomSlot(Random rng, List<AdvancedGoal> board)
        {
            List<int> availableSlots = [];
            for (int slot = 0; slot < 25; ++slot)
            {
                if (board[slot] == null)
                {
                    availableSlots.Add(slot);
                }
            }
            int slotID = rng.Next(availableSlots.Count);
            return availableSlots[slotID];
        }

        private AdvancedGoal PickRandomGoal(Random rng, List<AdvancedGoal> board, int slot)
        {
            List<AdvancedGoal> potentialGoals = [];
            double totalWeight = 0d;

            foreach (AdvancedGoal potentialGoal in _goals.Values)
            {
                if (!board.Contains(potentialGoal) && !GoalSlotIsExcluded(board, slot, potentialGoal))
                {
                    potentialGoals.Add(potentialGoal);
                    totalWeight += potentialGoal.Weight;
                }
            }

            int goalId = -1;
            double randomWeight = rng.NextDouble() * totalWeight;
            while (randomWeight > 0d)
            {
                ++goalId;
                randomWeight -= potentialGoals[goalId].Weight;
            }

            return potentialGoals[goalId];
        }

        private bool GoalSlotIsExcluded(List<AdvancedGoal> board, int slot, AdvancedGoal goal)
        {
            foreach (AdvancedGoal existingGoal in board)
            {
                if (GoalsExclude(existingGoal, goal, false))
                {
                    return true;
                }
            }

            int column = slot % 5;
            int row = slot / 5;
            int rowStart = slot - column;
            bool tlbr = column == row;
            bool trbl = column == 4 - row;
            List<int> tlbrSlots = [0, 6, 12, 18, 24];
            List<int> trblSlots = [4, 8, 12, 16, 20];
            for (int i = 0; i < 5; ++i)
            {
                if (GoalsExclude(board[column + 5 * i], goal, true))
                {
                    return true;
                }
                if (GoalsExclude(board[rowStart + i], goal, true))
                {
                    return true;
                }
                if (tlbr && GoalsExclude(board[tlbrSlots[i]], goal, true))
                {
                    return true;
                }
                if (trbl && GoalsExclude(board[trblSlots[i]], goal, true))
                {
                    return true;
                }
            }
            return false;
        }

        private bool GoalsExclude(AdvancedGoal goal1, AdvancedGoal goal2, bool line)
        {
            if (goal1 == null || goal2 == null)
            {
                return false;
            }
            if (line)
            {
                return goal1.LineExclusions.Contains(goal2.Name) ||
                    goal2.LineExclusions.Contains(goal1.Name);
            }
            return goal1.FullExclusions.Contains(goal2.Name) ||
                goal2.FullExclusions.Contains(goal1.Name);
        }

        public static void Exclude(Dictionary<string, AdvancedGoal> _goals, string goal1, string goal2, bool line = false)
        {
            if (line)
            {
                _goals[goal1].LineExclusions.Add(goal2);
                _goals[goal2].LineExclusions.Add(goal1);
            }
            else
            {
                _goals[goal1].FullExclusions.Add(goal2);
                _goals[goal2].FullExclusions.Add(goal1);
            }
        }

        public static void Unexclude(Dictionary<string, AdvancedGoal> _goals, string goal1, string goal2)
        {
            _goals[goal1].LineExclusions.Remove(goal2);
            _goals[goal2].LineExclusions.Remove(goal1);
            _goals[goal1].FullExclusions.Remove(goal2);
            _goals[goal2].FullExclusions.Remove(goal1);
        }
    }
}
