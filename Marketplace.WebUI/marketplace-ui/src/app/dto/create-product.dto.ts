export class CreateProductDto {
    title!: string;
    description!: string;
    location!: string;
    categoryId!: number;
    tagValuesDictionary?: { [id: number]: string };
}