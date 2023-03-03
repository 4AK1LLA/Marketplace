export class TagDto {
    constructor(id: number, name: string, isRequired: boolean, type: string, possibleValues?: string[] | null, remark?: string | null) {
        this.id = id;
        this.name = name;
        this.isRequired = isRequired;
        this.type = type;
        this.possibleValues = possibleValues;
        this.remark = remark;
    }

    id!: number;
    name!: string;
    isRequired!: boolean;
    type!: string;
    remark?: string | null;
    possibleValues?: string[] | null;
}