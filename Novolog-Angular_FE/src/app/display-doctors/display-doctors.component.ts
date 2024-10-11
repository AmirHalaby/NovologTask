import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { DisplayDoctorsService } from '../services/display-doctors.service';
import { Doctors } from '../models/Doctors';
import { ContactUsComponent } from '../contact-us/contact-us.component';
@Component({
  selector: 'app-display-doctors',
  templateUrl: './display-doctors.component.html',
  styleUrl: './display-doctors.component.css'
})
export class DisplayDoctorsComponent implements OnInit {

  listOfDoctors: Doctors[] = [];
  constructor(private service : DisplayDoctorsService){}

  ngOnInit(): void {
    this.service.GetDoctors().subscribe({ 
      next:(event: any)=>{
        this.listOfDoctors = event;
      },
      error:(err: HttpErrorResponse) => {
        console.log(err);
      }
    }); ;
  } 
  // onSubmit() : void {
  //   this._dialog.open(ContactUsComponent, {//ContactUsComponent
  //     data: newAttraction
  //   });
  // }
}