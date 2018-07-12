export interface ICartCatalogItem {
    shoppingCartCatalogItemId: number;
    product: {
      productId: number;
      name: string;
      description: string;
      oldPrice: number;
      oldPriceInStore: number;
      price: number;
      multipleAmount: number;
      sizeDescription: string;
      flavour: string;
      priceInStore: number;
      category: string;
      temperature: string;
      minOrderAmount: number;
      portions: number;
      preparationTime: number;
      isActive: true,
      isOnSale: true,
      pieDetail: {
        pieDetailId: number;
        name: string;
        shortDescription: string;
        longDescription: string;
        flickrAlbumId: number;
        isPieOfTheWeek: true,
        isActive: true,
        ingredients: string
      },
      pieDetailId: number;
      displayName: string;
      displayDescription: string
    };
    shoppingCartId: string;
    created: object;
    amount: number;
}
