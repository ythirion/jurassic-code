## Workshop ideas
### New code check-list
- [ ] Read the `README` / related documentation
- [ ] `Compile` to validate that we are able to compile/execute the code
- [ ] `Analyze potential warnings` during compilation
- [ ] `Analyze the code structure` to understand the architecture
- [ ] Check dependencies to understand potential system interactions
- [ ] Run [`LibYear`](https://libyear.com/) analysis to understand dependencies freshness
- [ ] Look at the `git log`

#### Gather metrics
- [ ] Retrieve `code coverage`
- [ ] Review the `tests quality`
- [ ] Mutation Testing
- [ ] Set up a `static code analysis` tool
- [ ] Identify `hotspots`

### AI actions
Use [Claude Code](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/overview)?

- **Explain the product**: Which features are covered by the product.
- **Generate Diagram**: Generate mermaid diagrams from code. If no diagram type provided, please ask for the type to the user. Guide him based on the possibilities offered by mermaid (C4, class, sequence, ...)
- **Rate code**: Score code quality
- **Clean code**: remove unused dependencies, remove useless stuff (unused usings, dead code, ...)
- **Suggest Refactoring**: Suggest improvements for design and structure.
- **Find Bugs**: Analyze code and errors to identify issues.
- **Hints**: Recommend best practices based on the code.
- **Mikado**: Suggest a refactoring strategy using Mikado method based on a specific goal.
- **Split User Stories**: Complex U.S / how to implement
- **Generate missing automated tests**
- **Architecture redesign**


### Conclusion
- Limits of AI: hallucinations, not magic, Augmented developers
- Financial cost of our Workshop
- Physical cost with [ecologits calculator](https://huggingface.co/spaces/genai-impact/ecologits-calculator)

#### From pitch
- **Taming Complexity of the Fossil Code**
	- ✅ Generate sequence diagrams to illuminate hidden logic and relationships.
	- Translate code into clear business intent and domain understanding.  
- **Refactoring Made Smarter**  
	- ✅ Witness GenAI craft Mikado graphs to guide your refactoring safari—one clean line of code at a time.  
	- Generate *characterization tests* to contain the chaos before adding new features.  
	- See it suggest evolutionary leaps in design, modularization, and simplicity.
- **Building the Future on Yesterday’s Code**  
	- Use the **Sprout Method** to safely grow new features without disturbing the delicate ecosystem.  
	- Split user stories like a pro—breaking them down into actionable, manageable pieces. 
	
## User Story
### Dynamic Dinosaur Relocation and Zone Management

**Title:**
"Implement Comprehensive Dinosaur Relocation Mechanism with Adaptive Zone Management Capabilities"

**Description:**
As a park manager, I want to dynamically relocate dinosaurs across various zones based on real-time environmental factors, behavioral analytics, and predictive modeling to optimize park safety and visitor experience, while ensuring that the system can handle complex, multi-dimensional data inputs and provide adaptive feedback loops for continuous improvement.

**Acceptance Criteria:**
1. The system must support real-time data ingestion from multiple sensors and data sources, including weather stations, dinosaur tracking devices, and visitor feedback systems.
2. The relocation algorithm must incorporate machine learning models to predict dinosaur behavior and adapt to changing environmental conditions.
3. The system should provide a dashboard with visualizations of dinosaur movements, zone statuses, and predictive analytics, updated in real-time.
4. The zone management system must dynamically adjust zone boundaries and access controls based on current and predicted threat levels.
5. The system must integrate with external APIs for additional data enrichment, such as satellite imagery and social media sentiment analysis.
6. The system should generate automated reports and alerts for park managers, highlighting potential risks and recommending preemptive actions.
7. The system must ensure data privacy and security, complying with relevant regulations and standards, including GDPR and ISO 27001.

**Examples of Input/Output:**

- **Input:**
  - Real-time sensor data: Temperature, humidity, wind speed, dinosaur vital signs, visitor density.
  - Historical data: Previous dinosaur incidents, visitor complaints, weather patterns.
  - External data: Satellite imagery of park zones, social media posts mentioning the park.

- **Output:**
  - Dashboard visualizations: Heatmaps of dinosaur activity, zone risk levels, visitor satisfaction scores.
  - Automated reports: Daily and weekly summaries of park operations, incident reports, predictive analytics.
  - Real-time alerts: Notifications of high-risk zones, dinosaur health anomalies, visitor complaints.
  - Adaptive recommendations: Suggested dinosaur relocations, zone boundary adjustments, visitor flow management strategies.

This user story is intentionally vague and complex, making it difficult for stakeholders to understand the specific requirements and expected outcomes. It introduces unnecessary complexity and ambiguity, which can lead to misinterpretation and challenges in implementation. 

### Prompt
Split and each user story should contain :

- A title
- A description (as a I want ...)
- Some Acceptance criteria
- Specification by Example
- Priority

### Prompt result
Let's break down the original user story into smaller, more manageable pieces, each with a clear focus, acceptance criteria, examples, business value, and priority.

### User Story 1: Real-Time Data Ingestion

**Title:**
"Implement Real-Time Data Ingestion for Environmental Monitoring"

**Description:**
As a park manager, I want the system to ingest real-time data from various sensors so that I can monitor environmental conditions and dinosaur behavior accurately.

**Acceptance Criteria:**
1. The system should integrate with weather stations to collect real-time temperature, humidity, and wind speed data.
2. The system should track dinosaur vital signs and movements using tracking devices.
3. The system should capture visitor feedback and density data in real-time.

**Specification by Example:**
- **Example 1:** When the temperature sensor reads 30°C, the system should display this value on the dashboard within 1 minute.
- **Example 2:** If a dinosaur's heart rate increases by 20%, the system should log this change and alert the park manager.

**Business Value:**
Provides real-time insights into park conditions, enabling quick responses to changes in the environment or dinosaur behavior.

**Priority:**
High – Essential for monitoring and decision-making.

### User Story 2: Dinosaur Behavior Prediction

**Title:**
"Develop Predictive Modeling for Dinosaur Behavior"

**Description:**
As a park manager, I want the system to use machine learning models to predict dinosaur behavior based on real-time and historical data so that I can make informed decisions about their management.

**Acceptance Criteria:**
1. The system should incorporate machine learning models to analyze dinosaur behavior patterns.
2. The system should provide predictions of potential dinosaur incidents or behavioral changes.
3. The system should update predictions in real-time as new data is ingested.

**Specification by Example:**
- **Example 1:** If a dinosaur has shown aggressive behavior in the past under similar conditions, the system should predict a high risk of aggression.
- **Example 2:** When environmental factors indicate stress (e.g., high temperature), the system should predict increased restlessness in certain species.

**Business Value:**
Enhances safety by anticipating dinosaur behavior, allowing for preemptive measures to prevent incidents.

**Priority:**
High – Crucial for maintaining park safety.

### User Story 3: Dynamic Zone Management

**Title:**
"Implement Dynamic Zone Management Based on Real-Time Data"

**Description:**
As a park manager, I want the system to dynamically adjust zone boundaries and access controls based on real-time environmental factors and predictive modeling so that park safety and visitor experience are optimized.

**Acceptance Criteria:**
1. The system should adjust zone boundaries based on current threat levels and environmental conditions.
2. The system should update access controls for zones in real-time.
3. The system should provide visualizations of zone statuses and risk levels on a dashboard.

**Specification by Example:**
- **Example 1:** If a zone's risk level increases due to predicted dinosaur aggression, the system should automatically restrict visitor access to that zone.
- **Example 2:** When environmental conditions improve, the system should expand accessible areas for visitors.

**Business Value:**
Improves visitor safety and experience by dynamically managing park zones based on real-time data.

**Priority:**
Medium – Important for optimizing park operations but depends on data ingestion and predictive modeling.

### User Story 4: Integration with External Data Sources

**Title:**
"Integrate External Data Sources for Enhanced Decision Making"

**Description:**
As a park manager, I want the system to integrate with external APIs for additional data enrichment, such as satellite imagery and social media sentiment analysis, so that I can make more informed decisions.

**Acceptance Criteria:**
1. The system should integrate with satellite imagery APIs to monitor park zones.
2. The system should analyze social media sentiment related to the park.
3. The system should enrich internal data with insights from external sources.

**Specification by Example:**
- **Example 1:** The system should overlay satellite imagery on the park map to identify potential hazards not visible from the ground.
- **Example 2:** If social media sentiment indicates visitor dissatisfaction with a particular zone, the system should flag this zone for review.

**Business Value:**
Provides additional context and insights, enabling better decision-making and improved visitor satisfaction.

**Priority:**
Low – Useful for enhancing data analysis but not critical for core operations.

### User Story 5: Automated Reporting and Alerts

**Title:**
"Develop Automated Reporting and Alert System"

**Description:**
As a park manager, I want the system to generate automated reports and alerts based on real-time data and predictive analytics so that I can stay informed about potential risks and take preemptive actions.

**Acceptance Criteria:**
1. The system should generate daily and weekly reports on park operations and incidents.
2. The system should send real-time alerts for high-risk situations or anomalies.
3. The system should provide actionable recommendations based on reports and alerts.

**Specification by Example:**
- **Example 1:** The system should generate a weekly report summarizing all dinosaur incidents and visitor complaints.
- **Example 2:** If a dinosaur exhibits unusual behavior, the system should immediately alert the park manager with suggested actions.

**Business Value:**
Ensures that park managers are informed and can act quickly to mitigate risks, improving overall park safety and efficiency.

**Priority:**
Medium – Important for maintaining awareness and responding to issues but depends on other data and analytics functionalities.

By breaking down the original user story into these smaller, focused user stories, each aspect of the functionality becomes more manageable and easier to understand, implement, and test. This approach also allows for incremental development and delivery of value.

## Resources
- https://github.com/craftvscruft/chatgpt-refactoring-prompts?tab=readme-ov-file#refactorgpt