export class User {
    fullName: string = '';
    email: string = '';
    userName: string = '';
    roles: string[] = [];

    /**
     *
     */
    constructor(fullName: string, email: string, userName: string, roles: string[]) {
        this.fullName = fullName;
        this.email = email;
        this.userName = userName;
        this.roles = roles;
    }
}