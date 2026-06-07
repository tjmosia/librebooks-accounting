export interface IFormField<T = string> {
    value?: T;
    errorMessage?: string,
    erred?: boolean
}