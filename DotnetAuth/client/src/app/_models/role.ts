export class Role {
    name: string;
    isSelected: boolean;

    /**
     *
     */
    constructor(name: string, isSelected:boolean = false) {
        this.name = name;
        this.isSelected = isSelected;
    }
}