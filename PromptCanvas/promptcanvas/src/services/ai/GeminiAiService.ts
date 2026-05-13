import type { AiService, AiServiceResult } from './types'

const GEMINI_MODEL = 'gemini-2.0-flash'

export class GeminiAiService implements AiService {
  readonly providerName = 'Gemini'
  readonly apiKeyLabel = 'Google API Key'

  async sendPrompt(prompt: string, apiKey: string): Promise<AiServiceResult> {
    try {
      const response = await fetch(
        `https://generativelanguage.googleapis.com/v1beta/models/${GEMINI_MODEL}:generateContent?key=${encodeURIComponent(apiKey)}`,
        {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            contents: [{ parts: [{ text: prompt }] }],
          }),
        },
      )

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
