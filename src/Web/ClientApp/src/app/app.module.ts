import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TodoComponent } from './todo/todo.component';
import { OrderComponent } from './order/order.component';
import { TenantComponent } from './tenant/tenant.component';
import { ProductComponent } from './product/product.component';
import { UserComponent } from './user/user.component';
import { MeComponent } from './me/me.component';
import { CreateDemoComponent } from './create-demo/create-demo.component';
import { InviteComponent } from './invite/invite.component';
import { AcceptInviteComponent } from './accept-invite/accept-invite.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TodoComponent,
    OrderComponent,
    ProductComponent,
    TenantComponent,
    UserComponent,
    MeComponent,
    CreateDemoComponent,
    InviteComponent, AcceptInviteComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'todo', component: TodoComponent },
      { path: 'order', component: OrderComponent },
      { path: 'product', component: ProductComponent },
      { path: 'tenant', component: TenantComponent },
      { path: 'user', component: UserComponent },
      { path: 'me', component: MeComponent },
      { path: 'demo', component: CreateDemoComponent },
      { path: 'invite', component: InviteComponent },
      { path: 'accept-invite', component: AcceptInviteComponent },
    ]),
    BrowserAnimationsModule,
    ModalModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
