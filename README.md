# Umber

Umber is a simple utility executable for updating requirement versions in umbrella helm charts in a release pipeline.

__There is probably a better tool out there you should use.__

## Usage

- Download source code
- Compile to a single executable  
`dotnet publish -c Release -r linux-x64 /p:PublishSingleFile=true`
- Create Umbrella chart
```Yaml
# Example
apiVersion: v2
name: metachart
description: The umbrella helm chart
icon: http://missing.icon
type: application
version: 0.1.0
appVersion: 0.1.0

dependencies:
- name: project01
  version: 0.1.0
  repository: "file://../../Project01/charts/project01"
- name: project02
  version: 0.1.0
  repository: "file://../../Project02/charts/project02"
```

- Execute following command to replace chart  
`Umber -f {Chart.yaml} -c project01=0.0.2,project01=0.1.1`

```Yaml
# Result
apiVersion: v2
name: metachart
description: The umbrella helm chart
icon: http://missing.icon
type: application
version: 0.1.0
appVersion: 0.1.0

dependencies:
- name: project01
  version: 0.0.2
  repository: "file://../../Project01/charts/project01"
- name: project02
  version: 0.1.1
  repository: "file://../../Project02/charts/project02"
```