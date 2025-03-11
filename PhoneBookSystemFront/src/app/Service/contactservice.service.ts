import { Injectable } from '@angular/core';
import { ApiResponseContact, ApiResponsePaginatedContact, IContact } from '../../models/icontact';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ContactserviceService {
  private ApiResponseContact: ApiResponseContact = {} as ApiResponseContact;

  constructor(private HttpClient: HttpClient) { }




  GetAllContacts(pageNumber: number, pageSize: number): Observable<ApiResponsePaginatedContact> {
    return this.HttpClient.get<ApiResponsePaginatedContact>(
      `${environment.baseUrl}/Get_All_Contact?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }



  CreateContact(contact: IContact): Observable<ApiResponseContact> {
    return this.HttpClient.post<ApiResponseContact>(`${environment.baseUrl}/Create_Contact`, contact, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  }


  UpdateContact(contact: IContact): Observable<ApiResponseContact> {
    return this.HttpClient.put<ApiResponseContact>(`${environment.baseUrl}/Update_Contact`, contact, {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  DeleteContact(contactId: number): Observable<ApiResponseContact> {
    return this.HttpClient.delete<ApiResponseContact>(`${environment.baseUrl}/Delete_Contact/${contactId}`);
  }
  SearchContact(searchQuery: string): Observable<ApiResponsePaginatedContact> {
    console.log(searchQuery);
    
    return this.HttpClient.get<ApiResponsePaginatedContact>(
      `${environment.baseUrl}/Search_For_Contact?query=${searchQuery}`
    );
  }

}
