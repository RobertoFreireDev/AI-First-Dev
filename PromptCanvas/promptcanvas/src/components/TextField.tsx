type TextFieldProps = {
  id: string
  value: string
  rows?: number
  onChange: (nextValue: string) => void
}

function TextField({ id, value, rows = 3, onChange }: TextFieldProps) {
  return (
    <textarea
      id={id}
      value={value}
      onChange={(event) => onChange(event.target.value)}
      rows={rows}
    />
  )
}

export default TextField
