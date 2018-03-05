import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'table',
    templateUrl: './table.component.html'
})
export class TableComponent {
    public table: TableDetail;
    private sub: any;
    public serverName: string;
    public databaseName: string;
    public schema: string;
    public tableName: string;
    private http: Http;
    private baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    //ngOnInit() {
    //    //this.sub = this.route.params.subscribe(params => {
    //    //    this.serverName = params['serverName'];
    //    //    this.databaseName = params['databaseName'];
    //    //    //this.schema = params['schema'];
    //    //    //this.tableName = params['tableName'];
    //    //    console.log(this.baseUrl + 'api/servers/' + this.serverName + '/databases/' + this.databaseName + '/tables/');
    //    //    this.http.get(this.baseUrl + 'api/servers/' + this.serverName + '/databases/' + this.databaseName + '/tables/Person.Address').subscribe(result => {
    //    //        console.log(result);
    //    //        this.table = result.json() as Table;
    //    //        console.log(this.table)
    //    //    }, error => console.error(error));
    //    });
    //}

    //ngOnDestroy() {
    //    this.sub.unsubscribe();
    //}
}

interface TableDetail {
    serverName: string;
    databaseName: string;
    name: string;
    schema: string;
    description: string;
}