import type { AiService, AiServiceResult } from './types'

// OpenRouter exposes an OpenAI-compatible Chat Completions API.
// Docs: https://openrouter.ai/docs (endpoint: /api/v1/chat/completions)
const OPENROUTER_MODEL = 'openai/gpt-4o-mini'

export class OpenRouterAiService implements AiService {
  readonly providerName = 'OpenRouter'
  readonly apiKeyLabel = 'OpenRouter API Key'

  async sendPrompt(prompt: string, apiKey: string): Promise<AiServiceResult> {
    try {
      const response = await fetch('https://openrouter.ai/api/v1/chat/completions', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${apiKey}`,
          // Optional but recommended by OpenRouter. These help with analytics / attribution.
          'HTTP-Referer': window.location.origin,
          'X-Title': 'PromptCanvas',
        },
        body: JSON.stringify({
          model: OPENROUTER_MODEL,
          messages: [{ role: 'user', content: prompt }],
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

