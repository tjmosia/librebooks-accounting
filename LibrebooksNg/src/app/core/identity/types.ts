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
    company?: ICompany
}

export interface ICompany {
  id: string;
  name: string;
  logo: string;
}

export interface IIdentity {
  user: IUser;
  roles: string[];
  claims: IClaim[];
  company?: ICompany;
}
