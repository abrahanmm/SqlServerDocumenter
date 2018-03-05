import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { ServersComponent } from './components/servers/servers.component';
import { DatabasesComponent } from './components/databases/databases.component';
import { TablesComponent } from './components/tables/tables.component';
import { TableDetailComponent } from './components/tables/tableDetail.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        ServersComponent,
        DatabasesComponent,
        TablesComponent,
        TableDetailComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'servers', component: ServersComponent },
            { path: 'servers/:serverName/databases', component: DatabasesComponent },
            { path: 'servers/:serverName/databases/:databaseName/tables', component: TablesComponent },
            { path: 'servers/:serverName/databases/:databaseName/tables/:fullName', component: TableDetailComponent },       
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
