# SimpleLog4Net
A simple work that packages ‘log4net.dll' and its configuration file into a single file.

* Environment:
  * .NET Framework Version: **4.0**
  * log4net Version: **2.0.8**
  * log4net.dll: dll for **.NET Framework 4.0**
  * **Visual Studio 2013**

* Code Example:
``` C# code
// Add 'SimpleLog4Net.dll' to References list.
using SimpleLog4Net

//SimpleLogger logger = new SimpleLogger("Program");
SimpleLogger logger = new SimpleLogger(typeof(Program));
logger.Debug("Debug");
//logger.Info("Info");
//logger.Warn("Warn");
//logger.Error("Error");
//logger.Fatal("Fatal");

logger.Debug("Debug", new Exception("Debug exception"));
//logger.Info("Info", new Exception("Info exception"));
//logger.Warn("Warn", new Exception("Warn exception"));
//logger.Error("Error", new Exception("Error exception"));
//logger.Fatal("Fatal", new Exception("Fatal exception"));
```

* Log Example:
```
// File: .\Log\2018-01-21.log

================
Time: 2018-01-21 17:36:24,830
Level: DEBUG
TaskID: [8]
Class: Namespace.Program
Description: Debug

================
Time: 2018-01-21 17:36:24,844
Level: DEBUG
TaskID: [8]
Class: Namespace.Program
Description: Debug
System.Exception: Debug exception

```
