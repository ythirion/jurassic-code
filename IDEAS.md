## Workshop ideas
### New code check-list
- [ ] `Compile` to validate that we are able to compile/execute the code
- [ ] `Analyze potential warnings` during compilation
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

### AI actions
- **Rate code**: Score code quality
- **Suggest Refactoring**: Suggest improvements for design and structure.
- **Find Bugs**: Analyze code and errors to identify issues.
- **Generate Diagram**: Generate mermaid diagrams from code. If no diagram type provided, please ask for the type to the user. Guide him based on the possibilities offered by mermaid (C4, class, sequence, ...).
- **Hints**: Recommend best practices based on the code.
- **Mikado**: Suggest a refactoring strategy using Mikado method based on a specific goal. 

## Resources
- https://github.com/craftvscruft/chatgpt-refactoring-prompts?tab=readme-ov-file#refactorgpt