
export interface IApiError {
  code: string
  description: string
}

export interface IApiResult<T = undefined>{
  succeeded: boolean,
  errors: IApiError[],
  model: T
}
