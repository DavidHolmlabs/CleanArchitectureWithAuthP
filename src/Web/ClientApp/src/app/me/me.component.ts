import { Component } from '@angular/core';
import { AuthUserInfoDto, AuthUsersClient } from '../web-api-client';

@Component({
  selector: 'app-me',
  templateUrl: './me.component.html',
  styleUrls: ['./me.component.css']
})
export class MeComponent {
  public me: AuthUserInfoDto;

  constructor(
    private authUsersClient: AuthUsersClient) {
    authUsersClient.getAuthUserInfo().subscribe({
      next: result => this.me = result,
      error: error => console.error(error)
    });
  }
}
