import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'tables',
    templateUrl: './tables.component.html'
})
export class TablesComponent {
    public tables: Table[];
    private sub: any;
    public serverName: string;
    public databaseName: string;
    private http: Http;
    private baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.serverName = params['serverName'];
            this.databaseName = params['databaseName'];
            console.log(this.baseUrl + 'api/servers/' + this.serverName + '/databases/' + this.databaseName + '/tables');
            this.http.get(this.baseUrl + 'api/servers/' + this.serverName + '/databases/' + this.databaseName + '/tables').subscribe(result => {
                console.log(result);
                this.tables = result.json() as Table[];
                console.log(this.tables)
            }, error => console.error(error));
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}

interface Table {
    serverName: string;
    databaseName: string;
    name: string;
    schema: string;
    description: string;
}