## Workshop ideas
### New code check-list
- [x] Read the `README` / related documentation
  - Pretty quick here, there is none...
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


- [x] `Analyze the code structure` to understand the architecture
![Backend](img/solution-back.png)

Here are some quick insights here:
- 2 `Controllers`: `Park` and `WeatherForecast`
  - Why do we care about Weather here?
- Requests may be the `Dtos`
  - Give an idea of the supported features: `Add Dinosaur`, `Move Dinosaur`, ...
- Usage of Db2
- Why is there a `ReflectionHelper`?
- Bad naming: `Class1`, `Init`
  - What are their purpose?
- Where is the business logic here?
  - Not really a clear architecture from the folder organization...

![Front-end](img/front-hierarchy.png)
- Pretty clear organization on the front-end with isolated components
  - It exposes the different exposed features
- `eslint` seems to be used here: quality at heart?
- `vite` is used as well


- [x] Check dependencies to understand potential system interactions
  - List dependencies for the back-end part

```shell
dotnet list JurassicCode.sln package
```

![List dependencies](img/dependencies-back.png)

Based on it, we can deduct:
- Some integrations are made using [`Kafka`](https://www.nuget.org/packages/confluent.kafka/)
- [`Polly`](https://www.nuget.org/packages/Polly) is used for which purpose? (Circuit breaker, Retry, ...)
- [`Swashbuckle`](https://www.nuget.org/packages/Swashbuckle.AspNetCore) is referenced meaning an `Open API` may be generated to document the `API`
- [`FluentAssertions`](https://www.nuget.org/packages/FluentAssertions) is used in the tests
  - Making them readable?

> Weird stuff: there is no `Db2` connector 🤔

Let's do the same for the front-end:
```shell
npm list --depth=0
```

![Front dependencies](img/dependencies-front.png)
- Nothing exotic here
- [`axios`](https://www.npmjs.com/package/axios) may be used to call `APIs`

- [ ] Run [`LibYear`](https://libyear.com/) analysis to understand dependencies freshness
  - Resources about how to [Keep Dependencies Up-to-Date](https://xtrem-tdd.netlify.app/Flavours/Practices/keep-dependencies-up-to-date)

```shell
dotnet tool install -g libyear
dotnet libyear
```

> We are almost 42 libyears behind for the back-end part... 

![Libyear on back-end](img/dotnet-libyear.png)

```shell
npx libyear
```

> We are better on the front-end side with a drift lower than 1 libyear 

![Libyear on front-end](img/js-libyear.png)

More info about drift [here](https://github.com/jdanil/libyear?tab=readme-ov-file#metrics).

- [x] Look at the `git log`
  - Not relevant here because we are on a workshop code 🙃

#### Gather metrics
Our tools and development ecosystem allow us to quickly gather metrics to observe the code quality in a fairly factual manner:

- [ ] Retrieve `code coverage` 
- [ ] Set up a `static code analysis` tool
- [ ] Identify `hotspots` (where they are located)
- [ ] Review the `tests quality`

- [ ] (Optional) Detect Linguistic Anti-Patterns with `ArchUnit`