export class MainCategoryDto {
    route!: string;
    name!: string;
    photoUrl!: string;
    subCategories!: { route: string, name: string }[];
}

export class MainCategoryPostDto {
    name!: string;
    photoUrl!: string;
    subCategories!: CategoryDto[];
}

export class CategoryDto {
    id!: number;
    name!: string;
}