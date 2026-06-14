export const intents = {
    error: "error",
    success: "success",
    warning: "warning",
    info: "info",
    none: "none",
} as const

export type Intent = (typeof intents)[keyof typeof intents];

export const intentClasses = {
  [intents.error]: 'wx--error',
  [intents.success]: 'wx--success',
  [intents.warning]: 'wx--warning',
  [intents.info]: 'wx--info',
  [intents.none]: '',
};
