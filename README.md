# VstsMetrics
A console application for calculating a couple of work item metrics (throughput and cycle time) from Visual Studio Team Services. It could easily be extended with other queries that focus on changes to work items over time.

## Before You Begin
This application was written to get around a lack of capability (at the time) in VSTS. It's slow and inefficient but much easier than any manual way I've found of doing this up to now. When the 'business rules for work item types' feature in the [VSTS roadmap](https://blogs.msdn.microsoft.com/visualstudioalm/2017/01/26/team-services-process-customization-roadmap-jan-2017/) is released this application should no longer be neccessary, as you should be able to configure rules that make this information trivial to get with simple work item queries.

It's also worth checking out the [VSTS integration with PowerBI](https://www.visualstudio.com/en-us/docs/report/powerbi/connect-vso-pbi-vs). I don't follow this as closely, but it's possible improvements there will make this appplication obsolete too.

## Build 

It's a straight Visual Studio 2015 solution file. I've used [Costura.Fody](https://github.com/Fody/Costura) to produce a single .exe that makes deployment and distribution easier.

## Usage

### Throughput

### Cycle Time
