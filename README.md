# Marketplace web project :smiley:
## Planned features
- Users can buy and sell products;
- Sign up & sign in;
- Search products by categories;
- Chat between buyer and seller;
- Like product feature;
- ...

## Products, Categories and Tags
Users will interact with main categories that may contain subcategories with related tags, and products of every category may have tag values related to this category. For example, product dto will be like this:

    product: {
      id: 1,
      title: "Table for work and study",
      description: "Very good table",
      category: "Table",
      tags: [
        "Price": "100",
        "Size": "10x5",
        "Material": "Wood"
      ]
    }

## UI implementation
- Navbar is a navigation header that is placed at the top of the page
- Main categories is a section where users can pick what they need
- Sidebar is an extended version of main categories wich has subcategories for routing users to products page
