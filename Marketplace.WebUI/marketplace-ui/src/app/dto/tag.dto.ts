export class TagDto {
    id!: number;
    name!: string;
    isRequired!: boolean;
    type!: string;
    remark?: string | null;
    possibleValues?: { value: string }[] | null;
}