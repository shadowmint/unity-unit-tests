//
//  Copyright 2012  douglasl
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using System.Collections.Generic;

namespace n.Test
{
  /** 
   * Setup and run tests by extending this class 
   * <p>
   * Notice this is a simple and stupid test runner; it does *not*
   * support running tests in parallel.
   */
	public abstract class nTestRunner
	{
    /** Implement class binding here */
    protected abstract void Setup(nTestSuite tests);
    
    /** Run tests for this project with the provider writer */
    public void Run (Type writer)
    {
      var args = Args ();
      if (args.ContainsKey ("testOutputPath")) {
        var path = args ["testOutputPath"];
        var suite = new nTestSuite ();
        Setup (suite);
        RunTests (suite);
        SaveTestResults (path, suite, writer);
      } else {
        throw new Exception("Invalid request: Invoke this using: $UNITY -batchmode -quit -projectPath $PROJECT -executeMethod $METHOD testOutputPath=$OUTPUT");
      }
    }
    
    /** Read the command line arguments */
    private static IDictionary<string, string>Args() {
      var rtn = new Dictionary<string, string>();
      var raw = Environment.GetCommandLineArgs();
      foreach (var item in raw) {
        var parts = item.Split('=');
        if (parts.Length == 2) 
          rtn[parts[0]] = parts[1];
        else
          rtn[parts[0]] = "";
      }
      return rtn;
    }
    
    /** Save test results */
    private void SaveTestResults (string path, nTestSuite suite, Type writer)
    {
      var instance = (nTestWriter) Activator.CreateInstance(writer);
      instance.Write(path, suite);
    }
    
    /** Run the tests */
    private void RunTests(nTestSuite suite)
    {
      nTestResultSet item = null;
      while ((item = suite.Next()) != null) {
        var tests = item.Results;
        foreach (var test in tests) {
          try {
            var instance = (nTestBase) Activator.CreateInstance(item.Target);
            instance.Results = test;
            test.Target.Invoke(instance, new object[]{});
          }
          catch(Exception e) {
            test.Error = e;
            test.Success = false;
          }
        }
      }
    }
	}
}

