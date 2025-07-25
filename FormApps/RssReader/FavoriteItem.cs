﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader {
    public class FavoriteItem {
        public required string DisplayName { get; set; }
        public required string Value { get; set; }
        public required bool CanDelete { get; set; }

        public override string ToString() {
            return DisplayName;
        }
    }
}
