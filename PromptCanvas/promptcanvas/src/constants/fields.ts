import type { FieldConfig, FormValues } from '../types/promptCanvas'

export const fields: FieldConfig[] = [
  { id: 'taskContext', label: 'Task context', type: 'string' },
  { id: 'tone', label: 'Tone context', type: 'string' },
  {
    id: 'backgroundData',
    label: 'Background data, documents, and images',
    type: 'string[]',
  },
  { id: 'rules', label: 'Detailed task description and rules', type: 'string[]' },
  { id: 'examples', label: 'Examples', type: 'string[]' },
  {
    id: 'conversationHistory',
    label: 'Include conversation history',
    type: 'boolean',
  },
  { id: 'task', label: 'Immediate task', type: 'string' },
  { id: 'reasoning', label: 'Thinking step by step', type: 'string[]' },
  { id: 'outputFormat', label: 'Output formatting', type: 'string' },
  { id: 'prefilledResponse', label: 'Prefilled response', type: 'string' },
]

const defaultValues: FormValues = {
  taskContext:
    'You are an expert JavaScript educator helping developers understand complex JavaScript concepts with clear explanations and practical examples.',
  tone: 'Friendly, clear, educational, and slightly conversational. Avoid academic jargon. Explain concepts in simple terms.',
  backgroundData: [
    'JavaScript is single-threaded but uses an event loop to handle asynchronous operations.',
    'Call Stack executes synchronous code.',
    'Microtasks include Promise callbacks and queueMicrotask.',
    'Macrotasks include setTimeout, setInterval, and DOM events.',
    'Microtasks always run before the next macrotask.',
    'https://developer.mozilla.org/en-US/docs/Web/JavaScript/Event_loop',
  ],
  rules: [
    'Explain the JavaScript event loop in simple terms.',
    'Explain Call Stack, Microtasks, and Macrotasks.',
    'Use a small code example to demonstrate execution order.',
    'Avoid overly technical wording.',
    'Structure the explanation so beginners can follow easily.',
    'Use headings and short paragraphs.',
    'Include one visual mental model if possible.',
  ],
  examples: [
    'Example explanation style: Think of the event loop like a waiter checking tasks in a restaurant queue.',
    "Example code snippet: Promise.resolve().then(() => console.log('microtask'))",
    'Example teaching style: Start simple, then add more technical detail.',
  ],
  conversationHistory: true,
  task: 'Write a beginner-friendly blog post explaining the JavaScript Event Loop and how microtasks and macrotasks work.',
  reasoning: [
    'First explain why JavaScript needs the event loop.',
    'Then describe the Call Stack.',
    'Then explain the Microtask Queue.',
    'Then explain the Macrotask Queue.',
    'Provide a code example showing execution order.',
    'Summarize the key takeaway developers should remember.',
  ],
  outputFormat:
    'Markdown article with:\n- Title\n- Short introduction\n- Section headings\n- Code blocks\n- Bullet summary at the end',
  prefilledResponse:
    '# Understanding the JavaScript Event Loop\nJavaScript may look like it runs many things at once, but in reality it runs on a single thread...\n"""',
}

export const createInitialValues = (): FormValues => {
  const values: FormValues = {
    ...defaultValues,
    backgroundData: [...(defaultValues.backgroundData as string[])],
    rules: [...(defaultValues.rules as string[])],
    examples: [...(defaultValues.examples as string[])],
    reasoning: [...(defaultValues.reasoning as string[])],
  }

  for (const field of fields) {
    if (field.type === 'string' && values[field.id] === undefined) values[field.id] = ''
    if (field.type === 'string[]' && values[field.id] === undefined) values[field.id] = ['']
    if (field.type === 'boolean' && values[field.id] === undefined) values[field.id] = false
  }

  return values
}
