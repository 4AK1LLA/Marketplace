export class ProductDto {
    id!: number;
    title!: string;
    publicationDate!: string;
    location!: string;
    mainPhotoUrl!: string;
    condition?: string;
    priceInfo?: string;

    liked?: boolean;
}