﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternConsole
{
    public static class Extensionscs
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items,
                                                       int maxItems)
        {
            return items.Select((item, inx) => new { item, inx })
                        .GroupBy(x => x.inx / maxItems)
                        .Select(g => g.Select(x => x.item));
        }
    }
}
