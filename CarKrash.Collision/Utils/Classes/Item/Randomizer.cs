using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/* E.g. Potions randomizer
 * keys for the rarity
 * sprite values for that rarity
 * 
 */
namespace CarKrash.Collision.Utils
{

    [Serializable]
    public class Randomizer
    {
        public List<Sprite> usedSprites = new List<Sprite>();
        public List<int> usedIndex = new List<int>();

        public string[] keys;
        public Sprite[] values;

        public void Clear()
        {
            usedIndex.Clear();
            usedSprites.Clear();
        }

        public Sprite FindByKey(string key)
        {
            List<int> indicies = new List<int>();
            List<Sprite> temp = new List<Sprite>();


            Sprite[] tempSprites = values.Except(usedSprites).ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == key && !usedIndex.Contains(i))
                {
                    indicies.Add(i);
                    usedIndex.Add(i);
                }
            }



            for (int i = 0; i < indicies.Count; i++)
            {
                temp.Add(tempSprites[indicies[i]]);
            }

            int rand = UnityEngine.Random.Range(0, temp.Count);

            usedSprites.Add(temp[rand]);
            return temp[rand];
        }
    }
}
