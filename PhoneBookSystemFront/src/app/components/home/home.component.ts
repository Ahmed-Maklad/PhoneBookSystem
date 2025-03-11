import { ApiResponseContact } from './../../../models/icontact';
import { AfterViewChecked, ChangeDetectorRef, Component, NgZone, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ContactserviceService } from '../../Service/contactservice.service';
import { IContact, ApiResponsePaginatedContact, IApiResponse, IContactPaginated } from '../../../models/icontact';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit  {
  apiResponsePaginatedContact: IApiResponse<IContactPaginated> = {
    data: { data: [], count: 0 },
    currentPage: 1,
    isSuccess: false,
    msg: ''
  };
  apiResponseContact: ApiResponseContact = {} as ApiResponseContact;
  contact: IContact = {} as IContact;
  isEditMode: boolean = false;
  currentPage: number = 1;
  pageSize: number = 10;
  totalContacts: number = 0;
  showForm: boolean = false;
  SearchQuery: string = '';
contactForm: any;

  constructor(private _ContactService: ContactserviceService , private zone: NgZone) { }


   // Lifecycle Hook This Give All Contact When Component is Initialized
  ngOnInit() {
    this.getAllContacts();
  }

// Get All Contacts From The API With Pagination for Data
  getAllContacts() {
    this._ContactService.GetAllContacts(this.currentPage, this.pageSize).subscribe({
      next: (data: ApiResponsePaginatedContact) => {
        this.apiResponsePaginatedContact = data;
        this.totalContacts = data.data.count;
        console.log('Check ApiResponse:', this.apiResponsePaginatedContact);

      },
      error: (error) => {
        console.log('GetAll Error:', error);
      }
    });
  }

  // When User Click on Create Button Will Open The Form
  openCreateForm() {
    this.contact = {} as IContact;
    this.isEditMode = false;
    this.showForm = true;
  }

  // When User Click on Edit Button Will Open The Form adn Display Data to Edit
  openUpdateForm(contactToEdit: IContact) {
    this.contact = { ...contactToEdit };
    this.isEditMode = true;
    this.showForm = true;
  }

  // Close Form When User Click on Close Button
  closeForm() {
    this.showForm = false;
  }


  onSubmit() {
    if (this.isEditMode) {
      this.updateContact();
    } else {
      this.createContact();
    }
  }
// Create Contact
  createContact() {
    console.log('Check Contact Data:', this.contact);
    this._ContactService.CreateContact(this.contact).subscribe({
      next: (apiResponse: ApiResponseContact) => {
        this.apiResponseContact = apiResponse;
        console.log('Check ApiResponseContact:', apiResponse);
        this.getAllContacts();
        this.closeForm();
      },
      error: (error) => {
        console.log('Create Error:', error);
      }
    });
  }
// Update Contact
  updateContact() {
    this._ContactService.UpdateContact(this.contact).subscribe({
      next: (apiResponse: ApiResponseContact) => {
        this.apiResponseContact = apiResponse;
        console.log('Check ApiResponseContact:', apiResponse);
        this.getAllContacts();
        this.closeForm();
      },
      error: (error) => {
        console.log('Update Error:', error);
      }
    });
  }

  // Delete Contact
  deleteContact(contactId: number) {
    if (confirm('Are you sure you want to delete this contact?')) {
      this._ContactService.DeleteContact(contactId).subscribe({
        next: (apiResponse: ApiResponseContact) => {
          this.apiResponseContact = apiResponse;
          console.log('Check ApiResponseContact:', apiResponse);
          this.getAllContacts();
        },
        error: (error) => {
          console.log('Delete Error:', error);
        }
      });
    }
  }

  // Search Contact
  searchForContact() {
    this._ContactService.SearchContact(this.SearchQuery).subscribe({
      next: (response: any) => {
          this.apiResponsePaginatedContact = {
            data: {
              data: response.data,  
              count: response.data.length  
            },
            currentPage: 1,  
            isSuccess: response.isSuccess,
            msg: response.msg
          };
          
          console.log('Check ApiResponse after:', this.apiResponsePaginatedContact);
      },
      error: (error) => {
        console.log('Search Error:', error);
        this.apiResponsePaginatedContact = {
          data: {
            data: [],  
            count: 0 
          },
          currentPage: 1,  
          isSuccess: false,
          msg: error.error.message
        };
      }
    });
  }


  // Pagination Next Page
  goToNextPage() {
    if ((this.currentPage * this.pageSize) < this.totalContacts) {
      this.currentPage++;
      this.getAllContacts();
    }
  }


// Pagination Prev Page
  goToPreviousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.getAllContacts();
    }
  }


  totalPages(): number {
    return Math.ceil(this.totalContacts / this.pageSize) || 1;
  }

// Clear The Sea[disabled]="contactForm.invalid"rch Query
  clearSearch()
  {
    this.SearchQuery = '';
    this.getAllContacts();
  }
  
}
