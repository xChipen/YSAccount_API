using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helpers
{
    public class StringHelper
    {
        public static bool ListIsEmpty<T>(List<T> list)
        {
            if (list == null)
            {
                return true;
            }

            return !list.Any();
        }
    }
}