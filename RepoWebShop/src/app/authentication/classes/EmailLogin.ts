export class EmailLogin {
    email: string;
    password: string;
    constructor (email: string, pass: string) {
        this.email = email;
        this.password = pass;
    }
}
