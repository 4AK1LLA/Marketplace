export class MainCategoryDto {
    route!: string;
    name!: string;
    photoUrl!: string;
    subCategories!: { route: string, name: string }[];
}