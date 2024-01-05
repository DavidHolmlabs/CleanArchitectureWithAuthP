import { Component } from '@angular/core';
import { AuthUser, AuthUsersClient } from '../web-api-client';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent {
  users: AuthUser[] = [];

  constructor(
    private authUsersClient: AuthUsersClient) {
    authUsersClient.getAuthUsers().subscribe({
      next: result => this.users = result,
      error: error => console.error(error)
    });
  }
}
