using LinkEngine.Entities;
using System.Collections.Generic;

namespace LinkEngine.Gameplay.Quests
{
    public class Quest
    {
        int id;
        string name;
        string desc;
        int rewardExp;
        int rewardGold;

        public Quest(int _id, string _name, string _desc, int _rewardExp, int _rewardGold)
        {
            id = _id;
            name = _name;
            desc = _desc;
            rewardExp = _rewardExp;
            rewardGold = _rewardGold;

        }
        public Quest(Quest quest)
        {
            id = quest.ID;
            name = quest.Name;
            desc = quest.Description;
            rewardExp = quest.RewardExperiencePoints;
            rewardGold = quest.RewardGold;
            QuestCompletionItems = quest.QuestCompletionItems;
            RewardItem = quest.RewardItem;
        }

        public int ID { get {return id; } set {id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return desc; } set { desc = value; } }
        public int RewardExperiencePoints { get { return rewardExp; } set { rewardExp = value; } }
        public int RewardGold { get { return rewardGold; } set { rewardGold = value; } }

        public Items.Item RewardItem { get; set; }
        public List<QuestCompletionItem> QuestCompletionItems { get; set; }
    }
    public class PlayerQuest
    {
        public Quest Details { get; set; }
        public bool IsCompleted { get; set; }

        public PlayerQuest(Quest details)
        {
            Details = details;
            IsCompleted = false;
        }
        public PlayerQuest(Quest details, string isCompleted)
        {
            Details = details;
            IsCompleted = bool.Parse(isCompleted);
        }
    }
    public class QuestCompletionItem
    {
        public Items.Item Details { get; set; }
        public int Quantity { get; set; }

        public QuestCompletionItem(Items.Item details, int quantity)
        {
            Details = details;
            Quantity = quantity;
        }
    }
}
