export class MainCategoryDto {
    id!: number;
    name!: string;
    photoUrl!: string;
    subCategories!: CategoryDto[];
}

export class CategoryDto {
    id!: number;
    name!: string;
}