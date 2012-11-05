using UnityEngine;
using System.Collections;
using n.Test;

public class MyTests : nTestBase {

  [nTest]
  public void test_thing_one() {
  }
  
  [nTest]
  public void test_other_thing() {
  }
  
  [nTest]
  public void test_thing_with_debug() {
    Debug ("This is a message");
    Debug ("This is a message too");
  }
  
  [nTest]
  public void test_failing_thing ()
  {
    var blah = false;
    blah.ShouldBe(true);
  }
}
