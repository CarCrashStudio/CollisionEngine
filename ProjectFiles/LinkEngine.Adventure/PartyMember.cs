﻿using System;
using System.Linq;

namespace LinkEngine.Adventure
{
    /// <summary>
    /// PartyMember is a class that inherits the Adventurer class. PartyMember can be used in combat.
    /// </summary>
    public class PartyMember : Adventurer
    {
        public PartyMember(int id, string name, Class clss, int level, int exp, int maxExp) : base(id, name, clss, level, exp, maxExp, 0, name)
        {


        }
    }
}
