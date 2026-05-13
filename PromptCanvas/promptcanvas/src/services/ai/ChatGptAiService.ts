import type { AiService, AiServiceResult } from './types'

const OPENAI_MODEL = 'gpt-3.5-turbo'

export class ChatGptAiService implements AiService {
  readonly providerName = 'ChatGPT'
  readonly apiKeyLabel = 'OpenAI API Key'

  async sendPrompt(prompt: string, apiKey: string): Promise<AiServiceResult> {
    try {
      const response = await fetch('https://api.openai.com/v1/responses', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${apiKey}`,
        },
        body: JSON.stringify({
          model: OPENAI_MODEL,
          input: prompt,
        }),
      })

      if (!response.ok) {
        const errorPayload = await response.json().catch(() => ({}))
        return {
          ok: false,
          error: {
            error: `${this.providerName} API request failed.`,
            status: response.status,
            details: errorPayload,
          },
        }
      }

      const responsePayload = await response.json()
      return { ok: true, data: responsePayload }
    } catch (error) {
      return {
        ok: false,
        error: {
          error: `Unable to reach ${this.providerName} API.`,
          details: error instanceof Error ? error.message : 'Unknown error',
        },
      }
    }
  }
}
