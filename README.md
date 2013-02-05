# Eleven41.Logging

Copyright (C) 2013, Eleven41 Software

Eleven41.Logging is a general purpose logging framework, capable of logging to many different outputs.
Those include:

* Console
* File
* Windows Event Log
* Multiple outputs

Various logging levels exist, including:

* Diagnostic
* Info
* Warning
* Error

## Get It on NuGet!

	Install-Package Eleven41.Logging

## LICENSE
[MIT License](https://github.com/eleven41/Eleven41.Logging/blob/master/LICENSE.md)

## REQUIREMENTS

* Visual Studio 2012

## Sample Code

	ILog log = new ConsoleLog();
	log.Log(LogLevels.Diagnostic, "This is my diagnostic message");
	
By passing around the ILog object,your application can easily filter messages.  In addition, by only changing the object construction, your application can switch from logging-to-console to logging-to-file.
