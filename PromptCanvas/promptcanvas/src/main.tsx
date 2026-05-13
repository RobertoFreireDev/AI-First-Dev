import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { OpenRouterAiService } from './services/ai/OpenRouterAiService.ts'

const aiService = new OpenRouterAiService()

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App aiService={aiService} />
  </StrictMode>,
)
