---
description: Guidelines for building PromptCanvas --- a clean, cozy, and
  friendly React interface that helps users structure prompts for AI
  using an interactive form canvas.
name: prompt-canvas
---

# PromptCanvas Skill

PromptCanvas is a clean, cozy, and friendly prompt builder UI that helps
users construct structured prompts for AI using an intuitive
form-based canvas.

The goal is to remove friction from prompt engineering by transforming a
raw prompt into guided structured inputs.

Users fill in sections, and the system composes a final prompt
automatically.

------------------------------------------------------------------------

# Form UI Types

PromptCanvas should automatically choose UI components based on field
type.

  Field Type       UI Component
  ---------------- --------------------
  string           textarea
  string\[\]       multiple textarea
  boolean          toggle switch

------------------------------------------------------------------------

# Example React Field Config

``` js
const fields = [
  { id: "taskContext", label: "Task context", type: "string" },
  { id: "tone", label: "Tone context", type: "string" },
  { id: "backgroundData", label: "Background data", type: "string[]" },
  { id: "rules", label: "Detailed task description and rules", type: "string[]" },
  { id: "examples", label: "Examples", type: "string[]" },
  { id: "conversationHistory", label: "Include conversation history", type: "boolean" },
  { id: "task", label: "Immediate task", type: "string" },
  { id: "reasoning", label: "Thinking step by step", type: "string[]" },
  { id: "outputFormat", label: "Output formatting", type: "string" },
  { id: "prefilledResponse", label: "Prefilled response", type: "string" }
]
```

------------------------------------------------------------------------

# Prompt Assembly

When the user submits the form, PromptCanvas composes a structured
prompt.

Example output:

- Task Context: {taskContext}
- Tone: {toneContext}
- Background Data: {backgroundData}
- Rules: {rules}
- Examples: {examples}
- Conversation History: {history}
- Immediate Task: {task}
- Reasoning Guidance: {reasoning}
- Output Format: {outputFormat}
- Prefilled Response: {prefilledResponse}

------------------------------------------------------------------------