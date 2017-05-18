# VstsMetrics
A console application for calculating a couple of work item metrics (throughput and cycle time) from Visual Studio Team Services. It could easily be extended with other queries that focus on changes to work items over time.

## Before You Begin
This application was written to get around a lack of capability (at the time) in VSTS. It's slow and inefficient but much easier than any manual way I've found of doing this up to now. When the 'business rules for work item types' feature in the [VSTS roadmap](https://blogs.msdn.microsoft.com/visualstudioalm/2017/01/26/team-services-process-customization-roadmap-jan-2017/) is released this application should no longer be neccessary, as you should be able to configure rules that make this information trivial to get with simple work item queries.

It's also worth checking out the [VSTS integration with PowerBI](https://www.visualstudio.com/en-us/docs/report/powerbi/connect-vso-pbi-vs). I don't follow this as closely, but it's possible improvements there will make this appplication obsolete too.

## Build 

It's a straight Visual Studio 2015 solution file. I've used [Costura.Fody](https://github.com/Fody/Costura) to produce a single .exe that makes deployment and distribution easier.

## Usage

### Throughput

```
vsts.metrics.exe throughput --help

VstsMetrics 1.0.0.0
Copyright ©  2017

  -d, --doneState       (Default: Done) The work item state you project is
                        using to indicate a work item is 'Done'.

  -u, --projectUrl      Required. The URL to the TFS/VSTS project collection.
                        E.g. for VSTS: https://{your-account}.visualstudio.com

  -t, --pat             Required. Your personal authentication token. Used to
                        authenticate to the VSTS REST API.

  -p, --projectName     Required. The name of the team project you want to
                        query.

  -q, --query           Required. The full path to the query that will return
                        the work items you want to gather metrics on.

  -o, --outputFormat    (Default: Pretty) Valid output formats are Pretty, JSON
                        and CSV.
```

### Cycle Time

```
vsts.metrics.exe cycleTime --help

VstsMetrics 1.0.0.0
Copyright ©  2017

  -i, --initialState    The state that work items leave to begin a cycle of
                        work.

  -e, --endState        The work item state that is the end of the work cycle.

  -x, --strict          (Default: False) Controls how the start time of a work
                        item is calculated. When --strict is specified, the
                        first state transition out of --initialState is used,
                        otherwise the last state transition out of the
                        --initialState is used as the start time.

  -u, --projectUrl      Required. The URL to the TFS/VSTS project collection.
                        E.g. for VSTS: https://{your-account}.visualstudio.com

  -t, --pat             Required. Your personal authentication token. Used to
                        authenticate to the VSTS REST API.

  -p, --projectName     Required. The name of the team project you want to
                        query.

  -q, --query           Required. The full path to the query that will return
                        the work items you want to gather metrics on.

  -o, --outputFormat    (Default: Pretty) Valid output formats are Pretty, JSON
                        and CSV.

```
