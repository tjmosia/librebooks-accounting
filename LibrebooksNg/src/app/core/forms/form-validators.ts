const emailRegex = /^[\w.!#$%&'*+/=?^`{|}~-]+@[a-z\d](?:[a-z\d-]{0,61}[a-z\d])?(?:\.[a-z\d](?:[a-z\d-]{0,61}[a-z\d])?)*$/i
const passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.*\s).{8,}/

export const validators = {
    isValidEmail: (value: string) => emailRegex.test(value),
    isValidPassword: (value: string) => passwordRegex.test(value),
}


export { validators as formValidators }