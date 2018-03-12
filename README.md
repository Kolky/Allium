# Allium
Google Analytics v3 Tracker in C#. Using the [Google Measurement Protocol](https://developers.google.com/analytics/devguides/collection/protocol/v1/). Most of the Enhanced E-Commerce features are not (yet) supported.

## Status
[![Build status](https://ci.appveyor.com/api/projects/status/cdf6u2da6lrtqmwe?svg=true)](https://ci.appveyor.com/project/Kolky/allium)
[![NuGet](https://badge.fury.io/nu/Allium.svg)](https://www.nuget.org/packages/Allium/1.0.0)

## Example

Basic example:
```
using (var session = new AnalyticsSession("UA-XXXX-Y"))
{
	// Set Parameters
	session.App.ApplicationName = "TestApp";
	session.Start();

	// Track an event
	session.TrackEventHit("Core", "Started").Send();
} // Finishes the session
```

## Libraries
- StyleCop.Analyzers
- Validation

### For Development/Testing
- NUnit
- OpenCover
- ReportGenerator
- RhinoMocks

### For Publishing
- NuProj
