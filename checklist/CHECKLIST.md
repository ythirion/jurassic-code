## Workshop ideas
### New code check-list
- [x] `Compile` to validate that we are able to compile/execute the code
  - We can run those commands
```shell
dotnet build JurassicCode.sln
npm install --prefix jurassic-ui 
npm run build --prefix jurassic-ui

// Or use the build.sh script
bash build.sh
```

Here are the results:
- Regarding the back-end part:

![Back-end warnings](img/warning-back.png)

- Regarding the `client` part:

![Npm install](img/audit.png)

![Front-end warnings](img/warning-front.png)

- [x] `Analyze potential warnings` during compilation

- Usage of `netcoreapp3.1` which is not supported anymore
> Microsoft.NET.EolTargetFrameworks.targets(32,5): Warning NETSDK1138 : The target framework 'netcoreapp3.1' is out of support and will not receive security updates in the future. Please refer to https://aka.ms/dotnet-core-support for more information about the support policy.

- Warnings in the system:
  - Some identified `vulnerabilities` are found in the `ui` and `api` dependencies
- There are a mix of languages in the system: `C#`, `VB .NET`, `typescript`

- [ ] `Analyze the code structure` to understand the architecture 
- [ ] Check dependencies to understand potential system interactions
  - [ ] Run `LibYear` analysis to know dependencies freshness
- [ ] Read the `README` / related documentation
- [ ] Look at the `git log`

#### Gather metrics
Our tools and development ecosystem allow us to quickly gather metrics to observe the code quality in a fairly factual manner:

- [ ] Retrieve `code coverage` 
- [ ] Set up a `static code analysis` tool
- [ ] Identify `hotspots` (where they are located)
- [ ] Review the `tests quality`

- [ ] (Optional) Detect Linguistic Anti-Patterns with `ArchUnit`