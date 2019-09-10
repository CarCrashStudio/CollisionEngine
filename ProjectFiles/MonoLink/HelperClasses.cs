﻿using MonoLink2D.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoLink2D
{
    public static class Helpers
    {
        public static Random Random = new Random();
        public static Attributes Sum(this IEnumerable<Attributes> attributes)
        {
            var finalAttributes = new Attributes();
            if (attributes != null)
                foreach (var attribute in attributes)
                    finalAttributes += attribute;
            return finalAttributes;
        }
    }
}
