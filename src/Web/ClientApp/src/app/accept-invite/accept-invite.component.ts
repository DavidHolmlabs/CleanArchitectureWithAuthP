import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AcceptInviteCommand, AuthUsersClient } from '../web-api-client';

@Component({
  selector: 'app-accept-invite',
  templateUrl: './accept-invite.component.html',
  styleUrls: ['./accept-invite.component.css']
})

export class AcceptInviteComponent {
  acceptInviteCommand: AcceptInviteCommand = new AcceptInviteCommand();

  constructor(
    private activatedRoute: ActivatedRoute,
    private authUsersClient: AuthUsersClient,
    private router: Router) {
    this.activatedRoute.queryParams.subscribe((result: Params) => {
      this.acceptInviteCommand.verify = result["verify"];
    });
  }

  register() {
    this.authUsersClient.acceptInvite(this.acceptInviteCommand).subscribe({
      next: result => this.router.navigate(['/order']),
      error: error => console.error(error)
    });
  }
}
