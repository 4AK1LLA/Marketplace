export interface BasicInfo {
    title: string;
    description: string;
    location: string;
    categoryId: number;
}

export interface TagValue {
    tagId: number;
    value: string;
}

export interface ToastInfo {
    header: string;
    body: string;
    delay?: number;
}