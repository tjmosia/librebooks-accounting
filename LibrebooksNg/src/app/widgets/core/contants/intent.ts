export const intents = {
    error: "error",
    success: "success",
    warning: "warning",
    info: "info"
} as const

export type Intent = typeof intents[keyof typeof intents];

export const intentClasses: { [key in Intent]: string } = {
    [intents.error]: "wx-intent-error",
    [intents.success]: "wx-intent-success",
    [intents.warning]: "wx-intent-warning",
    [intents.info]: "wx-intent-info"
}
