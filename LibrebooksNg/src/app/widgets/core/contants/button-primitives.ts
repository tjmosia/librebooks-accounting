
export const buttonVariants = Object.freeze({
    primary: "primary",
    outlined: "outlined",
    minimal: "minimal"
})

export type ButtonVariant = typeof buttonVariants[keyof typeof buttonVariants];

export const buttonVariantClasses: { [key in ButtonVariant]: string } = {
    [buttonVariants.primary]: "wx--primary",
    [buttonVariants.outlined]: "wx--outlined",
    [buttonVariants.minimal]: "wx--minimal"
}

export const buttonSizes = Object.freeze({
    small: "small",
    medium: "medium",
    large: "large"
})

export type ButtonSize = typeof buttonSizes[keyof typeof buttonSizes];

export const buttonSizeClasses: { [key in ButtonSize]: string } = {
    [buttonSizes.small]: "wx--small",
    [buttonSizes.medium]: "wx--medium",
    [buttonSizes.large]: "wx--large"
}