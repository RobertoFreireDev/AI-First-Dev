type ToggleFieldProps = {
  id: string
  checked: boolean
  onChange: (nextValue: boolean) => void
}

function ToggleField({ id, checked, onChange }: ToggleFieldProps) {
  return (
    <label className="switch" htmlFor={`${id}-toggle`}>
      <input
        id={`${id}-toggle`}
        type="checkbox"
        checked={checked}
        onChange={(event) => onChange(event.target.checked)}
      />
      <span className="slider" />
    </label>
  )
}

export default ToggleField
