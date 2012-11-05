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
