using System;
using System.Collections.Generic;

namespace Narin.Unity.IAP {

    public class QuerySkuInfoEventArgs: EventArgs {
        List<ProductDetail> SkusInfo;

        public QuerySkuInfoEventArgs(List<ProductDetail> skusInfo) {
            SkusInfo = skusInfo;
        }
    }

}