export class ProductDto {
    id!: number;
    title!: string;
    description!: string;
    publicationDate!: Date;
    location!: string;
    tagValues!: { value: string, name: string }[];
    photos!: { id: number, isMain: boolean, url: string }[];
}