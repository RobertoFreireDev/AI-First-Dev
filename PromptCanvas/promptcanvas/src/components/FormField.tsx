import { useState } from 'react'
import type { FieldConfig } from '../types/promptCanvas'
import CollapseToggleButton from './CollapseToggleButton'
import ListField from './ListField'
import TextField from './TextField'
import ToggleField from './ToggleField'

type FormFieldProps = {
  field: FieldConfig
  value: string | string[] | boolean
  onStringChange: (fieldId: string, nextValue: string) => void
  onBooleanChange: (fieldId: string, nextValue: boolean) => void
  onListItemChange: (fieldId: string, index: number, nextValue: string) => void
  onListItemAdd: (fieldId: string) => void
  onListItemRemove: (fieldId: string, index: number) => void
}

function FormField({
  field,
  value,
  onStringChange,
  onBooleanChange,
  onListItemChange,
  onListItemAdd,
  onListItemRemove,
}: FormFieldProps) {
  const [isTextCollapsed, setIsTextCollapsed] = useState(true)

  return (
    <div className="field">
      {field.type === 'string' ? (
        <div className="fieldLabelRow">
          <label htmlFor={field.id}>{field.label}</label>
          <CollapseToggleButton
            collapsed={isTextCollapsed}
            controlsId={`${field.id}-text`}
            onToggle={() => setIsTextCollapsed((prev) => !prev)}
            expandLabel="Expand text field"
            collapseLabel="Collapse text field"
          />
        </div>
      ) : (
        <label htmlFor={field.id}>{field.label}</label>
      )}

      {field.type === 'string' ? (
        !isTextCollapsed ? (
          <div id={`${field.id}-text`}>
            <TextField
              id={field.id}
              value={value as string}
              onChange={(nextValue) => onStringChange(field.id, nextValue)}
            />
          </div>
        ) : null
      ) : null}

      {field.type === 'string[]' ? (
        <ListField
          fieldId={field.id}
          items={value as string[]}
          onItemChange={(index, nextValue) =>
            onListItemChange(field.id, index, nextValue)
          }
          onAddItem={() => onListItemAdd(field.id)}
          onRemoveItem={(index) => onListItemRemove(field.id, index)}
        />
      ) : null}

      {field.type === 'boolean' ? (
        <ToggleField
          id={field.id}
          checked={value as boolean}
          onChange={(nextValue) => onBooleanChange(field.id, nextValue)}
        />
      ) : null}
    </div>
  )
}

export default FormField
