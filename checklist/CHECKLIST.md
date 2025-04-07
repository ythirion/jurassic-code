## New code check-list
### Read the `README` / related documentation
  - Pretty quick here, there is none...

### `Compile` to validate that we are able to compile/execute the code
We can run those commands:
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

### `Analyze potential warnings` during compilation
- Usage of `netcoreapp3.1` which is not supported anymore
> Microsoft.NET.EolTargetFrameworks.targets(32,5): Warning NETSDK1138 : The target framework 'netcoreapp3.1' is out of support and will not receive security updates in the future. Please refer to https://aka.ms/dotnet-core-support for more information about the support policy.

- Warnings in the system:
  - Some identified `vulnerabilities` are found in the `ui` and `api` dependencies
- There are a mix of languages in the system: `C#`, `VB .NET`, `typescript`


### `Analyze the code structure` to understand the architecture
![Backend](img/solution-back.png)

Here are some quick insights:
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


### Check dependencies to understand potential system interactions
List dependencies for the back-end part:
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

### Run [`LibYear`](https://libyear.com/) analysis to understand dependencies freshness
Resources about how to [Keep Dependencies Up-to-Date](https://xtrem-tdd.netlify.app/Flavours/Practices/keep-dependencies-up-to-date)

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

### Look at the `git log`
Not relevant here because we are on a workshop code 🙃

### Gather metrics
Our tools and development ecosystem allow us to quickly gather metrics to observe the code quality in a fairly factual manner:

#### Retrieve `code coverage`
  - A coverage metric showing how much source code a test suite executes, from none to `100%`

![Line coverage](img/line-coverage.png)

```shell
dotnet tool install --global JetBrains.dotCover.GlobalTool
dotnet dotcover test --dcReportType=HTML JurassicCode.sln
```

![Dotcover](img/dotcover.png)

> There is a pretty high code coverage on the back-end part with `81%`!!!

There is no test on the `front-end`: code coverage of `0%` here...

#### Review the `tests quality`
We start by reading a random test:

```csharp
[Fact]
// Bad naming: what is the intent here?
// Too many reasons to fail here... making it hard to understand / maintain
public void TestAddAndMoveDinosaursWithZoneToggle()
{
    // Arrange: a lot of Arrange
    // What is relevant in this setup for the outcome of our test?
    DataAccessLayer.Init(new Database());
    // Not clear what is setup with this boolean
    // Hardcoded values
    _parkService.AddZone("Test Zone 1", true);
    _parkService.AddZone("Test Zone 2", false);
    
    _parkService.AddDinosaurToZone("Test Zone 1", new Dinosaur { Name = "TestDino1", Species = "T-Rex", IsCarnivorous = true });
    _parkService.AddDinosaurToZone("Test Zone 1", new Dinosaur { Name = "TestDino2", Species = "Velociraptor", IsCarnivorous = true });

    // Act
    Action moveToClosedZone = () => _parkService.MoveDinosaur("Test Zone 1", "Test Zone 3", "TestDino1");
    // Assert
    moveToClosedZone.Should().Throw<Exception>().WithMessage("Zones are closed or do not exist.");

    // Arrange
    _parkService.ToggleZone("Test Zone 2");
    // Act
    _parkService.MoveDinosaur("Test Zone 1", "Test Zone 2", "TestDino1");

    // Assert
    var zone2Dinosaurs = _parkService.GetDinosaursInZone("Test Zone 2");
    zone2Dinosaurs.Should().Contain(d => d.Name == "TestDino1");
    var zone1Dinosaurs = _parkService.GetDinosaursInZone("Test Zone 1");
    zone1Dinosaurs.Should().NotContain(d => d.Name == "TestDino1");

    // Assert another behavior
    bool canCoexist = _parkService.CanSpeciesCoexist("T-Rex", "Velociraptor");
    canCoexist.Should().BeFalse();
    canCoexist = _parkService.CanSpeciesCoexist("Triceratops", "Velociraptor");
    canCoexist.Should().BeTrue();

    // Assert another behavior
    _parkService.ToggleZone("Test Zone 1");
    Action addToClosedZone = () => _parkService.AddDinosaurToZone("Test Zone 1", new Dinosaur { Name = "TestDino3", Species = "Triceratops", IsCarnivorous = false });
    addToClosedZone.Should().Throw<Exception>().WithMessage("Zone is closed or does not exist.");
}
```

#### Mutation Testing
Let's gather metrics on the test by using the [`Mutation Testing`](https://xtrem-tdd.netlify.app/Flavours/Testing/mutation-testing) technique.
- It will test our tests by introducing MUTANTS (fault) into our production code during the test execution:
  - To check that the test is failing
  - If the test pass, there is an issue
- We can introduce mutants manually
  - When working on legacy code for example
  - When doing some TDD

Coverage metrics are a good negative indicator but a bad positive one:
- Too little coverage in your code base -> 10%
- Demonstrates you are not testing enough
- The reverse isn’t true
  - Even 100% coverage isn’t a guarantee that you have a good-quality test suite...

Here are the steps:
- `Step 1`: Generate mutants

![Generate mutants](img/generate-mutants.png)

- `Step 2`: Kill them all
  - Check that all your tests are green on the non-mutated business code
  - Take the mutants one by one
    - Place them in front of the wall of the shot
    - Fire a salvo of unit tests
- `Step 3`: Make the assessment
  - Who survived? Who was killed?
  - If your tests fail then the mutant is killed
  - If your tests passed, the mutant survived

```gherkin
As a mutant code
When tests are launched
I am detected
So the code is correctly tested

As a mutant code
When tests are launched
I am NOT detected
So the code is NOT correctly tested
```
![Mutation score](img/mutation-score.png)

> The higher the percentage of mutants killed, the more effective your tests are.

Let's run it:
```shell
dotnet stryker
```

> The result is bad with a mutation score at `43.26%` 😱

![Run stryker](img/run-stryker.png)

Here is the detailed report with explanations:
![Mutation score](img/stryker.png)

- `Killed`: At least one test failed while this mutant was active.
  - The mutant is killed. This is what you want, good job!
- `Survived`: When all tests passed while this mutant was active, the mutant survived
- `Timeout`: The running of tests with this mutant active resulted in a timeout.
  - For example, the mutant resulted in an infinite loop in your code.
- `No coverage`: The mutant isn't covered by one of your tests and survived as a result.
- `Ignored`: The mutant wasn't tested because it is ignored.
  - Not count against your mutation score but will show up in reports.

### Set up a `static code analysis` tool
- [ ] Identify `hotspots` (where they are located)


- [ ] (Optional) Detect Linguistic Anti-Patterns with `ArchUnit`