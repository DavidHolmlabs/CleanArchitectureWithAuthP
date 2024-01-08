import { Component } from '@angular/core';
import { AuthUsersClient, NavMenuDto } from '../web-api-client';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;
  navMenuDto: NavMenuDto;

  constructor(
    private authUsersClient: AuthUsersClient) {
    authUsersClient.getNavMenu().subscribe({
      next: result => this.navMenuDto = result,
      error: error => console.error(error)
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
