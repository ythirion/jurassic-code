# Styled Components Cleanup Guide

After analyzing the components, only the `Scanner` and `SecurityCamera` components in `jurassic-ui/src/components/styled/index.ts` are truly unused and can be safely removed. The other components that were initially flagged (`DinoTracker`, `DinoBlip`, `WarningFlash`, `VisitorCounter`, and `Terminal`) are actually used in the `ParkStatus.tsx` component.

## Changes to Make in `jurassic-ui/src/components/styled/index.ts`

Remove the following code:

```diff
- export const Scanner = styled.div`
-   position: relative;
-   width: 100%;
-   height: 5px;
-   background-color: rgba(235, 190, 23, 0.3);
-   overflow: hidden;
-   
-   &:before {
-     content: '';
-     position: absolute;
-     top: 0;
-     left: -50%;
-     width: 50%;
-     height: 100%;
-     background: linear-gradient(to right, transparent, ${theme.colors.accent}, transparent);
-     animation: scan 2s infinite linear;
-   }
-   
-   @keyframes scan {
-     0% {
-       left: -50%;
-     }
-     100% {
-       left: 100%;
-     }
-   }
- `;

- export const SecurityCamera = styled.div`
-   position: relative;
-   width: 100%;
-   aspect-ratio: 16/9;
-   background-color: #111;
-   border-radius: ${theme.borderRadius.small};
-   overflow: hidden;
-   margin-bottom: ${theme.spacing.md};
-   
-   &:before {
-     content: 'LIVE';
-     position: absolute;
-     top: 10px;
-     right: 10px;
-     background-color: ${theme.colors.danger};
-     color: white;
-     padding: 2px 6px;
-     border-radius: 4px;
-     font-size: 0.7rem;
-     font-weight: bold;
-   }
-   
-   &:after {
-     content: '';
-     position: absolute;
-     top: 0;
-     left: 0;
-     width: 100%;
-     height: 100%;
-     background: repeating-linear-gradient(
-       0deg,
-       rgba(0, 0, 0, 0.1),
-       rgba(0, 0, 0, 0.1) 1px,
-       transparent 1px,
-       transparent 2px
-     );
-     pointer-events: none;
-   }
- `;
```

## Renaming `Class1.cs` to `ParkService.cs`

The analysis of `Class1.cs` shows:

1. The file already uses the name `ParkService` as the class name: `public partial class ParkService : IParkService`
2. The namespace is `namespace JurassicCode;`
3. The using statements are:
   - `using JurassicCode.Db2;`
   - `using System;`
   - `using System.Collections.Generic;`

Renaming the file from `Class1.cs` to `ParkService.cs` can be done without code changes. Simply renaming the file is sufficient as the internal class name already matches what we want the file name to be.