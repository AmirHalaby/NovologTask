import { Component, Input ,OnInit} from '@angular/core';
import { DisplayDoctorsService } from '../services/display-doctors.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ResponseData } from '../models/ResponseData';

@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrl: './contact-us.component.css'
})

export class ContactUsComponent implements OnInit {

  constructor(private _service : DisplayDoctorsService){}

  @Input() fullName = '';
  @Input() phoneNumber = '';
  @Input() email = 'Amir@gmail.com';
  responseDate : ResponseData = {fullName: this.fullName ,phoneNumber: this.phoneNumber, email: this.email};

  ngOnInit(): void {


  }

  onSave(){
    this.responseDate.fullName = this.fullName
    this.responseDate.phoneNumber = this.phoneNumber
    this.responseDate.email = this.email
    this._service.contactUs(this.responseDate).subscribe({
      next:(val: any)=>{
        alert(`פרטי הקשר נשמרו בהצלחה`);
      },
      error:(err:HttpErrorResponse) => {
        if(err.name ==="HttpErrorResponse" ){
          alert(err.error)
        }
      }
    });   
   }
}
