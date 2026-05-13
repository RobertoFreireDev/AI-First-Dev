import Button from './Button'

type PromptOutputProps = {
  value: string
  responseValue: string
  isLoadingResponse: boolean
  providerName: string
  onRequest: () => void
}

const escapeHtml = (text: string) =>
  text.replaceAll('&', '&amp;').replaceAll('<', '&lt;').replaceAll('>', '&gt;')

const highlightJson = (input: string) => {
  if (!input.trim()) return ''

  let formatted = input
  try {
    formatted = JSON.stringify(JSON.parse(input), null, 2)
  } catch {
    return `<span class="jsonToken jsonString">${escapeHtml(input)}</span>`
  }

  const escaped = escapeHtml(formatted)
  return escaped.replace(
    /("(\\u[\da-fA-F]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g,
    (match) => {
      if (match.startsWith('"') && match.endsWith(':')) {
        return `<span class="jsonToken jsonKey">${match}</span>`
      }
      if (match.startsWith('"')) {
        return `<span class="jsonToken jsonString">${match}</span>`
      }
      if (match === 'true' || match === 'false') {
        return `<span class="jsonToken jsonBoolean">${match}</span>`
      }
      if (match === 'null') {
        return `<span class="jsonToken jsonNull">${match}</span>`
      }
      return `<span class="jsonToken jsonNumber">${match}</span>`
    },
  )
}

function PromptOutput({ value, responseValue, isLoadingResponse, providerName, onRequest }: PromptOutputProps) {
  const rendered = highlightJson(value)
  const renderedResponse = highlightJson(responseValue)

  return (
    <section className="output">
      <h2>{providerName} API payload</h2>
      {value ? (
        <pre className="jsonViewer" aria-label={`Assembled ${providerName} API JSON payload`}>
          <code dangerouslySetInnerHTML={{ __html: rendered }} />
        </pre>
      ) : (
        <div className="jsonPlaceholder">
        </div>
      )}
      <Button
        type="button"
        onClick={onRequest}
        disabled={!value.trim() || isLoadingResponse}
      >
        {isLoadingResponse ? 'Requesting...' : 'Send Request'}
      </Button>
      <h2>{providerName} API response</h2>
      {isLoadingResponse ? (
        <pre className="jsonViewer" aria-label={`${providerName} API response loading`}>
          <code>{'{\n  "status": "Loading response..."\n}'}</code>
        </pre>
      ) : responseValue ? (
        <pre className="jsonViewer" aria-label={`${providerName} API JSON response`}>
          <code dangerouslySetInnerHTML={{ __html: renderedResponse }} />
        </pre>
      ) : (
        <div className="jsonPlaceholder">
        </div>
      )}
    </section>
  )
}

export default PromptOutput
