
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

  constructor(place: google.maps.places.PlaceResult) {
    const zipCode = place.address_components.find(x => x.types.includes('postal_code'));
    if (zipCode) {
      this.zipCode = zipCode.long_name;
    }
    const streetNumber = place.address_components.find(x => x.types.includes('street_number'));
    if (streetNumber) {
      this.streetNumber = streetNumber.long_name;
    }
    const streetName = place.address_components.find(x => x.types.includes('route'));
    if (streetName) {
      this.streetName = streetName.long_name;
    }
    if (place.geometry) {
      this.latitude = place.geometry.location.lat();
      this.longitude = place.geometry.location.lng();
    }
  }
}

