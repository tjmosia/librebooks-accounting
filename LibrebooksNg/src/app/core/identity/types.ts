export interface IClaim {
    type: string
    value: string
}

export interface IUser {
    name: string
    surname: string
    email: string
    roles: string[]
    claims: IClaim[]
}