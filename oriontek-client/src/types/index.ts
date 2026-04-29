export interface Address {
  id: string;
  clientId: string;
  street: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
}

export interface Client {
  id: string;
  name: string;
  email: string;
  phone: string;
  createdAt: string;
  addresses: Address[];
}

export interface CreateClientDto {
  name: string;
  email: string;
  phone: string;
}

export interface UpdateClientDto {
  name: string;
  email: string;
  phone: string;
}

export interface CreateAddressDto {
  street: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
}

export interface UpdateAddressDto {
  street: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
}