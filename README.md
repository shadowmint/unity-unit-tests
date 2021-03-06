Unity unit tests
==

Simple example showing off how to do unit tests in unit.

The unit tests are run by the python runner:

    import os
    import datetime
    import subprocess

    unity = '/Applications/Unity/Unity.app/Contents/MacOS/Unity'
    cwd = os.getcwd()

    today = datetime.date.today()
    output = cwd + "/testResults/" + str(today) + "__testResults.txt"; 

    cmd = [ unity, "-batchmode", "-quit", "-projectPath", cwd, "-executeMethod", "Balls.Hello.RunTests", "testOutputPath="+output ]
    subprocess.call(cmd)

    cmd = [ "/bin/cat", output ]
    subprocess.call(cmd)

This invokes a small custom unit testing framework, that generates output like this:

    Test results:
    ============

    Raw test results:
    ----------------
    - MyTests: 3/4 passed
    --      - MyTests::test_thing_one
    --      - MyTests::test_other_thing
    --      - MyTests::test_thing_with_debug
    ------------------ DEBUG BLOCK -----------------
    This is a message
    This is a message too

    ------------------------------------------------
    -- fail - MyTests::test_failing_thing
    ------------------ ERROR BLOCK -----------------
    System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.Exception: Invalid boolean condition
      at n.Test.nAssert.ShouldBe (Boolean actual, Boolean expected) [0x00007] in /Users/doug/projects/unity/unity-tests/Assets/n/Test/nAssert.cs:25 
      at MyTests.test_failing_thing () [0x00002] in /Users/doug/projects/unity/unity-tests/Assets/tests/MyTests.cs:25 
      at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[],System.Exception&)
      at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00000] in <filename unknown>:0 
      --- End of inner exception stack trace ---
      at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00000] in <filename unknown>:0 
      at System.Reflection.MethodBase.Invoke (System.Object obj, System.Object[] parameters) [0x00000] in <filename unknown>:0 
      at n.Test.nTestRunner.RunTests (n.Test.nTestSuite suite) [0x0003b] in /Users/doug/projects/unity/unity-tests/Assets/n/Test/nTestRunner.cs:77 
    ------------------------------------------------
    - MyTests2: 2/2 passed
    --      - MyTests2::test_in_other_class
    --      - MyTests2::another_test_in_other_class

    Summary test results:
    --------------------

    - MyTests: 3/4 passed
    --      - MyTests::test_thing_one
    --      - MyTests::test_other_thing
    --      - MyTests::test_thing_with_debug
    -- fail - MyTests::test_failing_thing

    - MyTests2: 2/2 passed
    --      - MyTests2::test_in_other_class
    --      - MyTests2::another_test_in_other_class

    Summary results:
    ---------------
    - MyTests: 3/4 passed
    - MyTests2: 2/2 passed
    - Total: 1 tests failed
