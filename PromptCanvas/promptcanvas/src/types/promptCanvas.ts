export type FieldType = 'string' | 'string[]' | 'boolean'

export type FieldConfig = {
  id: string
  label: string
  type: FieldType
}

export type FormValue = string | string[] | boolean

export type FormValues = Record<string, FormValue>
