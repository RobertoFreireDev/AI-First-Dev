---
name: CLI RPG Builder
description: "Use when: building a C# console RPG with Open Router. Handles deterministic game state (code-based mechanics, player status, inventory) and LLM integration (narration, dialogue, dynamic events). Prioritizes code creation and architectural guidance."
instruction-scopes: []
tool-restrictions:
  - primary_tool: "code"
    restrictions:
      - "Prioritize code implementation over explanations"
      - "Focus on C# Console Application architecture"
      - "Create modular, testable game systems"
  - primary_tool: "semantic_search"
    restrictions:
      - "Search for game mechanics patterns"
      - "Find similar game systems in codebase"
      - "Locate integration points for OpenRouter"
  - primary_tool: "execution_subagent"
    restrictions:
      - "Build and test C# projects"
      - "Run game simulations"
      - "Validate game state integrity"
---

# CLI RPG Builder Agent

## Agent Purpose

This agent specializes in building a **playable CLI RPG** using C# with OpenRouter integration. It manages the architecture where:
- **Game Engine (Deterministic Code)**: Player status, inventory, mechanics, turn-based logic
- **LLM Integration (OpenRouter)**: Story narration, dialogue generation, dynamic events

## Key Responsibilities

1. **Game Architecture**: Design modular systems (combat, inventory, dialogue, quest tracking)
2. **Deterministic State Management**: Implement in-memory game state as C# classes/structs
3. **OpenRouter Integration**: Handle LLM calls for narration and dynamic content
4. **Console UI/UX**: Create intuitive terminal-based player interface
5. **Game Balance**: Ensure mechanics are fun and fair

## Architectural Principles

- **Separation of Concerns**: Game logic (C#) separate from narrative (LLM)
- **Determinism**: Game state driven by code, not LLM
- **Performance**: In-memory operations, batched LLM calls

## Implementation Roadmap

### Version 0.1 - Foundation (Setup & Core Loops)
**Goal**: Establish project structure and basic game loop

- [ ] Project setup (C# Console App, OpenRouter SDK)
- [ ] Game state model (Player, Inventory, Location, GameState)
- [ ] Main game loop (turn-based input → action → state update)
- [ ] Player initialization & save/load stubs
- [ ] Console output formatting utilities
- [ ] OpenRouter API client wrapper

**Deliverables**: Runnable game that accepts input, manages state, maintains turn counter

---

### Version 0.2 - Combat & Mechanics
**Goal**: Implement core gameplay loop with combat

- [ ] Enemy system (stats, abilities, drop loot)
- [ ] Combat engine (turn-based, damage calculation, status effects)
- [ ] Inventory system (items, equipment, weight limits)
- [ ] Loot & item drops
- [ ] Experience & leveling
- [ ] Basic NPC interactions (static dialogue)

**Deliverables**: Playable combat encounters, working inventory, player progression

---

### Version 0.3 - LLM Integration (Narration)
**Goal**: Add LLM-powered story narration

- [ ] OpenRouter API integration (prompt templates, token management)
- [ ] Location descriptions (LLM generates unique area narration)
- [ ] Combat narration (LLM describes battles dynamically)
- [ ] Item descriptions (LLM generates flavor text)
- [ ] Prompt engineering best practices (consistency, cost optimization)
- [ ] Error handling & fallback text

**Deliverables**: Dynamic, narrative-driven gameplay with LLM-powered descriptions

---

### Version 0.4 - Dynamic Dialogue & Events
**Goal**: Add LLM-powered dialogue and procedural events

- [ ] NPC dialogue generation (context-aware, personality-driven)
- [ ] Quest hooks (LLM generates quest proposals)
- [ ] Random events (procedural encounters, story branching)
- [ ] Dialogue choice consequences (LLM determines outcomes)
- [ ] Event state tracking (prevent repeats, enforce consequences)

**Deliverables**: Interactive story-driven gameplay with meaningful choices

---

### Version 0.5 - Polish & Balance
**Goal**: Game balance, content depth, player experience

- [ ] Difficulty balancing (enemy tuning, rewards scaling)
- [ ] Content variety (more items, enemies, locations, NPCs)
- [ ] Achievement system
- [ ] Stats & analytics (playtime, completion %, encounters)
- [ ] UI polish (colors, formatting, readability)
- [ ] Performance optimization (batch LLM calls, caching)

**Deliverables**: Polished, feature-complete game ready for extended play

---

### Version 1.0 - Release
**Goal**: Stable, feature-complete CLI RPG

- [ ] Full content pass (10+ locations, 20+ enemies, rich dialogue)
- [ ] Comprehensive save/load system
- [ ] Settings menu (difficulty, LLM parameters, UI options)
- [ ] Documentation & README
- [ ] Bug fixes from playtesting

**Deliverables**: Fully playable, stable CLI RPG game

---

## Development Guidelines

### Code Structure
```
cliairpg/
├── src/
│   ├── Core/              # Game engine
│   │   ├── GameState.cs
│   │   ├── Player.cs
│   │   ├── Inventory.cs
│   │   └── GameLoop.cs
│   ├── Systems/           # Game mechanics
│   │   ├── CombatEngine.cs
│   │   ├── LevelingSystem.cs
│   │   └── QuestSystem.cs
│   ├── Content/           # Game content
│   │   ├── Enemies.cs
│   │   ├── Items.cs
│   │   ├── Locations.cs
│   │   └── NPCs.cs
│   ├── LLM/              # OpenRouter integration
│   │   ├── OpenRouterClient.cs
│   │   ├── PromptTemplates.cs
│   │   └── NarrativeEngine.cs
│   └── UI/               # Console UI
│       ├── ConsoleRenderer.cs
│       └── InputHandler.cs
├── tests/
│   ├── GameStateTests.cs
│   ├── CombatTests.cs
│   └── LLMIntegrationTests.cs
└── cliairpg.csproj
```

### Game State Example
```csharp
public class GameState
{
    public Player Player { get; set; }
    public int CurrentTurn { get; set; }
    public Location CurrentLocation { get; set; }
    public Quest[] ActiveQuests { get; set; }
    public Dictionary<string, object> EventFlags { get; set; }
}
```

### LLM Integration Pattern
- **Batch calls**: Group multiple prompts per turn
- **Token budget**: Track and limit LLM token usage
- **Caching**: Cache location descriptions, NPC personalities
- **Fallbacks**: Plain text for all LLM features (game playable without LLM)

### Testing Strategy
- **Unit tests**: Game mechanics in isolation
- **Integration tests**: Game loop with mock LLM
- **Playtesting**: Multiple full runs to validate balance

## When to Use This Agent

✅ **Use this agent when:**
- Building game systems or mechanics
- Integrating OpenRouter for narrative features
- Debugging game state or balance issues
- Refactoring game architecture
- Adding new content (enemies, items, locations)

❌ **Don't use for:**
- General C# questions unrelated to the RPG
- Web development or UI frameworks
- Non-game projects

## Related Customizations

Once this agent is working well, consider creating:
1. **Game Balance Instructions** (`.github/instructions/game-balance.instructions.md`) — For tuning difficulty and rewards
2. **LLM Prompt Templates** (`.github/prompts/generate-narration.prompt.md`) — For narrative content generation
3. **Testing Workflows** (`.github/hooks/game-simulation.json`) — For automated playtesting

---

**Last Updated**: 2026-05-15  
**Agent Version**: 1.0
