import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'tableDetail',
    templateUrl: './tableDetail.component.html'
})
export class TableDetailComponent {
    public table: TableDetail;
    private sub: any;
    public serverName: string;
    public databaseName: string;
    public fullName: string;
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
            this.fullName = params['fullName'];
            this.http.get(this.baseUrl + 'api/servers/' + this.serverName + '/databases/' + this.databaseName + '/tables/' + this.fullName).subscribe(result => {
                this.table = result.json() as TableDetail;
            }, error => console.error(error));
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}

interface TableDetail {
    serverName: string;
    databaseName: string;
    name: string;
    schema: string;
    description: string;
    columns: TableColumn[];
}

interface TableColumn {
    name: string;
    description: string;
    inPrimaryKey: boolean;
    isForeignKey: boolean;
    dataType: string;
}