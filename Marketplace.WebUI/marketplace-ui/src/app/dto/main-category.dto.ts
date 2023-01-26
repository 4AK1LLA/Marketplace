export class MainCategoryDto {
    route!: string;
    name!: string;
    photoUrl!: string;
    subCategories!: CategoryDto[];
}

export class CategoryDto {
    route!: string;
    name!: string;
}