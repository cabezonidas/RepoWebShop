
export class DeliveryAddress {
  'addressLine1': string;
  'streetNumber': string;
  'streetName': string;
  'state': string;
  'country': string;
  'distance': number;
  'deliveryInstructions': string;
  'deliveryCost': number;
  'deliveryAddressId': number;

  'latitude': number;
  'longitude': number;

  constructor(streetNumber: string, streetName: string, lat: number, lng: number) {
    this.streetName = streetName;
    this.streetNumber = streetNumber;
    this.latitude = lat;
    this.longitude = lng;
  }

}

