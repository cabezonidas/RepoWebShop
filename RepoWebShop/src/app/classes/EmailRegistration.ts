export class EmailRegistration {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    constructor (name: string, lastName: string, email: string, pass: string) {
        this.email = email;
        this.password = pass;
        this.firstName = name;
        this.lastName = lastName;
    }
}
