﻿using Newtonsoft.Json;
namespace CarKrash.Collision.Utils
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Attributes
    {
        [JsonProperty] protected int strength;
        [JsonProperty] protected int dexterity;
        [JsonProperty] protected int constitution;
        [JsonProperty] protected int intelligence;
        [JsonProperty] protected int wisdom;
        [JsonProperty] protected int charisma;

        public Modifier mainModifiers;
        [JsonProperty] public Modifier equipmentModifiers;
        [JsonProperty] public Modifier buffModifiers;
        public Modifier totalModifiers;

        public void Start()
        {
            int strengthModifier = (strength - 10) / 2;
            int dexterityModifier = (dexterity - 10) / 2;
            int constitutionModifier = (constitution - 10) / 2;
            int intelligenceModifier = (intelligence - 10) / 2;
            int wisdomModifier = (wisdom - 10) / 2;
            int charismaModifier = (charisma - 10) / 2;

            mainModifiers = new Modifier(strengthModifier, dexterityModifier, constitutionModifier, intelligenceModifier, wisdomModifier, charismaModifier);
        }
        public void Update()
        {
            if (mainModifiers == null) { return; }

            mainModifiers.SetStrength((strength - 10) / 2);
            mainModifiers.SetDexterity((dexterity - 10) / 2);
            mainModifiers.SetConstitution((constitution - 10) / 2);
            mainModifiers.SetIntelligence((intelligence - 10) / 2);
            mainModifiers.SetWisdom((wisdom - 10) / 2);
            mainModifiers.SetCharisma((charisma - 10) / 2);

            totalModifiers = mainModifiers + equipmentModifiers + buffModifiers;
        }

        public void SetStrength(int amt)
        {
            strength = amt;
        }
        public void SetDexterity(int amt)
        {
            dexterity = amt;
        }
        public void SetConstitution(int amt)
        {
            constitution = amt;
        }
        public void SetIntelligences(int amt)
        {
            intelligence = amt;
        }
        public void SetWisdom(int amt)
        {
            wisdom = amt;
        }
        public void SetCharisma(int amt)
        {
            charisma = amt;
        }

        public void IncreaseStrength(int amt)
        {
            strength += amt;
        }
        public void IncreaseDexterity(int amt)
        {
            dexterity += amt;
        }
        public void IncreaseConstitution(int amt)
        {
            constitution += amt;
        }
        public void IncreaseIntelligence(int amt)
        {
            intelligence += amt;
        }
        public void IncreaseWisdom(int amt)
        {
            wisdom += amt;
        }
        public void IncreaseCharisma(int amt)
        {
            charisma += amt;
        }

        public void DecreaseStrength(int amt)
        {
            strength -= amt;
            if (strength < 1)
                strength = 1;
        }
        public void DecreaseDexterity(int amt)
        {
            dexterity -= amt;
            if (dexterity < 1)
                dexterity = 1;
        }
        public void DecreaseConstitution(int amt)
        {
            constitution -= amt;
            if (constitution < 1)
                constitution = 1;
        }
        public void DecreaseIntelligence(int amt)
        {
            intelligence -= amt;
            if (intelligence < 1)
                intelligence = 1;
        }
        public void DecreaseWisdom(int amt)
        {
            wisdom -= amt;
            if (wisdom < 1)
                wisdom = 1;
        }
        public void DecreaseCharisma(int amt)
        {
            charisma -= amt;
            if (charisma < 1)
                charisma = 1;
        }

        public override string ToString()
        {
            string r = "--- Attributes ---\n";
            r += $"Strength: {strength}({totalModifiers.Strength})\n";
            r += $"Dexterity: {dexterity}({totalModifiers.Dexterity})\n";
            r += $"Constitution: {constitution}({totalModifiers.Constitution})\n";
            r += $"Intelligence: {intelligence}({totalModifiers.Intelligence})\n";
            r += $"Wisdom: {wisdom}({totalModifiers.Wisdom})\n";
            r += $"Charisma: {strength}({totalModifiers.Charisma})\n";
            return r;
        }
    }
}
