using System;
using System.Collections.Generic;

namespace Narin.Unity.IAP {

    public class QuerySkuInfoEventArgs: EventArgs {
        public List<ProductDetail> SkusInfo;

        public QuerySkuInfoEventArgs(List<ProductDetail> skusInfo) {
            SkusInfo = skusInfo;
        }
    }

}