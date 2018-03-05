import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'databases',
    templateUrl: './databases.component.html'
})
export class DatabasesComponent {
    public databases: Database[];
    private sub: any;
    public serverName: string;
    private http: Http;
    private baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.serverName = params['serverName']; // (+) converts string 'id' to a number
            this.http.get(this.baseUrl + 'api/servers/' + this.serverName + '/databases').subscribe(result => {
                this.databases = result.json() as Database[];
            }, error => console.error(error));
            // In a real app: dispatch action to load the details here.
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}

interface Database {
    name: string;
    displayName: string;
    description: string;
}