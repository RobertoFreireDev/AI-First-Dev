import type { ButtonHTMLAttributes, ReactNode } from 'react'

type ButtonVariant = 'primary' | 'secondary'

type ButtonProps = {
  children: ReactNode
  variant?: ButtonVariant
} & ButtonHTMLAttributes<HTMLButtonElement>

const variantClassMap: Record<ButtonVariant, string> = {
  primary: 'submitBtn',
  secondary: 'secondaryBtn',
}

function Button({ children, variant = 'primary', className = '', ...rest }: ButtonProps) {
  const variantClass = variantClassMap[variant]
  const classes = `appBtn ${variantClass} ${className}`.trim()

  return (
    <button {...rest} className={classes}>
      {children}
    </button>
  )
}

export default Button
