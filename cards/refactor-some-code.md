## 🔁 Refactor Some Code  
**Understand quickly what / how to refactor a given piece of code**

### 🎯 Intent  
Improve the internal structure of code without changing its behavior, to make it easier to understand, test, and evolve. Refactoring legacy code often requires strategic, low-risk changes.

### 🔍 When to Use  
- When code is hard to read or reason about  
- When adding a new feature into a tangled area  
- After having characterized behavior through tests  
- When working in a high-risk area of the codebase

### 🤖 How to Use with AI Assistants  

AI can assist in spotting code smells, suggesting incremental refactorings, or even generating before/after examples based on design principles.

#### Prompt Examples  
- *"Identify code smells in this class and suggest ways to refactor it."*  
- *"What is the responsibility of this method? Can it be split into smaller methods?"*  
- *"Refactor this piece of code using the Sprout Method."*  
- *"Show me a Mikado graph of refactorings needed to isolate this dependency."*  
- *"Propose a rename plan for this file to clarify its role."*

### 🧱 Patterns to Apply  
- **Mikado Method**  
  A systematic way to plan and perform large-scale refactorings by exploring changes incrementally and backing out when necessary. Helps build a safe refactoring graph.

- **Sprout Method**  
  Instead of modifying existing fragile code directly, sprout new methods or classes and delegate responsibilities gradually.
  > Helps isolate changes and avoid regressions.

- **Refactoring to Patterns**  
  e.g., Replace Conditional with Polymorphism, Extract Class, Introduce Parameter Object

### 🛠️ Related Craft/Agile Practices  
- **Refactoring Legacy Code Safely**  
- **Characterization Tests First** (from Michael Feathers)  
- **Rename → Extract → Move → Replace** as low-risk refactoring steps  
- **Evolutionary Design**  
- **Boy Scout Rule**: Leave the code cleaner than you found it

### 📚 Go Deeper  
- [Refactoring – Martin Fowler](https://martinfowler.com/books/refactoring.html)  
- [The Mikado Method](https://mikadomethod.info)  
- [Working Effectively with Legacy Code – Michael Feathers](https://www.oreilly.com/library/view/working-effectively-with/0131177052/) 
- [The key points of Working Effectively with Legacy code](https://understandlegacycode.com/blog/key-points-of-working-effectively-with-legacy-code/)   
- [Refactoring Guru](https://refactoring.guru/)

---