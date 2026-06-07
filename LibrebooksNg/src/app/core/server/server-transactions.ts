export interface IServerError {
    code: string
    description: string
}

export interface IServerResult<T = undefined> {
    succeeded: boolean
    errors: IServerError[]
    model: T
}