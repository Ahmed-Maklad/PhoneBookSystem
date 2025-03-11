export interface IContact {
  id: number;
  name: string;
  phoneNumber: string;
  email: string;
}


export interface IContactPaginated {
  data: IContact[];
  count: number;
}

// Generic API response To Use With All API Calls
export interface IApiResponse<T> {
  currentPage: number;
  isSuccess: boolean;
  msg: string;
  data: T;
}

// Use With Single Contact When Create Or Update Contact
export type ApiResponseContact = IApiResponse<IContact>;

// Use this type to Display All Contact As a List
export type ApiResponsePaginatedContact = IApiResponse<IContactPaginated>;


