import { useMemo, useState } from 'react'
import type { FormEvent } from 'react'
import './App.css'
import Button from './components/Button'
import FormField from './components/FormField'
import PromptOutput from './components/PromptOutput'
import { createInitialValues, fields } from './constants/fields'
import type { AiService } from './services/ai/types'
import type { FormValues } from './types/promptCanvas'
import { assemblePrompt } from './utils/promptAssembly'

const DEFAULT_API_KEY_STORAGE_KEY = 'promptcanvas.AIApiKey'

const formatAiResponse = (response: unknown) => JSON.stringify(response, null, 2)

type AppProps = {
  aiService: AiService
}

function App({ aiService }: AppProps) {
  const [values, setValues] = useState<FormValues>(() => createInitialValues())
  const [assembledPrompt, setAssembledPrompt] = useState('')
  const [aiResponse, setAiResponse] = useState('')
  const [isAiLoading, setIsAiLoading] = useState(false)
  const [isKeyDialogOpen, setIsKeyDialogOpen] = useState(false)
  const [aiApiKeyInput, setAiApiKeyInput] = useState(
    () => localStorage.getItem(DEFAULT_API_KEY_STORAGE_KEY) ?? '',
  )
  const [keySavedNotice, setKeySavedNotice] = useState('')

  const promptPreview = useMemo(() => assembledPrompt, [assembledPrompt])

  const requestAi = async () => {
    if (!assembledPrompt.trim()) {
      setAiResponse(
        JSON.stringify({ error: 'Assemble a payload before sending the AI request.' }, null, 2),
      )
      return
    }

    const payload = assembledPrompt
    setAiResponse('')

    const apiKey = localStorage.getItem(DEFAULT_API_KEY_STORAGE_KEY)?.trim() ?? ''
    if (!apiKey) {
      setAiResponse(
        JSON.stringify(
          { error: `Missing API key. Open "${aiService.apiKeyLabel}" and save a valid key first.` },
          null,
          2,
        ),
      )
      return
    }

    setIsAiLoading(true)
    try {
      const result = await aiService.sendPrompt(payload, apiKey)
      if (!result.ok) {
        setAiResponse(formatAiResponse(result.error))
        return
      }
      setAiResponse(formatAiResponse(result.data))
    } catch (error) {
      setAiResponse(
        JSON.stringify(
          {
            error: `Unexpected ${aiService.providerName} integration error.`,
            details: error instanceof Error ? error.message : 'Unknown error',
          },
          null,
          2,
        ),
      )
    } finally {
      setIsAiLoading(false)
    }
  }

  const onSubmit = (event: FormEvent) => {
    event.preventDefault()
    setAssembledPrompt(assemblePrompt(values))
    setAiResponse('')
  }

  const updateStringField = (fieldId: string, nextValue: string) => {
    setValues((prev) => ({ ...prev, [fieldId]: nextValue }))
  }

  const updateBooleanField = (fieldId: string, nextValue: boolean) => {
    setValues((prev) => ({ ...prev, [fieldId]: nextValue }))
  }

  const updateListField = (fieldId: string, index: number, nextValue: string) => {
    setValues((prev) => {
      const currentList = [...(prev[fieldId] as string[])]
      currentList[index] = nextValue
      return { ...prev, [fieldId]: currentList }
    })
  }

  const addListItem = (fieldId: string) => {
    setValues((prev) => {
      const currentList = [...(prev[fieldId] as string[])]
      return { ...prev, [fieldId]: [...currentList, ''] }
    })
  }

  const removeListItem = (fieldId: string, index: number) => {
    setValues((prev) => {
      const currentList = [...(prev[fieldId] as string[])]
      if (currentList.length === 1) return prev
      currentList.splice(index, 1)
      return { ...prev, [fieldId]: currentList }
    })
  }

  const openKeyDialog = () => {
    setIsKeyDialogOpen(true)
    setKeySavedNotice('')
  }

  const closeKeyDialog = () => {
    setIsKeyDialogOpen(false)
    setKeySavedNotice('')
  }

  const onSaveAIApiKey = (event: FormEvent) => {
    event.preventDefault()
    localStorage.setItem(DEFAULT_API_KEY_STORAGE_KEY, aiApiKeyInput.trim())
    setKeySavedNotice(`${aiService.apiKeyLabel} saved locally.`)
  }

  const apiKeyPlaceholder = (() => {
    switch (aiService.providerName) {
      case 'Gemini':
        return 'AIza...'
      case 'OpenRouter':
        return 'sk-or-v1-...'
      default:
        return 'sk-...'
    }
  })()

  return (
    <main className="page">
      <section className="canvas">
        <div className="canvasHeader">
          <h1>PromptCanvas</h1>
          <Button type="button" variant="secondary" className="apiKeyBtn" onClick={openKeyDialog}>
            {aiService.apiKeyLabel}
          </Button>
        </div>
        <p className="subtitle">Build a structured {aiService.providerName} prompt using guided sections.</p>

        <form className="form" onSubmit={onSubmit}>
          {fields.map((field) => (
            <FormField
              key={field.id}
              field={field}
              value={values[field.id]}
              onStringChange={updateStringField}
              onBooleanChange={updateBooleanField}
              onListItemChange={updateListField}
              onListItemAdd={addListItem}
              onListItemRemove={removeListItem}
            />
          ))}

          <Button type="submit">
            Assemble Prompt
          </Button>
        </form>
      </section>

      <PromptOutput
        value={promptPreview}
        responseValue={aiResponse}
        isLoadingResponse={isAiLoading}
        providerName={aiService.providerName}
        onRequest={requestAi}
      />

      {isKeyDialogOpen && (
        <div className="dialogOverlay" role="presentation" onClick={closeKeyDialog}>
          <section
            className="dialogCard"
            role="dialog"
            aria-modal="true"
            aria-labelledby="ai-api-key-title"
            onClick={(event) => event.stopPropagation()}
          >
            <h2 id="ai-api-key-title">{aiService.apiKeyLabel}</h2>
            <p className="subtitle">Paste your key below to store it in local browser storage.</p>
            <form className="dialogForm" onSubmit={onSaveAIApiKey}>
              <label htmlFor="ai-api-key-input">API key</label>
              <input
                id="ai-api-key-input"
                type="password"
                value={aiApiKeyInput}
                onChange={(event) => setAiApiKeyInput(event.target.value)}
                autoComplete="off"
                placeholder={apiKeyPlaceholder}
              />
              <div className="dialogActions">
                <Button type="submit">
                  Save
                </Button>
                <Button type="button" variant="secondary" onClick={closeKeyDialog}>
                  Close
                </Button>
              </div>
              {keySavedNotice && <p className="savedNotice">{keySavedNotice}</p>}
            </form>
          </section>
        </div>
      )}
    </main>
  )
}

export default App
