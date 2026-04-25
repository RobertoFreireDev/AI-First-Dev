# AI-First-Dev

AI-first development is a paradigm where artificial intelligence is the core driver of the software lifecycle, rather than an afterthought, placing AI agents at the center of planning, coding, and testing

## Monorepo

Monorepo-style development is a software development approach where:

- You develop multiple projects in the same repository.
- The projects can depend on each other, so they can share code.
- When you make a change, you do not rebuild or retest every project in the monorepo. Instead, you only rebuild and retest the projects that can be affected by your change.

### Risks:

#### Repository governance risk:

In a monorepo, without proper code ownership and review policies, other teams may modify code without the owning team’s awareness.

Solution: Implement CODEOWNERS on GitHub. Owners are automatically requested for review based on matching rules. PR cannot be merged without owner approval


## References:

- [monorepo](https://nx.dev/blog/monorepo-is-not-monolith#misconceptions)