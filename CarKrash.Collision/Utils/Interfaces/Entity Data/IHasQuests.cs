using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CarKrash.Collision.Unity2D.ScriptableObjects;

namespace CarKrash.Collision.Utils
{
    public interface IHasQuests
    {
        Signal PlayerQuestAddedSignal { get; }
        Signal QuestRemovedSignal { get; }
        List<Quest> QuestsAvailable { get; }

        void TakeQuest(Quest quest);
    }
}