using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using n.Test;
using n.Test.Writers;

namespace Balls {
  public class Hello : nTestRunner {
    protected override void Setup(nTestSuite tests) {
      tests.type = typeof(MyTests);
      tests.type = typeof(MyTests2);
    }
    
    public static void RunTests() {
      new Hello().Run(typeof(nSimpleTestWriter));
    }
  }
}
