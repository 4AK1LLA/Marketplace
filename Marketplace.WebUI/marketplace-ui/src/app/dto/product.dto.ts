export class ProductDto {
    id!: number;
    title!: string;
    publicationDate!: string;
    location!: string;
    tagValues!: { value: string, name: string }[];
    mainPhotoUrl!: string;

    condition?: string;
    price?: string;

    liked?: boolean;
}