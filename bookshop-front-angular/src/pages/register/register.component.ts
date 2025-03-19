import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { lastValueFrom } from 'rxjs';
import { SharedStateService } from '../../services/shared-state.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(
    private http: HttpClient,
    public sharedState: SharedStateService,
    private router: Router
  ) { }

  async register() {
    try {
      const response = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:7001/register",
          { Email: this.sharedState.email, Password: this.sharedState.password },
          { responseType: 'text' as 'json' }
        )
      );
      this.sharedState.email = "";
      this.sharedState.password = "";
      this.router.navigate(["/"]);
      window.alert("Register successfull.");
    }
    catch (err) {
      console.log(err);
      window.alert("Register unsuccessfull.");
    }
  }
}
