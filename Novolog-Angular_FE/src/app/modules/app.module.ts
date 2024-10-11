
import { NgModule } from '@angular/core';
import { AppComponent } from '../app.component';
import { RouterOutlet } from '@angular/router';
import { DisplayDoctorsComponent } from '../display-doctors/display-doctors.component';
import { BrowserModule } from '@angular/platform-browser'
import { HttpClientModule} from '@angular/common/http';
import { ContactUsComponent } from '../contact-us/contact-us.component';
@NgModule({
    declarations: [
        AppComponent,
        DisplayDoctorsComponent,
        ContactUsComponent
    ],
    imports: [ RouterOutlet, BrowserModule, HttpClientModule],
    providers: [  ],
    bootstrap: [AppComponent]
})   
export class AppModule { }  