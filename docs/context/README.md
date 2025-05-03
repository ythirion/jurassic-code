# Context is key

Be it the souurce code or very fine instructions, these comparisons highlight the importance of context when interacting with a LLM.

## First experiment - understand the codebase

The first expermient to [understand the codebase](Understand_the_codebase.md) with or without context (yes, even without codesource to base it's answer upon, an LLM, is somehow helpful) highlight a very interresting point : the context includes ALL files available in the codebase.
That's a good place to reference [Architecture Decision Record](https://adr.github.io) so that future answer can use them to be more accurate.

## Second experiment - assess the code quality

The second try is to [assess the code quality](Assess_the_code_quality.md). This time, we have a source code and an LLM. The LLM has access to all files in the codebase, but it also has access to the source code itself.

While being shown that the more clear the prompt the more accurate the answer, there are some parts that were not raised

I had to ask 6 times "Anything else ?" before the LLM deemed its answer complete and respond "no".
Two reasons for this :
1. The main goal of the model is guess next word, so it produces content, to be fair, I expected an infinite loop here.
2. The token windows and the fact that the answers have a limited lenght too. Therefore, you'll have to guesstimate when the model has finished its answer by yourself based upon the quality and the overall scope of its response.

Other interresting fact, it didn't guess properly some stuff. First coming to mind is the fact that there is a big part in VB.NET and it's not detected as at least a friction. Second one is the richardson maturity model. In this application we are closer to 1 than 2.

