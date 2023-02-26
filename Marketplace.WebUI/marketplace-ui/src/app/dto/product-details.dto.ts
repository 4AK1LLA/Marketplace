export class ProductDetailsDto {
    id!: number;
    title!: string;
    description!: string;
    publicationDate!: string;
    location!: string;
    tagValues!: { value: string, name: string }[];
    photos!: { isMain: boolean, url: string }[];
}