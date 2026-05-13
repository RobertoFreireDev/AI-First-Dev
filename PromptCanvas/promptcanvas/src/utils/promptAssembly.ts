import type { FormValues } from '../types/promptCanvas'

const formatList = (items: string[]) => {
  const cleanItems = items.map((item) => item.trim()).filter(Boolean)
  if (cleanItems.length === 0) return '(none)'
  return cleanItems.join(' | ')
}

export const assemblePrompt = (values: FormValues) => {
  const payload = {
    'Task Context': values.taskContext as string,
    Tone: values.tone as string,
    'Background Data': formatList(values.backgroundData as string[]),
    Rules: formatList(values.rules as string[]),
    Examples: formatList(values.examples as string[]),
    'Conversation History': (values.conversationHistory as boolean) ? 'Included' : 'Not included',
    'Immediate Task': values.task as string,
    'Reasoning Guidance': formatList(values.reasoning as string[]),
    'Output Format': values.outputFormat as string,
    'Prefilled Response': values.prefilledResponse as string,
  }

  return JSON.stringify(payload, null, 2)
}
