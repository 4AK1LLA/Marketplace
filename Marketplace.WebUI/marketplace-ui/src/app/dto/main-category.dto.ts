export class MainCategoryDto {
    route!: number;
    name!: string;
    photoUrl!: string;
    subCategories!: CategoryDto[];
}

export class CategoryDto {
    route!: number;
    name!: string;
}