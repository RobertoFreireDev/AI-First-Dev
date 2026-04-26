# AI-First-Dev

AI-first development is a paradigm where artificial intelligence is the core driver of the software lifecycle, rather than an afterthought, placing AI agents at the center of planning, coding, and testing

## AGENTS.md

**Problem:** AI agent doesn’t know your project. AI agent needs different kinds of information: what is the build command? What is the style guide? How do you run an individual test? Which architectural patterns should be followed?

**Solution:** Create a “README for machines.” That’s what AGENTS.md (Markdown file) is about—and the entire ecosystem of Agent Skills that is emerging around it. 

The core principle: be concise and practical. No long texts explaining the project’s philosophy. The agent needs clear rules and executable commands.

AGENTS.md is an emerging, ecosystem-agnostic convention for documenting projects for AI agents, but it is not an official standard. Some tools—like Claude Code—use their own formats (e.g., CLAUDE.md) as the primary source of instructions

In a monorepo, global rules are typically defined at the root, with more specific or overriding rules defined within each package.

Each tool (e.g., Cursor, Claude Code) defines its own rules for prioritizing and discovering instruction files (such as "AGENTS.md", "CLAUDE.md", etc.). These tools may also automatically include those files in the agent’s context for new sessions, but the behavior is tool-specific and not standardized

## Spec Driven Development

<p align="center">
  <img src="./imagens/specdrivendevelopmentworkflow.png?raw=true">
</p>

## Monorepo

Monorepo-style development is a software development approach where:

- You develop multiple projects in the same repository.
- The projects can depend on each other, so they can share code.
- When you make a change, you do not rebuild or retest every project in the monorepo. Instead, you only rebuild and retest the projects that can be affected by your change.

### Suggestion:

Use Nx. Smart Monorepo Build System & CI

### Risks:

#### Repository governance risk:

Without proper code ownership and review policies, other teams may modify code without the owning team’s awareness.

**Solution:** Use CODEOWNERS in GitHub. It automatically assigns reviewers (individuals or teams) based on matching rules for directories, files, or extensions. With branch protection enabled, pull requests can’t be merged without approval from the designated owners.

#### Big Ball of Mud:

When multiple projects share a monorepo without clear boundaries, the codebase can quickly become a Big Ball of Mud—a tangled, hard-to-understand system where dependencies are unclear, changes feel risky, and overall maintainability declines

**Solution:** In .NET (C#), split modules into separate projects (assemblies) to enforce boundaries, define clear namespaces per module, and mark implementation types as `internal` by default so they are inaccessible outside the module. Complement this with architectural tests to automatically detect and prevent boundary violations.

#### Codebase is too big for AI

A common concern is that AI coding agents can't handle monorepos because there's too much code, too many projects, too much context

**Solution:** Configure Tasks with well-defined scopes and constrained context, ensuring the agent operates only within a specific module or set of relevant files instead of the entire codebase

#### Large-scale changes

Changes on a shared library will affect all the applications that depend on it.

**Solution:** In .NET, publish shared code as versioned nuget packages and require consumers to opt into upgrades rather than inheriting changes automatically. Also, CI pipelines should run cross-project tests and impact analysis to detect breaking changes early

## References:

- [AGENTS.md](https://agents.md/)
- [AGENTS.md and Agent Skills](https://www.techleads.club/c/blog/agents-md-e-agent-skills-por-debaixo-dos-panos-dos-ai-coding-agents)
- [nx.dev](https://nx.dev/docs/getting-started/intro)
- [monorepo](https://nx.dev/blog/monorepo-is-not-monolith#misconceptions)
- [CODEOWNERS](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/about-code-owners)
- [Enterprise Spec Driven Development](https://www.infoq.com/articles/enterprise-spec-driven-development/)
- [run-aspnetcore-microservices](https://github.com/aspnetrun/run-aspnetcore-microservices)