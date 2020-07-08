using System;
using System.Collections.Generic;

namespace Narin.Unity.IAP {

    public class QuerySkuInfoEventArgs: EventArgs {
        List<SkuInfo> SkusInfo;

        public QuerySkuInfoEventArgs(List<SkuInfo> skusInfo) {
            SkusInfo = skusInfo;
        }
    }

}