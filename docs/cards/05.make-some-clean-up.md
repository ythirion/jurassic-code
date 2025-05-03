## 🧹 Make Some Cleanup  
**Make a bit of cleanup in the code**

### 🎯 Intent  
Reduce noise and improve clarity by keeping your dependencies up to date and removing obsolete or unused code elements, outdated dependencies, and unnecessary complexity. These small cleanups pave the way for safer refactoring and better maintainability.

### 🔍 When to Use  
- When exploring a legacy codebase with unclear dependencies
- Before introducing new tooling or test coverage
- To reduce technical debt with low-risk changes
- As a warm-up refactoring session to build confidence

### 🤖 How to Use with AI Assistants  

Assistants can help identify unused elements and recommend safe removal or modernization actions.

#### Prompt Examples  
- *"List any dead code in this file or solution and explain why it can be removed."*  
- *"Which NuGet dependencies are unused in this .NET project?"*  
- *"Which parts of this codebase are still targeting outdated .NET APIs or frameworks?"*  
- *"Can you suggest modern equivalents for deprecated or obsolete .NET constructs in this code?"*  
- *"Is this helper class still used anywhere? If not, can it be safely deleted?"*

### 🧰 Common Cleanup Targets  
- **Dead Code**  
  - Unused classes, methods, or variables  
  - Commented-out legacy blocks  
- **Dead Dependencies**  
  - Unused NuGet packages  
  - Transitive dependencies pulled in but never used  
- **Outdated Frameworks**  
  - Old .NET versions (e.g. targeting .NET Framework 4.x vs .NET 6+)  
  - Deprecated APIs

### 🛠️ Related Craft/Agile Practices  
- **Boy Scout Rule**: *"Always leave the code cleaner than you found it."*  
- **YAGNI (You Aren’t Gonna Need It)**  
- **Evolutionary Architecture**: Prepare for gradual modernization  
- **Refactoring Patterns**: Safe deletion, dependency slimming, etc.

### 📚 Go Deeper  
- [Refactoring Techniques](https://refactoring.com/catalog/)  
- [The Boy Scout Rule](https://97-things-every-x-should-know.gitbooks.io/97-things-every-programmer-should-know/content/en/thing_08/)  
- [DepClean: Detecting Unused Dependencies](https://github.com/castor-software/depclean)  
- [Libyear: How old are your dependencies ?](https://libyear.com)

---