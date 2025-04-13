# Some word about Large Language Models and the technology behind

Large Language Model is a technology, no magic or sentiency behind. 
It tries to predict and fill the next one.
And to achieve this, a tremendous amount of work have been produced:
- It encodes a word as a huge vector
- Vector is an array (maybe a multi-dimensional one) that links the work to various concepts. For instance, Madrid and Rome should have roughly the same value in the "capital" space and should be in the same geometric space also where Spain and Italy are.
- It can, but it's limited, retains memory of the discussion.

So even though there is no sentiency, you can pull the discussion toward the "nice" vector space by being kind yourself, using "please" and "thank you" for instance. 
Seems crazy right ?

As a machine, it's limited, and have to abide by what it was made for: predicting and filling the next token (a machine representation of a word).

As such, it have biases and limits such as : 


## Hallucinations
While trying other LLM, including local ones (I dont remember which one), it didn't have access to the local files and asked me for a github repository. Which I gave and experimented with.
Later on, when I got back to Github Copilot, I also asked it to do the analysis of the gitub repository...
It started hallucinate telling me about a Java spring boot project containing a Dinosaur Entity bean...
Unfortunately, I lost the discussion, but I had to confront it before it told me it couldn't access internet.

Here how sometimes [hallucination bites you](1-HALLUCINATION.md)

## Limitations

LLM are about how much information can be processed by the AI. IT's called a [token window](2-TOKENWINDOW.md). After some echanges with GPT-4o, I could learn that it's only able to handle 8000 tokens at a time. That's around 100-150 lines of code. 

WHen I run routhgly the same exchange using Claude 3.7, we learned that the context window is of around 200_000 tokens, so between 12_000 and 18_000 lines of *properly formatted C#* code.