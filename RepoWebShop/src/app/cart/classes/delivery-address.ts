
export class DeliveryAddress {
  'addressLine1': string;
  'streetNumber': string;
  'streetName': string;
  'zipCode': string;
  'state': string;
  'country': string;
  'distance': number;
  'deliveryInstructions': string;
  'deliveryCost': number;
  'deliveryAddressId': number;

  'latitude': number;
  'longitude': number;

  constructor(zipCode: string, streetNumber: string, streetName: string, lat: number, lng: number) {
    this.zipCode = zipCode;
    this.streetName = streetName;
    this.streetNumber = streetNumber;
    this.latitude = lat;
    this.longitude = lng;
  }

}

