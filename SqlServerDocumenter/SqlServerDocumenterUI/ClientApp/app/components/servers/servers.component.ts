import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'servers',
    templateUrl: './servers.component.html',
    styleUrls: ['./servers.component.css']
})
export class ServersComponent {
    public servers: Server[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/servers').subscribe(result => {
            this.servers = result.json() as Server[];
        }, error => console.error(error));
    }
}

interface Server {
    name: string;
    displayName: string;
    description: string;
}