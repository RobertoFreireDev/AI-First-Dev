type CollapseToggleButtonProps = {
  collapsed: boolean
  controlsId: string
  onToggle: () => void
  expandLabel: string
  collapseLabel: string
}

function CollapseToggleButton({
  collapsed,
  controlsId,
  onToggle,
  expandLabel,
  collapseLabel,
}: CollapseToggleButtonProps) {
  const label = collapsed ? expandLabel : collapseLabel

  return (
    <button
      type="button"
      className="iconBtn secondaryBtn collapseToggleBtn"
      onClick={onToggle}
      aria-expanded={!collapsed}
      aria-controls={controlsId}
      aria-label={label}
      title={label}
    >
      {collapsed ? '\u25BC' : '\u25B2'}
    </button>
  )
}

export default CollapseToggleButton
