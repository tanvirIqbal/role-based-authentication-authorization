export class User {
    fullName: string = '';
    email: string = '';
    userName: string = '';
    role: string = '';

    /**
     *
     */
    constructor(fullName: string, email: string, userName: string, role: string) {
        this.fullName = fullName;
        this.email = email;
        this.userName = userName;
        this.role = role;
    }
}