import { Component } from '@angular/core';
import { TenantDto, TenantsClient } from '../web-api-client';

@Component({
  selector: 'app-tenant',
  templateUrl: './tenant.component.html',
  styleUrls: ['./tenant.component.css']
})
export class TenantComponent {
  public tenants: TenantDto[] = [];

  constructor(private tenantsClient: TenantsClient) {
    tenantsClient.getTenants().subscribe({
      next: result => this.tenants = result,
      error: error => console.error(error)
    });
  }

  startAccess(tenant: TenantDto): void {
    this.tenantsClient.startAccess(tenant.tenantId).subscribe({
      next: result => console.log(result),
      error: error => console.error(error)
    });
  }

  stopAccess(): void {
    this.tenantsClient.stopAccess().subscribe({
      next: result => console.log(result),
      error: error => console.error(error)
    });
  }
}
