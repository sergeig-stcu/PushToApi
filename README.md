# README

## Overview

This application is for generating messages based on template and sending to MuleSoft event endpoint.

It uses the following concepts:

1. Configuration file contains environment specific configuration data.  Not checked in.
2. Template file.  Any text file with any extension, except 'yaml'.
3. Model file.  Same file as template file, but with 'yaml' extension.  Provdes static data to replace in template.  Some of the data can be dynamically set by application if not set by the file.  Examples of dynamic parameters: id, time stamp.  See code for details.

The application prompts before sending data to the Endpoint, so it can be used as a basic console based template generator.

Template system is based on Razor.

## Usage

```
dotnet run -- templates/event.json user/dev.yaml
```
## Environment File Example

If file is stored in 'user' directory it will be ignored by GIT.

```yaml
BasicAuth:
    Id: encompass
    Password: password_from_azure_usually_no_need_to_escape
PathPrefix: https://muledev.stcu.local
```

## Model File Details

You can add new parameters to the Model without its recompilation as long as new parameter is under Ext property:

```yaml
Ext:
    Name: World
```

## Expandability

The Config, Model, Template concepts should support other scenarios well.
