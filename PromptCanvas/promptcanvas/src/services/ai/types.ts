export type AiServiceError = {
  error: string
  status?: number
  details?: unknown
}

export type AiServiceResult =
  | { ok: true; data: unknown }
  | { ok: false; error: AiServiceError }

export interface AiService {
  readonly providerName: string
  readonly apiKeyLabel: string
  sendPrompt(prompt: string, apiKey: string): Promise<AiServiceResult>
}
