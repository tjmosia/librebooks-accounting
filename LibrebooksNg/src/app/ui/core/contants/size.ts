
export const sizes = {
  small: "small",
  medium: "medium",
  large: "large",
} as const

export type Size = typeof sizes[keyof typeof sizes]

export const sizeClasses = {
  [sizes.small]: 'wx--small',
  [sizes.medium]: 'wx--medium',
  [sizes.large]: 'wx--large',
};
