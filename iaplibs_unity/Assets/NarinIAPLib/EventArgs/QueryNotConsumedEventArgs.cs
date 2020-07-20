using System;
using System.Collections.Generic;

namespace Narin.Unity.IAP {

    public class QueryNotConsumedEventArgs: EventArgs {
        public List<PurchaseEventArgs> Purchases;

        public QueryNotConsumedEventArgs(List<PurchaseEventArgs> purchases) {
            Purchases = purchases;
        }
    }

}