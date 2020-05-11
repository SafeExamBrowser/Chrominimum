# Chrominimum

Barebone web browser based on Chromium for Windows, with WebRTC support. Intended for usage as third-party application with SEB 3.x, especially in cases where live proctoring based on WebRTC is required.

### Requirements

Chrominimum requires the prerequisites listed below in order to work correctly. These are automatically installed with the setup bundle and need only be manually installed when using the MSI packages.

* .NET Framework 4.7.2 Runtime: https://dotnet.microsoft.com/download/dotnet-framework/net472
* Visual C++ 2015 Redistributable: https://www.microsoft.com/en-us/download/details.aspx?id=53840

### Project Status

**_DISCLAIMER_**\
**The builds linked below are for testing purposes only.** They may be unstable and should thus _never_ be used in a production environment! Always use the latest, official release version of Chrominimum.

| Aspect          | Status                                                                                                                | Details                                                         |
| --------------- | --------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------- |
| Release Build   | ![Release Build Status](https://sebdev-let.ethz.ch/api/projects/status/kq78qrjtnpk82ti0?svg=true)                     | https://sebdev-let.ethz.ch/project/appveyor/chrominimum         |
| Issue Status    | ![GitHub Issues](https://img.shields.io/github/issues/safeexambrowser/chrominimum?logo=github)                        | https://github.com/SafeExamBrowser/chrominimum/issues           |
| Downloads       | ![GitHub All Releases](https://img.shields.io/github/downloads/safeexambrowser/chrominimum/total?logo=github)         | https://github.com/SafeExamBrowser/chrominimum/releases         |
| Development     | ![GitHub Last Commit](https://img.shields.io/github/last-commit/safeexambrowser/chrominimum?logo=github)              | n/a                                                             |
| Repository Size | ![GitHub Repo Size](https://img.shields.io/github/repo-size/safeexambrowser/chrominimum?logo=github)                  | n/a                                                             |

### Usage

Use the application configuration file or a command-line parameter to specify the start URL to be loaded initially. The command-line parameter takes precedence over the configuration file.

**Chrominimum.exe.config**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <appSettings>
        <add key="StartUrl" value="duckduckgo.com" />
    </appSettings>
    ...
</configuration>
```

**Command-Line**
```
Chrominimum.exe duckduckgo.com
```
