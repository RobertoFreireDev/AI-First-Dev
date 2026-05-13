import { useState } from 'react'
import CollapseToggleButton from './CollapseToggleButton'
import TextField from './TextField'

type ListFieldProps = {
  fieldId: string
  items: string[]
  onItemChange: (index: number, nextValue: string) => void
  onAddItem: () => void
  onRemoveItem: (index: number) => void
}

function ListField({
  fieldId,
  items,
  onItemChange,
  onAddItem,
  onRemoveItem,
}: ListFieldProps) {
  const [isCollapsed, setIsCollapsed] = useState(true)

  return (
    <div className="listField">
      <div className="listFieldHeader">
        <span className="listCount">{items.length} item{items.length === 1 ? '' : 's'}</span>
        <CollapseToggleButton
          collapsed={isCollapsed}
          controlsId={`${fieldId}-items`}
          onToggle={() => setIsCollapsed((prev) => !prev)}
          expandLabel="Expand list"
          collapseLabel="Collapse list"
        />
      </div>

      {!isCollapsed ? (
        <div id={`${fieldId}-items`}>
          {items.map((item, index) => (
            <div key={`${fieldId}-${index}`} className="listRow">
              <TextField
                id={`${fieldId}-${index}`}
                value={item}
                rows={2}
                onChange={(nextValue) => onItemChange(index, nextValue)}
              />
              <button
                type="button"
                className="iconBtn removeItemBtn"
                onClick={() => onRemoveItem(index)}
                disabled={items.length === 1}
                aria-label={`Remove item ${index + 1}`}
                title="Remove item"
              >
                x
              </button>
            </div>
          ))}

          <button
            type="button"
            className="iconBtn addItemBtn"
            onClick={onAddItem}
            aria-label="Add item"
            title="Add item"
          >
            +
          </button>
        </div>
      ) : null}
    </div>
  )
}

export default ListField
