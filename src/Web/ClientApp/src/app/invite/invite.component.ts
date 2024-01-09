import { Component } from '@angular/core';
import { AuthUsersClient, InviteDto } from '../web-api-client';

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.css']
})
export class InviteComponent {
  email: string;
  inviteDto: InviteDto = new InviteDto();

  constructor(
    private authUsersClient: AuthUsersClient) {
  }

  invite() {
    this.authUsersClient.invite(this.email).subscribe({
      next: result => this.inviteDto = result,
      error: error => console.error(error)
    });
  }
}
